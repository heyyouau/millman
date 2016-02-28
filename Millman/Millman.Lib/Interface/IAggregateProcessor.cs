using System.Collections.Generic;
using Millman.Lib.Domain;

namespace Millman.Lib.Interface
{
    public interface IAggregateProcessor
    {
        void AddResults(IEnumerable<TotalTempLine> lines);
        List<ScenarioLineAggregate> GenerateAggregateResultSet();
    }
}