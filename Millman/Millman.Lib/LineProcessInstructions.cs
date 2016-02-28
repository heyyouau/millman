using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Millman.Lib.Domain;
using Millman.Lib.Interface;
using Millman.Lib.Utilitites;

namespace Millman.Lib
{
    /// <summary>
    /// The collection of configuration instructions
    /// </summary>
    public class LineProcessInstructions : ILineProcessInstructions
    {
        private List<ScenarioLineAggregate> _aggregates = new List<ScenarioLineAggregate>();
        private Dictionary<string, ILineProcessor> _processors = new Dictionary<string, ILineProcessor>();

        /// <summary>
        /// Returns a dictionary of Line process instructions
        /// </summary>
        public Dictionary<string, ILineProcessor> Processors
        {
            get { return _processors; }
        }

        /// <summary>
        /// Parses a line of tab delimited text into a process instruction
        /// </summary>
        /// <param name="processLine">A tab delimited line of text that confirms to the required format</param>
        public void AddProcessCommand(string processLine)
        {
            var parts = LineSplitter.Split(processLine);

            if (parts.Length != 3)
                throw new Exception(string.Format("Invalid Configuration Line : {0}", processLine));

            var processor = new LineProcessor();
            processor.VariableType = parts[0];

            var statsOp = statisticCalculation.Invalid;
            if (parts[1].TryParseOperationType(out statsOp))
                processor.OperationType = statsOp;
            else
                throw new Exception(string.Format("Unrecognised statistics operation = {0}", parts[1]));

            var period = periodChoice.Invalid;
            if (parts[2].TryParsePeriod(out period))
                processor.Period = period;
            else
                throw new Exception(string.Format("Unrecognised period choice = {0}", parts[2]));

            _processors.Add(processor.Key, processor);

        }
        
        /// <summary>
        /// Locates a value of relevance based on the variable type 
        /// </summary>
        /// <param name="values">The set of values for the current scenario and variable type</param>
        /// <param name="variableType">The name of the variabke type</param>
        /// <returns></returns>
        public List<RelevantValue> ExtractPeriodValueOfInterest(List<PeriodValue> values, string variableType)
        {
            var results = new List<RelevantValue>();
            if (_processors.Values.Any(t => t.VariableType == variableType))
            {
                foreach (var instruction in _processors.Values.Where(e => e.VariableType == variableType))
                {

                    switch (instruction.Period)
                    {
                        case periodChoice.FirstValue:
                            results.Add(new RelevantValue(instruction, values.First().Value));
                            break;
                        case periodChoice.LastValue:
                            results.Add(new RelevantValue(instruction, values.Last().Value));
                            break;
                        case periodChoice.MinPeriodValue:
                            results.Add(new RelevantValue(instruction, values.Min(t => t.Value)));
                            break;
                        case periodChoice.MaxPeriodValue:
                            results.Add(new RelevantValue(instruction, values.Max(t => t.Value)));
                            break;
                    }
                }
            }
            else 
                throw new Exception("No instruction for supplied variable type");

            return results;

        }
    }
}
