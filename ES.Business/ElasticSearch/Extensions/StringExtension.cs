using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace ES.Business.ElasticSearch
{
    /// <summary>
    /// 
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string GetIndex<T>(this string index) where T : class
        {
            return !string.IsNullOrWhiteSpace(index) ? index : typeof(T).Name.ToLower();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToFirstLower(this string str)
        {
            return str.Substring(0, 1).ToLower() + str.Substring(1);
        }

        /// <summary>
        ///     Convert a string to camel-case.
        /// </summary>
        /// <param name="value">Input string to be camel-cased.</param>
        /// <returns>String that has been converted to camel-case.</returns>
        public static string ToCamelCase(this string value)
        {
            var words = Regex.Split(value, "(?<!(^|[A-Z]))(?=[A-Z])|(?<!^)(?=[A-Z][a-z])");
            return string.Concat(words.First().ToLowerInvariant(), string.Concat(words.Skip(1)));
        }
    }
}