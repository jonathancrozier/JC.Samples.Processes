using JC.Samples.Processes.Helpers;
using Serilog;
using System;
using System.Diagnostics;

namespace JC.Samples.Processes
{
    /// <summary>
    /// Main Program class.
    /// </summary>
    class Program
    {
        #region Methods

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args">The command-line arguments</param>
        static void Main(string[] args)
        {
            // Configure Serilog.
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Information()
               .WriteTo.Console()
               .CreateLogger();

            // Output display heading (Create Hidden Process).
            // ===============================================
            string hiddenProcessOutputMessage = "Create Hidden Process";

            Console.WriteLine(hiddenProcessOutputMessage);
            Console.WriteLine("".PadLeft(hiddenProcessOutputMessage.Length, '='));
            Console.WriteLine();

            // Create a hidden process and start it.
            Process process = ProcessHelper.CreateHiddenProcess("notepad", @"C:\Windows\System32\drivers\etc\hosts");
            process.Start();

            // Output display heading (Processing Is Running).
            // ===============================================
            string isRunningOutputMessage = "Process Is Running";

            Console.WriteLine();
            Console.WriteLine(isRunningOutputMessage);
            Console.WriteLine("".PadLeft(isRunningOutputMessage.Length, '='));
            Console.WriteLine();

            // Check if a process is running.
            bool isRunning = ProcessHelper.ProcessIsRunning("notepad");

            // Output display heading (Kill Process).
            // ======================================
            string killProcessOutputMessage = "Kill Process";

            Console.WriteLine();
            Console.WriteLine(killProcessOutputMessage);
            Console.WriteLine("".PadLeft(killProcessOutputMessage.Length, '='));
            Console.WriteLine();

            // Stop a process.
            ProcessHelper.KillProcess("notepad");

            // Inform the user that the program has completed.
            Console.WriteLine();
            Console.WriteLine("Press any key to exit");

            Console.ReadKey();
        }

        #endregion
    }
}