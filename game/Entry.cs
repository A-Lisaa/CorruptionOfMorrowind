using game.creatures;
using game.items.magic;


namespace game
{

    public static class Entry {

        public static void Enchant(SoulGem soulGem) {
            if (soulGem.SoulSize == null) {
                Console.WriteLine("Soul gem is empty");
                return;
            }

            Console.WriteLine($"Enchanted item using {soulGem}");

            if (!soulGem.Features.HasFlag(SoulGem.SoulGemFeatures.Indestructible)) {
                Console.WriteLine("Destroyed soul gem");
                return;
            }
            Console.WriteLine("Soul gem is indestructible");
        }

        public static void Main() {
            Creeper creeper = new();

            AzuraStarSoulGem azura = new(creeper);

            Enchant(azura);

            PettySoulGem pettySoulGem = new(creeper);

            Enchant(pettySoulGem);

            CommonSoulGem commonSoulGem = new();

            Enchant(commonSoulGem);
        }
    }
}