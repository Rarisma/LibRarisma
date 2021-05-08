using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRarisma
{
    static class Utils
    {
        //This removes empty strings in a list
        public static void CleanList(this List<String> InputList) { InputList.RemoveAll(str => string.IsNullOrEmpty(str)); }
    }
}
