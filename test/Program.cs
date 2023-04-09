using Serilog;

using utils.logger;

namespace test {
    public static class Program {
        public static void Main() {
            Log.Logger = AliceLog.GetLogger(true);
        }
    }
}