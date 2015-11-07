using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public static  class StringExtensions
    {
        public static bool IsNotNullOrEmpty(this string value)
        {
            bool result = value !=null && value.Trim() !=String.Empty;
            return result;
        }

    }
}
