using System;
using PipefittersSupply.Domain.Base;
namespace PipefittersSupply.Domain.HumanResources.Employees
{
    public class EmployeeId : Value<EmployeeId>
    {
        private int Value { get; }

        public EmployeeId(int value)
        {
            if (value == default)
            {
                throw new ArgumentException("Employee Id must be specified", nameof(value));
            }

            Value = value;
        }

        public static implicit operator int(EmployeeId self) => self.Value;

        public static implicit operator EmployeeId(string value) => new EmployeeId(int.Parse(value));

        public bool Equals(EmployeeId other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Value.Equals(other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Value.Equals((EmployeeId)obj);
        }

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value.ToString();
    }
}
