using System;

namespace PipefittersSupplyCompany.SharedKernel.CommonValueObjects
{
    public class IsActive : ValueObject
    {
        public bool Value { get; }

        protected IsActive() { }

        private IsActive(bool value) : this() => Value = value;

        public static implicit operator bool(IsActive self) => self.Value;

        public static IsActive Create(bool status)
        {
            CheckValidity(status);
            return new IsActive(status);
        }

        private static void CheckValidity(bool value)
        {

        }
    }
}