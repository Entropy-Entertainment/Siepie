using NSubstitute;
using NUnit.Framework;
using Player.Interaction;

public class NpcInteract_SubscribeToInteractEvent
{
  [Test]
  public void NpcInteract_SubscribeToInteractEventSimplePasses()
  {
    // Arrange
    NpcInteract npcInteractMock = Substitute.For<NpcInteract>();
    // Act
    npcInteractMock.SubscribeToInteractEvent();
    // Assert
    npcInteractMock.Received(1).SubscribeToInteractEvent();
  }

}
