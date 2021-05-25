using System.Collections.Generic;
using PipefittersSupply.Framework;

namespace PipefittersSupply.Domain.Lookup
{
    public interface IEmployeeLookup
    {
        IEnumerable<EmployeeLkupItem> FindEmployeesByEmployeeType(int typeID);

        IEnumerable<EmployeeLkupItem> FindEmployeesBySupervisorId(int supervisorID);
    }

    public class EmployeeLkupItem : Value<EmployeeLkupItem>
    {
        public int EmployeeId { get; set; }
        public int EmployeeTypeId { get; set; }
        public int SupervisorId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        // public static EmployeeLkupItem None = new EmployeeLkupItem
        // {
        //     EmployeeId = -1,
        //     EmployeeTypeId = -1,
        //     SupervisorId = -1,
        //     LastName = null,
        //     FirstName = null,
        //     MiddleInitial = null
        // };
    }
}