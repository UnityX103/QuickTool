using UnityEngine;

namespace FrameworkExtensions.SystemClass.Uint
{
     public static class IntStaticExtension
    {
        static string[] NumbersF = new string[] { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖", "拾" };
        static string[] UnitsF = new string[] { "", "拾", "佰", "千", "万", "拾", "佰", "千", "亿", "拾" };
        
        static string[] Numbers = new string[] { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九", "十" };
        static string[] Units = new string[] { "", "十", "百", "千", "万", "十", "百", "千", "亿", "十" };

        public static string ToChinese(this int self)
        {
            bool minus = self < 0;
            uint num = (uint)Mathf.Abs(self);
            return (minus ? "负" : "") + ParseNumbers(num);
        }

        public static string ParseNumbers(uint n)
        {
            string result = "";
            string s = n.ToString();
            int zero = 0;//连续出现的零的个数
            int length = s.Length;
            for (int i = 0; i < length; i++)
            {
                int number = s[i] - '0';
                int unit = length - i - 1;
                //一十X、一十X万、一十X亿，一十X万亿开头时,省略开头的一
                if ((length + 2) % 4 == 0 && i == 0 && number == 1 && zero == 0)
                {
                    result += Units[unit];
                }
                //不为零时直接输出数字+单位
                else if (number != 0)
                {
                    result += Numbers[number] + Units[unit];
                    zero = 0;
                }
                //为零且在最后一位时，如果前面有连续零，去掉前面的零，否则不操作
                else if (unit == 0)
                {
                    if (zero > 0) result = result.Substring(0, result.Length - 1);
                    else if (length == 1) result += Numbers[0];
                }
                //是亿等级时，必然打出亿。
                //是万等级时，如果没有亿则必然打出万，如果有亿但整个万级别不都是零也打出万
                else if (unit == 8 || (unit == 4 && (length <= 8 || zero < 3)))
                {
                    if (zero > 0)
                    {
                        result = result.Substring(0, result.Length - 1);                   
                    }
                    zero = 0;//重置zero
                    result += Units[length - i - 1];
                }
                //中间普通的零
                else
                {
                    if (zero == 0)
                    {
                        result += Numbers[0];
                    }
                    zero++;
                }
            }
            return result;
        }
    }
}