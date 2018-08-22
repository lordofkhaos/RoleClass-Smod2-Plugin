using Smod2.Commands;
using Smod2;
using Smod2.API;
//using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Collections.Generic;
//using System.Xml;
//using System.Xml.Serialization;
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

        public class XRanks
        {
            public string RankName { get; set; }
            public string Class { get; set; }
            public string[] Items { get; set; }
            public string ItemNo { get; set; }
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
                string path = @"rc-config.dat";
                if (args.Length > 1)
                {
                    string cl = args[1].ToLower();
                    if (args != null && args.Length > 2) 
                    {
                        int len = args.Length - 2;
                        //string[] itemlist = new string[len];
                        List<string> itemlist = new List<string>();
                        //List<int> itemno = new List<int>();

                        for (int i = 2; i < args.Length; i++)
                        {
                            //int itemint = i - 2;
                            //itemno.Add(itemint);
                            itemlist.Add(args[i]);
                        }

                        string[] itemarray = itemlist.ToArray();

                        //List<string> itemnumbers = itemno.ConvertAll<string>(delegate (int i) { return i.ToString(); });
                        //string[] itemnums = itemnumbers.ToArray();

                        string[] classitems = null;
                        if (cl != null)
                        {
                            classitems[0] = cl;
                        }

                        for (int i = 0; i < itemarray.Length; i++)
                        {
                            int j = i + 1;
                            itemarray[i] = classitems[j];

                        }

                        Dictionary<string, string[]> table = new Dictionary<string, string[]>();
                        table.Add(x, classitems);

                        BinaryFormatter formatter = new BinaryFormatter();
                        if (!File.Exists(path)) 
                        { 
                            FileStream fs = new FileStream(path, FileMode.Create); 

                            try
                            {
                                formatter.Serialize(fs, table);
                                return new string[] { "Saved configuration for " + x + ":" + cl };
                            }
                            catch (SerializationException e)
                            {
                                return new string[] { "Encountered exception: " + e };
                                throw;
                            }
                            finally
                            {
                                fs.Close();
                            }
                        }
                        else 
                        { 
                            FileStream fs = new FileStream(path, FileMode.Append);

                            try
                            {
                                formatter.Serialize(fs, table);
                                return new string[] { "Saved configuration for " + x + ":" + cl };
                            }
                            catch (SerializationException e)
                            {
                                return new string[] { "Encountered exception: " + e };
                                throw;
                            }
                            finally
                            {
                                fs.Close();
                            }
                        }

                        //using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
                        //{
                        //    writer.Write(x);
                        //    writer.Write(cl);
                        //    foreach (string item in itemarray)
                        //    {
                        //        writer.Write(item);
                        //    }
                        //}



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