using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS
{
	class ConvertnumbersToLetters_JAPAN
	{
		private static readonly string[] DIGITS = {
			"",
			"",
			"二",
			"三",
			"四",
			"五",
			"六",
			"七",
			"八",
			"九"
		};


        public static string generateNumber(double thanhtien)
        {
			//convert number to array int
			string thanhtienstring = thanhtien.ToString();
			int index_numbeer = thanhtienstring.Length;
			//Convert numbers To array int
			int[] digits = new int[index_numbeer];
			for (int i = 0; i < thanhtienstring.Length; i++)
			{
				digits[i] = TextUtils.ToInt(thanhtienstring.Substring(i, 1));
			}
			// 0 or nothing
			if (digits.Length == 0 || (digits.Length == 1 && digits[0] == 0))
                return GetZero();

			var generatedNumber = new List<string>();

            // seperate the digits into groups
            for (int power = 0; power < digits.Length; power += 4)
            {
                int index = digits.Length - 1 - power;
                generatedNumber.InsertRange(0,generateDigitGroup(digits, index, power));
            }

            // lots of zeroes? -> 0
            if (generatedNumber.Count == 0)
                generatedNumber.Add(GetZero());

			//string result=string.Join("", generatedNumber);
			return join(generatedNumber);

		}


        private static List<string> generateDigitGroup(int[] digits, int lastIndex, int digitGroupPower)
		{
			var digitGroup = new List<string>();
			bool containsNonZero = false;
			bool isOne = true;

			for (int power = 0, index = lastIndex; power < 4 && index >= 0; power++, index--)
			{
				int digit = digits[index];

                if (digit < 0 || digit > 9)
                    throw new Exception("Digits are only numbers from 0 to 9");

                if (digit == 0)
					continue;
				containsNonZero = true;

				if (power != 0 || digit != 1)
					isOne = false;

				string digitString = getDigit(digit, power);
				if (digitString != null && !string.IsNullOrEmpty(digitString))
					digitGroup.Insert(0, digitString);
			}

			if (isOne && digitGroupPower != 0)
				digitGroup.Clear();

			if (containsNonZero && digitGroupPower != 0)
			{
				string suffix = getDigitGroupSuffix(digitGroupPower);

				if (suffix == null)
					throw new Exception("The number is too big");

				if (!string.IsNullOrEmpty(suffix))
					digitGroup.Add(suffix);
			}

			return digitGroup;
		}
		protected static string GetZero()
		{
			return "零";
		}
		protected static string getDigitGroupSuffix(int power)
		{
			switch (power)
			{
				case 4:
					return "万";
				case 8:
					return "億";
			}

			return null;
		}
		protected static string getDigit(int digit, int power)
		{
			switch (power)
			{
				case 0:
					return digit == 1 ? "一" : DIGITS[digit];
				case 1:
					return DIGITS[digit] + "十";
				case 2:
					return DIGITS[digit] + "百";
				case 3:
					return DIGITS[digit] + "千";
			}

			return null;
		}
		protected static string join(List<string> generatedNumber)
		{
			return string.Join("", generatedNumber);
		}
	}

}