using game.items.junk;
using game.items.weapons.melee.generic.axes.oneHand;
using Serilog;

namespace game
{

    public static class Program {

        public static void Main() {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("logs/game.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            OneHandAxeEbony axe = new();
            Glass glass = new();

            Log.Debug("Все хуйня, закрываем лавочку");

            Console.WriteLine(axe);
            Console.WriteLine(glass);
        }
    }
}