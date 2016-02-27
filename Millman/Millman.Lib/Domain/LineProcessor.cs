using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Millman.Lib.Interface;

namespace Millman.Lib.Domain
{
    public class LineProcessor : ILineProcessor
    {

        public LineProcessor()
        {
            
        }

        public string VariableType { get; set; }
        public periodChoice Period { get; set; }
        public statisticCalculation OperationType { get; set; }

        
    }
}
