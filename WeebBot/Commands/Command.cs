using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeebBot.Commands
{
    public abstract class Command
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string ParamName { get; private set; }
        public string ParamDescription { get; private set; }

        public Command(string name, string description, string param = null, string paramDescription = null)
        {
            Name = name;
            Description = description;
            ParamName = param;
            ParamDescription = paramDescription;
        }

        public virtual string RunFromLine(string param, SocketMessage msg)
        {
            if (string.IsNullOrWhiteSpace(param))
            {
                return Execute(msg, null);
            }
            else
            {
                return Execute(msg, param.Trim());
            }
        }

        public abstract string Execute(SocketMessage msg, string param);

        public string GetHelpLine()
        {
            return Name + (ParamName == null ? "" : " " + ParamName) + " - " + Description + (ParamName != null ? " '" + ParamName + "': " + ParamDescription : "");
        }
    }
}
