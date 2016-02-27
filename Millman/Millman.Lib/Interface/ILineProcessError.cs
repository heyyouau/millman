using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Millman.Lib.Interface
{
    public interface ILineProcessError
    {
        int ErrorCode { get; }
        string Message { get;  }
    }
}
