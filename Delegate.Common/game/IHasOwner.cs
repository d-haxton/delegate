namespace Delegate.Tera.Common.game
{
    interface IHasOwner
    {
        EntityId OwnerId { get; set; }
        Entity Owner { get; set; }
    }
}
