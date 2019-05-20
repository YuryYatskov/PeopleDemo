using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyControls
{
    public static class Placement
    {
        public static Func<IList> FuncGetMyTabControlItemsSource { get; set; }

        internal static IList GetMyTabControlItemsSource(Func<IList> func) => func();
    }
}
