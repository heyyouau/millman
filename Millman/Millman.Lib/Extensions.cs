using Millman.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Millman.Lib
{
    public static class Extensions
    {
        public static bool TryParseOperationType(this string test, out OperationType operationType)
        {
            var result = false;
            operationType = OperationType.Invalid;

            test = test.ToUpperInvariant();
            switch (test)
            {
                case "AVEPOLLOANYIELD":
                    operationType = OperationType.AvePolLoanYield;
                    break;
                case "CASHPREM":
                    operationType = OperationType.CashPrem;
                    break;
                case "RESVASSUMED":
                    operationType = OperationType.ResvAssumed;
                    break;
            }

            result = operationType != OperationType.Invalid;


            return result;
        }
    }
}
