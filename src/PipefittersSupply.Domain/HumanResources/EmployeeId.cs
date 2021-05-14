using System;
using PipefittersSupply.Framework;

namespace PipefittersSupply.Domain.HumanResources
{
    public class EmployeeId : Value<EmployeeId>
    {
        private readonly Guid _value;

        public EmployeeId(Guid value) => _value = value;

    }
}