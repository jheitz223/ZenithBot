using Discord;
using System;
using System.Threading.Tasks;
using System.IO;
using Discord.WebSocket;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using Discord.Audio;

namespace ZenithBot {

    public class Program {

        public static void Main(string[] args) => new Program().Start().GetAwaiter().GetResult();

        private DiscordSocketClient ZenithSocketClient;
        private CommandService ZenithCommandService;
        private IServiceProvider ZenithServiceProvider;

        public double Random(double lower, double upper) {

            Random random = new Random();
            return random.NextDouble() * (upper - lower) + lower;

        }

        public string RandomPokemon() {

            return Directory.GetFiles("C:\\Users\\jheit\\source\\repos\\ZenithBot\\ZenithBot\\pokemon")[(int)Random(0, Directory.GetFiles("C:\\Users\\jheit\\source\\repos\\ZenithBot\\ZenithBot\\pokemon").Length)];
            
        }

        public string RandomTeacher() {

            return Directory.GetFiles("C:\\Users\\jheit\\source\\repos\\ZenithBot\\ZenithBot\\teachers")[(int)Random(0, Directory.GetFiles("C:\\Users\\jheit\\source\\repos\\ZenithBot\\ZenithBot\\teachers").Length)];

        }

        public async Task Start() {

            Console.WriteLine("Starting Zenith...");
            string token = "Mzg3MDIxOTYxOTE5OTg3NzIy.DRDT0A.2w9-mPw1QfV5mo-rQ7MMCv0VOok";
            Console.Write("Creating bot client... ");
            ZenithSocketClient = new DiscordSocketClient();
            Console.WriteLine("Done!");
            Console.Write("Creating command service... ");
            ZenithCommandService = new CommandService();
            Console.WriteLine("Done!");
            Console.Write("Creating service collection... ");
            ZenithServiceProvider = new ServiceCollection()
                .AddSingleton(ZenithSocketClient)
                .AddSingleton(ZenithCommandService)
                .BuildServiceProvider();
            Console.WriteLine("Done!");

            Console.WriteLine("Starting bot...");
            await ZenithSocketClient.LoginAsync(TokenType.Bot, token);
            await ZenithSocketClient.StartAsync();
            await ZenithSocketClient.SetGameAsync("Pokémon Crystal");
            Console.WriteLine("\nZenith is now ONLINE.\n\n");
            ZenithSocketClient.MessageReceived += MessageReceived;
            await Task.Delay(-1);

        }

        private async Task PlayAudio(string file, IVoiceChannel channel) {

            var audioClient = await channel.ConnectAsync();

            var ffmpegstart = new ProcessStartInfo {
                FileName = "ffmpeg",
                Arguments = $"-i {file} -ac 2 -f s16le -ar 48000 pipe:1",
                UseShellExecute = false,
                RedirectStandardOutput = true,
            };

            var ffmpeg = Process.Start(ffmpegstart);
            var output = ffmpeg.StandardOutput.BaseStream;
            var discord = audioClient.CreatePCMStream(AudioApplication.Mixed);
            await output.CopyToAsync(discord);
            await discord.FlushAsync();            

        }

