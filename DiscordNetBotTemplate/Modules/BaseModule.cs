using System.Threading.Tasks;
using Discord.Commands;

namespace DiscordNetBotTemplate.Modules;

public class BaseModule : ModuleBase<SocketCommandContext>
{
    [Command("ping")]
    [Summary("Pong!")]
    public Task PongAsync()
        => ReplyAsync("Pong from C#.");

    [Command("hello")]
    [Summary("Hello World!")]
    public Task HelloWorldAsync()
        => ReplyAsync("Hello World! from C#.");
}