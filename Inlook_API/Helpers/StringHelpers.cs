using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inlook_API.Helpers
{
    public static class StringHelpers
    {
        public static string FirstCharToUpper(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
            return char.ToUpper(input[0]) + input[1..];
        }
    }
}
