using System;
using PipefittersSupply.Domain.Common;
using PipefittersSupply.Domain.HumanResources.Employees;
using PipefittersSupply.Domain.HumanResources.TimeCards;
using Xunit;

namespace PipefittersSupply.Tests.UnitTests.HumanResources
{
    public class TimeCardTest
    {
        [Fact]
        public void ShouldReturnValid_TimeCard_UsingCtor()
        {
            var endDate = new DateTime(2021, 1, 31);
            var timecardId = new TimeCardId(1);
            var employeeId = new EmployeeId(1);
            var supervisorId = new EmployeeId(10);
            var periodEndDate = PayPeriodEndDate.FromDateTime(endDate);
            var regularHrs = RegularHours.FromInterger(168);
            var overtimeHrs = OvertimeHours.FromInterger(50);

            var timeCard = new TimeCard
            (
                timecardId,
                employeeId,
                supervisorId,
                periodEndDate,
                regularHrs,
                overtimeHrs
            );

            Assert.IsType<TimeCard>(timeCard);
            Assert.Equal(1, timeCard.Id);
            Assert.Equal(1, timeCard.EmployeeId);
            Assert.Equal(10, timeCard.SupervisorId);
            Assert.Equal(endDate, timeCard.PayPeriodEnded);
            Assert.Equal(168, timeCard.RegularHours);
            Assert.Equal(50, timeCard.OvertimeHours);
            Assert.NotNull(timeCard.CreatedDate);
            Assert.NotEqual(default(DateTime), timeCard.CreatedDate);
        }

        [Fact]
        public void ShouldReturn_TimeCard_PayPeriodEndDate_ValueObjComparision()
        {
            var periodEndDate = new DateTime(2021, 1, 31);
            var endDate = PayPeriodEndDate.FromDateTime(periodEndDate);

            Assert.IsType<PayPeriodEndDate>(endDate);

            // Shows the DateTime value being pulled from an instance of PayPeriodEndDate.
            // This is what allows comparison between two instance's values
            Assert.Equal(periodEndDate, endDate);
        }

        [Fact]
        public void ShouldReturnValid_RegularHours()
        {
            var hours = RegularHours.FromInterger(168);

            Assert.IsType<RegularHours>(hours);
            Assert.Equal(168, hours);
        }

        [Fact]
        public void ShouldRaiseError_RegularHours_TooLarge()
        {
            Action action = () => RegularHours.FromInterger(200);

            var caughtException = Assert.Throws<ArgumentException>(action);

            Assert.Contains("Regular hours must be between 0 and 185 hours.", caughtException.Message);
        }

        [Fact]
        public void ShouldRaiseError_RegularHours_Negative()
        {
            Action action = () => RegularHours.FromInterger(-1);

            var caughtException = Assert.Throws<ArgumentException>(action);

            Assert.Contains("Regular hours must be between 0 and 185 hours.", caughtException.Message);
        }

        [Fact]
        public void ShouldReturnValid_OvertimeHours()
        {
            var hours = OvertimeHours.FromInterger(50);

            Assert.IsType<OvertimeHours>(hours);
            Assert.Equal(50, hours);
        }

        [Fact]
        public void ShouldRaiseError_OvertimeHours_TooLarge()
        {
            Action action = () => OvertimeHours.FromInterger(202);

            var caughtException = Assert.Throws<ArgumentException>(action);

            Assert.Contains("Overtime hours must be between 0 and 201 hours.", caughtException.Message);
        }

        [Fact]
        public void ShouldRaiseError_OvertimeHours_Negative()
        {
            Action action = () => OvertimeHours.FromInterger(-1);

            var caughtException = Assert.Throws<ArgumentException>(action);

            Assert.Contains("Overtime hours must be between 0 and 201 hours.", caughtException.Message);
        }
    }
}
