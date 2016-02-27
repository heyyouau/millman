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
    public class LineProcessInstructions : ILineProcessInstructions
    {
        private List<ScenarioLineAggregate> _aggregates = new List<ScenarioLineAggregate>();
        private Dictionary<string, ILineProcessor> _processors = new Dictionary<string, ILineProcessor>();

        public Dictionary<string, ILineProcessor> Processors
        {
            get { return _processors; }
        }

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

            _processors.Add(processor.VariableType, processor);

        }

        public double ExtractPeriodValueOfInterest(List<PeriodValue> values, string variableType)
        {
            double result = 0f;
            if (_processors.ContainsKey(variableType))
            {
                var instruction = _processors[variableType];

                switch (instruction.Period)
                {
                    case periodChoice.FirstValue:
                        result = values.First().Value;
                        break;
                    case periodChoice.LastValue:
                        result = values.Last().Value;
                        break;
                    case periodChoice.MinPeriodValue:
                        result = values.Min(t => t.Value);
                        break;
                    case periodChoice.MaxPeriodValue:
                        result = values.Max(t => t.Value);
                        break;
                }
            }
            else 
                throw new Exception("No instruction for supplied variable type");

            return result;

        }
    }
}
