using Smod2.Commands;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;

namespace ExamplePlugin
{
    class Commands : ICommandHandler
    {
        private ExamplePlugin plugin;
        public void SaveCommand(ExamplePlugin plugin)
        {
            this.plugin = plugin;
        }

        public string GetCommandDescription()
        {
            // This prints when someone types HELP SAVE
            return "Saves a set of items for the role/class.";
        }

        public string GetUsage()
        {
            return "SAVE";
        }

        //public string[] OnCall(ICommandSender sender, string[] args)
        //{
        //    return new string[] { "Currently all this command does is send this string!" };
        //}

        public string[] OnCall(ICommandSender sender, string[] args)
        {
            //string path = @"..\k.json";
            if (args.Length > 0)
            {
                Dictionary<string, List<string>> _data = new Dictionary<string, List<string>>();
                string x = args[0].ToLower();
                string path = @"..\k.json";
                List<string> item = new List<string>();
                int i = 0;
                do
                {
                    item.Add(args[i]);
                    i++;
                } while (i <= args.Length);
                _data.Add(x, item);
                using (StreamWriter file = File.CreateText(path))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, _data);
                }
                return new string[] { "Saved configuration" };
            }
            else
            {
                return new string[] { GetUsage() };
            }

        }

    }
}
//save something 15,2,26,1
//save owner 1,2,3
//save laneklfhak 