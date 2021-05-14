using System;
using PipefittersSupply.Domain.Financing;
using Xunit;

namespace PipefittersSupply.Tests
{
    public class MoenyTest
    {
        [Fact]
        public void Money_Obj_with_same_amt_should_be_equal()
        {
            var firstAmount = new Money(5);
            var secondAmount = new Money(5);

            Assert.Equal(firstAmount, secondAmount);
        }
    }
}
