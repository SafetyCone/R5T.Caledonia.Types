using System;
using System.Diagnostics;


namespace R5T.Caledonia
{
    /// <summary>
    /// Static functionality allowing use from any static, but explicitly-typed, context.
    /// </summary>
    public static class CommandLineInvocationRunner
    {
        /// <summary>
        /// The base run implementation. Synchronous. Returns exit code.
        /// </summary>
        public static int Run(string command, string arguments, DataReceivedEventHandler receiveOutputData, DataReceivedEventHandler receiveErrorData)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(command, arguments)
            {
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                RedirectStandardInput = true
            };

            Process process = new Process
            {
                StartInfo = startInfo
            };

            process.OutputDataReceived += receiveOutputData;
            process.ErrorDataReceived += receiveErrorData;

            process.Start();
            process.BeginOutputReadLine(); // Must occur after start?
            process.BeginErrorReadLine(); // Must occur after start?

            process.WaitForExit();

            process.OutputDataReceived -= receiveOutputData;
            process.ErrorDataReceived -= receiveErrorData;

            int exitCode = process.ExitCode; // Must get value before closing the process?

            process.Close();

            return exitCode;
        }

        public static int Run(CommandLineInvocation invocation)
        {
            var exitCode = CommandLineInvocationRunner.Run(invocation.Command, invocation.Arguments, invocation.ReceiveOutputData, invocation.ReceiveErrorData);
            return exitCode;
        }

        public static CommandLineInvocationResult Run(string command, string arguments)
        {
            var result = new CommandLineInvocationResult();

            var invocation = Types.Utilities.NewCommandLineInvocation(command, arguments, result);

            result.ExitCode = CommandLineInvocationRunner.Run(invocation);

            return result;
        }
    }
}
