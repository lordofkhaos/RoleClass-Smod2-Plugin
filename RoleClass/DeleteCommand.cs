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
    class DeleteCommand : ICommandHandler
    {
        private RoleClass plugin;
        public void DelCmd(RoleClass plugin)
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
            if (args != null && args.Length > 0)
            {
                string x = args[0].ToLower();
                string path = @"dictionary.bin";
                if (args.Length > 1)
                {
                    string cl = args[1].ToLower();
                    List<string> rankNames = new List<string>();
                    List<string> cls = new List<string>();
                    List<string> classitems = new List<string>();


                    BinaryFormatter formatter = new BinaryFormatter();
                    if (File.Exists(path))
                    {
                        var table = new Dictionary<string, List<string>>();
                        using (FileStream s = File.OpenRead("dictionary.bin"))
                        {
                            table = (Dictionary<string, List<string>>)formatter.Deserialize(s);
                        }

                        foreach(KeyValuePair<string, List<string>> j in table)
                        {
                            foreach (string y in j.Value)
                                classitems.Add(y);
                            rankNames.Add(j.Key);

                            string _cl = classitems[0].ToString();
                            cl.Trim('-', '_');
                            cl.Replace("scp", "");
                            _cl.Trim('-', '_');
                            _cl.Replace("scp", "");

                            if (rankNames.Contains(x))
                            {
                                rankNames.Remove(x);
                            }
                        }

                        return new string[] { "Removed configuration for " + x + ":" + cl };
                    }
                    else
                    {
                        return new string[] { "Unable to find file!" };
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