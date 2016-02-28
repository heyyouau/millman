using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Millman.Lib.Interface;

namespace Millman.Lib.Errors
{
    /// <summary>
    /// An error caused by a failure of the system to be able to parse the incomming text.
    /// </summary>
    public class LineError : ILineProcessError
    {
        public static ILineProcessError InvalidSceneIdError(string invalidValue)
        {
            return new LineError()
            {
                ErrorCode = 1,
                Message = string.Format("{0} cannot be parsed into a valid SceneId", invalidValue)
            };
        }

        public static ILineProcessError InvalidLineFormat(string invalidValue)
        {
            return new LineError()
            {
                ErrorCode = 2,
                Message = string.Format("\"{0}\" has an invalid format", invalidValue)
            };
        }


        internal static ILineProcessError InvalidValueError(string invalidNumber)
        {
            return new LineError()
            {
                ErrorCode = 4,
                Message = string.Format("\"{0}\" is an invalid value", invalidNumber)
            };
        }

        private LineError()
        {

        }

        public int ErrorCode
        {
            get;
            private set;
           
        }

        public string Message
        {
            get;
            private set;
        }



      
    }
}
