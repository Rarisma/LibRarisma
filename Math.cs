using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRarisma
{
    class Math
    {
        public static Random RandInt = new();

        /// <summary>
        /// Gets a mean of a given set of numbers
        /// </summary>
        /// <param name="ListOfNumbers"></param>
        /// <returns></returns>
        public static Int64 Mean(List<Int64> ListOfNumbers) { return ListOfNumbers.Sum() / ListOfNumbers.Count; }

        /// <summary> Gets the Range of a set of numbers </summary>
        public static Int64 Range(Int64[] ListOfNumbers) { return ListOfNumbers.Max() - ListOfNumbers.Min(); }

        /// <summary>
        /// Returns the input as a postive number
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static double MakePostive(double input) { if (input < 0) { return input * -1; } else { return input; } }

        /// <summary>
        /// Returns the input as a negative number
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static double MakeNegative(double input) { if (input > 0) { return input * -1; } else { return input; } }

        /// <summary>
        /// Returns a number between two given numbers
        /// </summary>
        /// <param name="Number0"></param>
        /// <param name="Number1"></param>
        /// <returns></returns>
        public static Int64 Random(Int32 Number0, Int32 Number1) { return RandInt.Next(Number0, Number1); }
    }
}
