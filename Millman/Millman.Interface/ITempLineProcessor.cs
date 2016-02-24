using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Millman.Interface
{
    public interface ITempLineProcessor
    {
        ITotalTempLine ParseLine(int lineId, string line);
    }
}
