﻿using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace LogIn
{
	public partial class BookingSeatsScreen : Form
	{
		public BookingSeatsScreen()
		{
			InitializeComponent();
		}

		private void BookingSeatsScreen_Load(object sender, EventArgs e)
		{
			panel1.BackColor = Color.FromArgb(200, 0, 0, 0);
			// Database Connection:
			SqlConnection cnn = new SqlConnection(@"Data Source = LPTVIVIANACSA\SQLSERVER01; Initial Catalog = BookingDB; Integrated Security = True");
			cnn.Open();

			s1.BackColor = Color.Red;

		}
		private void Label1_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
