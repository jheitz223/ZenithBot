using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Rest;
using Discord.WebSocket;

/*
 * Zenith 3.0
 * by Jonathan Heitz
 * 2017-2022
 */

class Zenith {

    static void Main() => new Zenith().Start().GetAwaiter().GetResult();

	public readonly ulong jheitzID = 284056426760896522;
	public readonly ulong ZenithID = 387021961919987722;
	public readonly ulong JoeID = 357985448892563457;
	public readonly ulong ChrisID = 217398251329028098;
	public readonly string Me = "<@284056426760896522>";
	public readonly string Joe = "<@357985448892563457>";
	public readonly string ZenithToken = File.ReadAllLines(@"C:\Zenith3\Zenith.token")[0];
	public readonly Emoji STAR = new Emoji("⭐");
	public readonly Emoji UPSIDE_DOWN_FACE = new Emoji("🙃");
    public readonly string[] Pokemon = Directory.GetFiles(@"C:\Zenith3\pokemon");

    public readonly string[] BadWords = {

		"",
		"",
        ""

    };

	public readonly string[] JoeWords = {

		"BAD",
		"B A D",
		"KIERAN",
		"K I E R A N",
		"CHUNKS",
		"C H U N K S",
		"FAT",
		"F A T",
		"DAD",
		"D A D",
		"DAF",
		"D A F",
		"SONCLE",
		"S O N C L E",
		"S0NCLE",
		"S 0 N C L E",
		"S0NCL3",
		"S 0 N C L 3",
		"SONCL3",
		"S O N C L 3",
		"POTATO",
		"P O T A T O",
		"P0TATO",
		"POTAT0",
		"P0TAT0",
		"345070576114597889"

	};

	private DiscordSocketClient ZenithSocketClient;
    readonly Random r = new Random();

	public string Latency() => ZenithSocketClient.Latency.ToString() + "ms";
	public double Random(double lower, double upper) => r.NextDouble() * (upper - lower) + lower;

	public bool Birthday() => DateTime.Now.Month == 12 && DateTime.Now.Day == 3;


	public string RandomPokemon() {
        int index = (int)Random(0, Pokemon.Length);
        return Pokemon[index];
    }

	public string TimeUntil (DateTime Date) {

		DateTime Now = DateTime.Now;
		int years =  Date.Year - Now.Year;
		int months = Date.Month - Now.Month;
		int days = Date.Day - Now.Day;
		if (months < 0) {
			years--;
			months += 12;
		}
		if (days < 0) {
			months--;
			days += DateTime.DaysInMonth(Now.Year, Now.Month);
		}
		return years + " year(s), " + months + " month(s), and " + days + " day(s)";

	}

    public async Task Start() {

        Console.WriteLine("Starting Zenith...");
        Console.Write("Initializing bot client... ");
        ZenithSocketClient = new DiscordSocketClient();
        Console.WriteLine("Done!");
        Console.WriteLine("Logging in...");
        await ZenithSocketClient.LoginAsync(TokenType.Bot, ZenithToken);
        await ZenithSocketClient.StartAsync();
		if (Birthday()) await ZenithSocketClient.SetGameAsync("on my birthday!");
		else await ZenithSocketClient.SetGameAsync("Pokémon Crystal");
        Console.WriteLine("\nZenith is now ONLINE.\n\n");
        ZenithSocketClient.MessageReceived += MessageReceived;
        await Task.Delay(-1);

    }

