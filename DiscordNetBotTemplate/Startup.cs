using System;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordNetBotTemplate.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DiscordNetBotTemplate
{
    public class Startup
    {
        private DiscordSocketClient _client;
        
        public async Task Initialize()
        {
            // You should dispose a service provider created using ASP.NET
            // when you are finished using it, at the end of your app's lifetime.
            // If you use another dependency injection framework, you should inspect
            // its documentation for the best way to do this.
            await using var services = ConfigureServices();
            _client = services.GetRequiredService<DiscordSocketClient>();

            _client.Log += LogAsync;
            services.GetRequiredService<CommandService>().Log += LogAsync;

            // Tokens should be considered secret data and never hard-coded.
            // We can read from the environment variable to avoid hardcoding.
            await _client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("DISCORD_BOT_TOKEN"));
            await _client.StartAsync();

            // Here we initialize the logic required to register our commands.
            await services.GetRequiredService<CommandHandlingService>().InitializeAsync();

            await Task.Delay(Timeout.Infinite);
        }
        
        private Task OnReady()
        {
            // Logs the bot name and all the servers that it's connected to
            Console.WriteLine($"Connected to these servers as '{_client.CurrentUser.Username}': ");
            foreach (var guild in _client.Guilds) 
                Console.WriteLine($"- {guild.Name}");

            // Set the activity from the environment variable or fallback to 'I'm alive!'
            _client.SetGameAsync(Environment.GetEnvironmentVariable("DISCORD_BOT_ACTIVITY") ?? "I'm alive!", 
                type: ActivityType.CustomStatus);
            Console.WriteLine($"Activity set to '{_client.Activity.Name}'");

            return Task.CompletedTask;
        }

        private Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log.ToString());

            return Task.CompletedTask;
        }

        private static ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton<CommandService>()
                .AddSingleton<CommandHandlingService>()
                .BuildServiceProvider();
        }
    }
}