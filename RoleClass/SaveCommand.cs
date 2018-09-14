using RoleClass;
using Smod2.Commands;
using Smod2;
using Smod2.API;
using System;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Collections.Generic;
//using System.Xml;
//using System.Xml.Serialization;
using System.Threading;

namespace RoleClass.Commands
{
    class SaveCommand : ICommandHandler
    {
        private RoleClass plugin;
        public void SaveCmd(RoleClass plugin)
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
                string path = @"rc-config.dat";
                if (args.Length > 1)
                {
                    string cl = args[1].ToLower();
                    if (args != null && args.Length > 2)
                    {
                        int len = args.Length - 2;
                        List<string> itemlist = new List<string>();

                        for (int i = 2; i < args.Length; i++)
                        {
                            itemlist.Add(args[i]);
                        }

                        string[] itemarray = itemlist.ToArray();

                        List<string> classitems = new List<string>();
                        if (cl != null)
                        {
                            classitems.Add(cl);
                        }

                        for (int i = 0; i < itemarray.Length; i++)
                        {
                            int j = i + 1;
                            classitems.Add(itemarray[i]);

                        }

                        Dictionary<string, List<string>> table = new Dictionary<string, List<string>>()
                        {
                            [x] = classitems
                        };

                        IFormatter formatter = new BinaryFormatter();
                        if (!File.Exists(path))
                        {
                            using (FileStream s = File.Open("dictionary.bin", FileMode.OpenOrCreate))
                            {
                                formatter.Serialize(s, table);
                                return new string[] { "Saved configuration for " + x + ":" + cl };
                            }
                        }
                        else
                        {
                            using (FileStream s = File.Open("dictionary.bin", FileMode.OpenOrCreate))
                            {
                                formatter.Serialize(s, table);
                                return new string[] { "Saved configuration for " + x + ":" + cl };
                            }
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
            else
            {
                return new string[] { GetUsage() };
            }

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
//* Note: XML is to-be-added *
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