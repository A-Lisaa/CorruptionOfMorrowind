using Serilog.Events;

namespace game {
    public static class Program {
        private static void ConfigureLogger(bool debugEnabled = false) {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");

            var loggerConfiguration = new LoggerConfiguration()
               .WriteTo.File($"logs/{timestamp}.log");

#if DEBUG
            loggerConfiguration.MinimumLevel.Debug();
#endif
            if (debugEnabled) {
                loggerConfiguration.MinimumLevel.Debug();
            }

            Log.Logger = loggerConfiguration.CreateLogger();
        }

        private static void Main(string[] args) {
            ConfigureLogger(args.Contains("debug"));

            if (Log.IsEnabled(LogEventLevel.Debug)) {
                Log.Information("Debug enabled");
            }
        }
    }
}