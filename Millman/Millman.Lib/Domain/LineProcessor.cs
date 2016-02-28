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

        /// <summary>
        /// VariableType_Period_OperationType
        /// </summary>
        public string Key
        {
            get
            {
                return string.Format("{0}_{1}_{2}", VariableType, Period, OperationType);
            }
        }

        
    }
}
