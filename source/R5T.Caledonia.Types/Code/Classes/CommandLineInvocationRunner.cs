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

        public static CommandLineInvocationResult Run(CommandLineInvocation invocation)
        {
            var result = new CommandLineInvocationResult();

            void ReceiveOutputData(object sender, DataReceivedEventArgs e)
            {
                invocation.ReceiveOutputData(sender, e);

                result.ReceiveOutputData(sender, e);
            }

            void ReceiveErrorData(object sender, DataReceivedEventArgs e)
            {
                invocation.ReceiveErrorData(sender, e);

                result.ReceiveErrorData(sender, e);
            }

            result.ExitCode = CommandLineInvocationRunner.Run(invocation.Command, invocation.Arguments, ReceiveOutputData, ReceiveErrorData);

            return result;
        }

        public static CommandLineInvocationResult Run(string command, string arguments)
        {
            var invocation = new CommandLineInvocation()
            {
                Command = command,
                Arguments = arguments,
            };

            var result = CommandLineInvocationRunner.Run(invocation);
            return result;
        }
    }
}
