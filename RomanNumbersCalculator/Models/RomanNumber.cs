using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanNumbersCalculator.Models
{
    internal class RomanNumber: IComparable, ICloneable
    {
        private ushort number = 1;

        private string romanNumber = "";
        public RomanNumber(ushort number)
        {
            if (number < 1 || number > 3999) throw new RomanNumberException();

            this.number = number;

            romanNumber = "";
            
            string[] ones = { "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX" };
            string[] tens = { "", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC" };
            string[] hundreds = { "", "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM" };
            string[] thousands = { "", "M", "MM", "MMM" };

            romanNumber += thousands[(number / 1000) % 10];
            romanNumber += hundreds[(number / 100) % 10];
            romanNumber += tens[(number / 10) % 10];
            romanNumber += ones[number % 10];
        }

        public RomanNumber(string number)
        {

            romanNumber = number;

            Dictionary<char, ushort> romanToArabicNumbers = new Dictionary<char, ushort>
            {
                { 'I', 1 },
                { 'V', 5 },
                { 'X', 10 },
                { 'L', 50 },
                { 'C', 100 },
                { 'D', 500 },
                { 'M', 1000 }
            };
            if (number.Length == 1) this.number = romanToArabicNumbers[number[0]];
            else
            {
                ushort arabicNumber = 0, numberDigit = 0;
                while (numberDigit < number.Length - 1)
                {
                    if (romanToArabicNumbers[number[numberDigit]] < romanToArabicNumbers[number[numberDigit + 1]])
                    {
                        arabicNumber += (ushort)(romanToArabicNumbers[number[numberDigit + 1]] - romanToArabicNumbers[number[numberDigit]]);
                        numberDigit += 2;
                    }
                    else
                    {
                        arabicNumber += romanToArabicNumbers[number[numberDigit]];
                        numberDigit++;
                        if (numberDigit == number.Length - 1)
                            arabicNumber += romanToArabicNumbers[number[numberDigit]];
                    }
                }
                this.number = arabicNumber;
            }
            if (number != new RomanNumber(this.number).ToString()) throw new RomanNumberException();
            if (this.number < 1 || this.number > 3999) throw new RomanNumberException();
        }

        public static RomanNumber Add(RomanNumber RomanNumber1, RomanNumber RomanNumber2)
        {
            return new RomanNumber((ushort)(RomanNumber1.number + RomanNumber2.number));
        }
        public static RomanNumber Sub(RomanNumber RomanNumber1, RomanNumber RomanNumber2)
        {
            return new RomanNumber((ushort)(RomanNumber1.number - RomanNumber2.number));
        }
        public static RomanNumber Mul(RomanNumber RomanNumber1, RomanNumber RomanNumber2)
        {
            return new RomanNumber((ushort)(RomanNumber1.number * RomanNumber2.number));
        }
        public static RomanNumber Div(RomanNumber RomanNumber1, RomanNumber RomanNumber2)
        {
            return new RomanNumber((ushort)(RomanNumber1.number / RomanNumber2.number));

        }

        public int CompareTo(object? obj)
        {
            if (obj is RomanNumber num) return number.CompareTo(num.number);
            else throw new ArgumentException("Unable to compare this parameter.");
        }

        public object Clone() => MemberwiseClone();

        public override string ToString() => romanNumber;

        public ushort ToUInt16() => number;
    }
}
