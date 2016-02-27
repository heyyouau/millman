using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Millman.Lib.Interface;

namespace Millman.Lib
{
    public class InputStreamReader : IInputReader
    {

        private StreamReader _file; 

        public InputStreamReader(string path)
        {
            if (!File.Exists(path))
                throw new Exception(string.Format("Could not find file at {0}", path));
            _file = new System.IO.StreamReader(path);
        }


        public string ReadNext()
        {
            var t = _file.ReadLine();
            if (t == null)
                _file.Close();

            return t;
        }
    }
}
