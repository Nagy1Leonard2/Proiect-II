namespace LogIn
{
	partial class UserControl_Search_Rating
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.progressBar2 = new System.Windows.Forms.ProgressBar();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold);
			this.label1.ForeColor = System.Drawing.Color.Coral;
			this.label1.Location = new System.Drawing.Point(47, 39);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(103, 19);
			this.label1.TabIndex = 0;
			this.label1.Text = "Min. Rating:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold);
			this.label2.ForeColor = System.Drawing.Color.Coral;
			this.label2.Location = new System.Drawing.Point(47, 156);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(110, 19);
			this.label2.TabIndex = 1;
			this.label2.Text = "Max. Rating:";
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(50, 95);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(293, 23);
			this.progressBar1.TabIndex = 2;
			// 
			// progressBar2
			// 
			this.progressBar2.Location = new System.Drawing.Point(50, 212);
			this.progressBar2.Name = "progressBar2";
			this.progressBar2.Size = new System.Drawing.Size(293, 23);
			this.progressBar2.TabIndex = 3;
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold);
			this.button1.ForeColor = System.Drawing.Color.Coral;
			this.button1.Location = new System.Drawing.Point(226, 310);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(127, 53);
			this.button1.TabIndex = 4;
			this.button1.Text = "Submit";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// UserControl_Search_Rating
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.button1);
			this.Controls.Add(this.progressBar2);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "UserControl_Search_Rating";
			this.Size = new System.Drawing.Size(502, 450);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.ProgressBar progressBar2;
		private System.Windows.Forms.Button button1;
	}
}
