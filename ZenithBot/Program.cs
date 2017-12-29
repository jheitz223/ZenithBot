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

        public string SchoolShooter() {

            return Directory.GetFiles("C:\\Users\\jheit\\source\\repos\\ZenithBot\\ZenithBot\\school shooters")[(int)Random(0, Directory.GetFiles("C:\\Users\\jheit\\source\\repos\\ZenithBot\\ZenithBot\\school shooters").Length)];

        }

        public string AcousticChild() {

            return Directory.GetFiles("C:\\Users\\jheit\\source\\repos\\ZenithBot\\ZenithBot\\acoustic childs")[(int)Random(0, Directory.GetFiles("C:\\Users\\jheit\\source\\repos\\ZenithBot\\ZenithBot\\acoustic childs").Length)];

        }

        public string me = "<@284056426760896522>";
        public string token = System.IO.File.ReadAllLines("C:\\Users\\jheit\\source\\repos\\ZenithBot\\ZenithBot\\Zenith.token")[0];

        public async Task Start() {

            Console.WriteLine("Starting Zenith...");
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

            if ((message.Content.ToUpper() == "IM NOT GAY" || message.Content.ToUpper() == "I'M NOT GAY") && message.Author.Username == "Volare") {

                await message.Channel.SendMessageAsync("Yes, Socci, you *are* gay.");

            }

            if (message.Content == "merry xmas" && message.Author.Username == "jheitz223") await message.Channel.SendMessageAsync("Merry Christmas everyone! :D :two_hearts: :christmas_tree:");

            if (message.Content.ToUpper() == "ITS NOT A TRAP" || message.Content.ToUpper() == "SHES NOT A TRAP" || message.Content.ToUpper() == "SHE'S NOT A TRAP" || message.Content.ToUpper() == "IT'S NOT A TRAP") {

                await message.Channel.SendMessageAsync("Sorry, but it's definitely a trap.");

            }

            if (message.Content.Substring(0, 3).ToUpper() == "IM " && message.Author.Username != "Zenith") {

                await message.Channel.SendMessageAsync("Hello," + message.Content.Substring(2) + ", I'm Zenith!");

            }else if (message.Content.Substring(0, 4).ToUpper() == "I'M " && message.Author.Username != "Zenith") {

                await message.Channel.SendMessageAsync("Hello," + message.Content.Substring(3) + ", I'm Zenith!");

            }

            if (message.Content.ToUpper() == "ZENITH, SHOW ME A GAY BOI") {

                await message.Channel.SendFileAsync("C:\\Users\\jheit\\source\\repos\\ZenithBot\\ZenithBot\\pictures\\socci.jpg");
                await message.Channel.SendMessageAsync("This is Christopher Socci. He's still closeted.");

            }

            if (message.Content == "!xdc90p" && message.Author.Username == "jheitz223") {

                await message.Channel.SendMessageAsync("I will be right back, Heitz has to work on me again!");

            }

            if (message.Content == "p09cdx!" && message.Author.Username == "jheitz223") {

                await message.Channel.SendMessageAsync("I'm back everyone!");

            }

            if (message.Content.ToUpper() == "ZENITH, SHOW ME A RANDOM POKEMON") {

                await message.Channel.SendFileAsync(RandomPokemon());
                await message.Channel.SendMessageAsync("Here you go!");

            }

            if (message.Content.ToUpper() == "ZENITH, SHOW ME A SHITTY TEACHER") {

                await message.Channel.SendFileAsync(RandomTeacher());
                await message.Channel.SendMessageAsync("Welcome to :b:aint :b:oseph :b:igh s:b:hool.");

            }

            if (message.Content.ToUpper() == "ZENITH, SHOW ME A SCHOOL SHOOTER") {

                //  Suggestions:
                //  -Bustin Bill
                //  -Liam Cotter
                //  -Alex Urhik
                //  -Diljeet
                await message.Channel.SendFileAsync(SchoolShooter());
                await message.Channel.SendMessageAsync("Be sure to keep an eye out, especially when he reaches into his backpack.");

            }

            if (message.Content.ToUpper() == "ZENITH, SHOW ME AN AUTISTIC CHILD") {

                //  Suggestions:
                //  -Parmalee
                //  -Schwall
                //  -Gbogi
                //  -Diljeet
                await message.Channel.SendFileAsync(AcousticChild());
                await message.Channel.SendMessageAsync("This is as autistic as they get.");

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

            if (message.Content.ToUpper().Substring(0, 13) == "ZENITH, SAY: ") {

                if (message.Author.Username == "jheitz223") {

                    await message.Channel.SendMessageAsync(message.Content.Substring(14));

                }else {

                    await message.Channel.SendMessageAsync("Sorry, but only " + me + " can make me say stuff :P (Please don't do the \"I'm\" thing!!!)\nYou can use ```Zenith, spam: ``` if you like.");

                }

            }

        }

    }

}
