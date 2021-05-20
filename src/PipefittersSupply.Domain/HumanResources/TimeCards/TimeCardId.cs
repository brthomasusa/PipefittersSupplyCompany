using System;
using PipefittersSupply.Framework;

namespace PipefittersSupply.Domain.HumanResources.TimeCards
{
    public class TimeCardId : IEquatable<TimeCardId>
    {
        private int Value { get; }

        public TimeCardId(int value)
        {
            if (value == default)
            {
                throw new ArgumentException("TimeCard Id must be specified", nameof(value));
            }

            Value = value;
        }

        public static implicit operator int(TimeCardId self) => self.Value;

        public bool Equals(TimeCardId other)
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

            return Equals((TimeCardId)obj);
        }

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value.ToString();

    }
}
