using System.Collections.Generic;
using Millman.Lib.Domain;

namespace Millman.Lib.Interface
{
    public interface IAggregateProcessor
    {
        void AddResult(TotalTempLine line);
        List<ScenarioLineAggregate> GenerateAggregateResultSet(ILineProcessInstructions instructions);
    }
}