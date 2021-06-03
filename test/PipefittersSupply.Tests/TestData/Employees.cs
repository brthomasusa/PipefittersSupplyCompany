using System;
using System.Collections.Generic;
using PipefittersSupply.Domain.HumanResources.Employees;
using PipefittersSupply.Domain.Common;
using PipefittersSupply.Domain.Lookup;


namespace PipefittersSupply.Tests.TestData
{
    public static class Employees
    {
        private static readonly IStateProvinceLookup _stateProvinceLookup = new MockStateProvinceCodeLookup();

        public static IEnumerable<Employee> GetEmployees()
        {
            return new List<Employee>()
            {
                new Employee
                (
                    new EmployeeId(1),
                    EmployeeTypeIdentifier.FromInterger(2),
                    new EmployeeId(1),
                    EmployeeLastName.FromString("Sanchez"),
                    EmployeeFirstName.FromString("Ken"),
                    EmployeeMiddleInitial.FromString("J"),
                    EmployeeSSN.FromString("123789999"),
                    AddressLine1.FromString("321 Tarrant Pl"),
                    AddressLine2.FromString("Apt 1"),
                    City.FromString("Fort Worth"),
                    StateProvinceCode.FromString("TX", _stateProvinceLookup),
                    Zipcode.FromString("78965"),
                    Telephone.FromString("817-987-1234"),
                    MaritalStatus.FromString("M"),
                    TaxExemption.FromInterger(5),
                    EmployeePayRate.FromDecimal(40.00M),
                    EmployeeStartDate.FromDateTime(new DateTime(1998,12,2)),
                    IsActive.FromBoolean(true)
                ),
                new Employee
                (
                    new EmployeeId(2),
                    EmployeeTypeIdentifier.FromInterger(2),
                    new EmployeeId(1),
                    EmployeeLastName.FromString("Phide"),
                    EmployeeFirstName.FromString("Terri"),
                    EmployeeMiddleInitial.FromString("P"),
                    EmployeeSSN.FromString("638912345"),
                    AddressLine1.FromString("3455 South Corinth Circle"),
                    AddressLine2.FromString("Apt 2"),
                    City.FromString("Dallas"),
                    StateProvinceCode.FromString("TX", _stateProvinceLookup),
                    Zipcode.FromString("75224"),
                    Telephone.FromString("872-217-1234"),
                    MaritalStatus.FromString("M"),
                    TaxExemption.FromInterger(1),
                    EmployeePayRate.FromDecimal(28.00M),
                    EmployeeStartDate.FromDateTime(new DateTime(2014,9,22)),
                    IsActive.FromBoolean(true)
                ),
                new Employee
                (
                    new EmployeeId(3),
                    EmployeeTypeIdentifier.FromInterger(2),
                    new EmployeeId(1),
                    EmployeeLastName.FromString("Duffy"),
                    EmployeeFirstName.FromString("Terri"),
                    EmployeeMiddleInitial.FromString("P"),
                    EmployeeSSN.FromString("899632147"),
                    AddressLine1.FromString("38 Reiger Ave"),
                    AddressLine2.FromString("Unit 10"),
                    City.FromString("Dallas"),
                    StateProvinceCode.FromString("TX", _stateProvinceLookup),
                    Zipcode.FromString("75214"),
                    Telephone.FromString("214-217-1234"),
                    MaritalStatus.FromString("M"),
                    TaxExemption.FromInterger(2),
                    EmployeePayRate.FromDecimal(30.00M),
                    EmployeeStartDate.FromDateTime(new DateTime(2018,10,17)),
                    IsActive.FromBoolean(true)
                ),
                new Employee
                (
                    new EmployeeId(4),
                    EmployeeTypeIdentifier.FromInterger(2),
                    new EmployeeId(1),
                    EmployeeLastName.FromString("Goldberg"),
                    EmployeeFirstName.FromString("Jozef"),
                    EmployeeMiddleInitial.FromString("L"),
                    EmployeeSSN.FromString("036889999"),
                    AddressLine1.FromString("6667 Melody Lane"),
                    AddressLine2.FromString("A2"),
                    City.FromString("Dallas"),
                    StateProvinceCode.FromString("TX", _stateProvinceLookup),
                    Zipcode.FromString("75231"),
                    Telephone.FromString("469-545-5874"),
                    MaritalStatus.FromString("S"),
                    TaxExemption.FromInterger(1),
                    EmployeePayRate.FromDecimal(29.00M),
                    EmployeeStartDate.FromDateTime(new DateTime(2013,2,28)),
                    IsActive.FromBoolean(true)
                )
            };
        }
    }
}