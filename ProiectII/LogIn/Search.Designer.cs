namespace LogIn
{
	partial class Search
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
			this.label1 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.userControl_Search_Title1 = new LogIn.UserControl_Search_Title();
			this.userControl_Search_Rating1 = new LogIn.UserControl_Search_Rating();
			this.userControl_Search_Genre1 = new LogIn.UserControl_Search_Genre();
			this.userControl_Search_DateTime1 = new LogIn.UserControl_Search_DateTime();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.label1.Location = new System.Drawing.Point(421, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(17, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "X";
			this.label1.Click += new System.EventHandler(this.label1_Click);
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.Color.Black;
			this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Location = new System.Drawing.Point(188, 347);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(89, 41);
			this.button1.TabIndex = 1;
			this.button1.Text = "SEARCH";
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.userControl_Search_Title1);
			this.panel1.Controls.Add(this.userControl_Search_Rating1);
			this.panel1.Controls.Add(this.userControl_Search_Genre1);
			this.panel1.Controls.Add(this.userControl_Search_DateTime1);
			this.panel1.Location = new System.Drawing.Point(26, 36);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(399, 289);
			this.panel1.TabIndex = 2;
			// 
			// userControl_Search_Title1
			// 
			this.userControl_Search_Title1.Location = new System.Drawing.Point(0, 3);
			this.userControl_Search_Title1.Name = "userControl_Search_Title1";
			this.userControl_Search_Title1.Size = new System.Drawing.Size(399, 289);
			this.userControl_Search_Title1.TabIndex = 3;
			// 
			// userControl_Search_Rating1
			// 
			this.userControl_Search_Rating1.Location = new System.Drawing.Point(0, 3);
			this.userControl_Search_Rating1.Name = "userControl_Search_Rating1";
			this.userControl_Search_Rating1.Size = new System.Drawing.Size(399, 289);
			this.userControl_Search_Rating1.TabIndex = 2;
			// 
			// userControl_Search_Genre1
			// 
			this.userControl_Search_Genre1.Location = new System.Drawing.Point(0, 3);
			this.userControl_Search_Genre1.Name = "userControl_Search_Genre1";
			this.userControl_Search_Genre1.Size = new System.Drawing.Size(399, 289);
			this.userControl_Search_Genre1.TabIndex = 1;
			// 
			// userControl_Search_DateTime1
			// 
			this.userControl_Search_DateTime1.Location = new System.Drawing.Point(0, 0);
			this.userControl_Search_DateTime1.Name = "userControl_Search_DateTime1";
			this.userControl_Search_DateTime1.Size = new System.Drawing.Size(399, 289);
			this.userControl_Search_DateTime1.TabIndex = 0;
			// 
			// Search
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.ClientSize = new System.Drawing.Size(450, 400);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label1);
			this.ForeColor = System.Drawing.Color.Coral;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "Search";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Search";
			this.Load += new System.EventHandler(this.Search_Load);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Panel panel1;
		private UserControl_Search_Title userControl_Search_Title1;
		private UserControl_Search_Rating userControl_Search_Rating1;
		private UserControl_Search_Genre userControl_Search_Genre1;
		private UserControl_Search_DateTime userControl_Search_DateTime1;
	}
}