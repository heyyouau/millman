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
            if (args.Length != 2)
            {
                Console.WriteLine("Useage: Please provide the location of the total temp file and the configuration file as arguments");
                Console.ReadLine();
            }

            try
            {
                Console.WriteLine("Commencing Process");
                var tempFile = new InputStreamReader(args[0]);
                var configFile = new InputStreamReader(args[1]);
                var calcManager = new CalculationManager(new TotalTempLineProcessor());
                Console.WriteLine("Processing complete - generating results file");
                var results = calcManager.RunProcess(tempFile, configFile);
                
                var sb = new StringBuilder();
                results.ForEach(res => sb.AppendFormat("{0}\t{1}\t{2}\n", res.VariableType, res.CalculationType, res.Value));

                File.WriteAllText("output.txt", sb.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
}
