namespace GalaxyLearn.Core.Generators
{
    public class NameGenerator
    {
        public static string GenerateUniqueCode()
        {
           return Guid.NewGuid().ToString().Replace("-", "");
        }

        public static string GenerateRandomFourDigitNumber()
        {
            var random = new Random();
            return random.Next(1000, 9999).ToString();
        }
    }
}
