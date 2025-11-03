using NSubstitute;
using NUnit.Framework;
using Player.Interaction;

public class NpcInteract_SubscribeToInteractEvent
{
  [Test]
  public void NpcInteract_SubscribeToInteractEventSimplePasses()
  {
    NpcInteract npcInteractMock = Substitute.For<NpcInteract>();

    npcInteractMock.SubscribeToInteractEvent();

    npcInteractMock.Received(1).SubscribeToInteractEvent();
  }

}