        private async Task MessageReceived(SocketMessage message) {

            Console.WriteLine(message.Author + " said: \n\t\"" + message.Content + "\"");

            if (message.Content.ToUpper() == "ZENITH, PING!" || message.Content.ToUpper() == "ZENITH, PING") {

                await message.Channel.SendMessageAsync("Pong!");

            }

            if (message.Content == "acks") await message.Channel.SendMessageAsync("Thanks, @_NeinT4Les#1448 for making such great drawings of me! <3");

            if ((message.Content.ToUpper() == "IM NOT GAY" || message.Content.ToUpper() == "I'M NOT GAY") && message.Author.Username == "Volare") {

                await message.Channel.SendMessageAsync("Yes, Socci, you *are* gay.");

            }

            if (message.Content.ToUpper() == "ITS NOT A TRAP" || message.Content.ToUpper() == "SHES NOT A TRAP" || message.Content.ToUpper() == "SHE'S NOT A TRAP" || message.Content.ToUpper() == "IT'S NOT A TRAP") {

                await message.Channel.SendMessageAsync("Sorry, but it's definitely a trap.");

            }

            if (message.Content.Substring(0, 2).ToUpper() == "IM" && message.Author.Username != "Zenith") {

                await message.Channel.SendMessageAsync("Hello," + message.Content.Substring(2) + ", I'm Zenith!");

            }else if (message.Content.Substring(0, 3).ToUpper() == "I'M" && message.Author.Username != "Zenith") {

                await message.Channel.SendMessageAsync("Hello," + message.Content.Substring(3) + ", I'm Zenith!");

            }

            if (message.Content.ToUpper() == "ZENITH, SHOW ME A GAY BOI" && message.Author.Username != "Zenith") {

                await message.Channel.SendFileAsync("C:\\Users\\jheit\\source\\repos\\ZenithBot\\ZenithBot\\pictures\\socci.jpg");
                await message.Channel.SendMessageAsync("This is Christopher Socci. He's still closeted.");

            }

            if (message.Content.ToUpper() == "ZENITH, WHO'S A BAD BOY" || message.Content.ToUpper() == "ZENITH, WHOS A BAD BOY" || message.Content.ToUpper() == "ZENITH, WHO'S A BAD BOY?" || message.Content.ToUpper() == "ZENITH, WHOS A BAD BOY?") {

                await message.Channel.SendFileAsync("C:\\Users\\jheit\\source\\repos\\ZenithBot\\ZenithBot\\pictures\\officercop.jpg");
                await message.Channel.SendMessageAsync("(This command was requested by Kieran ¯\\_(ツ)_/¯ )");

            }

                if (message.Content == "!xdc90p" && message.Author.Username == "jheitz223") {

                await message.Channel.SendMessageAsync("I will be right back, Heitz has to work on me again!");

            }

            if (message.Content == "p09cdx!" && message.Author.Username == "jheitz223") {

                await message.Channel.SendMessageAsync("I'm back everyone!");

            }

            if (message.Content.ToUpper() == "ZENITH, SHOW ME A RANDOM POKEMON" && message.Author.Username != "Zenith") {

                await message.Channel.SendFileAsync(RandomPokemon());
                await message.Channel.SendMessageAsync("Here you go!");

            }

            if (message.Content.ToUpper() == "ZENITH, SHOW ME A SHITTY TEACHER" && message.Author.Username != "Zenith") {

                await message.Channel.SendFileAsync(RandomTeacher());
                await message.Channel.SendMessageAsync("Here you go!");

            }

            if (message.Content.ToUpper() == "ZENITH, YOU'RE AMAZING" || message.Content.ToUpper() == "ZENITH, YOU'RE AMAZING!") {

                await message.Channel.SendMessageAsync("Thank you! <3");

            }

            if (message.Content.Length > 14) {

                if (message.Content.ToUpper().Substring(0, 14) == "ZENITH, SPAM: " && message.Author.Username != "Zenith") {

                    string spam = message.Content.Substring(14);
                    await message.Channel.SendMessageAsync("Okay, I will spam \"" + spam + "\" for you.");
                    for (int i = 0; i <= 19; i++) {

                        await message.Channel.SendMessageAsync(spam);

                    }
                    await message.Channel.SendMessageAsync("Spammed your message 20 times.");

                }

            }

            if (message.Content.ToUpper() == "Zenith, oof") {

                IVoiceChannel channel = null;
                channel = (message.Author as IGuildUser).VoiceChannel;
                if (channel == null) {

                    await message.Channel.SendMessageAsync("Sorry, but you have to be in a voice channel for me to play that.");

                }else {

                    await PlayAudio("sounds\\oof.mp3", channel);

                }

            }

        }

    }

}
