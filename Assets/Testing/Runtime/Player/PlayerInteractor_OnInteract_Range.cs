using NUnit.Framework;
using Player.Interaction;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PlayerInteractor_OnInteract_Range
{
  GameObject stubPlayer;
  GameObject stubNpc;
  PlayerInteractor playerInteractor;
  Action<GameObject, GameObject> interactionHandler;
  bool playerInteractFired = false;

  [UnitySetUp]
  public IEnumerator TestSetup()
  {
    yield return SceneManager.LoadSceneAsync("GeneralTestScene", LoadSceneMode.Single);

    yield return null;
  }

  void setupInScene()
  {
    // Create dummy NPC to interact with
    stubNpc = new GameObject("StubNpc");
    stubNpc.AddComponent<NpcInteract>();

    // In the test scene, there is a Player object already
    stubPlayer = GameObject.Find("Player");
    Assert.IsNotNull(stubPlayer, "Player GameObject not found in GeneralTestScene");

    if (!stubPlayer.TryGetComponent<PlayerInteractor>(out playerInteractor))
      playerInteractor = stubPlayer.AddComponent<PlayerInteractor>();
    // Set interaction distance || make sure its not zero
    playerInteractor.InteractDistance = 2.0f;
    // Refresh IInteractableObjects list
    playerInteractor.InteractableObjects = IInteractable.GetAllInteractableItems();

    // Subscribe to PlayerInteract event to see that when it fires 
    interactionHandler = (player, interactedObject) => playerInteractFired = true;
    PlayerInteractor.PlayerInteract += interactionHandler;

    // Set locations outside of interaction range
    stubPlayer.transform.position = Vector3.zero;
    stubNpc.transform.position = new Vector2(playerInteractor.InteractDistance, 0) + Vector2.right;
  }

  [UnityTest]
  public IEnumerator PlayerInteractor_Range_Mono()
  {
    // Arrange
    setupInScene();
    var movePositionAmount = new Vector3(2, 0);

    // Act & Assert
    Assert.IsNotNull(stubPlayer, "Player object not found in scene!");
    Assert.IsNotNull(stubNpc, "NPC object not found in scene!");
    Assert.IsNotNull(playerInteractor.InteractableObjects, "No interactables in scene for test");

    Assert.GreaterOrEqual(Vector3.Distance(stubPlayer.transform.position, stubNpc.transform.position), playerInteractor.InteractDistance);
    Debug.Log($"{playerInteractor.gameObject} shouldn't find anything here ignore the debug log");
    playerInteractor.OnInteract();
    Assert.IsFalse(playerInteractFired, "Npc is in range before moving - how did that happen?");

    stubPlayer.transform.position += movePositionAmount;
    Assert.LessOrEqual(Vector3.Distance(stubPlayer.transform.position, stubNpc.transform.position), playerInteractor.InteractDistance, "Player not in range after moving in test");
    playerInteractor.OnInteract();
    Assert.IsTrue(playerInteractFired, "PlayerInteract event did not fire when in range");

    yield return null;
  }

  [TearDown]
  public void Teardown()
  {
    UnityEngine.Object.Destroy(stubNpc);
    PlayerInteractor.PlayerInteract -= interactionHandler;
  }
}
