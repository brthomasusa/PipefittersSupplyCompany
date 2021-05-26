using System;
using PipefittersSupply.Framework;

namespace PipefittersSupply.Domain.Purchasing.PurchaseOrder
{
    public class VendorPartNumber : Value<VendorPartNumber>
    {
        public string Value { get; }

        internal VendorPartNumber(string value) => Value = value;

        public static implicit operator string(VendorPartNumber self) => self.Value;

        public static VendorPartNumber FromString(string value)
        {
            CheckValidity(value);
            return new VendorPartNumber(value);
        }

        private static void CheckValidity(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("Null or zero-length vendor part numbers are invalid.", nameof(value));
            }

            if (value.Length > 25)
            {
                throw new ArgumentOutOfRangeException("Vendor part number can not be longer than 25 characters.", nameof(value));
            }
        }
    }
}