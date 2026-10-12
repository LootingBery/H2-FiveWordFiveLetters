namespace CollectionTester;
internal static class Rng
{
    private static readonly Random SRandom = new Random();
    public static int Range(int lower, int higher) { return SRandom.Next(lower, higher); }
}