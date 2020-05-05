namespace LogIn
{
	partial class Home
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.userControl_Title1 = new LogIn.UserControl_Title();
			this.userControl_Rating1 = new LogIn.UserControl_Rating();
			this.userControl_DateTime1 = new LogIn.UserControl_DateTime();
			this.userControl_Genre1 = new LogIn.UserControl_Genre();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel4 = new System.Windows.Forms.Panel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.label3 = new System.Windows.Forms.Label();
			this.button5 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.userControl_Search_DateTime1 = new LogIn.UserControl_Search_DateTime();
			this.userControl_Search_Genre1 = new LogIn.UserControl_Search_Genre();
			this.userControl_Search_Rating1 = new LogIn.UserControl_Search_Rating();
			this.userControl_Search_Title1 = new LogIn.UserControl_Search_Title();
			this.userControl_Main1 = new LogIn.UserControl_Main();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.DimGray;
			this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.label1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.Coral;
			this.label1.Location = new System.Drawing.Point(657, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(22, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "X";
			this.label1.Click += new System.EventHandler(this.Label1_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.DimGray;
			this.label2.Cursor = System.Windows.Forms.Cursors.Hand;
			this.label2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.Coral;
			this.label2.Location = new System.Drawing.Point(574, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(77, 23);
			this.label2.TabIndex = 1;
			this.label2.Text = "Logout";
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.Transparent;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.userControl_Main1);
			this.panel1.Controls.Add(this.userControl_Search_Title1);
			this.panel1.Controls.Add(this.userControl_Search_Rating1);
			this.panel1.Controls.Add(this.userControl_Search_Genre1);
			this.panel1.Controls.Add(this.userControl_Search_DateTime1);
			this.panel1.Controls.Add(this.userControl_Title1);
			this.panel1.Controls.Add(this.userControl_Rating1);
			this.panel1.Controls.Add(this.userControl_DateTime1);
			this.panel1.Controls.Add(this.userControl_Genre1);
			this.panel1.Location = new System.Drawing.Point(177, 48);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(502, 450);
			this.panel1.TabIndex = 2;
			// 
			// userControl_Title1
			// 
			this.userControl_Title1.BackColor = System.Drawing.Color.DimGray;
			this.userControl_Title1.Location = new System.Drawing.Point(-1, -1);
			this.userControl_Title1.Name = "userControl_Title1";
			this.userControl_Title1.Size = new System.Drawing.Size(502, 450);
			this.userControl_Title1.TabIndex = 4;
			// 
			// userControl_Rating1
			// 
			this.userControl_Rating1.BackColor = System.Drawing.Color.Silver;
			this.userControl_Rating1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.userControl_Rating1.Location = new System.Drawing.Point(-1, -1);
			this.userControl_Rating1.Name = "userControl_Rating1";
			this.userControl_Rating1.Size = new System.Drawing.Size(502, 450);
			this.userControl_Rating1.TabIndex = 3;
			// 
			// userControl_DateTime1
			// 
			this.userControl_DateTime1.BackColor = System.Drawing.Color.DimGray;
			this.userControl_DateTime1.Location = new System.Drawing.Point(-1, -1);
			this.userControl_DateTime1.Name = "userControl_DateTime1";
			this.userControl_DateTime1.Size = new System.Drawing.Size(502, 450);
			this.userControl_DateTime1.TabIndex = 2;
			// 
			// userControl_Genre1
			// 
			this.userControl_Genre1.BackColor = System.Drawing.Color.Gray;
			this.userControl_Genre1.Location = new System.Drawing.Point(-1, -1);
			this.userControl_Genre1.Name = "userControl_Genre1";
			this.userControl_Genre1.Size = new System.Drawing.Size(502, 450);
			this.userControl_Genre1.TabIndex = 1;
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel2.Controls.Add(this.panel4);
			this.panel2.Controls.Add(this.panel3);
			this.panel2.Controls.Add(this.label3);
			this.panel2.Controls.Add(this.button5);
			this.panel2.Controls.Add(this.button4);
			this.panel2.Controls.Add(this.button3);
			this.panel2.Controls.Add(this.button2);
			this.panel2.Controls.Add(this.button1);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel2.ForeColor = System.Drawing.Color.LightGray;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(162, 510);
			this.panel2.TabIndex = 3;
			// 
			// panel4
			// 
			this.panel4.BackColor = System.Drawing.Color.Coral;
			this.panel4.Location = new System.Drawing.Point(-1, 277);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(13, 48);
			this.panel4.TabIndex = 1;
			// 
			// panel3
			// 
			this.panel3.BackColor = System.Drawing.Color.Coral;
			this.panel3.Location = new System.Drawing.Point(-1, 186);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(13, 48);
			this.panel3.TabIndex = 0;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Century Gothic", 10F);
			this.label3.ForeColor = System.Drawing.Color.Coral;
			this.label3.Location = new System.Drawing.Point(24, 152);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(107, 21);
			this.label3.TabIndex = 0;
			this.label3.Text = "SEARCH BY:";
			// 
			// button5
			// 
			this.button5.Cursor = System.Windows.Forms.Cursors.Hand;
			this.button5.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
			this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button5.Font = new System.Drawing.Font("Century Gothic", 10F);
			this.button5.ForeColor = System.Drawing.Color.LightSalmon;
			this.button5.Image = global::LogIn.Properties.Resources.search;
			this.button5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button5.Location = new System.Drawing.Point(11, 322);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(150, 46);
			this.button5.TabIndex = 1;
			this.button5.Text = "     Rating";
			this.button5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.button5.UseVisualStyleBackColor = true;
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// button4
			// 
			this.button4.Cursor = System.Windows.Forms.Cursors.Hand;
			this.button4.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
			this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button4.Font = new System.Drawing.Font("Century Gothic", 10F);
			this.button4.ForeColor = System.Drawing.Color.LightSalmon;
			this.button4.Image = global::LogIn.Properties.Resources.search;
			this.button4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button4.Location = new System.Drawing.Point(11, 277);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(150, 51);
			this.button4.TabIndex = 1;
			this.button4.Text = "     Genre";
			this.button4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// button3
			// 
			this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
			this.button3.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
			this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button3.Font = new System.Drawing.Font("Century Gothic", 10F);
			this.button3.ForeColor = System.Drawing.Color.LightSalmon;
			this.button3.Image = global::LogIn.Properties.Resources.search;
			this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button3.Location = new System.Drawing.Point(11, 233);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(150, 51);
			this.button3.TabIndex = 1;
			this.button3.Text = " Date/Time";
			this.button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button2
			// 
			this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
			this.button2.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
			this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button2.Font = new System.Drawing.Font("Century Gothic", 10F);
			this.button2.ForeColor = System.Drawing.Color.LightSalmon;
			this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
			this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button2.Location = new System.Drawing.Point(11, 186);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(150, 51);
			this.button2.TabIndex = 0;
			this.button2.Text = "      Title";
			this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.button1.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.ForeColor = System.Drawing.Color.Coral;
			this.button1.Location = new System.Drawing.Point(-1, -1);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(162, 62);
			this.button1.TabIndex = 0;
			this.button1.Text = "logo";
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// userControl_Search_DateTime1
			// 
			this.userControl_Search_DateTime1.Location = new System.Drawing.Point(-5, -1);
			this.userControl_Search_DateTime1.Name = "userControl_Search_DateTime1";
			this.userControl_Search_DateTime1.Size = new System.Drawing.Size(502, 450);
			this.userControl_Search_DateTime1.TabIndex = 5;
			this.userControl_Search_DateTime1.Click += new System.EventHandler(this.OnClick);
			// 
			// userControl_Search_Genre1
			// 
			this.userControl_Search_Genre1.Location = new System.Drawing.Point(-1, -1);
			this.userControl_Search_Genre1.Name = "userControl_Search_Genre1";
			this.userControl_Search_Genre1.Size = new System.Drawing.Size(502, 450);
			this.userControl_Search_Genre1.TabIndex = 6;

			this.userControl_Search_Genre1.Click += new System.EventHandler(this.OnClick);
			// 
			// userControl_Search_Rating1
			// 
			this.userControl_Search_Rating1.Location = new System.Drawing.Point(-1, -1);
			this.userControl_Search_Rating1.Name = "userControl_Search_Rating1";
			this.userControl_Search_Rating1.Size = new System.Drawing.Size(502, 450);
			this.userControl_Search_Rating1.TabIndex = 7;
			this.userControl_Search_Rating1.Click += new System.EventHandler(this.OnClick);
			// 
			// userControl_Search_Title1
			// 
			this.userControl_Search_Title1.Location = new System.Drawing.Point(-1, -1);
			this.userControl_Search_Title1.Name = "userControl_Search_Title1";
			this.userControl_Search_Title1.Size = new System.Drawing.Size(502, 450);
			this.userControl_Search_Title1.TabIndex = 8;
			this.userControl_Search_Title1.Click += new System.EventHandler(this.OnClick);
			// 
			// userControl_Main1
			// 
			this.userControl_Main1.BackColor = System.Drawing.Color.White;
			this.userControl_Main1.Location = new System.Drawing.Point(-1, -1);
			this.userControl_Main1.Name = "userControl_Main1";
			this.userControl_Main1.Size = new System.Drawing.Size(502, 450);
			this.userControl_Main1.TabIndex = 9;
			// 
			// Home
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.ClientSize = new System.Drawing.Size(691, 510);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "Home";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Home";
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Panel panel4;
		private UserControl_Title userControl_Title1;
		private UserControl_Rating userControl_Rating1;
		private UserControl_DateTime userControl_DateTime1;
		private UserControl_Genre userControl_Genre1;
		private UserControl_Main userControl_Main1;
		private UserControl_Search_Title userControl_Search_Title1;
		private UserControl_Search_Rating userControl_Search_Rating1;
		private UserControl_Search_Genre userControl_Search_Genre1;
		private UserControl_Search_DateTime userControl_Search_DateTime1;
	}
}