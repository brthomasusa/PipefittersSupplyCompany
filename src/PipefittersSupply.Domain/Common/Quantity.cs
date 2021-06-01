using System;
using PipefittersSupply.Domain.Base;

namespace PipefittersSupply.Domain.Common
{
    public class Quantity
    {
        public int Value { get; }

        protected Quantity() { }


        internal Quantity(int value) => Value = value;

        public static implicit operator int(Quantity self) => self.Value;

        public static Quantity FromInterger(int value)
        {
            CheckValidity(value);
            return new Quantity(value);
        }

        private static void CheckValidity(int value)
        {
            if (value < 0)
            {
                throw new ArgumentException("Quantity can not be negative!", nameof(value));
            }
        }
    }
}