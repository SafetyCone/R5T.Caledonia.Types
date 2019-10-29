using System;
using System.Text;


namespace R5T.Caledonia
{
    public class CommandLineInvocationResult
    {
        public int ExitCode { get; set; }
        public bool AnyError { get; private set; } = false;
        private StringBuilder OutputCollector { get; } = new StringBuilder();
        private StringBuilder ErrorCollector { get; } = new StringBuilder();


        public void AddOutput(string line)
        {
            this.OutputCollector.AppendLine(line);
        }

        public string GetOutputText()
        {
            var output = this.OutputCollector.ToString();
            return output;
        }

        public void AddError(string line)
        {
            this.AnyError = true;

            this.ErrorCollector.AppendLine(line);
        }

        public string GetErrorText()
        {
            var output = this.ErrorCollector.ToString();
            return output;
        }
    }
}
