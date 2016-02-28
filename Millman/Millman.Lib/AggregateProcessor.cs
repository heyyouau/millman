using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Millman.Lib.Domain;
using Millman.Lib.Interface;

namespace Millman.Lib
{
    /// <summary>
    /// Collects the parsed line results and calcualtes the output result set.
    /// </summary>
    public class AggregateProcessor : IAggregateProcessor
    {
        private List<TotalTempLine> _lineResults = new List<TotalTempLine>();

        /// <summary>
        /// Adds a parsed result line from the input value file to the collection to be processed by GenerateAggregateResultSet
        /// </summary>
        /// <param name="line">The processed line</param>
        public void AddResults(IEnumerable<TotalTempLine> lines)
        {
            _lineResults.AddRange(lines);
        }


        /// <summary>
        /// Uses the collection of process instructions and the collected line results to create the set of calculated results
        /// </summary>
        /// <param name="instructions">The set of instructions generated from the config file</param>
        /// <returns></returns>
        public List<ScenarioLineAggregate> GenerateAggregateResultSet()
        {
            //get the distinct list of calclation types
            var types = _lineResults.Where(e => e.HasOperation).Select(t => t.VariableType).Distinct();
            var results = new List<ScenarioLineAggregate>();

            foreach (var t in types)
            {
                foreach (var op in _lineResults.Where(o => o.VariableType == t).Select(x => x.OperationType).Distinct())
                {
                    var calcValues = _lineResults.Where(e => e.HasOperation && e.VariableType == t && e.OperationType == op);
                    //calculate the stats for each operation on this data type
                    
                        var value = 0d;
                        switch (op)
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
                            CalculationType = op.ToString()
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
