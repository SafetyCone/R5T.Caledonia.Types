using System;
using System.Diagnostics;


namespace R5T.Caledonia
{
    public class CommandLineInvocation
    {
        #region Static

        public static CommandLineInvocation New(string command, string arguments, bool suppressConsoleOutput = false)
        {
            DataReceivedEventHandler receiveOutputData;
            DataReceivedEventHandler receiveErrorData;
            if(suppressConsoleOutput)
            {
                receiveOutputData = CommandLineInvocation.DoNothing;
                receiveErrorData = CommandLineInvocation.DoNothing;
            }
            else
            {
                receiveOutputData = CommandLineInvocation.ConsoleReceiveOutputData;
                receiveErrorData = CommandLineInvocation.ConsoleReceiveErrorData;
            }

            var invocation = new CommandLineInvocation()
            {
                Command = command,
                Arguments = arguments,
                ReceiveOutputData = receiveOutputData,
                ReceiveErrorData = receiveErrorData,
            };

            return invocation;
        }

        public static void DoNothing(object sender, DataReceivedEventArgs dataReceived)
        {
            // Do nothing.
        }

        public static void ConsoleReceiveOutputData(object sender, DataReceivedEventArgs dataReceived)
        {
            if(dataReceived.Data is null)
            {
                return;
            }

            Console.Out.WriteLine(dataReceived.Data);
        }

        public static void ConsoleReceiveErrorData(object sender, DataReceivedEventArgs dataReceived)
        {
            if (dataReceived.Data is null)
            {
                return;
            }

            Console.Error.WriteLine(dataReceived.Data);
        }

        #endregion


        public string Command { get; set; }
        public string Arguments { get; set; }
        public DataReceivedEventHandler ReceiveOutputData { get; set; }
        public DataReceivedEventHandler ReceiveErrorData { get; set; }
    }
}
