using NUnit.Framework;
using Player.Interaction;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.InputSystem.LowLevel;

public class PlayerInteractor_Range : InputTestFixture
{
  Gamepad stubController;
  GameObject stubPlayer;
  GameObject stubNpc;
  PlayerInteractor playerInteractor;
  Action<GameObject, GameObject> interactionHandler;
  bool playerInteractFired = false;

  [UnitySetUp]
  public IEnumerator TestSetup()
  {
    // Load the test scene
    SceneManager.LoadScene("GeneralTestScene");
    // Wait a frame so the scene and its GameObjects are available
    yield return null;

    // Create stub input device
    stubController = InputSystem.AddDevice<Gamepad>();

    // In the test scene, there is a Player object already
    stubPlayer = GameObject.Find("Player");
    Assert.IsNotNull(stubPlayer, "Player GameObject not found in GeneralTestScene");

    if (!stubPlayer.TryGetComponent<PlayerInteractor>(out playerInteractor))
      playerInteractor = stubPlayer.AddComponent<PlayerInteractor>();

    // Create dummy NPC to interact with
    stubNpc = new GameObject("StubNpc");
    stubNpc.AddComponent<NpcInteract>();

    // Subscribe to PlayerInteract event to see that when it fires 
    interactionHandler = (player, interactedObject) => playerInteractFired = true;
    PlayerInteractor.PlayerInteract += interactionHandler;

    // Set locations outside of interaction range
    stubPlayer.transform.position = Vector3.zero;
    stubNpc.transform.position = new Vector2(playerInteractor.InteractDistance, 0) + Vector2.right;

    yield break;
  }

  [UnityTest]
  public IEnumerator PlayerInteractor_Range_Mono()
  {
    Set(stubController.leftStick, Vector2.right);
    // The button on gamepads that maps to player interaction
    Set(stubController.buttonWest, 1.0f);
    InputSystem.Update();

    if (playerInteractFired == true)
    {
      Assert.That(Vector3.Distance(stubPlayer.transform.position, stubNpc.transform.position) <= playerInteractor.InteractDistance);
    }
    else yield return null;
  }

  [TearDown]
  public void Teardown()
  {
    UnityEngine.Object.Destroy(stubNpc);
    PlayerInteractor.PlayerInteract -= interactionHandler;
    InputSystem.RemoveDevice(stubController);
    SceneManager.UnloadSceneAsync("GeneralTestScene");
  }
}
