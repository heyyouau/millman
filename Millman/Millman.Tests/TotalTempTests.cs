using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Millman.Lib;
using Millman.Interface;

namespace Millman.Tests
{
    [TestClass]
    public class TotalTempTests
    {

        [TestMethod]
        public void InvalidFormatReturnsObjectInError()
        {
            //setup
            var objectUnderTest = new TotalTempLineProcessor("");

            //execute
            var line = objectUnderTest.ParseLine(1, GetBadLine());

            //assert
            Assert.AreEqual(true, line.InError);
            Assert.AreEqual(1, line.Errors.Count);
            Assert.AreEqual(2, line.Errors.First().ErrorCode);
            
        }

        [TestMethod]
        public void GetCashPremTextxReturnsCashPremLine()
        {
            //setup
            var objectUnderTest = new TotalTempLineProcessor("");

            //execute
            var line = objectUnderTest.ParseLine(1, GetCashPrem());

            //assert
            Assert.AreEqual(OperationType.CashPrem, line.LineOperation);
        }

        private string GetCashPrem()
        {
            return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t", 1, "CashPrem", 0, 165215335.38, 130922548.81, 107196660.00, 92462698.42, 84655947.13);
        }

        private string GetBadLine()
        {
            return "some rubbishy line";
        }
    }
}
