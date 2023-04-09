using Serilog;

namespace utils.logger {
    public static class AliceLog {
        public static Serilog.Core.Logger GetLogger(bool debugEnabled = false) {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");

            var loggerConfiguration = new LoggerConfiguration()
               .WriteTo.File($"logs/{timestamp}.log");

#if DEBUG
            loggerConfiguration.MinimumLevel.Debug();
#endif
            if (debugEnabled) {
                loggerConfiguration.MinimumLevel.Debug();
            }

            return loggerConfiguration.CreateLogger();
        }
    }
}
