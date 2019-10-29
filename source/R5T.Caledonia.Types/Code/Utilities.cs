using System;


namespace R5T.Caledonia.Types
{
    public static class Utilities
    {
        public static CommandLineInvocation NewCommandLineInvocation(string command, string arguments, CommandLineInvocationResult result)
        {
            var invocation = new CommandLineInvocation()
            {
                Command = command,
                Arguments = arguments,
                ReceiveOutputData = result.ReceiveOutputData,
                ReceiveErrorData = result.ReceiveErrorData,
            };

            return invocation;
        }
    }
}
