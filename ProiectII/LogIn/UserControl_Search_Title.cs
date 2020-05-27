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

			// Clear previous data:
			comboBox1.Items.Clear();
			comboBox1.ResetText();

			// Database Connection:
			SqlConnection cnn = new SqlConnection(Program.DB_ConnectionString_Booking());
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

		// Static variable to be used in the next Form:
		public static string Movie = "";

		// Search button event:
		private void button1_Click(object sender, EventArgs e)
		{
			Movie = comboBox1.Text;
		}
	}
}
