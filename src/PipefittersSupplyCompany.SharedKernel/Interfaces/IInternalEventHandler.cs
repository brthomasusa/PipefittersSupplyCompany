namespace PipefittersSupplyCompany.SharedKernel.Interfaces
{
    public interface IInternalEventHandler
    {
        void Handle(BaseDomainEvent @event);
    }
}