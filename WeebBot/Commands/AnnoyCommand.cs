using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace WeebBot.Commands
{
    public class AnnoyCommand : Command
    {
        public AnnoyCommand()
            : base("annoy", "The bot will harass them.", "username", "The username of the player to harass.")
        {

        }

        public override string Execute(SocketMessage msg, string param)
        {
            if (string.IsNullOrWhiteSpace(param))
            {
                return "Username parameter required!";
            }

            string name = param.Trim();

            if (Program.Annoy.Contains(name))
            {
                msg.Channel.SendMessageAsync("That person is already on the annoy list.");
            }
            else
            {
                Program.Annoy.Add(name);
                msg.Channel.SendMessageAsync("Got it, " + name + " is now on my naughty list.");
            }

            return null;
        }
    }
}
