using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeebBot.Commands;

namespace WeebBot
{
    public static class CommandParser
    {
        public static Dictionary<string, Command> Commands = new Dictionary<string, Command>();
        public const string HEADER = "*weeb";

        public static void Init()
        {
            RegisterCommand(new HelpCommand());
            RegisterCommand(new InfoCommand());
            RegisterCommand(new EnableCommand());
            RegisterCommand(new DisableCommand());
            RegisterCommand(new AnnoyCommand());
            RegisterCommand(new StopAnnoyCommand());
            RegisterCommand(new RandomCommand());
        }

        private static void RegisterCommand(Command c)
        {
            if (c == null)
                return;

            Commands.Add(c.Name, c);
        }

        public static CheckResult CheckForCommand(SocketMessage msg, out string customError)
        {
            customError = null;

            if (msg.Author.IsBot)
                return CheckResult.NOT_COMMAND; // Bots can't issue commands.

            if (msg.Author.Status != UserStatus.Online)
                return CheckResult.NOT_COMMAND;

            string content = msg.Content.Trim();

            if (content.StartsWith(HEADER))
            {
                // It is a command...

                // *weeb help - Displays all commands.
                // *weeb annoy x - Will display meme crap for every message sent from user x, regardless of random state.
                // *weeb stop-annoy x - Will stop displaying meme crap for every message sent from user x.
                // *weeb random x - Sets the 1-in-x chance to display the meme.
                // *weeb disable - Stops sending random events, and clears the annoy list.
                // *weeb enable - Enables random sending.
                // *weeb info - Info about me.

                // Check to see if they typed just that...
                if(content.Trim() == HEADER)
                {
                    return CheckResult.JUST_HEADER_ERROR;
                }

                // Find what command it is.
                string cmd = content.Replace(HEADER, "").Trim();

                string[] words = cmd.Split(' ');
                if(words.Length == 0 || words.Length > 2)
                {
                    return CheckResult.BAD_COMMAND_ERROR;
                }

                string command = words[0].Trim().ToLowerInvariant();
                if (!Commands.ContainsKey(command))
                {
                    return CheckResult.UNKNOWN_COMMAND_ERROR;
                }

                string param = null;
                if(words.Length > 1)
                    param = words[1].Trim();

                var commandObj = Commands[command];
                var error = commandObj.RunFromLine(param, msg);

                if(error != null)
                {
                    customError = error;
                    return CheckResult.INTERNAL_COMMAND_ERROR;
                }

                return CheckResult.IS_COMMAND;
            }
            else
            {
                return CheckResult.NOT_COMMAND;
            }
        }
    }

    public enum CheckResult : byte
    {
        JUST_HEADER_ERROR,
        BAD_COMMAND_ERROR,
        UNKNOWN_COMMAND_ERROR,
        INTERNAL_COMMAND_ERROR,
        IS_COMMAND,
        NOT_COMMAND
    }
}
