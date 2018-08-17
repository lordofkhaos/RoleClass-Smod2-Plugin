using Smod2.Commands;

namespace ExamplePlugin
{
	class HelloWorldCommand : ICommandHandler
	{
		private ExamplePlugin plugin;
		public HelloWorldCommand(ExamplePlugin plugin)
		{
			this.plugin = plugin;
		}

		public string GetCommandDescription()
		{
			// This prints when someone types HELP HELLO
			return "Prints hello world";
		}

		public string GetUsage()
		{
			// This prints when someone types HELP HELLO
			return "HELLO";
		}

		public string[] OnCall(ICommandSender sender, string[] args)
		{
			// This will print 3 lines in console.
			return new string[] { "Hello plugin user!", "My name is Khaos.", "If you see this, it means the plugin is running!" };
		}
	}
    //class TestCommand : ICommandSender
    //{
    //    private ExamplePlugin plugin;
    //    public TestCommand(ExamplePlugin plugin)
    //    {
    //        this.plugin = plugin;
    //    }

    //    public string GetCommandDescription()
    //    {
    //        // This prints when someone types HELP TEST
    //        return "Prints the config options for testing purposes";
    //    }

    //    public string GetUsage()
    //    {
    //        // This prints when someone types HELP TEST
    //        return "TEST";
    //    }

    //    public string[] OnCall(ICommandSender sender, string[] args)
    //    {
    //        // return new string[] {"Config Dictionary Values:", Plugin.dictionary, "Dictionary to int Values:", dict }
    //        return new string[] { "Plugin is fully functional." };
    //    }
    //}
}
