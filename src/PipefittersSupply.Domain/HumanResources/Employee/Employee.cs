namespace PipefittersSupply.Domain.HumanResources.Employee
{
    public class Employee
    {
        public EmployeeId Id { get; }

        public EmployeeLastName LastName { get; private set; }

        public Employee(EmployeeId id)
        {
            Id = id;
        }
    }
}