﻿using System.Data.SqlClient;
using System.Windows.Forms;

namespace LogIn
{
	public partial class UserControl_Title : UserControl
	{
		public UserControl_Title()
		{
			InitializeComponent();
			// Database Connection:
			SqlConnection cnn = new SqlConnection(@"Data Source = LPTVIVIANACSA\SQLSERVER01; Initial Catalog = BookingDB; Integrated Security = True");
			cnn.Open();
		}
	}
}
