using Millman.Interface;
using Millman.Lib.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Millman.Lib
{
    public class TotalTempLine : ITotalTempLine
    {

        public TotalTempLine()
        {
            Errors = new List<ILineProcessError>();
            Values = new List<PeriodValue>();
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

        public int LineId
        {
            get;
            set;

        }

        public OperationType LineOperation
        {
            get;
            set;
        }

        public int ScenarioId
        {
            get;
            set;
        }

        public List<PeriodValue> Values
        {
            get;
            private set;
        }

        public ILineProcessResult GetLineResult(ILineProcessor processor)
        {
            throw new NotImplementedException();
        }


    }
}
