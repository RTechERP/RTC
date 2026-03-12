using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BMS
{
    public static class NumberMoneyToText
    {
        //private static Regex ConvertToUnsign_rg = null;
        private static readonly string[] UNITS = { "", "nghìn", "triệu", "tỉ" };
        private static readonly string[] DIGITS = { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };

        private static string[] ones = { "", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        private static string[] tens = { "", "", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
        private static string[] thousandsGroups = { "", " thousand", " million", " billion" };

        private static List<UnitCurrency> UNIT_CURRENCY = new List<UnitCurrency>() {
            new UnitCurrency{Name = "VND", Description = new { nameenglish = "Vietnamese dong", unitenglish = "dong", namevietnamese = "đồng", unitvietnamese = "đồng"} },
            new UnitCurrency{Name = "USD", Description = new { nameenglish = "Dollars", unitenglish = "cent", namevietnamese = "đô", unitvietnamese = "cent"} },
            new UnitCurrency{Name = "EUR", Description = new { nameenglish = "Euro", unitenglish = "cent", namevietnamese = "euro", unitvietnamese = "cent"} },
            new UnitCurrency{Name = "RMB", Description = new { nameenglish = "Renminbi", unitenglish = "bad", namevietnamese = "nhân dân tệ", unitvietnamese = "bad"} },
            new UnitCurrency{Name = "JPY", Description = new { nameenglish = "Japanese yen", unitenglish = "sen", namevietnamese = "yên", unitvietnamese = "sen"} },
        };

        #region Chuyển số tiền thành chữ tiếng việt
        private static List<string> ReadTwo(int b, int c, bool hasHundred)
        {
            var output = new List<string>();

            switch (b)
            {
                case 0:
                    if (hasHundred && c == 0)
                        break;
                    if (hasHundred)
                        output.Add("lẻ");
                    if (c != 0)
                        output.Add(DIGITS[c]);
                    break;

                case 1:
                    output.Add("mười");
                    if (c == 5)
                        output.Add("lăm");
                    else if (c != 0)
                        output.Add(DIGITS[c]);
                    break;

                default:
                    output.Add(DIGITS[b]);
                    output.Add("mươi");
                    if (c == 1)
                        output.Add("mốt");
                    else if (c == 5)
                        output.Add("lăm");
                    else if (c != 0)
                        output.Add(DIGITS[c]);
                    break;
            }

            return output;
        }

        private static List<string> ReadThree(int a, int b, int c, bool readZeroHundred)
        {
            var output = new List<string>();

            if (a != 0 || readZeroHundred)
                output.AddRange(new[] { DIGITS[a], "trăm" });

            output.AddRange(ReadTwo(b, c, a != 0 || readZeroHundred));
            return output;
        }

        public static string ConvertNumberToText(string num)
        {
            var needZeroCount = num.Length % 3;
            if (needZeroCount != 0)
                needZeroCount = 3 - needZeroCount;

            num = new string('0', needZeroCount) + num;

            var output = new List<string>();
            for (var i = 0; i < num.Length / 3; i++)
            {
                int a = TextUtils.ToInt(num.Substring(i * 3, 1));
                int b = TextUtils.ToInt(num.Substring(i * 3 + 1, 1));
                int c = TextUtils.ToInt(num.Substring(i * 3 + 2, 1));

                bool isFirstGroup = i == 0 || (a == 0 && b == 0 && c == 0);
                output.AddRange(ReadThree(a, b, c, !isFirstGroup));

                if (a != 0 || b != 0 || c != 0)
                {
                    var unit = UNITS[num.Length / 3 - 1 - i];
                    if (unit != "")
                        output.Add(unit);
                }
            }

            return string.Join(" ", output);
        }

        //public static string ConvertNumberToTextVietNamese(decimal num, string currencyType)
        //{
        //    string numberText = TextUtils.ToString(num);
        //    string numberMoneyText = "";
        //    if (!string.IsNullOrEmpty(numberText.Trim()))
        //    {
        //        string decimalPart = "";
        //        List<string> number = new List<string>();
        //        if (numberText.Contains('.'))
        //        {
        //            string intergerPart = numberText.Substring(0, numberText.LastIndexOf('.'));
        //            decimalPart = numberText.Substring(numberText.LastIndexOf('.') + 1);
        //            string separator = numberText.Substring(numberText.LastIndexOf('.'), 1);

        //            if (decimalPart == "00")
        //            {
        //                number.Add(intergerPart);
        //            }
        //            else
        //            {
        //                number.AddRange(new[] { intergerPart, separator, decimalPart });
        //            }
        //        }
        //        else
        //        {
        //            number.Add(numberText);
        //        }

        //        //string currenyName = "";
        //        //string currenyUnit = "";
        //        var currencyUnit = UNIT_CURRENCY.FirstOrDefault(x => x.Name == currencyType);
        //        string currenyName = "";
        //        string currenyUnit = "";
        //        if (currencyUnit != null)
        //        {
        //            var descriptionCurrency = currencyUnit.Description.GetType();
        //            if (currencyType == "RMB" & decimalPart != "00")
        //            {
        //                currenyUnit = TextUtils.ToString(descriptionCurrency.GetProperty("namevietnamese").GetValue(currencyUnit.Description));
        //            }
        //            else if (currencyType == "RMB" & decimalPart == "00")
        //            {
        //                currenyName = TextUtils.ToString(descriptionCurrency.GetProperty("namevietnamese").GetValue(currencyUnit.Description));
        //            }
        //            else
        //            {
        //                currenyName = TextUtils.ToString(descriptionCurrency.GetProperty("namevietnamese").GetValue(currencyUnit.Description));
        //                currenyUnit = TextUtils.ToString(descriptionCurrency.GetProperty("unitvietnamese").GetValue(currencyUnit.Description));
        //            }

        //            //var descriptionCurrency = currencyUnit.Description.GetType();
        //            //currenyName = TextUtils.ToString(descriptionCurrency.GetProperty("namevietnamese").GetValue(currencyUnit.Description));
        //            //currenyUnit = TextUtils.ToString(descriptionCurrency.GetProperty("unitvietnamese").GetValue(currencyUnit.Description));
        //        }

        //        List<string> output = new List<string>();
        //        for (int i = 0; i < number.Count; i++)
        //        {
        //            if (i == 0)
        //            {
        //                output.Add(ConvertNumberToText(number[i]).Trim());
        //                //output.Add("đồng");
        //                if (!string.IsNullOrEmpty(currenyName))
        //                {
        //                    output.Add(currenyName);
        //                }
        //            }
        //            else if (i == 1)
        //            {
        //                output.Add(currencyType == "RMB" ? "phẩy" : "và");
        //            }
        //            else
        //            {
        //                output.Add(ConvertNumberToText(number[i]).Trim());

        //                if (!string.IsNullOrEmpty(currenyUnit))
        //                {
        //                    output.Add(currenyUnit);
        //                }
        //            }
        //        }

        //        //if (number.Count == 1) output.Add("chẵn");

        //        numberMoneyText = string.Join(" ", output);
        //        if (!string.IsNullOrEmpty(numberMoneyText))
        //        {
        //            numberMoneyText = numberMoneyText[0].ToString().ToUpper() + numberMoneyText.Substring(1);
        //        }
        //    }

        //    return numberMoneyText + ".";
        //}

        public static string ConvertNumberToTextVietNamese(decimal num, string currencyType)
        {
            string numberText = TextUtils.ToString(num);
            string numberMoneyText = "";
            if (!string.IsNullOrEmpty(numberText.Trim()))
            {
                string decimalPart = "";
                List<string> number = new List<string>();
                if (numberText.Contains('.'))
                {
                    string intergerPart = numberText.Substring(0, numberText.LastIndexOf('.'));
                    decimalPart = numberText.Substring(numberText.LastIndexOf('.') + 1);
                    string separator = numberText.Substring(numberText.LastIndexOf('.'), 1);

                    if (decimalPart == "00")
                    {
                        number.Add(intergerPart);
                    }
                    else
                    {
                        number.AddRange(new[] { intergerPart, separator, decimalPart });
                    }
                }
                else
                {
                    number.Add(numberText);
                }

                //string currenyName = "";
                //string currenyUnit = "";
                var currencyUnit = UNIT_CURRENCY.FirstOrDefault(x => x.Name == currencyType);
                string currenyName = "";
                string currenyUnit = "";
                if (currencyUnit != null)
                {
                    var descriptionCurrency = currencyUnit.Description.GetType();
                    if (currencyType == "RMB" & decimalPart != "00")
                    {
                        currenyUnit = TextUtils.ToString(descriptionCurrency.GetProperty("namevietnamese").GetValue(currencyUnit.Description));
                    }
                    else if (currencyType == "RMB" & decimalPart == "00")
                    {
                        currenyName = TextUtils.ToString(descriptionCurrency.GetProperty("namevietnamese").GetValue(currencyUnit.Description));
                    }
                    else
                    {
                        currenyName = TextUtils.ToString(descriptionCurrency.GetProperty("namevietnamese").GetValue(currencyUnit.Description));
                        currenyUnit = TextUtils.ToString(descriptionCurrency.GetProperty("unitvietnamese").GetValue(currencyUnit.Description));
                    }

                    //var descriptionCurrency = currencyUnit.Description.GetType();
                    //currenyName = TextUtils.ToString(descriptionCurrency.GetProperty("namevietnamese").GetValue(currencyUnit.Description));
                    //currenyUnit = TextUtils.ToString(descriptionCurrency.GetProperty("unitvietnamese").GetValue(currencyUnit.Description));
                }

                List<string> output = new List<string>();
                for (int i = 0; i < number.Count; i++)
                {
                    if (i == 0)
                    {
                        string numtotext = number[i].Trim();
                        output.Add(ConvertNumberToText(number[i]).Trim());
                        //output.Add("đồng");
                        if (!string.IsNullOrEmpty(currenyName))
                        {
                            output.Add(currenyName);
                        }
                    }
                    else if (i == 1)
                    {
                        output.Add(currencyType == "RMB" ? "phẩy" : "và");
                    }
                    else
                    {
                        if (i == 2)
                        {
                            var digits = number[i].ToCharArray();
                            List<string> decimalWords = new List<string>();
                            foreach (var d in digits)
                            {
                                //decimalWords.Add(ConvertDigitToText(d));

                                var dd= TextUtils.ToInt64(d);
                            }
                                //decimalWords.Add(DocSo(TextUtils.ToInt64(number[i])));
                                decimalWords.Add(DocSoTu1Den99(TextUtils.ToInt(number[i])));
                            //for (int j = 0; j < digits.Count(); j++)
                            //{
                            //    decimalWords.Add(ConvertDigitToText(digits[j], j));
                            //    if (digits[0] != '0' && decimalWords.Count() > 0) decimalWords[0] += "mươi";
                            //}
                            output.Add(string.Join(" ", decimalWords).Trim());
                        }
                        else
                        {
                            string numtotext = number[i].Trim();
                            output.Add(ConvertNumberToText(number[i]).Trim());
                        }

                        if (!string.IsNullOrEmpty(currenyUnit))
                        {
                            output.Add(currenyUnit);
                        }
                    }
                }

                //if (number.Count == 1) output.Add("chẵn");

                numberMoneyText = string.Join(" ", output);
                if (!string.IsNullOrEmpty(numberMoneyText))
                {
                    numberMoneyText = numberMoneyText[0].ToString().ToUpper() + numberMoneyText.Substring(1);
                }
            }

            return numberMoneyText + ".";
        }
        //private static string ConvertDigitToText(char digit)
        //{
        //    switch (digit)
        //    {
        //        //case '0': return i == 0 ? "" : "mươi";
        //        case '1': return "một";
        //        case '2': return "hai";
        //        case '3': return "ba";
        //        case '4': return "bốn";
        //        case '5': return "năm";
        //        case '6': return "sáu";
        //        case '7': return "bảy";
        //        case '8': return "tám";
        //        case '9': return "chín";
        //        default: return "";
        //    }
        //}


        private static readonly string[] ChuSo =
        { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };

        public static string DocSoTu1Den99(int so)
        {
            //if (so < 0 || so > 99)
            //    throw new ArgumentOutOfRangeException(nameof(so), "Chỉ hỗ trợ từ 0 đến 99.");

            if (so < 10)
                return ChuSo[so];

            int chuc = so / 10;
            int donvi = so % 10;

            string kq = "";

            if (chuc == 1)
            {
                kq = "mười";
                if (donvi == 1)
                    kq += " một";
                else if (donvi == 5)
                    kq += " lăm";
                else if (donvi > 0)
                    kq += " " + ChuSo[donvi];
            }
            else
            {
                kq = ChuSo[chuc] + " mươi";

                if (donvi == 1)
                    kq += " mốt";
                else if (donvi == 5)
                    kq += " lăm";
                else if (donvi > 0)
                    kq += " " + ChuSo[donvi];
            }

            return kq.Trim();
        }

        public static string DocSo(long so)
        {
            if (so == 0) return "không";

            string chuoi = "";
            int hangDonVi, hangChuc, hangTram;
            string[] donViNhom = { "", " nghìn", " triệu", " tỷ" };
            int i = 0;

            while (so > 0)
            {
                int baSoCuoi = (int)(so % 1000);
                so /= 1000;

                if (baSoCuoi > 0)
                {
                    string nhom = DocBaSo(baSoCuoi);
                    chuoi = nhom + donViNhom[i] + (chuoi != "" ? " " + chuoi : "");
                }

                i++;
            }

            return chuoi.Trim();
        }

        private static string DocBaSo(int so)
        {
            int tram = so / 100;
            int chuc = (so % 100) / 10;
            int donvi = so % 10;

            string kq = "";

            if (tram > 0)
            {
                kq += ChuSo[tram] + " trăm";
                if (chuc == 0 && donvi > 0) kq += " lẻ";
            }

            if (chuc > 0 && chuc != 1)
            {
                kq += (kq != "" ? " " : "") + ChuSo[chuc] + " mươi";
                if (chuc > 1 && donvi == 1) kq += " mốt";
            }
            else if (chuc == 1)
            {
                kq += (kq != "" ? " " : "") + "mười";
                if (donvi == 1) kq += " một";
            }

            if (chuc != 0 && donvi == 5)
            {
                kq += " lăm";
            }
            else if (donvi > 0 && !(chuc == 0 && tram == 0))
            {
                if (!(chuc == 1 && donvi == 1))
                    kq += " " + ChuSo[donvi];
            }

            return kq.Trim();
        }
        #endregion

        #region Chuyển số tiền thành chữ tiếng anh
        public static string ConvertNumberToTextEnglish(decimal number, string currencyType)
        {

            if (number == 0) return "zero";

            string words = "";

            if (number < 0)
            {
                words += "minus ";
                number = Math.Abs(number);
            }

            // Split the number into integer and decimal parts
            long intPart = (long)number;
            int decimalPart = (int)((number - intPart) * 100);

            int thousandsCount = 0;

            // Convert integer part to words
            while (intPart > 0)
            {
                if (intPart % 1000 != 0)
                {
                    words = ConvertToWordsUnder1000((int)(intPart % 1000)) + thousandsGroups[thousandsCount] + " " + words;
                }
                intPart /= 1000;
                thousandsCount++;
            }

            var currencyUnit = UNIT_CURRENCY.FirstOrDefault(x => x.Name.ToLower().Trim() == currencyType.ToLower().Trim());
            string unit = "";
            string minUnit = "";
            if (currencyUnit != null)
            {
                var descriptionCurrency = currencyUnit.Description.GetType();
                if (currencyType == "RMB" & decimalPart > 0)
                {
                    minUnit = TextUtils.ToString(descriptionCurrency.GetProperty("nameenglish").GetValue(currencyUnit.Description));
                }
                else if (currencyType == "RMB" & decimalPart <= 0)
                {
                    unit = TextUtils.ToString(descriptionCurrency.GetProperty("nameenglish").GetValue(currencyUnit.Description));
                }
                else
                {
                    unit = TextUtils.ToString(descriptionCurrency.GetProperty("nameenglish").GetValue(currencyUnit.Description));
                    minUnit = TextUtils.ToString(descriptionCurrency.GetProperty("unitenglish").GetValue(currencyUnit.Description));
                }
            }
            words += unit + (decimalPart > 0 ? " " : ".");

            // Convert decimal part to words
            if (decimalPart > 0)
            {
                string joinText = currencyType == "RMB" ? "point " : "and ";
                words += joinText + ConvertToWordsUnder1000(decimalPart) + $" {minUnit}.";
            }
            words = words[0].ToString().ToUpper() + words.Substring(1);
            return words;
        }

        private static string ConvertToWordsUnder1000(int number)
        {
            string words = "";

            if (number >= 100)
            {
                words += ones[number / 100] + " hundred";
                number %= 100;
            }

            if (number >= 20)
            {
                words += ((words != "") ? " " : "") + tens[number / 10];
                number %= 10;
            }

            if (number > 0)
            {
                words += ((words != "") ? " " : "") + ones[number];
            }

            return words;
        }
        #endregion

        /// <summary>
        /// Chuyển tên tiếng việt sang tiếng anh
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ConvertVietnameseToEnglish(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return "";
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public class UnitCurrency
        {
            public string Name { get; set; }
            public object Description { get; set; }
        }
    }
}
