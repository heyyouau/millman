using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Millman.Lib;

namespace Millman
{
    class Program
    {
        static void Main(string[] args)
        {
            
            try
            {
                if (args.Length < 2 || args.Length > 3)
                {
                    Console.WriteLine("Useage: Please provide the location of the total temp file and the configuration file (in that order) as arguments separated by spaces.");
                    Console.WriteLine("An optional third argument can be provide to specify the name of the output file. (Otherwise, the default filename \"output.txt\" will be used)");
                    throw new Exception("Invalid parameters provided.");
                }

                if (!File.Exists(args[0]))
                    throw new Exception(string.Format("Could not locate data file {0}", args[0]));

                if (!File.Exists(args[1]))
                    throw new Exception(string.Format("Could not locate config file {0}", args[1]));

                Console.WriteLine("Commencing Process");
                var tempFile = new InputStreamReader(args[0]);
                var configFile = new InputStreamReader(args[1]);
                var calcManager = new CalculationManager(new TotalTempLineProcessor());
                var results = calcManager.RunProcess(tempFile, configFile);
                Console.WriteLine("Processing complete - generating results file");

                var sb = new StringBuilder();
                results.ForEach(res => sb.AppendFormat("{0}\t{1}\t{2}\r\n", res.VariableType, res.CalculationType, res.Value));

                var outputFileName = args.Length == 3 ? args[2] : "output.txt";

                File.WriteAllText(outputFileName, sb.ToString());
                Console.WriteLine("Results have been written to {0}", outputFileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Execution Concluded with errors");
            }
            Console.ReadLine();
        }
    }
}
