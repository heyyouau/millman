using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Millman.Lib;

namespace Millman.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class InputReaderTests
    {
        [TestMethod, ExpectedException(typeof(Exception))]
        public void InvalidFileThrowsException()
        {
            var sut = new InputStreamReader("X");

        }

        [TestMethod]
        public void ConstructorThrowsNoErrorWhenFilepathIsValid()
        {
            var sut = new InputStreamReader("Configuration.txt");

        }

        [TestMethod]
        public void ReadNextReturnsNullWhenFileIsCompletelyRead()
        {
            //setup
            using(var sut = new InputStreamReader("Configuration.txt"))
            {
                //execute
                var line = sut.ReadNext();
                while (line != null)
                {
                    line = sut.ReadNext();
                }

                line = sut.ReadNext();

                //assert
                Assert.IsNull(line);
            }
        }

      
    }
}