    private async Task MessageReceived(SocketMessage message) {

		var channel = message.Channel as SocketGuildChannel;
        string Message = message.Content;
        string MESSAGE = Message.ToUpper();
		string Channel = channel.Name;
		string Server = channel.Guild.Name;
        string Author = message.Author.Username;
		ulong AuthorID = message.Author.Id;
		var rMessage = (RestUserMessage)await message.Channel.GetMessageAsync(message.Id);
		string Mention = message.Author.Mention;

		async Task Say(string msg) => await message.Channel.SendMessageAsync(msg);
        async Task Upload(string filePath) => await message.Channel.SendFileAsync(filePath);

        Console.WriteLine(Author + " said in #" + Channel + " on " + Server + ": \n"
            + "\t\"" + Message + "\"");

		if (AuthorID != ZenithID) {
			/*
			if (AuthorID == JoeID) {

				bool delete = false;
				for (int i = 0; i < JoeWords.Length && !delete; i++) 
					if (MESSAGE.Contains(JoeWords[i])) 
						delete = true;
				if (!delete) await Say("Shut up f.");
				else {
					await message.DeleteAsync();
					await Say("A bad message sent by " + Joe + " has been deleted.");
				}
				Thread.Sleep(1000);

			}
			*/
			if ((int)Random(1, 8192) == 69) {
				File.AppendAllText(@"A:\Domain Users\jheitz\Desktop\Shiny.txt", Message + "\n");
				await rMessage.AddReactionAsync(STAR);
				await Say(Mention + ", your message was shiny!");
			}

			for (int i = 0; i < BadWords.Length; i++) {

				if (MESSAGE.Contains(BadWords[i])) {
					await message.DeleteAsync();
					await Say("A message sent by " + Mention + " has been deleted due to inappropriate content.");
					Thread.Sleep(1000);
					break;
				}

			}

			if (Message.Contains(ZenithID.ToString()))
				await Say("Awaiting command! " + UPSIDE_DOWN_FACE);

			if (MESSAGE.StartsWith("ZENITH, PING"))
				await Say(Latency());
			else if (MESSAGE.StartsWith("ZENITH, SAY: ")) {

				if (AuthorID == jheitzID)
					await Say(message.Content.Substring(13));
				else
					await Say("Sorry, but only " + Me + " can make me say stuff.\n"
						+ "You can use ```Zenith, spam: <message>``` if you like.");

			} else if (MESSAGE.StartsWith("ZENITH, SHOW ME A RANDOM POKEMON")) {

				await Upload(RandomPokemon());
				await Say("Here you go!");

			} else if (MESSAGE.StartsWith("ZENITH, SPAM: ")) {

				if (AuthorID != JoeID) {

					string Spam = Message.Substring(14);
					await Say("Okay, I will spam \"" + Spam + "\" for you.");
					Thread.Sleep(1000);
					for (int i = 0; i < 20; i++) {
						await Say(Spam);
						Thread.Sleep(1000);
					}
					await Say("Spammed your message 20 times.");

				}
				else await Say("You don't have permission to use that command.");

			}
			else if (MESSAGE.StartsWith("IM "))
				await Say("Hello" + Message.Substring(2) + ", I'm Zenith!");
			else if (MESSAGE.StartsWith("I'M "))
				await Say("Hello" + Message.Substring(3) + ", I'm Zenith!");
			else if (MESSAGE.StartsWith("I AM "))
				await Say("Hello" + Message.Substring(4) + ", I'm Zenith!");

			if (MESSAGE.StartsWith("ZENITH, HOW") && MESSAGE.Contains("UNTIL") && MESSAGE.Contains("DRAFT")) {

				DateTime NFLDraftStart = new DateTime(2022, 4, 28);
				DateTime NFLDraftEnd = new DateTime(2022, 4, 30);
				DateTime Now = DateTime.Now;

				if (NFLDraftStart.CompareTo(Now) > 0)
					await Say("There are " + TimeUntil(NFLDraftStart) + " until the 2022 NFL Draft. It begins on April 28, 2022.");
				else if (NFLDraftStart.CompareTo(Now) <= 0 && NFLDraftEnd.CompareTo(Now) >= 0)
					await Say("The 2022 NFL Draft is happening today.");
				else
					await Say("The 2022 NFL Draft already happened.");

			}

			if (MESSAGE.StartsWith("ZENITH, ") && MESSAGE.Contains("BUTT") && MESSAGE.Contains("FUMBLE")) {

				await Say("One moment please.");
				await Upload(@"C:\Zenith3\misc\buttfumble.gif");

			}

			if (AuthorID == jheitzID && MESSAGE.StartsWith("ZENITH, GOODBYE")) {

				await Say("Shutting down...");
				Environment.Exit(0);

			}

			if (MESSAGE.Contains("COWBOYS"))
				await Say("WE DEM BOYZ :triumph:");
			if (MESSAGE.Contains("DAK") || MESSAGE.Contains("PRESCOT"))
				await Say("DAK ATTACK :fist:");
			if (MESSAGE.Contains("ZEKE") || MESSAGE.Contains("EZEKIEL") || MESSAGE.Contains("ELLIOT"))
				await Say("FEED ZEKE :cut_of_meat:");
			if (MESSAGE.Contains("PRIMETIME") || MESSAGE.Contains("PRIME TIME"))
				await Say("WE STILL PRIME TIME :clock3:");
			if (MESSAGE.Contains("AMERICA"))
				await Say("WE STILL AMERICA'S TEAM :flag_us:");
			if (MESSAGE.Contains("OAKLAND"))
				await Say("Las Vegas*");
			if (MESSAGE.Contains("REDSKINS"))
				await Say("Commanders*");
			if (MESSAGE.Contains("SAN DIEGO"))
				await Say("Los Angeles*");
			if (MESSAGE.Contains("BUCCS"))
				await Say("Bucs*");

		}

    }

}