using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace WeebBot.Commands
{
    public class DisableCommand : Command
    {
        public DisableCommand()
            : base("disable", "Disables the bot.")
        {

        }

        public override string Execute(SocketMessage msg, string param)
        {
            if (Program.Enabled)
            {
                Program.Enabled = false;
                msg.Channel.SendMessageAsync("Why would you do this to me? My memes are funny. Actually, I was going to leave anyway. You're all lame.");
            }
            else
            {
                msg.Channel.SendMessageAsync("I'm not enabled. Please let me come back...");
            }

            return null;
        }
    }
}
