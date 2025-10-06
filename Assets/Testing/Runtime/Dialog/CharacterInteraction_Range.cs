using NUnit.Framework;
using Player.Interaction;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteractor_Range
{
  GameObject player;
  GameObject npc;
  [SetUp]
  public void Setup()
  {
    SceneManager.LoadScene("GeneralTestScene");
    player = new("Player");
    player.AddComponent<PlayerInteractor>();
    npc = new("NPC");
    npc.AddComponent<NpcInteract>();
  }
  [Test]
  public void PlayerInteractor_CalculateDistancesCorrectly()
  {
    // Arrange
    PlayerInteractor playerInteractor = player.GetComponent<PlayerInteractor>();
    playerInteractor.InteractDistance = 5f;
    player.transform.position = Vector2.zero;
    npc.transform.position = new Vector2(4.9f, 0);
  }
  [Test]
  public void PlayerInteractor_NpcListHasCorrectTypes()
  {
    // Arrange
    CharacterInteraction characterInteraction = new();

    // Act
    characterInteraction.NpcList.AddRange(new List<NpcInteract>
    {
      new GameObject().AddComponent<NpcInteract>(),
      new GameObject().AddComponent<NpcInteract>()
    });

    // Assert
    Assert.That(characterInteraction.NpcList, Is.All.InstanceOf<NpcInteract>());
  }

  [Test]
  public void PlayerInteractor_NpcListGetsNpcsInScene()   {
    // Arrange
    SceneManager.LoadScene("GeneralTestScene");
    CharacterInteraction characterInteraction = new();
    characterInteraction.getAllNpcsWithInteractableTag();
    // Act
    int npcCount = characterInteraction.NpcList.Count;
    // Assert
    Assert.Greater(npcCount, 0, "NpcList should contain NPCs from the scene.");
  }



}
