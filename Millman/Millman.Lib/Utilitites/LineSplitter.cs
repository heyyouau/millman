using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Millman.Lib.Utilitites
{
    public static class LineSplitter
    {
        public static string[] Split(string line)
        {
            return line.Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
