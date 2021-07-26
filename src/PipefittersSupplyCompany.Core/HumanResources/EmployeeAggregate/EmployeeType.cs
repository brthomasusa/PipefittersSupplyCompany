using System.Collections.Generic;
using Ardalis.GuardClauses;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate
{
    public class EmployeeType : BaseEntity<int>
    {
        private List<Employee> _employees = new List<Employee>();

        protected EmployeeType() { }

        public EmployeeType(int id, string employeeTypeName)
            : this()
        {
            Id = id;
            EmployeeTypeName = Guard.Against.NullOrEmpty(employeeTypeName, nameof(employeeTypeName));
        }

        public string EmployeeTypeName { get; private set; }

        public IEnumerable<Employee> Employees => _employees.AsReadOnly();
    }
}