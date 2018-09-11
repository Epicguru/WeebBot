using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace WeebBot.Commands
{
    public class InfoCommand : Command
    {
        public InfoCommand()
            : base("info", "Tells you stuff about the bot.")
        {

        }

        public override string Execute(SocketMessage msg, string param)
        {
            StringBuilder str = new StringBuilder();
            foreach (var user in Program.Annoy)
            {
                str.Append("  > ");
                str.AppendLine(user);
            }
            msg.Channel.SendMessageAsync("James was bored and made a bot that makes really relevant memes.\n" + 
            "The bot is currently " + (Program.Enabled ? "enabled.\n" : "disabled.\n") +
            (Program.Annoy.Count > 0 ? "The current user that are being annoyed are:\n" + str.ToString() : "No users are on the annoy list.") + "\n" + 
            "The current random meme chance is " + (Program.RandomChance * 100f).ToString("N2") + "%\n" +
            "I have sent a total of " + Program.TotalSent + " really edgy dank memes."
            );

            return null;
        }
    }
}
