using System;
using System.Collections.Generic;
using System.Text;

namespace RomanNumerals
{
    public static class Expression
    {
        public static string ToRoman(this int number)
        {
            Dictionary<string, int> romanNumerals = new Dictionary<string, int>(){
            {"M", 1000},
            {"CM", 900},
            {"D", 500},
            {"CD", 400},
            {"C", 100},
            {"XC", 90},
            {"L", 50},
            {"XL", 40},
            {"X", 10},
            {"IX", 9},
            {"V", 5},
            {"IV", 4},
            {"I", 1},
        };
            var builder = new StringBuilder();

            foreach (var item in romanNumerals)
            {
                while (number >= item.Value)
                {
                    builder.Append(item.Key);
                    number -= item.Value;
                }
            }

            return builder.ToString();
        }


        public static int ToInt(this string romanNum)
        {
            RomanNumbers roman = new RomanNumbers();

            if (!roman.IsRomanNumeral(romanNum))
                throw new ArgumentException(romanNum + " is not a Roman number.");

            int last = 0;
            int current = 0;
            int output = 0;

            for (int i = 0; i < romanNum.Length; i++)
            {
                if (i != 0)
                    last = current;
                current = roman._romanNumerals[romanNum[i]];

                if (last < current && last != 0)
                {
                    last = current - last;
                    current = 0;
                }

                if (i + 1 == romanNum.Length)
                    output += current;

                output += last;
            }
            return output;
        }
    }
}
