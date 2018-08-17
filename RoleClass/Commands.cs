using Smod2.Commands;
using Newtonsoft.Json;

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

        public string[] OnCall(ICommandSender sender, string[] args)
        {
            return new string[] { "Currently all this command does is send this string!" };
        }


        //	//	private ExamplePlugin plugin;
        //	//	public HelloWorldCommand(ExamplePlugin plugin)
        //	//	{
        //	//		this.plugin = plugin;
        //	//	}

        //	//	public string GetCommandDescription()
        //	//	{
        //	//		// This prints when someone types HELP HELLO
        //	//		return "Prints hello world";
        //	//	}

        //	//	public string GetUsage()
        //	//	{
        //	//		// This prints when someone types HELP HELLO
        //	//		return "HELLO";
        //	//	}

        //	//	public string[] OnCall(ICommandSender sender, string[] args)
        //	//	{
        //	//		// This will print 3 lines in console.
        //	//		return new string[] { "Hello plugin user!", "My name is Khaos.", "If you see this, it means the plugin is running!" };
        //	//	}
        //	//}
    }
}
