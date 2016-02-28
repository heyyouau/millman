using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Millman.Lib;
using Millman.Lib.Domain;
using Millman.Lib.Interface;
using Moq;

namespace Millman.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class AggregateProcessorTests
    {
        [TestMethod]
        public void GenerateAggregateResultSetCalculatesAverage()
        {
            //setup
            var variableType = "X";
            var sut = new AggregateProcessor();
            var values = new List<double>() {10, 20, 30, 40};

            sut.AddResults(GetResultSet(new string[]{variableType},true, false, false, values.ToArray(), null, null));//

            //execute
            var results = sut.GenerateAggregateResultSet();


            //assert
            Assert.AreEqual(25, results.First().Value);
            Assert.AreEqual(statisticCalculation.Average.ToString(), results.First().CalculationType);
            Assert.AreEqual(variableType, results.First().VariableType);

        }


        [TestMethod]
        public void GenerateAggregateResultSetCalculatesAverageAndMaxOnSameVariable()
        {
            //setup
            var variableType = "X";
            var sut = new AggregateProcessor();
            var avvalues = new List<double>() { 10, 20, 30, 40 };
            var maxvalues = new List<double>() { 40, 50, 60, 40 };
 

            sut.AddResults(GetResultSet(new string[] { variableType }, true, true, false, avvalues.ToArray(), maxvalues.ToArray(), null));//

            //execute
            var results = sut.GenerateAggregateResultSet();


            //assert
            Assert.IsTrue(results.Any(r => r.VariableType == "X" && r.CalculationType == statisticCalculation.Average.ToString()));
            Assert.IsTrue(results.Any(r => r.VariableType == "X" && r.CalculationType == statisticCalculation.MaxValue.ToString()));
            Assert.AreEqual(25, results.First(r => r.VariableType == "X" && r.CalculationType == statisticCalculation.Average.ToString()).Value);
            Assert.AreEqual(60, results.First(r => r.VariableType == "X" && r.CalculationType == statisticCalculation.MaxValue.ToString()).Value);
            

        }

        [TestMethod]
        public void GenerateAggregateResultSetCalculatesMax()
        {
            //setup
            var variableType = "X";
            var sut = new AggregateProcessor();
            var values = new List<double>() { 10, 20, 30, 40 };


            sut.AddResults(GetResultSet(new string[] { variableType }, false, true, false, null, values.ToArray(), null));//

            //execute
            var results = sut.GenerateAggregateResultSet();


            //assert
            Assert.AreEqual(40, results.First().Value);
            Assert.AreEqual(statisticCalculation.MaxValue.ToString(), results.First().CalculationType);
            Assert.AreEqual(variableType, results.First().VariableType);

        }

        [TestMethod]
        public void GenerateAggregateResultSetCalculatesMin()
        {
            //setup
            var variableType = "X";
            var sut = new AggregateProcessor();
            var values = new List<double>() { 10, 20, 30, 40 };
  

            sut.AddResults(GetResultSet(new string[] { variableType }, false, false, true, null, null, values.ToArray()));//

            //execute
            var results = sut.GenerateAggregateResultSet();


            //assert
            Assert.AreEqual(10, results.First().Value);
            Assert.AreEqual(statisticCalculation.MinValue.ToString(), results.First().CalculationType);
            Assert.AreEqual(variableType, results.First().VariableType);

        }




        private List<TotalTempLine> GetResultSet(string[] variableTypes, bool withAverage, bool withMax, bool withMin, double[] averageValues, double[] maxValues, double[] minValues)
        {
            var testData = new List<TotalTempLine>();


            foreach (var v in variableTypes)
            {
                if (withAverage)
                    testData.AddRange(buildSet(v, statisticCalculation.Average, averageValues));
                if (withMax)
                    testData.AddRange(buildSet(v, statisticCalculation.MaxValue, maxValues));
                if (withMin)
                    testData.AddRange(buildSet(v, statisticCalculation.MinValue, minValues));
            }

          


            return testData;
        }

        private List<TotalTempLine> buildSet(string variable, statisticCalculation op, double[] values)
        {
            var testData = new List<TotalTempLine>();

            foreach (var val in values)
            {
                testData.Add(new TotalTempLine()
                {
                    HasOperation = true,
                    OperationType = op,
                    VariableType = variable,
                    ValueOfRelevance = val
                });
            }

            return testData;
        }
    }
}
