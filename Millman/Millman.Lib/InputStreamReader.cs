using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Millman.Lib.Interface;

namespace Millman.Lib
{
    /// <summary>
    /// Wraps around a stream reader object to assist in decoupling the input file from the processing
    /// </summary>
    public class InputStreamReader : IInputReader, IDisposable
    {

        private StreamReader _file; 

        public InputStreamReader(string path)
        {
            if (!File.Exists(path))
                throw new Exception(string.Format("Could not find file at {0}", path));
            _file = new System.IO.StreamReader(path);
        }

        public void Dispose()
        {
            _file.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Reads the next line of text from the input file.
        /// </summary>
        /// <returns>A line of text, or null if EOF</returns>
        public string ReadNext()
        {

            if (_file.EndOfStream)
                return null;

            var t = _file.ReadLine();
            if (t == null)
                _file.Close();

            return t;
        }
    }
}
