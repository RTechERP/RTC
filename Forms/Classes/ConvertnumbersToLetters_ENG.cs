using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS
{
    class ConvertnumbersToLetters_ENG
    {

        public static string MoneyToWords(double value)
        {
            decimal money = Math.Round((decimal)value, 2);
            int number = (int)money;
            int decimalValue = 0;
            string doller = string.Empty;
            string cents = string.Empty;
            doller = NumberToWords(number);
            if (money.ToString().Contains("."))
            {
                decimalValue = int.Parse(money.ToString().Split('.')[1]);
                cents = NumberToWords(decimalValue);
            }
            string result =doller;//!string.IsNullOrEmpty(cents) ? (decimalValue == 1 ? string.Format("{0} Doller and {1} Cent Only.", doller, cents) : string.Format("{0} Doller and {1} Cents Only.", doller, cents)) : string.Format("{0} Doller Only.", doller);
            return result;
        }

        public static string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            //if ((number / 1000000000) > 0)
            //{
            //    words += NumberToWords(number / 1000000) + " Billion ";
            //    number %= 1000000000;
            //}
            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " Million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " Thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " Hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
                var tensMap = new[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[(number) / 10];
                    if ((number % 10) > 0)
                        words += " " + unitsMap[(number) % 10];
                }
            }
            return words;
        }
    }
}
