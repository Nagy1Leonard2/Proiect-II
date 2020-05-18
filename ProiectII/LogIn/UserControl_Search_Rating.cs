﻿using System;
using System.Windows.Forms;

namespace LogIn
{
	public partial class UserControl_Search_Rating : UserControl
	{
		[NonSerialized]
		private EventHandler fClick;
		public event EventHandler Click
		{
			add { fClick += value; }
			remove { fClick -= value; }
		}

		protected void OnClick(object sender, EventArgs e)
		{
			EventHandler handler = fClick;
			if (fClick != null)
				handler(sender, e);
		}
		public UserControl_Search_Rating()
		{
			InitializeComponent();
			button1.Click += OnClick;

			trackBar1.Maximum = 10;
			trackBar1.TickFrequency = 1;
			trackBar2.Maximum = 10;
			trackBar2.TickFrequency = 1;

		}
		public static int Min = 0;
		public static int Max = 0;

		private void trackBar1_Scroll(object sender, EventArgs e)
		{
			label3.Text = "" + trackBar1.Value;
		}

		private void trackBar2_Scroll(object sender, EventArgs e)
		{
			label4.Text = "" + trackBar2.Value;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Min = Int32.Parse(label3.Text);
			Max = Int32.Parse(label4.Text);
		}
	}
}
