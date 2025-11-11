using NSubstitute;
using NUnit.Framework;
using Player.Interaction;

/// <summary>
/// This test leaves an event pointer behind after running in the PlayerInteractor script. 
/// I still want to keep this test around as this is a crucial function of the NpcInteract class.
/// </summary>
public class NpcInteract_SubscribeToInteractEvent
{
  [Test]
  public void NpcInteract_SubscribeToInteractEventSimplePass()
  {
    // Arrange
    NpcInteract npcInteractMock = Substitute.For<NpcInteract>();
    // Act
    npcInteractMock.SubscribeToInteractEvent();
    // Assert
    npcInteractMock.Received(1).SubscribeToInteractEvent();
  }
}
