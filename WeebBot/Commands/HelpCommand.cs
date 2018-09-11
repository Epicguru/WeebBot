using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeebBot.Commands
{
    public class HelpCommand : Command
    {
        public HelpCommand()
            : base("help", "Displays help and a list of commands.")
        {

        }

        public override string Execute(SocketMessage msg, string param)
        {
            StringBuilder content = new StringBuilder();

            content.AppendLine("Real men don't need help " + msg.Author.Username + ", but I guess you're a pussy:");
            content.AppendLine();
            foreach (var cmd in CommandParser.Commands)
            {
                content.Append("  >");
                content.AppendLine(cmd.Value.GetHelpLine().Trim());
            }

            msg.Channel.SendMessageAsync(content.ToString());

            return null;
        }
    }
}
