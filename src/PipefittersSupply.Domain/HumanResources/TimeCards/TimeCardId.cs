using System;
using PipefittersSupply.Domain.Base;

namespace PipefittersSupply.Domain.HumanResources.TimeCards
{
    public class TimeCardId : Value<TimeCardId>
    {
        public int Value { get; internal set; }

        protected TimeCardId() { }


        public TimeCardId(int value)
        {
            if (value == default)
            {
                throw new ArgumentException("TimeCard Id must be specified", nameof(value));
            }

            Value = value;
        }

        public static implicit operator int(TimeCardId self) => self.Value;

        public static implicit operator TimeCardId(string value) => new TimeCardId(int.Parse(value));

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
