using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LogIn
{
	public partial class UserControl_Search_Title : UserControl
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
		public UserControl_Search_Title()
		{
			InitializeComponent();
			button1.Click += OnClick;

			// Database Connection:
			SqlConnection cnn = new SqlConnection(@"Data Source = LPTVIVIANACSA\SQLSERVER01; Initial Catalog = BookingDB; Integrated Security = True");
			cnn.Open();

			// Select statement: 
			SqlCommand command0 = new SqlCommand("Select Title from Movies order by Title", cnn);
			SqlDataReader dR = command0.ExecuteReader();
			while (dR.Read())
			{
				comboBox1.Items.Add(dR["Title"]);
			}

			// Close the connection and dispose of the commands:
			command0.Dispose();
			dR.Close();
			cnn.Close();
		}
	}
}
