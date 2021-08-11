using System;
using Xunit;
using PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate;
using PipefittersSupplyCompany.Core.Shared;

namespace PipefittersSupplyCompany.UnitTests.Core.HumanResources.EmployeeAggregate
{
    public class EmployeeTests
    {
        [Fact]
        public void ShouldReturn_ValidExternalAgent()
        {
            var result = new ExternalAgent(Guid.NewGuid(), AgentType.Employee);

            Assert.IsType<ExternalAgent>(result);
        }

        [Fact]
        public void ShouldRaiseError_InvalidAgentTypeId()
        {
            Action action = () => new ExternalAgent(Guid.NewGuid(), 0);

            var caughtException = Assert.Throws<ArgumentException>(action);

            Assert.Contains("Undefined agent type.", caughtException.Message);
        }

        [Fact]
        public void ShouldRaiseError_UninitializedGuidForAgentId()
        {
            Action action = () => new ExternalAgent(new Guid(), AgentType.Employee);

            var caughtException = Assert.Throws<ArgumentNullException>(action);

            Assert.Contains("The agent id is required.", caughtException.Message);
        }

        [Fact]
        public void ShouldReturn_NewEmployee()
        {
            var employeeAgent = new ExternalAgent(Guid.NewGuid(), AgentType.Employee);

            Employee employee = new Employee
            (
                employeeAgent,
                employeeAgent.Id,
                "Sanchez",
                "Ken",
                "J",
                "321 Tarrant Pl",
                null,
                "Fort Worth",
                "TX",
                "78965",
                "123789999",
                "817-987-1234",
                "M",
                5,
                40.00M,
                new DateTime(1998, 12, 2),
                true
            );

            Assert.IsType<Employee>(employee);
        }

        [Fact]
        public void ShouldAttach_NewEmployeeToExternalAgent()
        {
            var employeeAgent = new ExternalAgent(Guid.NewGuid(), AgentType.Employee);

            Employee employee = new Employee
            (
                employeeAgent,
                employeeAgent.Id,
                "Sanchez",
                "Ken",
                "J",
                "321 Tarrant Pl",
                null,
                "Fort Worth",
                "TX",
                "78965",
                "123789999",
                "817-987-1234",
                "M",
                5,
                40.00M,
                new DateTime(1998, 12, 2),
                true
            );

            employeeAgent.SetEmployee(employee);
            Assert.IsType<Employee>(employeeAgent.Employee);
        }

        [Fact]
        public void ShouldRaiseError_SetEmployeeWithNull()
        {
            var employeeAgent = new ExternalAgent(Guid.NewGuid(), AgentType.Employee);

            Action action = () => employeeAgent.SetEmployee(null);

            var caughtException = Assert.Throws<ArgumentNullException>(action);

            Assert.Contains("The employee to link to the external agent is required.", caughtException.Message);
        }

        [Fact]
        public void ShouldRaiseError_Invalid_AgentAndAgentTypeCombo()
        {
            var employeeAgent = new ExternalAgent(Guid.NewGuid(), AgentType.Customer);

            Employee employee = new Employee
            (
                employeeAgent,
                employeeAgent.Id,
                "Sanchez",
                "Ken",
                "J",
                "321 Tarrant Pl",
                null,
                "Fort Worth",
                "TX",
                "78965",
                "123789999",
                "817-987-1234",
                "M",
                5,
                40.00M,
                new DateTime(1998, 12, 2),
                true
            );

            Action action = () => employeeAgent.SetEmployee(employee);

            var caughtException = Assert.Throws<InvalidOperationException>(action);

            Assert.Contains("Can not set employee if agent type does not equal employee.", caughtException.Message);
        }


        [Fact]
        public void ShouldRaiseError_Invalid_AttemptingToSetEmployeeThatIsAlreadySet()
        {
            var employeeAgent = new ExternalAgent(Guid.NewGuid(), AgentType.Employee);

            Employee employee = new Employee
            (
                employeeAgent,
                employeeAgent.Id,
                "Sanchez",
                "Ken",
                "J",
                "321 Tarrant Pl",
                null,
                "Fort Worth",
                "TX",
                "78965",
                "123789999",
                "817-987-1234",
                "M",
                5,
                40.00M,
                new DateTime(1998, 12, 2),
                true
            );

            employeeAgent.SetEmployee(employee);

            Action action = () => employeeAgent.SetEmployee(employee);

            var caughtException = Assert.Throws<InvalidOperationException>(action);

            Assert.Contains("The external agent and the Employee have already been linked, they can not be re-linked.", caughtException.Message);
        }

