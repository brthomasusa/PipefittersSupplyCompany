using System;
using PipefittersSupply.Domain.Common;
using PipefittersSupply.Domain.HumanResources.Employee;
using PipefittersSupply.Domain.HumanResources.TimeCard;
using Xunit;

namespace PipefittersSupply.Tests
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
        public void ShouldGet_TimeCard_PayPeriodEndDate_ValueObjComparision()
        {
            var periodEndDate = new DateTime(2021, 1, 31);
            var endDate = PayPeriodEndDate.FromDateTime(periodEndDate);

            Assert.IsType<PayPeriodEndDate>(endDate);

            // Shows the DateTime value being pulled from an instance of PayPeriodEndDate.
            // This is what allows comparison between two instance's values
            Assert.Equal(periodEndDate, endDate);
        }


    }
}