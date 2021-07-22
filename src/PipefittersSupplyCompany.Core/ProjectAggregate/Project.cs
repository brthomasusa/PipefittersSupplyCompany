using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using PipefittersSupplyCompany.Core.ProjectAggregate.Events;
using PipefittersSupplyCompany.SharedKernel;
using PipefittersSupplyCompany.SharedKernel.Interfaces;


namespace PipefittersSupplyCompany.Core.ProjectAggregate
{
    public class Project : BaseEntity<int>, IAggregateRoot
    {
        private List<ToDoItem> _items = new List<ToDoItem>();

        public string Name { get; private set; }

        public IEnumerable<ToDoItem> Items => _items.AsReadOnly();

        public ProjectStatus Status => _items.All(i => i.IsDone) ? ProjectStatus.Complete : ProjectStatus.InProgress;

        public Project(string name)
        {
            Name = Guard.Against.NullOrEmpty(name, nameof(name));
        }

        public void AddItem(ToDoItem newItem)
        {
            Guard.Against.Null(newItem, nameof(newItem));
            _items.Add(newItem);

            var newItemAddedEvent = new NewItemAddedEvent(this, newItem);
            Events.Add(newItemAddedEvent);
        }

        public void UpdateName(string newName)
        {
            Name = Guard.Against.NullOrEmpty(newName, nameof(newName));
        }
    }
}
