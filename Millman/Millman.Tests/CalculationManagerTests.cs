using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Millman.Lib;
using Millman.Lib.Interface;
using Moq;

namespace Millman.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class CalculationManagerTests
    {
        [TestMethod]
        public void CalculationManagerStepsThroughInputFilesCorrectly()
        {
            //setup
            var sut = new CalculationManager(new TotalTempLineProcessor());
            var lines = new List<string>();
            lines.Add("CashPrem\tAverage\tMaxValue");
            lines.Add("CashPrem\tMinValue\tMaxValue");
            var datalines = new List<string>();
            datalines.Add("ScenId\tVarName\tValue000\tValue001\tValue002\tValue003\tValue004\tValue005");
            datalines.Add("1\tCashPrem\t0\t165215335.38\t130922548.81\t107196660.00\t92462698.42\t84655947.13");

            var confCounter = 0;
            var dataCounter = 0;
            var mockConfigStream = GetMockFileStream(confCounter, lines);
            var mockDataStream = GetMockFileStream(dataCounter, datalines);

            //execute
            var results = sut.RunProcess(mockDataStream.Object, mockConfigStream.Object);

            //assert
            Assert.AreEqual(2, results.Count);

        }

        [TestMethod, ExpectedException(typeof(Exception))]
        public void ErrorsInConfigFileCauseException()
        {
            var sut = new CalculationManager(new TotalTempLineProcessor());
            var lines = new List<string>();
            lines.Add("CashPrem\tAverageRubbish\tMaxValue");
            var datalines = new List<string>();
            datalines.Add("ScenId\tVarName\tValue000\tValue001\tValue002\tValue003\tValue004\tValue005");
            datalines.Add("1\tCashPrem\t0\t165215335.38\t130922548.81\t107196660.00\t92462698.42\t84655947.13");

            var confCounter = 0;
            var dataCounter = 0;
            var mockConfigStream = GetMockFileStream(confCounter, lines);
            var mockDataStream = GetMockFileStream(dataCounter, datalines);

            //execute
            var results = sut.RunProcess(mockDataStream.Object, mockConfigStream.Object); 
        }

        [TestMethod, ExpectedException(typeof(Exception))]
        public void ErrorsInDataFileCauseException()
        {
            var sut = new CalculationManager(new TotalTempLineProcessor());
            var lines = new List<string>();
            lines.Add("CashPrem\tAverage\tMaxValue");
            var datalines = new List<string>();
            datalines.Add("ScenId\tVarName\tValue000\tValue001\tValue002\tValue003\tValue004\tValue005");
            datalines.Add("1\tCashPrem\t0\t165215335.38m\t130922548.81\t107196660.00\t92462698.42\t84655947.13");

            var confCounter = 0;
            var dataCounter = 0;
            var mockConfigStream = GetMockFileStream(confCounter, lines);
            var mockDataStream = GetMockFileStream(dataCounter, datalines);

            //execute
            var results = sut.RunProcess(mockDataStream.Object, mockConfigStream.Object);
        }


        private Mock<IInputReader> GetMockFileStream(int lineCounter, List<string> contents)
        {
            var mis = new Mock<IInputReader>();
            mis.Setup(x => x.ReadNext()).Returns(() =>
            {
                if (lineCounter < contents.Count)
                    return contents[lineCounter++];
                return null;

            });

            return mis;
        }


    }
}
