using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace DiscordNet.Commands.Modules
{
    // Create a module with no prefix
    public class BaseModule : ModuleBase<SocketCommandContext>
    {
        // ~say hello world -> hello world
        [Command("ping")]
        [Summary("Pong!")]
        public Task PongAsync()
            => ReplyAsync("Pong from C#.");

        [Command("hello")]
        [Summary("Hello World!")]
        public Task HelloWorldAsync()
            => ReplyAsync("Hello World! from C#.");

        // ReplyAsync is a method on ModuleBase

        [Command("purge")]
        [Summary("Deletes the specified amount of messages.")]
        public async Task PurgeChat(uint amount)
        {
            var messages = await Context.Channel.GetMessagesAsync((int)amount + 1).FlattenAsync();

            await ((SocketTextChannel)Context.Channel).DeleteMessagesAsync(messages);
            var m = await this.ReplyAsync($"Purge completed.");
            await m.DeleteAsync();
        }
    }
}