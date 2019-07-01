using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SpaghettyPlugin
{
	public partial class DisplayPasta : Form
	{
		private string _pasta;
		private string _origPasta;

		public DisplayPasta()
		{
			InitializeComponent();
		}

		public DisplayPasta(string label, string text) : this()
		{
			_origPasta = _pasta = text;
			pastaTx.Text = text;
			Text = label;
			refreshSaveEnabled();
		}

		private void refreshSaveEnabled()
		{
			saveBt.Enabled = _pasta != _origPasta;
		}

		public void SetOriginalPasta(string text)
		{
			_origPasta = text;
			refreshSaveEnabled();
		}

		public string GetPasta()
		{
			return _pasta;
		}

		private void pastaTx_TextChanged(object sender, EventArgs e)
		{
			_pasta = pastaTx.Text;
			refreshSaveEnabled();
		}
	}
}
