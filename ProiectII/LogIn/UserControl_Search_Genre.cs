using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LogIn
{
	public partial class UserControl_Search_Genre : UserControl
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

		public UserControl_Search_Genre()
		{
			InitializeComponent();
			button1.Click += OnClick;

			// Add the default values:
			comboBox1.Items.Add("Action");
			comboBox1.Items.Add("Anime");
			comboBox1.Items.Add("Comedy");
			comboBox1.Items.Add("Crime");
			comboBox1.Items.Add("Documentary");
			comboBox1.Items.Add("Drama");
			comboBox1.Items.Add("Horror");
			comboBox1.Items.Add("Thriler");
			comboBox1.Items.Add("Sci-Fi");
			comboBox1.Items.Add("Romance");
		}

		// Used in the next Form:
		public static List<string> st = new List<string>();
		public static List<string> id = new List<string>();

		// Search button event:
		private void button1_Click(object sender, EventArgs e)
		{
			// Database Connection:
			SqlConnection cnn = new SqlConnection(Program.DB_ConnectionString_Booking());
			cnn.Open();
						
			// Select statement: 
			SqlCommand command0 = new SqlCommand("Select Title, Id from Movies where Genre = @genre", cnn);
			SqlParameter genre = new SqlParameter();
			genre.ParameterName = "@genre";
			command0.Parameters.AddWithValue("@genre", (comboBox1.Text));

			using (command0)
			{
				SqlDataReader dR = command0.ExecuteReader();
				using (dR)
				{
					while (dR.Read())
					{
						st.Add(dR[0].ToString());
						id.Add(dR[1].ToString());
					}
				}
			}

			// Disposal:
			command0.Parameters.Clear();
			command0.Dispose();
			cnn.Close();
		}
	}
}
