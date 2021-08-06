using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Currency.Controllers
{
    public static class IsNumeri
    {
        public static bool IsNumeric(this string s)
        {
            float output;
            return float.TryParse(s, out output);
        }
    }
}
