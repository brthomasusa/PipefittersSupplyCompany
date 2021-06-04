using System.Collections.Generic;
using System.Linq;
using PipefittersSupply.Domain.Lookup;
using PipefittersSupply.Domain.HumanResources.Employees;

namespace PipefittersSupply.AppServices
{
    public class EmployeeLookup : IEmployeeLookup
    {
        private readonly IEnumerable<EmployeeLkupItem> _employeeLkup = new[]
        {
            new EmployeeLkupItem
            {
                EmployeeId = 101,
                EmployeeTypeId = 2,
                SupervisorId = 101,
                LastName = "Sanchez",
                FirstName = "Ken",
                MiddleInitial = "J"
            },
            new EmployeeLkupItem
            {
                EmployeeId = 108,
                EmployeeTypeId = 2,
                SupervisorId = 101,
                LastName = "Phide",
                FirstName = "Terri",
                MiddleInitial = "M"
            },
            new EmployeeLkupItem
            {
                EmployeeId = 109,
                EmployeeTypeId = 2,
                SupervisorId = 101,
                LastName = "Duffy",
                FirstName = "Terri",
                MiddleInitial = "L"
            },
            new EmployeeLkupItem
            {
                EmployeeId = 111,
                EmployeeTypeId = 2,
                SupervisorId = 101,
                LastName = "Goldberg",
                FirstName = "Jozef",
                MiddleInitial = "P"
            },
            new EmployeeLkupItem
            {
                EmployeeId = 114,
                EmployeeTypeId = 1,
                SupervisorId = 108,
                LastName = "Bushnell",
                FirstName = "Loretta",
                MiddleInitial = "J"
            },
            new EmployeeLkupItem
            {
                EmployeeId = 115,
                EmployeeTypeId = 1,
                SupervisorId = 109,
                LastName = "Jacknoff",
                FirstName = "Jorge",
                MiddleInitial = "C"
            },
            new EmployeeLkupItem
            {
                EmployeeId = 118,
                EmployeeTypeId = 4,
                SupervisorId = 109,
                LastName = "Thompson",
                FirstName = "Douglas",
                MiddleInitial = "J"
            },
            new EmployeeLkupItem
            {
                EmployeeId = 119,
                EmployeeTypeId = 4,
                SupervisorId = 109,
                LastName = "Hernandez",
                FirstName = "Jesus",
                MiddleInitial = "J"
            },
            new EmployeeLkupItem
            {
                EmployeeId = 120,
                EmployeeTypeId = 5,
                SupervisorId = 111,
                LastName = "Doe",
                FirstName = "Jonny",
                MiddleInitial = "A"
            },
            new EmployeeLkupItem
            {
                EmployeeId = 121,
                EmployeeTypeId = 5,
                SupervisorId = 111,
                LastName = "Smith",
                FirstName = "Samuel",
                MiddleInitial = "P"
            }
        };

        public IEnumerable<EmployeeLkupItem> FindEmployeesByEmployeeType(int typeID)
        {
            return _employeeLkup.Where(e => e.EmployeeTypeId == typeID).AsEnumerable<EmployeeLkupItem>();
        }

        public IEnumerable<EmployeeLkupItem> FindEmployeesBySupervisorId(int supervisorID)
        {
            return _employeeLkup.Where(e => e.EmployeeTypeId == supervisorID).AsEnumerable<EmployeeLkupItem>();
        }
    }
}