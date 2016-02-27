using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Millman.Lib.Errors;
using Millman.Lib.Domain;
using Millman.Lib.Interface;
using Millman.Lib.Utilitites;

namespace Millman.Lib
{
    public class TotalTempLineProcessor : ITempLineProcessor
    {
        private List<PeriodDefinition> _periods = new List<PeriodDefinition>();

        public void SetHeader(string header)
        {
            var parts = LineSplitter.Split(header);

            if (ValidateLineParts(parts))
            {
                for (var i = 2; i < parts.Length; i++)
                    _periods.Add(new PeriodDefinition()
                    {
                       Name = parts[i].Replace("Value", ""),
                       PeriodIndex = i
                    });
            }
            else
                throw new ApplicationException("Invalid Header format");
        }

        public List<PeriodDefinition> PeriodDefinitions
        {
            get
            {
                return _periods;
            }
        }

        public TotalTempLine ParseLine(int lineId, string line, ILineProcessInstructions instructions)
        {
            var result = new TotalTempLine() {
                LineId = lineId
            };

            var parts = LineSplitter.Split(line);

            if (parts.Length < 3) {
                result.InError = true;
                result.Errors.Add(LineError.InvalidLineFormat(line));
                return result;
            }

            //first part - get the scenarie
            int sceneid;

            if (int.TryParse(parts[0], out sceneid))
                result.ScenarioId = sceneid;
            else {
                result.Errors.Add(LineError.InvalidSceneIdError(parts[0]));
                return result;
            }
            

            //get the variable type for this line
            result.VariableType = parts[1];

            //only execute parsing and calculations if there is an instruction for this variable type
            if (instructions.Processors.ContainsKey(result.VariableType))
            {
                var periodValues = new List<PeriodValue>();

                //parse the values
                for (var i = 2; i < parts.Length; i++)
                {
                    double pv;

                    if (double.TryParse(parts[i], out pv))
                    {
                        periodValues.Add(new PeriodValue()
                        {
                            PeriodId = _periods[i - 2].Name,
                            Value = pv
                        });
                    }
                }
                result.HasOperation = true;
                result.ValueOfRelevance = instructions.ExtractPeriodValueOfInterest(periodValues, result.VariableType);
            }

            return result;
        }



        private bool ValidateLineParts(string[] parts)
        {
            if (parts.Length < 3)
                return false;
            //other validations would go here if there were any!
            return true;
        }
    }
}
