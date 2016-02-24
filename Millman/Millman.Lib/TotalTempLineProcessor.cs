using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Millman.Interface;
using Millman.Lib.Errors;
using Millman.Lib.Domain;

namespace Millman.Lib
{
    public class TotalTempLineProcessor : ITempLineProcessor
    {
        private List<PeriodDefinition> _periods = new List<PeriodDefinition>();

        public TotalTempLineProcessor(string header)
        {
            var parts = LineSpliter(header);

            if (ValidateLineParts(parts))
            {
                for (var i = 2; i < parts.Length; i++)
                    _periods.Add(new PeriodDefinition()
                    {
                       Name = parts[i].Replace("VAL", ""),
                       PeriodIndex = i
                    });
                        
            }
            else
                throw new ApplicationException("Invalid Header format");
        }

       

        public ITotalTempLine ParseLine(int lineId,  string line)
        {
            var result = new TotalTempLine() {
                LineId = lineId
            };

            var parts = LineSpliter(line);

            if (parts.Length < 3) {
                result.InError = true;
                result.Errors.Add(LineError.InvalidLineFormat(line));
                return result;
            }

            //firstpart
            int sceneid;

            if (int.TryParse(parts[0], out sceneid))
                result.ScenarioId = sceneid;
            else {
                result.Errors.Add(LineError.InvalidSceneIdError(parts[0]));
                return result;
            }
            //parseOperation
            OperationType type = OperationType.Invalid;

            if (parts[1].TryParseOperationType(out type))
                result.LineOperation = type;
            else {
                result.Errors.Add(LineError.UnknownOperationType(parts[1]));
                return result;
            }

            for (var i = 2; i < parts.Length; i++){
                double pv;
                
                if (double.TryParse(parts[i], out pv))
                {
                    result.Values.Add(new PeriodValue()
                    {
                        PeriodId = _periods[i].Name,
                        Value = pv
                    });
                }
            }


            return result;
        }


        private string[] LineSpliter(string line)
        {
            return line.Split(new[] { '\t' }, StringSplitOptions.None);
        }

        private bool ValidateLineParts(string[] parts)
        {
            if (parts.Length < 3)
                return false;
            //other validations would go here if there are any!
            return true;
        }
    }
}
