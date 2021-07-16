using PipefittersSupplyCompany.Core.ProjectAggregate;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.Core.ProjectAggregate.Events
{
    public class ToDoItemCompletedEvent : BaseDomainEvent
    {
        public ToDoItem CompletedItem { get; set; }

        public ToDoItemCompletedEvent(ToDoItem completedItem)
        {
            CompletedItem = completedItem;
        }
    }
}