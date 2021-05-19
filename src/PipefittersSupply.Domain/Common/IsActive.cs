using System;
using PipefittersSupply.Framework;

namespace PipefittersSupply.Domain.Common
{
    public class IsActive : Value<IsActive>
    {
        public bool Value { get; }

        internal IsActive(bool value) => Value = value;

        public static implicit operator bool(IsActive self) => self.Value;

        public static IsActive FromBoolean(bool status)
        {
            CheckValidity(status);
            return new IsActive(status);
        }

        private static void CheckValidity(bool value)
        {

        }
    }
}