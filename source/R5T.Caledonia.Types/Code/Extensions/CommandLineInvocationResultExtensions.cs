using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

using R5T.Magyar.IO;


namespace R5T.Caledonia
{
    public static class CommandLineInvocationResultExtensions
    {
        /// <summary>
        /// Trims the output text (including any ending \r\n from the output).
        /// </summary>
        public static string GetOutputTextTrimmed(this CommandLineInvocationResult result)
        {
            var output = result.GetOutputText().Trim();
            return output;
        }

        public static void ReceiveOutputData(this CommandLineInvocationResult result, object sender, DataReceivedEventArgs e)
        {
            if (e.Data is null)
            {
                return;
            }

            result.AddOutput(e.Data);
        }

        public static StringReader GetOutputReader(this CommandLineInvocationResult result)
        {
            var reader = new StringReader(result.GetOutputText());
            return reader;
        }

        public static IEnumerable<string> GetOutputLines(this CommandLineInvocationResult result)
        {
            using (var reader = result.GetOutputReader())
            {
                while (!reader.ReadLineIsEnd(out var line))
                {
                    yield return line;
                }
            }
        }

        public static void ReceiveErrorData(this CommandLineInvocationResult result, object sender, DataReceivedEventArgs e)
        {
            if (e.Data is null)
            {
                return;
            }

            result.AddError(e.Data);
        }

        public static StringReader GetErrorReader(this CommandLineInvocationResult result)
        {
            var reader = new StringReader(result.GetErrorText());
            return reader;
        }

        public static IEnumerable<string> GetErrorLines(this CommandLineInvocationResult result)
        {
            using (var reader = result.GetErrorReader())
            {
                while (!reader.ReadLineIsEnd(out var line))
                {
                    yield return line;
                }
            }
        }
    }
}
