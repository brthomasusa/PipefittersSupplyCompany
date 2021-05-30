namespace PipefittersSupply.Domain.Interfaces
{
    public interface IInternalEventHandler
    {
        void Handle(object @event);
    }
}