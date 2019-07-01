using LaunchySharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SpaghettyPlugin
{
	public class PastaPlugin : IPlugin
	{
		private const string PLUGIN_NAME = "Spaghetty";
		private const string SPG_EXT = ".spg";
		private const string NEW_EXT = ".nspg";
		private const string ICON_FILENAME = "pasta48.png";
		private const string PASTA_FILENAME = "pasta.json";

		private readonly string PLUGIN_LOWER = PLUGIN_NAME.ToLowerInvariant();

		private IDictionary<string, string> _spagBowl;
		private IPluginHost _pluginHost;
		private ICatItemFactory _catFactory;
		private uint? _hash;
		private string _iconPath;


		public void init(IPluginHost pluginHost)
		{
			this._pluginHost = pluginHost;
			this._catFactory = pluginHost.catItemFactory();

			_spagBowl = new Dictionary<string, string>();




			//TODO remove this
			_spagBowl.Add("facepalm-world", "![7f9158a9-7c33-43a2-8bfe-e081afb356ae-image.png](/assets/uploads/files/1561910847061-7f9158a9-7c33-43a2-8bfe-e081afb356ae-image.png)");

		}

		#region info getters
		public uint getID()
		{
			if (!_hash.HasValue)
			{
				_hash = _pluginHost.hash(PLUGIN_NAME);
			}

			return _hash.Value;
		}

		public string getName()
		{
			return PLUGIN_NAME;
		}

		private string getIcon()
		{
			if (string.IsNullOrEmpty(_iconPath))
			{
				var iconPath = _pluginHost.launchyPaths().getIconsPath();
				_iconPath = System.IO.Path.Combine(iconPath, ICON_FILENAME);
			}

			return _iconPath;
		}
		#endregion

		#region launchy show and hide triggers
		public void launchyHide()
		{
			//MessageBox.Show("Launchy hide, this will also be annoying");
		}

		public void launchyShow()
		{
			//MessageBox.Show("launchy show, no way this will get annoying");
		}
		#endregion

		#region dialog stuff
		public IntPtr doDialog()
		{
			return IntPtr.Zero;
		}

		public void endDialog(bool acceptedByUser)
		{
			//TODO figure out wtf
		}

		public bool hasDialog()
		{
			return false;
		}
		#endregion

		#region result management
		public void getCatalog(List<ICatItem> catalogItems)
		{
			catalogItems.Add(_catFactory.createCatItem(PLUGIN_NAME + SPG_EXT, PLUGIN_NAME, getID(), getIcon()));
		}

		public void getLabels(List<IInputData> inputDataList)
		{
			var firstData = inputDataList[0];
			var lowerText = firstData.getText().ToLowerInvariant();

			if (lowerText == PLUGIN_LOWER)
			{
				firstData.setLabel(getID());
			}
		}

		public void getResults(List<IInputData> inputDataList, List<ICatItem> resultsList)
		{
			var sp = inputDataList[0];
			if (!sp.hasLabel(getID())) { return; }

			var keys = _spagBowl.Keys;
			var inputText = "";


			if (inputDataList.Count >= 2)
			{
				inputText = inputDataList[1].getText();
			}

			//var input = inputDataList[1];
			//var inputText = input.getText();
			if (string.IsNullOrEmpty(inputText))
			{
				foreach (var k in keys)
				{
					resultsList.Add(createCat(k));
				}

				return;
			}

			var inputLower = inputText.Trim().ToLowerInvariant();

			var addNew = true;

			foreach (var k in keys)
			{
				//TODO match better, allow for non-consecutive chars
				// maybe use a cache
				var lk = k.ToLowerInvariant();
				if (lk == inputLower)
				{
					addNew = false;
					resultsList.Add(createCat(k));
				}
				else if (lk.Contains(inputLower))
				{
					resultsList.Add(createCat(k));
				}
			}

			if (addNew)
			{
				resultsList.Add(createNewPastaCat(inputText));
			}
		}

		private ICatItem createCat(string text, string fullPath)
		{
			var hash = _pluginHost.hash(fullPath);
			return _catFactory.createCatItem(fullPath, text, hash, getIcon());
		}

		private ICatItem createCat(string text)
		{
			var fullPath = text + SPG_EXT;
			return createCat(text, fullPath);
		}

		private ICatItem createNewPastaCat(string text)
		{
			var fullPath = $"{text}{NEW_EXT}";
			var label = $"New pasta '{text}'";
			return createCat(label, fullPath);
		}
		#endregion

		#region launching
		public void launchItem(List<IInputData> inputDataList, ICatItem item)
		{
			//so
			// item is worthless, it's just the main Spaghetty
			// inputDataList, and specifically .getTopResult of each input item, is what i want
			// so possibilities:
			// one input (just spaghetty): ? maybe nothing
			// two inputs: show the selected one; maybe open it for editing
			// three inputs: create/replace? maybe



			//TODO clean up this whole mess

			if (inputDataList.Count < 2)
			{
				var lp = _pluginHost.launchyPaths();
				var sb = new StringBuilder();
				var nl = Environment.NewLine;

				sb.Append(lp.getConfigPath());
				sb.Append(nl);
				sb.Append(lp.getIconsPath());
				sb.Append(nl);
				sb.Append(lp.getLaunchyPath());

				MessageBox.Show(sb.ToString());
				return;
			}

			var selectedPasta = inputDataList[1].getTopResult();
			var fp = selectedPasta.getFullPath();

			string key = "nothing";
			string text = "if you can read this, something went wrong";
			Boolean newPasta = false;

			if (fp.EndsWith(SPG_EXT))
			{
				key = fp.Substring(0, fp.Length - SPG_EXT.Length);
				_spagBowl.TryGetValue(key, out text);
			}
			else if (fp.EndsWith(NEW_EXT))
			{
				key = fp.Substring(0, fp.Length - NEW_EXT.Length);
				newPasta = true;
				if (inputDataList.Count > 2)
				{
					text = inputDataList[2].getText();
				}
				else
				{
					text = "";
				}
			}

			var pw = new DisplayPasta(key, text);

			if (newPasta)
			{
				pw.SetOriginalPasta("");
			}

			var result = pw.ShowDialog();
			if (result == DialogResult.OK)
			{
				//TODO add renaming
				_spagBowl[key] = pw.GetPasta();
			}
		}
		#endregion

		#region debug
		private void showLaunchItemStuff(List<IInputData> inputDataList, ICatItem item)
		{
			var sb = new StringBuilder();
			var nl = Environment.NewLine;

			sb.Append("Input data:");
			sb.Append(nl);
			foreach (var i in inputDataList)
			{
				var top = i.getTopResult();
				sb.Append("ID: ");
				sb.Append(i.getID());
				sb.Append(nl);
				sb.Append("Text: ");
				sb.Append(i.getText());
				sb.Append(nl);
				sb.Append("Has spaghetty label: ");
				sb.Append(i.hasLabel(getID()));
				sb.Append(nl);
				sb.Append("Top result");
				sb.Append(nl);
				sb.Append("Full path: ");
				sb.Append(top.getFullPath());
				sb.Append(nl);
				sb.Append("Icon path: ");
				sb.Append(top.getIconPath());
				sb.Append(nl);
				sb.Append("ID: ");
				sb.Append(top.getID());
				sb.Append(nl);
				sb.Append("Low name: ");
				sb.Append(top.getLowName());
				sb.Append(nl);
				sb.Append("Short name: ");
				sb.Append(top.getShortName());
				sb.Append(nl);
				sb.Append("Usage: ");
				sb.Append(top.getUsage());
				sb.Append(nl);

				sb.Append("-----");

				sb.Append(nl);
			}

			sb.Append("Full path: ");
			sb.Append(item.getFullPath());
			sb.Append(nl);
			sb.Append("Icon path: ");
			sb.Append(item.getIconPath());
			sb.Append(nl);
			sb.Append("ID: ");
			sb.Append(item.getID());
			sb.Append(nl);
			sb.Append("Low name: ");
			sb.Append(item.getLowName());
			sb.Append(nl);
			sb.Append("Short name: ");
			sb.Append(item.getShortName());
			sb.Append(nl);
			sb.Append("Usage: ");
			sb.Append(item.getUsage());
			sb.Append(nl);
			sb.Append("tostring: ");
			sb.Append(item.ToString());

			MessageBox.Show(sb.ToString());
		}
		#endregion
	}
}
