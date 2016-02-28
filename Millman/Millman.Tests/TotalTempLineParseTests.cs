using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Millman.Lib;
using Millman.Lib.Domain;
using Millman.Lib.Interface;
using Moq;


namespace Millman.Tests
{
    [TestClass]
    public class TotalTempLineParseTests
    {
        private List<double> _testValues = new List<double>() { 165215335.38, 130922548.81, 107196660.00, 92462698.42, 84655947.13 };
            
            [TestMethod]
        public void ValidHeaderIsCorrectlyParsed()
        {
            //setup // execute
            var objectUnderTest = new TotalTempLineProcessor();
            objectUnderTest.SetHeader(GetValidHeader());


            
            //assert
            Assert.AreEqual(5, objectUnderTest.PeriodDefinitions.Count);

        }

        [TestMethod]
        public void InvalidFormatReturnsObjectInError()
        {
            //setup
            var objectUnderTest = new TotalTempLineProcessor();
            objectUnderTest.SetHeader(GetValidHeader());
            //execute
            var lines = objectUnderTest.ParseLine(GetBadLine(), null);

            //assert
            Assert.AreEqual(true, lines.Any(e => e.InError));
            Assert.AreEqual(1, lines.First().Errors.Count);
            Assert.AreEqual(2, lines.First().Errors.First().ErrorCode);
            
        }

        [TestMethod]
        public void GetLineWithoutInstructionReturnsEmptyCollection()
        {
            //setup
            var objectUnderTest = new TotalTempLineProcessor();
            objectUnderTest.SetHeader(GetValidHeader());
            var moqInstructions = new Mock<ILineProcessInstructions>();
            moqInstructions.Setup(x => x.Processors).Returns(new Dictionary<string, ILineProcessor>());

            //execute
            var lines = objectUnderTest.ParseLine(GetCashPrem(), moqInstructions.Object);

            //assert
            Assert.AreEqual(0, lines.Count());
        }



        [TestMethod]
        public void ParseLineWithConfigurationForDataTypeSetsHasOperationToTrue()
        {
            //setup
            var objectUnderTest = new TotalTempLineProcessor();
            objectUnderTest.SetHeader(GetValidHeader());
            var moqInstructions = new Mock<ILineProcessInstructions>();
            var testInstruction = new LineProcessor()
            {
                Period = periodChoice.LastValue,
                OperationType = statisticCalculation.Average,
                VariableType = "CashPrem"
            };
            moqInstructions.Setup(x => x.Processors).Returns(new Dictionary<string, ILineProcessor>()
            {
                {testInstruction.Key, testInstruction}    
            });
            moqInstructions.Setup(t => t.ExtractPeriodValueOfInterest(It.IsAny<List<PeriodValue>>(), It.IsAny<string>()))
                .Returns(new List<RelevantValue>() { new RelevantValue() { Value = _testValues[4] } });

            //execute
            var lines = objectUnderTest.ParseLine(GetCashPrem(), moqInstructions.Object);

            //assert
            Assert.IsTrue(lines.First().HasOperation);
            Assert.AreEqual(_testValues[4], lines.First().ValueOfRelevance);
        }


        private LineProcessInstructions GetInstructions(string variableType, string stats, string period)
        {
            var instructions = new LineProcessInstructions();
            instructions.AddProcessCommand(string.Format("{0}\t{1}\t{2}\t", variableType, stats, period));
            return instructions;
        }


        private string GetValidHeader()
        {
            return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t", "ScenId", "VarName", "Value000", "Value001", "Value002", "Value003", "Value004");
        }

        private string GetCashPrem()
        {
            return string.Format("{0}\t{1}\t{2}", 1, "CashPrem", string.Join("\t", _testValues));
        }

        private string GetBadLine()
        {
            return "some rubbishy line";
        }
    }
}
