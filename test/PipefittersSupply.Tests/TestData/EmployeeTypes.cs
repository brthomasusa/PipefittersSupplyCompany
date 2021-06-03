using System.Collections.Generic;
using PipefittersSupply.Domain.HumanResources.Employees;

namespace PipefittersSupply.Tests.TestData
{
    public static class EmployeeTypes
    {
        public static IEnumerable<EmployeeType> GetEmployeeTypes()
        {
            return new List<EmployeeType>()
            {
                new EmployeeType(EmployeeTypeIdentifier.FromInterger(1), EmployeeTypeName.FromString("Accountant")),
                new EmployeeType(EmployeeTypeIdentifier.FromInterger(2), EmployeeTypeName.FromString("Administrator")),
                new EmployeeType(EmployeeTypeIdentifier.FromInterger(3), EmployeeTypeName.FromString("Maintenance")),
                new EmployeeType(EmployeeTypeIdentifier.FromInterger(4), EmployeeTypeName.FromString("Materials Handler")),
                new EmployeeType(EmployeeTypeIdentifier.FromInterger(5), EmployeeTypeName.FromString("Purchasing Agent")),
                new EmployeeType(EmployeeTypeIdentifier.FromInterger(6), EmployeeTypeName.FromString("Salesperson"))
            };
        }
    }
}