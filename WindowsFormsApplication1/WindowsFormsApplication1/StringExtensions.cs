using System;

namespace WindowsFormsApplication1
{
    public static class StringExtensions
    {
        /// <summary>
        /// Overload for Contains method, enabling it to ignore CaseSensitive
        /// </summary>
        /// <param name="source"></param>
        /// <param name="toCheck"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static bool Contains(this string source, string toCheck, bool ignoreCase)
        {
            StringComparison compOption;
            if (ignoreCase) compOption = StringComparison.OrdinalIgnoreCase;
            else compOption = StringComparison.CurrentCulture;

            return source.IndexOf(toCheck, compOption) >= 0;
        }
    }
}