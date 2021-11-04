using System;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.Core.Shared
{
    public class UserId : ValueObject
    {
        public Guid Value { get; }

        protected UserId() { }

        private UserId(Guid userID)
            : this()
        {
            Value = userID;
        }

        public static implicit operator Guid(UserId self) => self.Value;

        public static UserId Create(Guid userID)
        {
            CheckValidity(userID);
            return new UserId(userID);
        }

        private static void CheckValidity(Guid value)
        {
            if (value == default)
            {
                throw new ArgumentNullException("The user Id is required; it can not be a default Guid.", nameof(value));
            }
        }
    }
}