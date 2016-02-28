using Millman.Lib.Domain;
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
    /// Iterates over the incoming temp file and adds the parsed results to the result collection for aggrergate processing
    /// </summary>
    public class CalculationManager
    {
        private readonly ITempLineProcessor _lineProcessor;

        public CalculationManager(ITempLineProcessor lineProcessor)
        {
            _lineProcessor = lineProcessor;
        }
        public List<ScenarioLineAggregate> RunProcess(IInputReader tempFileReader, IInputReader configFileReader)
        {
            try
            {
                var instructions = new LineProcessInstructions();
                var tempLineResults = new AggregateProcessor();

                var line = configFileReader.ReadNext();
                while (line != null)
                {
                    instructions.AddProcessCommand(line);
                    line = configFileReader.ReadNext();
                }


                var lineNumber = 0;
                var tLine = tempFileReader.ReadNext();
                while (tLine != null)
                {
                    if (lineNumber == 0)
                        _lineProcessor.SetHeader(tLine);
                    else
                    {
                        var results = _lineProcessor.ParseLine(tLine, instructions);
                        if (results.Any(e => e.InError))
                            throw new Exception(string.Join(",", results.SelectMany(e => e.Errors.Select(m => string.Format("Error Code: {0}\tMessage: {1}\n", m.ErrorCode, m.Message)))));
                        
                        tempLineResults.AddResults(results);
                    }

                    lineNumber++;
                    tLine = tempFileReader.ReadNext();
                }
                return tempLineResults.GenerateAggregateResultSet();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
