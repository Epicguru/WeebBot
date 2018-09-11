using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace WeebBot.Commands
{
    public class RandomCommand : Command
    {
        public RandomCommand()
            : base("random", "Sets the random chance percentage.", "randomness", "A 0-100 value that a meme will be sent for each message.")
        {

        }

        public override string Execute(SocketMessage msg, string param)
        {
            if (string.IsNullOrWhiteSpace(param))
            {
                return "The randomness param is required.";
            }

            float res;
            bool worked = float.TryParse(param.Trim(), out res);

            if (!worked)
                return "Randomness param '" + param.Trim() + "' is not a number.";

            if (res < 0f)
                res = 0f;
            if (res > 100f)
                res = 100f;

            res /= 100f;

            Program.RandomChance = res;
            msg.Channel.SendMessageAsync("Random meme chance per message is now " + (res * 100f).ToString("N1") + "%");

            return null;
        }
    }
}
