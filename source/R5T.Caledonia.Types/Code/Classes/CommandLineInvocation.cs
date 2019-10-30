using System;
using System.Diagnostics;


namespace R5T.Caledonia
{
    public class CommandLineInvocation
    {
        #region Static

        public static CommandLineInvocation New(string command, string arguments)
        {
            var invocation = new CommandLineInvocation()
            {
                Command = command,
                Arguments = arguments,
                ReceiveOutputData = CommandLineInvocation.DefaultReceiveOutputData,
                ReceiveErrorData = CommandLineInvocation.DefaultReceiveErrorData,
            };

            return invocation;
        }

        public static void DefaultReceiveOutputData(object sender, DataReceivedEventArgs dataReceived)
        {
            if(dataReceived.Data is null)
            {
                return;
            }

            Console.WriteLine(dataReceived.Data);
        }

        public static void DefaultReceiveErrorData(object sender, DataReceivedEventArgs dataReceived)
        {
            if (dataReceived.Data is null)
            {
                return;
            }

            Console.WriteLine(dataReceived.Data);
        }

        #endregion


        public string Command { get; set; }
        public string Arguments { get; set; }
        public DataReceivedEventHandler ReceiveOutputData { get; set; }
        public DataReceivedEventHandler ReceiveErrorData { get; set; }
    }
}
