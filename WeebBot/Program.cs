using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeebBot
{
    public class Program
    {
        public static bool Enabled = true;
        public static float RandomChance = 5f / 100f;
        public static List<string> Annoy = new List<string>();
        public static long TotalSent = 0;

        public static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            var client = new DiscordSocketClient();

            client.Log += Log;

            client.MessageReceived += UponMessage;

            string token = "YourTokenHere";

            // Load up commands.
            CommandParser.Init();

            // Log in.
            Console.Write("Logging bot in...");
            await client.LoginAsync(TokenType.Bot, token);
            Console.WriteLine("Done!");

            // Start.
            Console.Write("Starting bot up...");
            await client.StartAsync();
            Console.WriteLine("Done!");

            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine("[LOG] " + msg.ToString());
            return Task.CompletedTask;
        }

        private async Task UponMessage(SocketMessage msg)
        {
            Console.WriteLine("[MSG] [" + msg.Author.Username + "] " + msg.Content.Trim());

            // Not from a bot...
            if (msg.Author.IsBot)
                return;

            if (msg.Author.Status != UserStatus.Online)
                return;

            string error;
            var result = CommandParser.CheckForCommand(msg, out error);

            switch (result)
            {
                case CheckResult.JUST_HEADER_ERROR:
                    await msg.Channel.SendMessageAsync("Why did you type just that, maggot. Do '" + CommandParser.HEADER + " help' if you have brain damage and forgot how to use the bot.");
                    break;
                case CheckResult.BAD_COMMAND_ERROR:
                    await msg.Channel.SendMessageAsync("What do you even mean? Do '" + CommandParser.HEADER + " help' if you have brain damage and forgot how to use the bot.");
                    break;
                case CheckResult.UNKNOWN_COMMAND_ERROR:
                    await msg.Channel.SendMessageAsync("That's not a real command, idiot. Do '" + CommandParser.HEADER + " help' if you have brain damage and forgot how to use the bot.");
                    break;
                case CheckResult.INTERNAL_COMMAND_ERROR:
                    await msg.Channel.SendMessageAsync("Well done, you broke the bot: " + error);
                    break;
                case CheckResult.IS_COMMAND:
                    // Do nothing, output or processing already done.
                    break;
                case CheckResult.NOT_COMMAND:
                    // TODO memeify
                    if(ShouldSendMeme(msg))
                        await SendMeme(msg);
                    break;
            }
        }

        private Random rand = new Random();
        private bool ShouldSendMeme(SocketMessage msg)
        {
            if (Program.Enabled == false)
                return false;

            if (Annoy.Contains(msg.Author.Username))            
                return true;            

            double chance = Program.RandomChance;
            var r = rand.NextDouble();
            return r <= RandomChance;
        }

        private async Task SendMeme(SocketMessage msg)
        {
            string tempPath = Path.GetTempFileName().Replace(".tmp", ".png");
            var bitmap = BitmapDrawer.GetMeme(msg.Content);
            bitmap.Save(tempPath, System.Drawing.Imaging.ImageFormat.Png);
            bitmap.Dispose();
            await msg.Channel.SendFileAsync(tempPath);
            File.Delete(tempPath);
            System.GC.Collect();

            TotalSent++;
        }
    }
}
