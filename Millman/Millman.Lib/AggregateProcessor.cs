using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Millman.Lib.Domain;
using Millman.Lib.Interface;

namespace Millman.Lib
{
    public class AggregateProcessor : IAggregateProcessor
    {
        private List<TotalTempLine> _lineResults = new List<TotalTempLine>();

        public void AddResult(TotalTempLine line)
        {
            _lineResults.Add(line);
        }

        public List<ScenarioLineAggregate> GenerateAggregateResultSet(ILineProcessInstructions instructions)
        {
            //get the distinct list of calclation types
            var types = _lineResults.Where(e => e.HasOperation).Select(t => t.VariableType).Distinct();
            var results = new List<ScenarioLineAggregate>();

            foreach (var t in types)
            {
                var calcValues = _lineResults.Where(e => e.HasOperation && e.VariableType == t);
                //calculate the stats for each operation on this data type
                foreach (var inst in instructions.Processors.Where(e => e.Key == t))
                {
                    var value = 0d;
                    switch (inst.Value.OperationType)
                    {
                        case statisticCalculation.Average:
                            value = CalculateAverage(calcValues);
                            break;
                        case statisticCalculation.MinValue:
                            value = CalculateMinValue(calcValues);
                            break;
                        case statisticCalculation.MaxValue:
                            value = CalculateMaxValue(calcValues);
                            break;
                    }
                    results.Add(new ScenarioLineAggregate()
                            {
                                VariableType = t,
                                Value = value,
                                CalculationType = inst.Value.OperationType.ToString()
                            });
                }
            }

            return results;
        }

        private double CalculateAverage(IEnumerable<TotalTempLine> lines)
        {
            return lines.Sum(t => t.ValueOfRelevance)/lines.Count();
        }

        private double CalculateMinValue(IEnumerable<TotalTempLine> lines)
        {
            return lines.Min(t => t.ValueOfRelevance);
        }

        private double CalculateMaxValue(IEnumerable<TotalTempLine> lines)
        {
            return lines.Max(t => t.ValueOfRelevance);
        }
    }
}
