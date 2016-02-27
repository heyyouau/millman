using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Millman.Lib.Domain
{
    public enum statisticCalculation
    {
        MinValue,
        MaxValue,
        Average,
        Invalid
    }

    public enum periodChoice
    {
        FirstValue,
        LastValue,
        MinPeriodValue,
        MaxPeriodValue,
        Invalid
    }
}
