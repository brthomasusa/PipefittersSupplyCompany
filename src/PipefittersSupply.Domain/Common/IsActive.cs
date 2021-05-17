using System;
using PipefittersSupply.Framework;

namespace PipefittersSupply.Domain.Common
{
    public class IsActive : Value<IsActive>
    {
        private readonly bool _isActive;

        private IsActive(bool value)
        {
            _isActive = value;
        }

        public static IsActive FromBoolean(bool status) => new IsActive(status);
    }
}