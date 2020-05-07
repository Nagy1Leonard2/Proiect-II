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
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			
		}

		private void button1_Click(object sender, EventArgs e)
		{
			string connetionString;
			SqlConnection cnn;
			connetionString = @"Data Source =DESKTOP-CJRBB7E; Initial Catalog = BookingDB; Integrated Security = True";
			cnn = new SqlConnection(connetionString);
			cnn.Open();

			SqlCommand command;
			//SqlDataReader dataReader;
			String sql;
			SqlParameter title = new SqlParameter();

			title.ParameterName = "@title";
			sql = "Select Title from Movie where Title=@title";

			command = new SqlCommand(sql, cnn);
			command.Parameters.AddWithValue("@title", (textBox1.Text));
			SqlDataReader da = command.ExecuteReader();

			var myString = "";

			while (da.Read())
			{
				myString = da.GetString(0);

			}

		}
	}
}
