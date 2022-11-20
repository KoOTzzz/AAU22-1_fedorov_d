using System;
using System.Diagnostics;
using System.IO;

namespace AAU22_1_fedorov_d
{
    internal class Program
    {
        public static int Main(string[] arguments)
        {
            if (arguments.Length == 1)
            {
                int N = Int32.Parse(arguments[0]);
                double sum = 0;
                for (int i = 1; i < N; i++) sum += 1.0 / i;
                Console.WriteLine(sum);
                return 0;
            }
            else
            {
                var process = new Process();
                process.StartInfo.FileName = "ipconfig.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.Start();
                StreamReader reader = process.StandardOutput;
                string output = reader.ReadToEnd();
                string ch = "IPv4";
                int indexOfChar = output.IndexOf(ch);
                output = output.Substring(indexOfChar);
                ch = "Маска";
                indexOfChar = output.IndexOf(ch);
                output = output.Substring(0,indexOfChar);
                ch = ":";
                indexOfChar = output.IndexOf(ch);
                output = output.Substring(indexOfChar);
                process.WaitForExit();
                Console.WriteLine("No N provided! Your IP" + output);
                return 1;
            }
        }
    }
}