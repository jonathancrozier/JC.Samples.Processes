using Serilog;
using System.Diagnostics;
using System.Linq;

namespace JC.Samples.Processes.Helpers
{
    /// <summary>
    /// Provides methods to help with interacting with processes.
    /// </summary>
    public class ProcessHelper
    {
        #region Methods

        /// <summary>
        /// Creates a process that runs in the background and redirects command line output.
        /// </summary>
        /// <param name="processFilePath">The file path for the process to start</param>
        /// <param name="processArguments">The command line arguments to use</param>
        /// <returns>The created background process</returns>
        public static Process CreateBackgroundProcess(string processFilePath, string processArguments)
        {
            Log.Information("Creating background process '{0}' with arguments '{1}'", processFilePath, processArguments);

            var process                              = new Process();
            process.EnableRaisingEvents              = true;
            process.StartInfo.UseShellExecute        = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.FileName               = processFilePath;
            process.StartInfo.CreateNoWindow         = true;
            process.StartInfo.Arguments              = processArguments;
            
            Log.Information("Background process '{0}' with arguments '{1}' created", processFilePath, processArguments);

            return process;
        }

        /// <summary>
        /// Creates a process that runs in the background with no UI.
        /// </summary>
        /// <param name="processFilePath">The file path for the process to start</param>
        /// <param name="processArguments">The command-line arguments to use</param>
        /// <returns>The created hidden process</returns>
        public static Process CreateHiddenProcess(string processFilePath, string processArguments)
        {
            Log.Information("Creating hidden process '{0}' with arguments '{1}'", processFilePath, processArguments);

            var process                   = new Process();
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.FileName    = processFilePath;
            process.StartInfo.Arguments   = processArguments;

            Log.Information("Hidden process '{0}' with arguments '{1}' created", processFilePath, processArguments);

            return process;
        }

        /// <summary>
        /// Kills all instances of a specified process.
        /// </summary>
        /// <param name="processName">The name of the process to kill</param>
        /// <param name="timeout">The number of milliseconds to wait for the process to stop</param>
        public static void KillProcess(string processName, int timeout = 30000)
        {
            Log.Information("Checking if any processes named '{0}' exist", processName);

            Process[] processes = Process.GetProcessesByName(processName);

            int processesCount = processes.Count();

            if (processesCount > 0)
            {
                Log.Information("Found {0} existing process(es) named '{1}'", processesCount, processName);

                int currentProcessId = Process.GetCurrentProcess().Id;

                foreach (Process process in processes)
                {
                    if (process.Id == currentProcessId)
                    {
                        Log.Information("Ignoring existing process named '{0}' which matches current process ID of: {1}", processName, currentProcessId);
                    }
                    else
                    {
                        Log.Information("Killing existing process named '{0}'", processName);
                        process.Kill();
                        process.WaitForExit(timeout);

                        if (process.HasExited) Log.Information("Killed existing process named '{0}'", processName);
                        else Log.Information("Waited for 30 seconds, but couldn't kill process named '{0}'", processName);
                    }
                }
            }
            else Log.Information("No existing processes named '{0}' exist", processName);
        }

        /// <summary>
        /// Checks if a process is currently running.
        /// </summary>
        /// <param name="processName">The name of the process to check</param>
        /// <returns>True if the process is running, otherwise false</returns>
        public static bool ProcessIsRunning(string processName)
        {
            Log.Information("Checking if any processes named '{0}' exist", processName);
            
            Process[] processes = Process.GetProcessesByName(processName);

            int processesCount = processes.Count();

            if (processesCount > 0)
            {
                Log.Information("Found {0} existing process(es) named '{1}'", processesCount, processName);

                int currentProcessId = Process.GetCurrentProcess().Id;

                foreach (Process process in processes)
                {
                    if (process.Id == currentProcessId)
                    {
                        Log.Information("Ignoring existing process named '{0}' which matches current process ID of: {1}", processName, currentProcessId);
                    }
                    else
                    {
                        Log.Information("Found existing process named '{0}'", processName);
                        return true;
                    }
                }
            }
            else Log.Information("No existing processes named '{0}' exist", processName);

            return false;
        }

        #endregion
    }
}
