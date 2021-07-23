using System.Collections.Generic;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate
{
    public class EmployeeType : BaseEntity<int>
    {
        private EmployeeType() { }

        public EmployeeType(int id, string employeeTypeName)
        {
            Id = id;
            EmployeeTypeName = employeeTypeName;
        }

        public string EmployeeTypeName { get; private set; }
    }
}