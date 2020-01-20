using Smod2.API;
using System;
using System.Collections.Generic;
using System.IO;

namespace RoleClass
{
	internal class Aliases
	{
		private readonly string _classes_config = "krc_aliases_classes.txt";

		private readonly string _items_config = "krc_aliases_items.txt";

		public Dictionary<int, List<string>> RoleAliases { get; set; }
		
		public Dictionary<int, List<string>> ItemAliases { get; set; }

		public void Assign()
		{
			if (File.Exists(this._classes_config))
			{
				List<string> data = new List<string>();
				using (StreamReader sr = new StreamReader(this._classes_config))
				{
					string line = null;
					while ((line = sr.ReadLine()) != null)
					{
						data.Add(line);
					}
				}

				this.RoleAliases = GenerateAliases(data);
			}

			if (File.Exists(this._items_config))
			{
				List<string> data = new List<string>();
				using (StreamReader sr = new StreamReader(this._items_config))
				{
					string line = null;
					while ((line = sr.ReadLine()) != null)
					{
						data.Add(line);
					}
				}

				this.ItemAliases = GenerateAliases(data);
			}
		}


		private Dictionary<int, List<string>> GenerateAliases(List<string> file)
		{
			Dictionary<int, List<string>> result = new Dictionary<int, List<string>>();
			bool isValue = false;
			List<string> currentValues = null;
			int currentKey = -1;

			foreach (string line in file)
			{
				// Ignore comments
				if (line.Trim().StartsWith("#"))
					continue;

				// If line is a key
				if (line.Trim().EndsWith(":"))
				{
					// Add old values
					if (currentKey != -1)
					{
						result.Add(currentKey, currentValues);
					}

					// Set up for new values
					currentValues = new List<string>();
					isValue = true;
					currentKey = Convert.ToInt32(line.Replace(":", string.Empty));
					continue;
				}

				// If line is a value
				if (isValue)
				{
					currentValues.Add(line.Trim());
				}
			}

			return result;
		}
	}
}