using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Millman.Lib.Domain;

namespace Millman.Lib.Interface
{
    public interface ILineProcessor
    {
        string VariableType { get; set; }
        periodChoice Period { get; set; }
        statisticCalculation OperationType { get; set; }

        
    }
}
