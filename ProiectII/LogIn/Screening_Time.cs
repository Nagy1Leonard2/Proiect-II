using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LogIn
{
	public partial class Screening_Time : UserControl
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

		public Screening_Time()
        {
            InitializeComponent();
			button1.Click += OnClick;

			// Used variables:
			string date1;
			DateTime date0;
			string time1;
			DateTime time0;

			// Clear previous selected items:
			comboBox1.ResetText();
			comboBox2.Items.Clear();
			comboBox2.ResetText();

			// Database Connection:
			SqlConnection cnn = new SqlConnection(Program.DB_ConnectionString_Booking());
			cnn.Open();

			// Select statement:
			SqlCommand command0 = new SqlCommand("Select * from ScheduledMovies where MovieID = @mId", cnn);
			SqlParameter mId = new SqlParameter();
			mId.ParameterName = "@mId";
			command0.Parameters.AddWithValue("@mId", Add_Movie.id);

			SqlDataReader dataR = command0.ExecuteReader();
			while (dataR.Read())
			{
				date0 = DateTime.Parse(dataR["Date"].ToString());
				date1 = date0.ToString("dddd dd, MMMM yyyy");
				time0 = DateTime.Parse(dataR["Time"].ToString());
				time1 = time0.ToString("hh:mm tt");
				comboBox2.Items.Add(date1 + " - " + time1);
			}

			// Close connection and dispose after use:
			command0.Parameters.Clear();
			command0.Dispose();
			dataR.Close();
			cnn.Close();
		}

		// Save:
		private void button1_Click(object sender, EventArgs e)
		{
			// Database Connection:
			SqlConnection cnn = new SqlConnection(Program.DB_ConnectionString_Booking());
			cnn.Open();

			// Insert statement: 
			SqlCommand command1 = new SqlCommand("Insert into ScheduledMovies " +
				"(MovieID, RoomNameID, Date, Time) values " +
				"(@mvId, @rnId, @dt, @tm)", cnn);
			SqlParameter mvId = new SqlParameter();
			SqlParameter rnId = new SqlParameter();
			SqlParameter dt = new SqlParameter();
			SqlParameter tm = new SqlParameter();
			mvId.ParameterName = "@mvId";
			rnId.ParameterName = "@rnId";
			dt.ParameterName = "@dt";
			tm.ParameterName = "@tm";
			command1.Parameters.AddWithValue("@mvId", Add_Movie.id);
			command1.Parameters.AddWithValue("@rnId", "1");
			command1.Parameters.AddWithValue("@dt", comboBox2.Text);
			command1.Parameters.AddWithValue("@tm", comboBox1.Text);

			using (command1)
			{

				SqlDataAdapter dA = new SqlDataAdapter();
				dA.InsertCommand = command1;
				dA.InsertCommand.ExecuteNonQuery();

				MessageBox.Show("The data has been added.");

				dA.Dispose();
			}

			// Close connection and dispose after use:
			command1.Parameters.Clear();
			command1.Dispose();
			cnn.Close();
		}
		
		// Add more:
		private void button2_Click(object sender, EventArgs e)
		{
			// Variables:
			string date1;
			DateTime date0;
			string time1;
			DateTime time0;

			// Clear previous selected items:
			comboBox1.ResetText();
			comboBox2.Items.Clear();
			comboBox2.ResetText();

			// Database Connection:
			SqlConnection cnn = new SqlConnection(Program.DB_ConnectionString_Booking());
			cnn.Open();

			// Select statement:
			SqlCommand command0 = new SqlCommand("Select * from ScheduledMovies where MovieID = @mId", cnn);
			SqlParameter mId = new SqlParameter();
			mId.ParameterName = "@mId";
			command0.Parameters.AddWithValue("@mId", Add_Movie.id);

			SqlDataReader dataR = command0.ExecuteReader();
			while (dataR.Read())
			{
				date0 = DateTime.Parse(dataR["Date"].ToString());
				date1 = date0.ToString("dddd dd, MMMM yyyy");
				time0 = DateTime.Parse(dataR["Time"].ToString());
				time1 = time0.ToString("hh:mm tt");
				comboBox2.Items.Add(date1 + " - " + time1);
			}

			// Dispose after use:
			command0.Parameters.Clear();
			command0.Dispose();
			dataR.Close();

			// Insert statement: 
			SqlCommand command1 = new SqlCommand("Insert into ScheduledMovies " +
				"(MovieID, RoomNameID, Date, Time) values " +
				"(@mvId, @rnId, @dt, @tm)", cnn);
			SqlParameter mvId = new SqlParameter();
			SqlParameter rnId = new SqlParameter();
			SqlParameter dt = new SqlParameter();
			SqlParameter tm = new SqlParameter();
			mvId.ParameterName = "@mvId";
			rnId.ParameterName = "@rnId";
			dt.ParameterName = "@dt";
			tm.ParameterName = "@tm";
			command1.Parameters.AddWithValue("@mvId", Add_Movie.id);
			command1.Parameters.AddWithValue("@rnId", "1");
			command1.Parameters.AddWithValue("@dt", comboBox2.Text);
			command1.Parameters.AddWithValue("@tm", comboBox1.Text);

			using (command1)
			{

				SqlDataAdapter dA = new SqlDataAdapter();
				dA.InsertCommand = command1;
				dA.InsertCommand.ExecuteNonQuery();

				MessageBox.Show("The data has been added.");

				dA.Dispose();
			}

			// Close the connection and dispise after use:
			command1.Parameters.Clear();
			command1.Dispose();
			cnn.Close();			
		}
	}
}
