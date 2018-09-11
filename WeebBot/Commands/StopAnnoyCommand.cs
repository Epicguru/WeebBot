using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace WeebBot.Commands
{
    public class StopAnnoyCommand : Command
    {
        public StopAnnoyCommand()
            : base("stop-annoy", "The bot will stop harassing them.", "username", "The username of the player to stop harassing.")
        {

        }

        public override string Execute(SocketMessage msg, string param)
        {
            if (string.IsNullOrWhiteSpace(param))
            {
                return "Username parameter required!";
            }

            string name = param.Trim();

            if (!Program.Annoy.Contains(name))
            {
                msg.Channel.SendMessageAsync("That person is not on the annoy list.");
            }
            else
            {
                Program.Annoy.Add(name);
                msg.Channel.SendMessageAsync("Ok, " + name + " is no longer on the naughty list. I'm gonna keep my eye on them...");
            }

            return null;
        }
    }
}
