using AllieBot.Handlers;
using Discord;
using Discord.Net;
using Discord.WebSocket;
using Newtonsoft.Json;
using AllieBot.Providers;
using AllieBot.Services;

public class Program
{
	private DiscordSocketClient _client;
    private DiscordSocketConfig _discordSocketConfig;
    public PythonService _pythonProvider;
    public SlashCommandService _slashCommandService;
    public SlashCommandProvider _slashCommandProvider;

    public Program()
    {
        _discordSocketConfig = new DiscordSocketConfig { MessageCacheSize = 100 };
        _client = new DiscordSocketClient(_discordSocketConfig);
    }

    static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();
	
	public async Task MainAsync()
	{
        // When working with events that have Cacheable<IMessage, ulong> parameters,
        // you must enable the message cache in your config settings if you plan to
        // use the cached message entity.

		await _client.LoginAsync(TokenType.Bot, File.ReadAllText("token.txt"));
		await _client.StartAsync();

		_client.MessageUpdated += MessageUpdated;
		_client.Ready += InitClient;
        _client.SlashCommandExecuted += SlashCommandHandler;
		
		await Task.Delay(-1);
	}

	public async Task InitClient()
	{
        //TODO: MAKE THIS BETTER
        ulong guildId = 531954312885305344;

        var guildCommand = new Discord.SlashCommandBuilder()
            .WithName("list-roles")
            .WithDescription("Lists all roles of a user")
            .AddOption("user", ApplicationCommandOptionType.User, "The user whos roles you want to be listed", isRequired: true);

        var dotaPlayerCommand = new Discord.SlashCommandBuilder()
            .WithName("dota-player-info")
            .WithDescription("Lists player info given SteamID3 or Dota Buff ID")
            .AddOption("user", ApplicationCommandOptionType.Number, "The SteamID3 or Dota Buff ID of the user you want to pull information on", isRequired: true);

        var steamProfileCommand = new Discord.SlashCommandBuilder()
            .WithName("find-steam-user")
            .WithDescription("Lists the passed steam user given steam id")
            .AddOption("user", ApplicationCommandOptionType.String, "The SteamId of the steam user you want to pull information on", isRequired: true);

        //build discord commands
        try
        {            
            await _client.Rest.CreateGuildCommand(steamProfileCommand.Build(), guildId);
            await _client.Rest.CreateGuildCommand(guildCommand.Build(), guildId);
            await _client.Rest.CreateGuildCommand(dotaPlayerCommand.Build(), guildId);
        }
        catch (HttpException exception)
        {
            var json = JsonConvert.SerializeObject(exception.Errors, Formatting.Indented);
            Console.WriteLine(json);
        }
    }

    private async Task SlashCommandHandler(SocketSlashCommand command)
    {
        var _slashCommands = new SlashCommandController();
        
        // Let's add a switch statement for the command name so we can handle multiple commands in one event.
        switch (command.Data.Name)
        {
            case "list-roles":
                await _slashCommands.ListRoleCommand(command);
                break;
            case "dota-player-info":
                await _slashCommands.ListDota2PlayerInfo(command);
                break;
            case "find-steam-user":
                await _slashCommands.GetSteamUserGeneralInfo(command);
                break;
        }
    }

    private async Task MessageUpdated(Cacheable<IMessage, ulong> before, SocketMessage after, ISocketMessageChannel channel)
	{
		// If the message was not in the cache, downloading it will result in getting a copy of `after`.
		var message = await before.GetOrDownloadAsync();
		Console.WriteLine($"{message} -> {after}");
	}
}