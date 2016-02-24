using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Millman.Interface
{
    public interface ITotalTempLine
    {
        int LineId { get; set; }
        OperationType LineOperation { get; set; }
        ILineProcessResult GetLineResult(ILineProcessor processor);
        int ScenarioId { get; set; }
        List<PeriodValue> Values { get; }
        List<ILineProcessError> Errors { get; }
        bool InError { get; set; }
    }
}
