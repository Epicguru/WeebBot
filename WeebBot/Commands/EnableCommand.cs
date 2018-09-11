using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace WeebBot.Commands
{
    public class EnableCommand : Command
    {
        public EnableCommand()
            :base("enable", "Enables the bot.")
        {

        }

        public override string Execute(SocketMessage msg, string param)
        {
            if (!Program.Enabled)
            {
                Program.Enabled = true;
                msg.Channel.SendMessageAsync("I'm back bitches!");
            }
            else
            {
                msg.Channel.SendMessageAsync("Urm... I'm already enabled.");
            }

            return null;
        }
    }
}
