using Smod2.Commands;
using Smod2;
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
            Smod2.API.Player a = (Smod2.API.Player)sender;
            var s64 = a.SteamId;
            string inf = "Player: " + a ;
            string inf2 = "SteamId: " + s64 ;
            plugin.Debug(inf);
            plugin.Debug(inf2);
            //plugin.Info("Here");
            //return new string[] { args.Length.ToString() };
            //string path = @"..\k.json";
            if (args != null && args.Length > 0)
            {
                //Dictionary<string, Dictionary<string, List<string>>> _data = new Dictionary<string, Dictionary<string, List<string>>>();
                string x = args[0].ToLower();
                string path1 = @"rc-config-1.xml";
                if (args.Length > 1)
                {
                    string cl = args[1].ToLower();
                    //Dictionary<string, List<string>> classitems = new Dictionary<string, List<string>>();
                    //classitems.Add(cl, items);
                    if (args != null && args.Length > 2) 
                    {
                        int len = args.Length - 2;
                        //string[] itemlist = new string[len];

                        XmlDocument xmlDoc = new XmlDocument();
                        XmlNode rootNode = xmlDoc.CreateElement("ranks");
                        xmlDoc.AppendChild(rootNode);

                        XmlNode rank = xmlDoc.CreateElement(x);
                        XmlAttribute cla = xmlDoc.CreateAttribute("class");
                        cla.Value = cl;
                        rank.Attributes.Append(cla);

                        XmlNode items = xmlDoc.CreateElement("items");

                        for (int i = 2; i < args.Length; i++)
                        {
                            int itemint = i - 2;
                            string itemname = "item" + itemint;
                            XmlNode item = xmlDoc.CreateElement(itemname);
                            item.InnerText = args[i];
                        }

                        xmlDoc.AppendChild(items);
                        rootNode.AppendChild(rank);

                        xmlDoc.Save(path1);
                        //XmlWriter xmlWriter = XmlWriter.Create(path);

                        //xmlWriter.WriteStartDocument();
                        //xmlWriter.WriteStartElement("ranks");

                        //xmlWriter.WriteStartElement(x);
                        //xmlWriter.WriteStartElement(args[1]);

                        //for (int i = 2; i < args.Length; i++)
                        //{
                        //    //itemlist[i - 2] = args[i];
                        //    //xmlWriter.WriteString(args[i]);
                        //}

                        //xmlWriter.WriteEndElement();

                        //xmlWriter.WriteEndDocument();
                        //xmlWriter.Close();

                        return new string[] { "Saved configuration for " + x + ":" + cl };
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

        static public void Serialize()
        {
            
        }
    }
}

//***** Welcome to the bottom of the file *****
//**** Here are several things I wrote out to help me code: ****
//
//** Example Commands: **
//save something something 15,2,26,1
//save owner something 1,2,3
//save laneklfhak sakfneoiqoia 928,129u48,127487
//
//** Example Xml: **
//<ranks>
//  <owner class="scientist">
//      <items>
//          <item1>SomeItem</item1>
//          <item2>SomeOtherItem</item2>
//      </items>
//  </owner>
//  <admin class="class-d">
//      <items>
//          <item1>SomeItem</item1>
//          <item2>CouldBeAnyItem</item2>
//          <item3>CanHaveAsManyAsYouWant</item3>
//      </items>
//  </admin>
//</ranks>