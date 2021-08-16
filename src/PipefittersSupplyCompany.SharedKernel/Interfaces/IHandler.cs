namespace PipefittersSupplyCompany.SharedKernel.Interfaces
{
    public interface IHandler<T> where T : IDomainEvent
    {
        void Handle(T domainEvent);
    }
}