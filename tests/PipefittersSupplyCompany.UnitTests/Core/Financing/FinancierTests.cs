using System;
using Xunit;
using PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate;
using PipefittersSupplyCompany.Core.Shared;
using PipefittersSupplyCompany.SharedKernel.CommonValueObjects;
using PipefittersSupplyCompany.Core.Financing.FinancierAggregate;
using PipefittersSupplyCompany.Infrastructure.Application.Queries.Financing;

namespace PipefittersSupplyCompany.UnitTests.Core.Financing
{
    public class FinancierTests
    {
        [Fact]
        public void ShouldReturn_ValidExternalAgent()
        {
            var result = new ExternalAgent(Guid.NewGuid(), AgentType.Financier);

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
        public void ShouldReturn_NewFinancier()
        {
            var financierAgent = new ExternalAgent(Guid.NewGuid(), AgentType.Financier);

            Financier financier = new Financier
            (
                financierAgent,
                OrganizationName.Create("First Bank and Trust"),
                PhoneNumber.Create("555-555-5555"),
                IsActive.Create(true),
                new Guid("660bb318-649e-470d-9d2b-693bfb0b2744")
            );

            Assert.IsType<Financier>(financier);
        }


        [Fact]
        public void ShouldRaiseError_NewFinancier_WithNullExternalAgent()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Financier financier = new Financier
                (
                    null,
                    OrganizationName.Create("First Bank and Trust"),
                    PhoneNumber.Create("555-555-5555"),
                    IsActive.Create(true),
                    new Guid("660bb318-649e-470d-9d2b-693bfb0b2744")
                );
            });
        }

        [Fact]
        public void ShouldRaiseError_NewFinancier_WithInvalidExternalAgentType()
        {
            var agent = new ExternalAgent(Guid.NewGuid(), AgentType.Employee);

            Assert.Throws<InvalidOperationException>(() =>
            {
                Financier financier = new Financier
                (
                    agent,
                    OrganizationName.Create("First Bank and Trust"),
                    PhoneNumber.Create("555-555-5555"),
                    IsActive.Create(true),
                    new Guid("660bb318-649e-470d-9d2b-693bfb0b2744")
                );
            });
        }

        [Fact]
        public void Should_GiveMeTheRightAnswer()
        {
            FinancierListItem financierListItem = new FinancierListItem
            {
                FinancierId = new Guid("660bb318-649e-470d-9d2b-693bfb0b2744"),
                FinancierName = "Hello",
                Telephone = "123-456-7890",
                IsActive = true

            };

            Type readModelType = financierListItem.GetType();

            // Assert.IsType<Financier>(financier);
            Assert.NotNull(readModelType);
        }




        private DomainUser GetUser()
        {
            var agent = new ExternalAgent(Guid.NewGuid(), AgentType.Employee);

            Employee employee = new Employee
            (
                agent,
                SupervisorId.Create(agent.Id),
                PersonName.Create("Ken", "Sanchez", "J"),
                SSN.Create("123789999"),
                PhoneNumber.Create("817-987-1234"),
                MaritalStatus.Create("M"),
                TaxExemption.Create(5),
                PayRate.Create(40.00M),
                StartDate.Create(new DateTime(1998, 12, 2)),
                IsActive.Create(true)
            );

            return new DomainUser(agent.Id, "Jon", "Doe", "");
        }
    }
}