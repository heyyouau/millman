using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Millman.Lib.Domain;

namespace Millman.Lib.Utilitites
{
    public static class Extensions
    {
        public static bool TryParseOperationType(this string input, out statisticCalculation calculation)
        {
            calculation = statisticCalculation.Invalid;
            var result = true;

            switch (input.ToLower().Trim())
            {
                case "minvalue":
                    calculation = statisticCalculation.MinValue;
                    break;
                case "maxvalue":
                    calculation = statisticCalculation.MaxValue;
                    break;
                case "average":
                    calculation = statisticCalculation.Average;
                    break;
                default:
                    result = false;
                    break;
            }
            return result;
        }


        public static bool TryParsePeriod(this string input, out periodChoice choice)
        {
            choice = periodChoice.Invalid;
            var result = true;

            switch (input.ToLower().Trim())
            {
                case "firstvalue":
                    choice = periodChoice.FirstValue;
                    break;
                case "lastvalue":
                    choice = periodChoice.LastValue;
                    break;
                case "minvalue":
                    choice = periodChoice.MinPeriodValue;
                    break;
                case "maxvalue":
                    choice = periodChoice.MaxPeriodValue;
                    break;
                default:
                    result = false;
                    break;
            }
            return result;
        }
    }
}
