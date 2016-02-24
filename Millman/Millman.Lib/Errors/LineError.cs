﻿using Millman.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Millman.Lib.Errors
{
    public class LineError : ILineProcessError
    {
        public static ILineProcessError InvalidSceneIdError(string invalidValue)
        {
            return new LineError()
            {
                ErrorCode = 1,
                Message = string.Format("{0} cannot be parsed into a valid SceneId")
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

        internal static ILineProcessError UnknownOperationType(string invalidOperation)
        {
            return new LineError()
            {
                ErrorCode = 3,
                Message = string.Format("\"{0}\" is an unknown format", invalidOperation)
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