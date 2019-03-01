using System;
using System.Xml;

namespace AALife.Core
{
    public static class Extensions
    {
        public static bool IsNullOrDefault<T>(this T? value) where T : struct
        {
            return default(T).Equals(value.GetValueOrDefault());
        }

        public static string ElText(this XmlNode node, string elName)
        {
            return node.SelectSingleNode(elName).InnerText;
        }

        public static TResult Return<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator, TResult failureValue)
            where TInput : class
        {
            return o == null ? failureValue : evaluator(o);
        }

        public static bool IsNumber(this object o)
        {
            try
            {
                Convert.ToDecimal(o);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsDateTime(this object o)
        {
            try
            {
                Convert.ToDateTime(o);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
