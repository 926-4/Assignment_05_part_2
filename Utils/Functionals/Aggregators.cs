using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBB_SE_2024_Team_42.Utils.Functionals
{
    internal class Aggregators
    {
        public static int Addition(int firstTerm, int secondTerm) => firstTerm + secondTerm;
        public static string StringConcatWithSpaceBetween(string firstString, string secondString) => $"{firstString} {secondString}";
    }
}
