using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace LogIn
{
	public partial class UserControl_Title : UserControl
	{
		public UserControl_Title()
		{
			InitializeComponent();		
		}

		private void UserControl_Title_VisibleChanged(object sender, System.EventArgs e)
		{
			if (Visible == true)
			{
				// Variables:
				string st = "";
				string str = "";
				
				// Database Connection:
				SqlConnection cnn = new SqlConnection(Program.DB_ConnectionString_Booking());
				cnn.Open();

				// Select and Populate the movie details:
				str = DB_SelectMovieDetails(cnn, st);

				// Select, convert and populate the movie schedule:
				DB_SelectMovieSchedule(cnn, str);
				
				// Close the DB Connection:
				cnn.Close();
			}
		}

		// Variable used for data transfer to another userControl/Form:
		public static string Book = "";

		// Book button _click Event:
		private void button1_Click(object sender, System.EventArgs e)
		{
			Book = comboBox1.Text;

			this.ParentForm.DialogResult = DialogResult.OK;

			// Open the BookingSeatsScreen Form:
			BookingSeatsScreen bk = new BookingSeatsScreen();
			bk.ShowDialog();
		}

		// Select & Populate movie data:
		public string DB_SelectMovieDetails(SqlConnection cnn, string st)
		{
			// Variables:
			string check_Premiere = "";
			string ageRestr = "";
			string stri = "";

			// Select statement: 
			SqlCommand command0 = new SqlCommand("Select * from Movies where Title = @title", cnn);
			SqlParameter title = new SqlParameter();
			title.ParameterName = "@title";
			command0.Parameters.AddWithValue("@title", UserControl_Search_Title.Movie);

			SqlDataReader dR = command0.ExecuteReader();
			while (dR.Read())
			{
				stri = Populate_MovieDetails(dR, st, check_Premiere, ageRestr);
			}

			// Close and dispose after use:
			command0.Parameters.Clear();
			command0.Dispose();
			dR.Close();

			return stri;
		}

		// Select & populate scheduled dates:
		public void DB_SelectMovieSchedule(SqlConnection cnn, string st)
		{
			// Variables:
			string date1;
			DateTime date0;
			string time1;
			DateTime time0;

			// Select data:
			SqlCommand command1 = new SqlCommand("Select * from ScheduledMovies where MovieID = @mId", cnn);
			SqlParameter mId = new SqlParameter();
			mId.ParameterName = "@mId";
			command1.Parameters.AddWithValue("@mId", st);

			SqlDataReader dataR = command1.ExecuteReader();
			while (dataR.Read())
			{
				// Convert the Date to the wanted format:
				date0 = DateTime.Parse(dataR["Date"].ToString());
				date1 = date0.ToString("dddd dd, MMMM yyyy");

				// Convert the time to the wanted format:
				time0 = DateTime.Parse(dataR["Time"].ToString());
				time1 = time0.ToString("hh:mm tt");

				// Populate the scheduled date:
				comboBox1.Items.Add(date1 + " - " + time1);
			}

			// Close and dispose after use:
			command1.Parameters.Clear();
			command1.Dispose();
			dataR.Close();
		}


		// Populate the movie details fields:
		public string Populate_MovieDetails(SqlDataReader dR, string st, string check_Premiere, string ageRestr)
		{
			// Title:
			label1.Text = dR["Title"].ToString();
			
			// Genre:
			label15.Text = dR["Genre"].ToString();

			// Cast:
			label14.MaximumSize = new Size(200, 0);
			label14.AutoSize = true;
			label14.Text = dR["Cast"].ToString();

			// Creator:
			label13.Text = dR["Creator"].ToString();

			// Duration:
			label12.Text = dR["Duration"].ToString();

			// Restrictions:
			ageRestr = dR["Restrictions"].ToString();
			label11.Text = ageRestr + "+";
			label11.BorderStyle = BorderStyle.FixedSingle;

			// Premiere:
			check_Premiere = dR["Premiere"].ToString();
			if (check_Premiere == "0")
			{
				label16.Text = "No";
			}
			else
			{
				label16.Text = "Yes";
			}

			// Rating:
			label17.Text = dR["RatingIMDB"].ToString();

			// Description:
			label18.MaximumSize = new Size(400, 0);
			label18.AutoSize = true;
			label18.Text = dR["Descriprion"].ToString();

			// Price:
			label19.Text = dR["Price"].ToString();

			// Save id for later:
			st = dR["Id"].ToString();

			// Clear data for the Scheduled movie:
			comboBox1.Items.Clear();
			comboBox1.ResetText();

			// Movie Picture:
			pictureBox1.Image = Image.FromFile(Program.ImagesFolder() + "img" + st + ".jpg");
			pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

			return st;
		}
	}
}