        [Fact]
        public void ShouldRaiseError_Invalid_SetEmployeeWithIdThatDoesNotMatchAgentId()
        {
            var employeeAgent1 = new ExternalAgent(Guid.NewGuid(), AgentType.Employee);
            var employeeAgent2 = new ExternalAgent(Guid.NewGuid(), AgentType.Employee);

            Employee employee = new Employee
            (
                employeeAgent1,
                employeeAgent1.Id,
                "Sanchez",
                "Ken",
                "J",
                "321 Tarrant Pl",
                null,
                "Fort Worth",
                "TX",
                "78965",
                "123789999",
                "817-987-1234",
                "M",
                5,
                40.00M,
                new DateTime(1998, 12, 2),
                true
            );

            Action action = () => employeeAgent2.SetEmployee(employee);

            var caughtException = Assert.Throws<InvalidOperationException>(action);

            Assert.Contains("The external agent id and the employee id do not match.", caughtException.Message);
        }

        [Fact]
        public void ShouldRaiseError_SupervisorIdEqualDefaultGuid()
        {
            var employeeAgent = new ExternalAgent(Guid.NewGuid(), AgentType.Employee);

            Action action = () => new Employee
           (
               employeeAgent,
               new Guid(),
               "Sanchez",
               "Ken",
               "J",
               "321 Tarrant Pl",
               null,
               "Fort Worth",
               "TX",
               "78965",
               "123789999",
               "817-987-1234",
               "M",
               5,
               40.00M,
               new DateTime(1998, 12, 2),
               true
           );

            var caughtException = Assert.Throws<ArgumentNullException>(action);

            Assert.Contains("The supervisor id is required.", caughtException.Message);
        }

        [Fact]
        public void ShouldRaiseError_NewEmployee_WithNullLastName()
        {
            var employeeAgent = new ExternalAgent(Guid.NewGuid(), AgentType.Employee);

            Action action = () => new Employee
           (
               employeeAgent,
               Guid.NewGuid(),
               null,
               "Ken",
               "J",
               "321 Tarrant Pl",
               null,
               "Fort Worth",
               "TX",
               "78965",
               "123789999",
               "817-987-1234",
               "M",
               5,
               40.00M,
               new DateTime(1998, 12, 2),
               true
           );

            var caughtException = Assert.Throws<ArgumentNullException>(action);

            Assert.Contains("The employee last name is required.", caughtException.Message);
        }

        [Fact]
        public void ShouldRaiseError_NewEmployee_WithNullFirstName()
        {
            var employeeAgent = new ExternalAgent(Guid.NewGuid(), AgentType.Employee);

            Action action = () => new Employee
           (
               employeeAgent,
               Guid.NewGuid(),
               "Sanchez",
               null,
               "J",
               "321 Tarrant Pl",
               null,
               "Fort Worth",
               "TX",
               "78965",
               "123789999",
               "817-987-1234",
               "M",
               5,
               40.00M,
               new DateTime(1998, 12, 2),
               true
           );

            var caughtException = Assert.Throws<ArgumentNullException>(action);

            Assert.Contains("The employee first name is required.", caughtException.Message);
        }

        [Fact]
        public void ShouldRaiseError_NewEmployee_WithNullAddressLine1()
        {
            var employeeAgent = new ExternalAgent(Guid.NewGuid(), AgentType.Employee);

            Action action = () => new Employee
           (
               employeeAgent,
               Guid.NewGuid(),
               "Sanchez",
               "Ken",
               "J",
               null,
               null,
               "Fort Worth",
               "TX",
               "78965",
               "123789999",
               "817-987-1234",
               "M",
               5,
               40.00M,
               new DateTime(1998, 12, 2),
               true
           );

            var caughtException = Assert.Throws<ArgumentNullException>(action);

            Assert.Contains("The first address line is required.", caughtException.Message);
        }

        // private Employee GetEmployee()
        // {
        //     EmployeeID eeID = EmployeeID.Create(Guid.NewGuid());
        //     EmployeeType eeType = new EmployeeType(1, "Administrator");
        //     EmployeeID supvID = eeID;
        //     PersonName name = PersonName.Create("Joe", "Blow", "B");
        //     Address address = Address.Create("123 Main Street", "Apt 2", "Somewhere", "TX", "75654");
        //     SSN ssn = SSN.Create("587887964");
        //     Telephone phone = Telephone.Create("555-555-5555");
        //     MaritalStatus maritalStatus = MaritalStatus.Create("M");
        //     TaxExemption exemption = TaxExemption.Create(5);
        //     PayRate payRate = PayRate.Create(25.00M);
        //     StartDate startDate = StartDate.Create(new DateTime(2018, 6, 17));
        //     IsActive status = IsActive.Create(true);

        //     return new Employee
        //     (
        //         eeID, eeType, supvID, name, address, ssn, phone, maritalStatus, exemption, payRate, startDate, status
        //     );
        // }
    }
}