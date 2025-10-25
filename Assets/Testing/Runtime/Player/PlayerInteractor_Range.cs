using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Player.Interaction;

public class PlayerInteractor_Range : InputTestFixture
{
  Gamepad stubController;
  GameObject stubPlayer;
  GameObject stubNpc;
  PlayerInteractor playerInteractor;
  [SetUp]
  public void TestSetup()
  {
    // Load the test scene
    SceneManager.LoadScene("GeneralTestScene");
    // Create stub input device
    stubController = InputSystem.AddDevice<Gamepad>();
    // Create a stub player object with PlayerInteractor component
    stubPlayer = GameObject.Find("Player");
    if (!stubPlayer.TryGetComponent<PlayerInteractor>(out playerInteractor))
      stubPlayer.AddComponent<PlayerInteractor>();
    // Create dummy NPC to interact with
    stubNpc = new GameObject("StubNpc");
    stubNpc.AddComponent<NpcInteract>();

    // Set locations outside of interaction range
    stubPlayer.transform.position = Vector3.zero;
    stubNpc.transform.position = new Vector3(playerInteractor.InteractDistance + 1, 0);

    // Create stub class that can check if OnInteract was called
  }
  [UnityTest]
  public IEnumerator PlayerInteractWithEnumeratorPasses()
  {
    Set(stubController.leftStick, Vector2.right);
    // The button on gamepads that maps to player interaction
    Set(stubController.buttonWest, 1.0f);
    InputSystem.Update();
    // If npc is out of range, conversation should not start
    if (Vector3.Distance(stubPlayer.transform.position, stubNpc.transform.position) > playerInteractor.InteractDistance)
     // Assert.That(NpcInteract.InConversation == false);



    yield return null;
  }
}
