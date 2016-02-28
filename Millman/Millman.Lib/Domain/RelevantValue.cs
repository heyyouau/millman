using Millman.Lib.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Millman.Lib.Domain
{
    public class RelevantValue
    {
        public RelevantValue()
        {

        }

        public RelevantValue(ILineProcessor processInstructions, double value)
        {
            Value = value;
            Operation = processInstructions.OperationType;
            PeriodChoice = processInstructions.Period;
        }
        public double Value  { get; set; }
        public statisticCalculation Operation { get; set; }
        public periodChoice PeriodChoice { get; set; }
    }
}
