using System.Collections.Generic;
using Millman.Lib.Domain;

namespace Millman.Lib.Interface
{
    public interface ILineProcessInstructions
    {
        void AddProcessCommand(string processLine);
        List<RelevantValue> ExtractPeriodValueOfInterest(List<PeriodValue> values, string variableType);
        Dictionary<string, ILineProcessor> Processors { get; }
    }
}