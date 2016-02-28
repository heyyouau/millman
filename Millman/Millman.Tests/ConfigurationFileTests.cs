using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Millman.Lib;
using Millman.Lib.Domain;

namespace Millman.Tests
{
    [TestClass]
    public class ConfigurationFileTests
    {
        [TestMethod]
        public void MaxValuePeriodIsCorrectlyParsed()
        {
            //setup
            var line = string.Format("{0}\t{1}\t{2}", "CashPrem", "Average", "MaxValue");
            var sut = new LineProcessInstructions();

            //execute
            sut.AddProcessCommand(line);

            //assert
            Assert.AreEqual(1, sut.Processors.Count);
            Assert.AreEqual(periodChoice.MaxPeriodValue, sut.Processors["CashPrem_MaxPeriodValue_Average"].Period);
        }

        [TestMethod]
        public void LastPeriodIsCorrectlyParsed()
        {
            //setup
            var line = string.Format("{0}\t{1}\t{2}", "CashPrem", "Average", "LastValue");
            var sut = new LineProcessInstructions();

            //execute
            sut.AddProcessCommand(line);

            //assert
            Assert.AreEqual(1, sut.Processors.Count);
            Assert.AreEqual(periodChoice.LastValue, sut.Processors["CashPrem_LastValue_Average"].Period);
        }

        [TestMethod]
        public void FirstPeriodIsCorrectlyParsed()
        {
            //setup
            var line = string.Format("{0}\t{1}\t{2}", "CashPrem", "Average", "FirstValue");
            var sut = new LineProcessInstructions();

            //execute
            sut.AddProcessCommand(line);

            //assert
            Assert.AreEqual(1, sut.Processors.Count);
            Assert.AreEqual(periodChoice.FirstValue, sut.Processors["CashPrem_FirstValue_Average"].Period);
        }

        [TestMethod]
        public void MinPeriodValueIsCorrectlyParsed()
        {
            //setup
            var line = string.Format("{0}\t{1}\t{2}", "CashPrem", "Average", "MinValue");
            var sut = new LineProcessInstructions();

            //execute
            sut.AddProcessCommand(line);

            //assert
            Assert.AreEqual(1, sut.Processors.Count);
            Assert.AreEqual(periodChoice.MinPeriodValue, sut.Processors["CashPrem_MinPeriodValue_Average"].Period);
        }


        [TestMethod]
        public void FirstValueIsReturnedAsRelevantValue()
        {
            //setup
            var line = string.Format("{0}\t{1}\t{2}", "CashPrem", "Average", "FirstValue");
            var sut = new LineProcessInstructions();
            sut.AddProcessCommand(line);

            //execute
            var val = sut.ExtractPeriodValueOfInterest(GetTestValues(), "CashPrem");

            //assert
            Assert.AreEqual(100, val.First().Value);
        }


        [TestMethod]
        public void LastValueIsReturnedAsRelevantValue()
        {
            //setup
            var line = string.Format("{0}\t{1}\t{2}", "CashPrem", "Average", "LastValue");
            var sut = new LineProcessInstructions();
            sut.AddProcessCommand(line);

            //execute
            var val = sut.ExtractPeriodValueOfInterest(GetTestValues(), "CashPrem");

            //assert
            Assert.AreEqual(200, val.First().Value);
        }


        [TestMethod]
        public void MaxValueIsReturnedAsRelevantValue()
        {
            //setup
            var line = string.Format("{0}\t{1}\t{2}", "CashPrem", "Average", "MaxValue");
            var sut = new LineProcessInstructions();
            sut.AddProcessCommand(line);

            //execute
            var val = sut.ExtractPeriodValueOfInterest(GetTestValues(), "CashPrem");

            //assert
            Assert.AreEqual(400, val.First().Value);
        }

        [TestMethod]
        public void MinValueIsReturnedAsRelevantValue()
        {
            //setup
            var line = string.Format("{0}\t{1}\t{2}", "CashPrem", "Average", "MinValue");
            var sut = new LineProcessInstructions();
            sut.AddProcessCommand(line);

            //execute
            var val = sut.ExtractPeriodValueOfInterest(GetTestValues(), "CashPrem");

            //assert
            Assert.AreEqual(50, val.First().Value);
        }

        [TestMethod, ExpectedException(typeof(Exception))]
        public void ExceptionThrownIfUnconfiguredVariableType()
        {
            //setup
            var line = string.Format("{0}\t{1}\t{2}", "CashPrem", "Average", "MinValue");
            var sut = new LineProcessInstructions();
            sut.AddProcessCommand(line);

            //execute
            var val = sut.ExtractPeriodValueOfInterest(GetTestValues(), "x");

            
        }

        [TestMethod]
        public void MultipleCalculationsOnSameVariableCorrectlyLoad()
        {
            //setup
            var line1 = string.Format("{0}\t{1}\t{2}", "CashPrem", "Average", "MinValue");
            var line2 = string.Format("{0}\t{1}\t{2}", "CashPrem", "MaxValue", "MinValue");
            var sut = new LineProcessInstructions();
            

            //execute
            sut.AddProcessCommand(line1);
            sut.AddProcessCommand(line2);

            //assert
            Assert.AreEqual(2, sut.Processors.Count);
            Assert.IsTrue(sut.Processors.ContainsKey("CashPrem_MinPeriodValue_MaxValue"));
            Assert.IsTrue(sut.Processors.ContainsKey("CashPrem_MinPeriodValue_Average"));
        }

        private List<PeriodValue> GetTestValues()
        {
            return new List<PeriodValue>()
            {
                new PeriodValue()
                {
                    PeriodId = "000",
                    Value = 100
                },
                new PeriodValue()
                {
                    PeriodId = "000",
                    Value = 400
                },
                new PeriodValue()
                {
                    PeriodId = "000",
                    Value = 50
                },
                new PeriodValue()
                {
                    PeriodId = "000",
                    Value = 200
                },
            };
        }
    }
}
