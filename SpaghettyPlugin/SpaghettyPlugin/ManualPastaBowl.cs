using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SpaghettyPlugin
{
	class ManualPastaBowl : Dictionary<string, string>, ISerializingPastaBowl
	{
		private const string KEY_TAG = "## spaghet key:";
		private const string END_TAG = "## spaghet end";

		private readonly string _filename;

		public ManualPastaBowl(string filename) : base()
		{
			_filename = filename;
		}

		public void Load()
		{
			Load(_filename);
		}

		public void Load(string filename)
		{
			Clear();

			var lines = File.ReadAllLines(filename, Encoding.UTF8);

			string key = null;
			StringBuilder sb = new StringBuilder();

			//TODO maybe check for errors, idk
			foreach (var line in lines)
			{
				if (string.IsNullOrEmpty(line))
				{
					continue;
				}
				else if (line.StartsWith(KEY_TAG))
				{
					key = ParseKey(line);
					sb = new StringBuilder();
				}
				else if (line.StartsWith(END_TAG))
				{
					this[key] = sb.ToString();
				}
				else
				{
					sb.AppendLine(line);
				}
			}
		}

		private string ParseKey(string line)
		{
			return line.Substring(KEY_TAG.Length);
		}

		public void Save()
		{
			Save(_filename);
		}

		public void Save(string filename)
		{
			StringBuilder sb = new StringBuilder();

			foreach(var k in Keys)
			{
				var v = this[k];

				sb.Append(KEY_TAG);
				sb.AppendLine(k);
				sb.AppendLine(v);
				sb.AppendLine(END_TAG);
			}

			File.WriteAllText(filename, sb.ToString(), Encoding.UTF8);
		}
	}
}
