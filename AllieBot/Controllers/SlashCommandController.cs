using Discord.WebSocket;
using Discord;
using AllieBot.Services;
using Newtonsoft.Json;
using AllieBot.Models;
using AllieBot.Providers;
using System.Data;

namespace AllieBot.Handlers
{
    public class SlashCommandController
    {
        private SlashCommandService _slashCommandService;
        private SlashCommandProvider _slashCommandProvider;
        private PythonService _pythonProvider;

        public SlashCommandController() 
        {
            _slashCommandService = new SlashCommandService();
            _slashCommandProvider = new SlashCommandProvider();
            _pythonProvider = new PythonService();
    }
        
        //RETURNS THE LIST OF ROLES IN WHICH THE USER HAS
        public async Task ListRoleCommand(SocketSlashCommand command)
        {
            // We need to extract the user parameter from the command. since we only have one option and it's required, we can just use the first option.
            var guildUser = (SocketGuildUser)command.Data.Options.First().Value;

            // We remove the everyone role and select the mention of each role.
            var roleList = string.Join(",\n", guildUser.Roles.Where(x => !x.IsEveryone).Select(x => x.Mention));

            var embedBuilder = new EmbedBuilder()
                .WithAuthor(guildUser.ToString(), guildUser.GetAvatarUrl() ?? guildUser.GetDefaultAvatarUrl())
                .WithTitle("Roles")
                .WithDescription(roleList)
                .WithColor(Color.Green)
                .WithCurrentTimestamp();

            // Now, Let's respond with the embed.
            await command.RespondAsync(embed: embedBuilder.Build());
        }

        //RETURNS THE INFO OF A DOTA PLAYER FROM THE OPEN DOTA2 API
        public async Task ListDota2PlayerInfo(SocketSlashCommand command)
        {
            var dotaPlayerId = command.Data.Options.First().Value.ToString();

            var dotaInfo = _slashCommandService.GetPlayerAsync(dotaPlayerId ?? "");

            //transfer response into object
            dynamic respDto = JsonConvert.DeserializeObject(dotaInfo.Result) ?? "";

            //transfer object into dto for handler to use
            var dotaPlayer = new DotaPlayer()
            {
                userName = respDto.profile.personaname,
                rank = respDto.rank_tier,
            };

            //build embed
            var embedBuilder = new EmbedBuilder()
                .WithAuthor(dotaPlayer.userName)
                .WithTitle("Info")
                .WithDescription(_slashCommandProvider.ParseRank(dotaPlayer.rank))
                .WithColor(Color.Red)
                .WithCurrentTimestamp();

            //create embed and send to channel
            await command.RespondAsync(embed: embedBuilder.Build());
        }

        //RETURNS STEAM USER INFO FROM STEAM ID
        public async Task GetSteamUserGeneralInfo(SocketSlashCommand command)
        {
            //retrieve parameter from command
            var steamId = command.Data.Options.First().Value.ToString();

            //hit python provider to return 

            await command.RespondAsync(text: "COMMAND IS NOT IMPLEMENTED - BLAME HARRIS");

            //build embed (see example in ListDota2PlayerInfo)

            //create embed and send to channel (see example in ListDota2PlayerInfo)

        }
    }
}