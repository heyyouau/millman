using Millman.Lib.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Millman.Lib.Interface;

namespace Millman.Lib.Domain
{
    public class TotalTempLine 
    {
        

        public TotalTempLine()
        {
            Errors = new List<ILineProcessError>();
        }

        public TotalTempLine(ILineProcessError lineProcessError):this()
        {
            Errors.Add(lineProcessError);
            InError = true;
        }

        public List<ILineProcessError> Errors
        {
            get;
            private set;
            
        }

        public bool InError
        {
            get;
            set;
        }

        public string VariableType
        {
            get;
            set;
        }

        public int ScenarioId
        {
            get;
            set;
        }



        public double ValueOfRelevance { get; set; }

        public bool HasOperation { get; set; }

        public statisticCalculation OperationType { get; set; }

        
    }
}
