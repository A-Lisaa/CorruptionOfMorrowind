namespace utils.math {
    public static class AliceMath {
        public static string GetShortNumber(long number) {
            Dictionary<int, string> suffixes = new() {
                { 18, "E" },
                { 15, "P" },
                { 12, "T" },
                { 9, "G" },
                { 6, "M" },
                { 3, "k" },
                { 0, ""}
            };

            int powerOfTen = (int)System.Math.Log10(number);
            powerOfTen -= powerOfTen % 3;

            double numberRemainder = System.Math.Round(number / (System.Math.Pow(10, powerOfTen)), 3);

            var suffix = suffixes[powerOfTen];

            return numberRemainder.ToString() + suffix;
        }

        public static string GetShortNumber2(long number) {
            if (number >= System.Math.Pow(10, 18)) {
                double res = number / System.Math.Pow(10, 18);
                res = System.Math.Round(res * 100.0) / 100.0;
                return res + "E";
            }
            if (number >= System.Math.Pow(10, 15)) {
                double res = number / System.Math.Pow(10, 15);
                res = System.Math.Round(res * 100.0) / 100.0;
                return res + "P";
            }
            if (number >= System.Math.Pow(10, 12)) {
                double res = number / System.Math.Pow(10, 12);
                res = System.Math.Round(res * 100.0) / 100.0;
                return res + "T";
            }
            if (number >= System.Math.Pow(10, 9)) {
                double res = number / System.Math.Pow(10, 9);
                res = System.Math.Round(res * 100.0) / 100.0;
                return res + "G";
            }
            if (number >= System.Math.Pow(10, 6)) {
                double res = number / System.Math.Pow(10, 6);
                res = System.Math.Round(res * 100.0) / 100.0;
                return res + "M";
            }
            if (number >= System.Math.Pow(10, 3)) {
                double res = number / System.Math.Pow(10, 3);
                res = System.Math.Round(res * 100.0) / 100.0;
                return res + "k";
            }

            return number.ToString();
        }
    }
}