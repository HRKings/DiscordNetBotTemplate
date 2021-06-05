namespace DiscordNet
{
    public static class Program
    {
        private static void Main()
            => new Startup().Initialize().GetAwaiter().GetResult();
    }
}