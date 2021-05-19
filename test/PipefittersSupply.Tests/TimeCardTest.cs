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