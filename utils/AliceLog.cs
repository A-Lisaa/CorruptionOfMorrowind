using System.Diagnostics;

using Serilog;
using Newtonsoft.Json;

namespace utils.logger {
    public static class AliceLog {
        public static Serilog.Core.Logger GetLogger(bool debugEnabled = false) {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");

            var loggerConfiguration = new LoggerConfiguration()
                .WriteTo.File($"logs/{timestamp}.log", retainedFileCountLimit: 5);

#if DEBUG
            loggerConfiguration.MinimumLevel.Debug();
#endif
            if (debugEnabled) {
                loggerConfiguration.MinimumLevel.Debug();
            }

            return loggerConfiguration.CreateLogger();
        }
    }

    public readonly record struct LogMessage {
        private readonly string invoker;
        private readonly string message;
        private readonly string reason;
        private readonly (string Name, object Value)[] args;

        public LogMessage(string message, string reason, params (string Name, object Value)[] args) {
            this.message = message;
            this.reason = reason;
            this.args = args;
            invoker = new StackTrace(new StackFrame(1, true)).ToString().Trim();
        }

        public static implicit operator string(LogMessage logMessage) {
            Dictionary<string, object> messageDict = new() {
                { "Invoker", logMessage.invoker },
                { "Message", logMessage.message },
                { "Reason", logMessage.reason },
                { "Args", logMessage.args.ToDictionary(x => x.Name, x => x.Value) }
            };
            return JsonConvert.SerializeObject(messageDict, Formatting.Indented);
        }
    }
}
