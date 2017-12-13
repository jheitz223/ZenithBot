using Discord;
using System;
using System.Threading.Tasks;
using System.IO;
using Discord.WebSocket;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace ZenithBot {

    public class Program {

        public static void Main(string[] args) => new Program().Start().GetAwaiter().GetResult();

        //public string logName = "C:\\Users\\jheit\\source\\repos\\ZenithBot\\ZenithLogs\\log" + DateTime.Now + ".txt";

        private DiscordSocketClient ZenithSocketClient;
        private CommandService ZenithCommandService;
        private IServiceProvider ZenithServiceProvider;

        public double Random(double lower, double upper) {

            Random random = new Random();
            return random.NextDouble() * (upper - lower) + lower;

        }

        public string RandomPokemon() {

            return Directory.GetFiles("pokemon")[(int)Random(0, Directory.GetFiles("pokemon").Length)];
            
        }

        public async Task Start() {

            string token = "Mzg3MDIxOTYxOTE5OTg3NzIy.DRDT0A.2w9-mPw1QfV5mo-rQ7MMCv0VOok";
            //File.Create(logName);
            ZenithSocketClient = new DiscordSocketClient();
            ZenithCommandService = new CommandService();
            ZenithServiceProvider = new ServiceCollection()
                .AddSingleton(ZenithSocketClient)
                .AddSingleton(ZenithCommandService)
                .BuildServiceProvider();
            
            await ZenithSocketClient.LoginAsync(TokenType.Bot, token);
            await ZenithSocketClient.StartAsync();
            ZenithSocketClient.MessageReceived += MessageReceived;
            await Task.Delay(-1);

        }

        private async Task MessageReceived(SocketMessage message) {

            //if (message.Content.Length >= 9) {

            //    int j = 1;
            //    for (int i = 0; i < message.Content.Length; i++) {

            //        if (message.Content.Length >= 9 && message.Content.Substring(i, (10 - j)).ToUpper() == "IM NOT GAY" && message.Author.Username == "@Volare#8079") {

            //            await message.Channel.SendMessageAsync("Yes, Socci, you *are* gay.");
            //            break;

            //        }else if (message.Content.Length >= 10 && message.Content.Substring(i, (11 - j)).ToUpper() == "I'M NOT GAY") {

            //            await message.Channel.SendMessageAsync("Yes, Socci, you *are* gay.");
            //            break;

            //        }
            //        j++;

            //    }

            //}

            if (message.Content.ToUpper() == "ZENITH, PING!" || message.Content.ToUpper() == "ZENITH, PING") {

                await message.Channel.SendMessageAsync("Pong!");

            }

            if (message.Content.Substring(0, 2).ToUpper() == "IM") {

                await message.Channel.SendMessageAsync("Hello," + message.Content.Substring(2) + ", I'm Zenith!");

            }else if (message.Content.Substring(0, 3).ToUpper() == "I'M") {

                await message.Channel.SendMessageAsync("Hello," + message.Content.Substring(3) + ", I'm Zenith!");

            }

            if (message.Content.ToUpper() == "ZENITH, SHOW ME A GAY BOI") {

                await message.Channel.SendFileAsync("pictures\\socci.jpg");
                await message.Channel.SendMessageAsync("This is Christopher Socci. He's still closeted.");

            }

            if (message.Content == "!xdc90p") {

                await message.Channel.SendMessageAsync("I will be right back, Heitz has to work on me again!");

            }

            if (message.Content.ToUpper() == "ZENITH, SHOW ME A RANDOM POKEMON") {

                await message.Channel.SendFileAsync(RandomPokemon());
                await message.Channel.SendMessageAsync("Here you go!");

            }

        }

        private Task Log(LogMessage msg) {

            string[] thing = new string[4096];
            thing[0] = msg.ToString();
            Console.WriteLine(msg.ToString());
            //File.AppendAllLines(logName, thing);
            return Task.CompletedTask;

        }

    }

}
