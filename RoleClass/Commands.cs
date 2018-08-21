using Smod2.Commands;
using Smod2;
using Smod2.API;
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

        [XmlRoot("ranks")]
        public class Details
        {
            [XmlElement()]
            public string RankName { get; set; }
            [XmlAttribute("class")]
            public string Class { get; set; }
            [XmlArray("items")]
            public string[] Items { get; set; }
            [XmlArrayItem()]
            public string ItemNo { get; set; }
            [XmlText()]
            public string Item { get; set; }
        }


        public string[] OnCall(ICommandSender sender, string[] args)
        {
            //if (sender is Player pl)
            //{
            //    //Player a = (Player)sender;
            //    var s64 = pl.SteamId;
            //    string inf = "Player: " + pl;
            //    string inf2 = "Steamid: " + s64;
            //    if (!string.IsNullOrEmpty(pl.SteamId))
            //    {
            //        return new string[] { inf + inf2 };
            //        //plugin.Debug(inf2);
            //    }
            //}

            if (args != null && args.Length > 0)
            {
                string x = args[0].ToLower();
                //string path1 = @"rc-config-1.xml";
                if (args.Length > 1)
                {
                    string cl = args[1].ToLower();
                    if (args != null && args.Length > 2) 
                    {
                        int len = args.Length - 2;
                        //string[] itemlist = new string[len];
                        List<string> itemlist = new List<string>();
                        List<int> itemno = new List<int>();

                        for (int i = 2; i < args.Length; i++)
                        {
                            int itemint = i - 2;
                            itemno.Add(itemint);
                            itemlist.Add(args[i]);
                            //string itemname = "item" + itemint;
                            //XmlNode item = xmlDoc.CreateElement(itemname);
                            //item.InnerText = args[i];
                        }

                        //xmlDoc.AppendChild(items);
                        //rootNode.AppendChild(rank);

                        //xmlDoc.Save(path1);

                        Details details = new Details();
                        details.RankName = x;
                        details.Class = cl;

                        string[] itemarray = itemlist.ToArray();
                        details.Items = itemarray;

                        List<string> itemnumbers = itemno.ConvertAll<string>(delegate (int i) { return i.ToString(); });
                        string[] itemnums = itemnumbers.ToArray();

                        for (int i = 0; i < itemarray.Length; i++)
                        {
                            details.ItemNo = itemnums[i];
                            details.Item = itemarray[i];
                        }


                        XmlSerializer serializer = new XmlSerializer(typeof(Details));
                        using(TextWriter writer = new StreamWriter(@"rc-config.xml"))
                        {
                            serializer.Serialize(writer, details);
                         }

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

        //static public void Serialize(Details details)
        //{
        //    XmlSerializer serializer = new XmlSerializer(typeof(Details));
        //    using (TextWriter writer = new StreamWriter(@"rc-config-2.xml"))
        //    {
        //        serializer.Serialize(writer, details);
        //    }
        //}
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