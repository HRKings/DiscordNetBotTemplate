using Discord;
using Discord.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordNet.Modules
{
    public class HelpHandler : ModuleBase<SocketCommandContext>
    {
        private readonly CommandService _service;

        public HelpHandler(CommandService service)
        {
            _service = service;
        }

        [Command("Help")]
        [Summary("Lists all commands")]
        public async Task HelpAsync()
        {
            List<CommandInfo> commands = _service.Commands.ToList();
            EmbedBuilder embedBuilder = new EmbedBuilder();

            foreach (CommandInfo command in commands)
            {
                // Get the command Summary attribute information
                string embedFieldText = command.Summary ?? "No description available\n";

                embedBuilder.AddField(command.Name, embedFieldText);
            }

            await ReplyAsync("Here's a list of commands and their description: ", false, embedBuilder.Build());
        }
    }
}