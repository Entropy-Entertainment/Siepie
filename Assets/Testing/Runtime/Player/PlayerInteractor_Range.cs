using NUnit.Framework;
using Player.Interaction;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

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
    base.Setup();
    // Load the test scene
    SceneManager.LoadScene("GeneralTestScene");

    yield return null;

    // Create dummy NPC to interact with
    stubNpc = new GameObject("StubNpc");
    stubNpc.AddComponent<NpcInteract>();

    yield return null;

    // In the test scene, there is a Player object already
    stubPlayer = GameObject.Find("Player");
    Assert.IsNotNull(stubPlayer, "Player GameObject not found in GeneralTestScene");

    if (!stubPlayer.TryGetComponent<PlayerInteractor>(out playerInteractor))
      playerInteractor = stubPlayer.AddComponent<PlayerInteractor>();

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

    // Create stub input device
    stubController = InputSystem.AddDevice<Gamepad>();

    // simulate a click on the right face button (B)
    Click(stubController.buttonEast);   // or Click(stubController.rightShoulder);
    InputSystem.Update();
    yield return null; // let the frame run so Unity callbacks fire

    if (playerInteractFired == true)
    {
      Assert.That(Vector3.Distance(stubPlayer.transform.position, stubNpc.transform.position) <= playerInteractor.InteractDistance);
    }
    else yield return null;
  }

  [TearDown]
  public void Teardown()
  {
    base.TearDown();
    UnityEngine.Object.Destroy(stubNpc);
    PlayerInteractor.PlayerInteract -= interactionHandler;
  }
}
