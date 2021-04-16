using System;
using System.Collections.Generic;
using System.Linq;

namespace LibRarisma
{
    class Math
    {
        public static Random RandInt = new();

        public static Int64 Mean(List<Int32> ListOfNumbers) { return ListOfNumbers.Sum() / ListOfNumbers.Count; }

        /// <summary> Gets the Range of a set of numbers </summary>
        public static Int64 Range(List<Int32> ListOfNumbers) { return ListOfNumbers.Max() - ListOfNumbers.Min(); }

        public static double MakePostive(double input) { if (input < 0) { return input * -1; } else { return input; } }

        public static double MakeNegative(double input) { if (input > 0) { return input * -1; } else { return input; } }

        public static Int64 RandomInt(Int32 Number0, Int32 Number1) { return RandInt.Next(Number0, Number1); }

    }
}
