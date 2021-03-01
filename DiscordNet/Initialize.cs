using System;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordNet.Commands;
using DiscordNet.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DiscordNet
{
    public class Initialize
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;

        public Initialize(CommandService commands = null, DiscordSocketClient client = null)
        {
            _commands = commands ?? new CommandService();
            _client = client ?? new DiscordSocketClient();
        }

        public async Task Start(Func<LogMessage, Task> baseLog)
        {
            await _client.LoginAsync(TokenType.Bot, 
                Environment.GetEnvironmentVariable("TOKEN"));
            await _client.StartAsync();
            
            _client.Ready += OnReady;
            _client.Log += baseLog;
        }

        private Task OnReady()
        {
            // Prints to the console all the servers the bot is connected to
            Console.WriteLine($"Connected to these servers as '{_client.CurrentUser.Username}': ");

            foreach (var guild in _client.Guilds) Console.WriteLine($"- {guild.Name}");

            // Choose between the activities and change to the chosen one
            _client.SetGameAsync("and Testing");
            Console.WriteLine($"Activity set to 'Playing {_client.Activity.Name}'");

            return Task.CompletedTask;
        }

        public IServiceProvider BuildServiceProvider()
        {
            return new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .AddSingleton<CommandHandlingService>()
                .BuildServiceProvider();
        }
    }
}