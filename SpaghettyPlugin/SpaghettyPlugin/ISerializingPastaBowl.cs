using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaghettyPlugin
{
	interface ISerializingPastaBowl : IDictionary<string, string>
	{
		void Save();
		void Save(string filename);
		void Load();
		void Load(string filename);
	}
}
