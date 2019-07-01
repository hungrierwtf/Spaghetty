namespace SpaghettyPlugin
{
	partial class DisplayPasta
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.pastaTx = new System.Windows.Forms.TextBox();
			this.canBt = new System.Windows.Forms.Button();
			this.saveBt = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// pastaTx
			// 
			this.pastaTx.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pastaTx.Location = new System.Drawing.Point(12, 12);
			this.pastaTx.Multiline = true;
			this.pastaTx.Name = "pastaTx";
			this.pastaTx.Size = new System.Drawing.Size(472, 292);
			this.pastaTx.TabIndex = 0;
			this.pastaTx.TextChanged += new System.EventHandler(this.pastaTx_TextChanged);
			// 
			// canBt
			// 
			this.canBt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.canBt.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.canBt.Location = new System.Drawing.Point(409, 310);
			this.canBt.Name = "canBt";
			this.canBt.Size = new System.Drawing.Size(75, 23);
			this.canBt.TabIndex = 2;
			this.canBt.Text = "&Cancel";
			this.canBt.UseVisualStyleBackColor = true;
			// 
			// saveBt
			// 
			this.saveBt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.saveBt.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.saveBt.Location = new System.Drawing.Point(328, 310);
			this.saveBt.Name = "saveBt";
			this.saveBt.Size = new System.Drawing.Size(75, 23);
			this.saveBt.TabIndex = 1;
			this.saveBt.Text = "&Save";
			this.saveBt.UseVisualStyleBackColor = true;
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.Location = new System.Drawing.Point(12, 312);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(310, 20);
			this.textBox1.TabIndex = 3;
			this.textBox1.TabStop = false;
			// 
			// DisplayPasta
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.canBt;
			this.ClientSize = new System.Drawing.Size(496, 345);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.saveBt);
			this.Controls.Add(this.canBt);
			this.Controls.Add(this.pastaTx);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(512, 384);
			this.Name = "DisplayPasta";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Pasta la vista baby";
			this.TopMost = true;
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox pastaTx;
		private System.Windows.Forms.Button canBt;
		private System.Windows.Forms.Button saveBt;
		private System.Windows.Forms.TextBox textBox1;
	}
}