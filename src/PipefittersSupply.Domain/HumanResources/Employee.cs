using System;

namespace PipefittersSupply.Domain.HumanResources
{
    public class Employee
    {
        private EmployeeId _employeeID;

        public EmployeeId Id { get; }

        public string LastName { get; }

        public Employee(EmployeeId id, string lname)
        {
            Id = id;
            LastName = lname;
        }
    }
}