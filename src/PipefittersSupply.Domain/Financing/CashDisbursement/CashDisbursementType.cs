using System;
using PipefittersSupply.Domain.Base;
using PipefittersSupply.Domain.Common;

namespace PipefittersSupply.Domain.Financing.CashDisbursement
{
    public class CashDisbursementType : AggregateRoot<CashDisbursementTypeId>
    {
        public int CashDisbursementTypeId { get; private set; }

        protected CashDisbursementType() { }

        public CashDisbursementType
        (
            CashDisbursementTypeId id,
            EventTypeName eventType,
            PayeeTypeName payee
        ) =>
            Apply(new Events.CashDisbursementTypeCreated
            {
                Id = id,
                EventTypeName = eventType,
                PayeeTypeName = payee
            });

        public EventTypeName EventTypeName { get; private set; }
        public void UpdateEventTypeName(EventTypeName value) =>
            Apply(new Events.EventTypeNameUpdated
            {
                Id = Id,
                EventTypeName = EventTypeName,
                LastModifiedDate = LastModifiedDate
            });

        public PayeeTypeName PayeeTypeName { get; private set; }
        public void UpdatePayeeTypeName(PayeeTypeName value) =>
            Apply(new Events.PayeeTypeNameUpdated
            {
                Id = Id,
                PayeeTypeName = PayeeTypeName,
                LastModifiedDate = LastModifiedDate
            });

        public CreatedDate CreatedDate { get; private set; }

        public LastModifiedDate LastModifiedDate { get; private set; }

        protected override void EnsureValidState()
        {
            var valid = Id != null;

            if (!valid)
            {
                throw new InvalidEntityStateException(this, "Post-checks failed!");
            }
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case Events.CashDisbursementTypeCreated evt:
                    Id = new CashDisbursementTypeId(evt.Id);
                    EventTypeName = new EventTypeName(evt.EventTypeName);
                    PayeeTypeName = new PayeeTypeName(evt.PayeeTypeName);
                    CreatedDate = new CreatedDate(DateTime.Now);
                    CashDisbursementTypeId = evt.Id;
                    break;
                case Events.EventTypeNameUpdated evt:
                    EventTypeName = new EventTypeName(evt.EventTypeName);
                    LastModifiedDate = new LastModifiedDate(DateTime.Now);
                    break;
                case Events.PayeeTypeNameUpdated evt:
                    PayeeTypeName = new PayeeTypeName(evt.PayeeTypeName);
                    LastModifiedDate = new LastModifiedDate(DateTime.Now);
                    break;
            }
        }
    }
}