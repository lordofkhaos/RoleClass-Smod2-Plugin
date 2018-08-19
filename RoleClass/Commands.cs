using Smod2.Commands;
//using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.Threading;

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
            return "SAVE" + " RANK " + "CLASS " + "ITEMS LIST";
        }

        //public string[] OnCall(ICommandSender sender, string[] args)
        //{
        //    return new string[] { "Currently all this command does is send this string!" };
        //}

        public string[] OnCall(ICommandSender sender, string[] args)
        {
            //plugin.Info("Here");
            //return new string[] { args.Length.ToString() };
            //string path = @"..\k.json";
            if (args != null && args.Length > 0)
            {
                //Server.Write("stuff", ConsoleColor.Magenta);
                //Dictionary<string, Dictionary<string, List<string>>> _data = new Dictionary<string, Dictionary<string, List<string>>>();
                string x = args[0].ToLower();
                string path = @"..\config.xml";
                if (args.Length > 1)
                {
                    string cl = args[1];
                    //Dictionary<string, List<string>> classitems = new Dictionary<string, List<string>>();
                    //classitems.Add(cl, items);
                    if (args != null && args.Length > 2) 
                    {
                        //int len = args.Length - 2;
                        //string[] items = new string[len];

                        XmlWriter xmlWriter = XmlWriter.Create(path);

                        xmlWriter.WriteStartDocument();
                        xmlWriter.WriteStartElement("ranks");

                        xmlWriter.WriteStartElement(x);
                        xmlWriter.WriteStartElement(args[1]);

                        for (int i = 2; i < args.Length; i++)
                        {
                            //items[i - 2] = args[i];
                            xmlWriter.WriteString(args[i]);
                        }

                        xmlWriter.WriteEndElement();

                        xmlWriter.WriteEndDocument();
                        xmlWriter.Close();

                        //_data.Add(x, classitems);

                        //using (StreamWriter file = File.CreateText(path))
                        //{
                        //    XmlSerializer ser = new XmlSerializer(typeof(Dictionary<string, Dictionary<string, List<string>>>));
                        //    ser.Serialize(file, _data);
                        //    //JsonSerializer serializer = new JsonSerializer();
                        //    //serializer.Serialize(file, _data);
                        //}
                        return new string[] { "Saved configuration" };
                    }
                    else
                    {
                        return new string[] { GetUsage() };
                    }
                }
                else
                {
                    return new string[] { GetUsage() };
                }
            }
            else
            {
                return new string[] { GetUsage() };
            }

        }

    }
}
//save something something 15,2,26,1
//save owner something 1,2,3
//save laneklfhak sakfneoiqoia 928,129u48,127487