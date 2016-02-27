using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Millman.Lib.Domain;

namespace Millman.Lib.Interface
{
    public interface ITempLineProcessor
    {
        TotalTempLine ParseLine(int lineId, string line, ILineProcessInstructions instructions);
        void SetHeader(string line);
    }
}
