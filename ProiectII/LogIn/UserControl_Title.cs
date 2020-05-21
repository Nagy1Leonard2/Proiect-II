using System;
using System.Collections.Generic;
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
				// Database Connection:
				SqlConnection cnn = new SqlConnection(Program.DB_ConnectionString_Booking());
				cnn.Open();


				// Select statement: 
				SqlCommand command0 = new SqlCommand("Select * from Movies where Title = @title", cnn);
				SqlParameter title = new SqlParameter();
				title.ParameterName = "@title";
				command0.Parameters.AddWithValue("@title", UserControl_Search_Title.Movie);
				SqlDataReader dR = command0.ExecuteReader();
				string check_Premiere;
				string ageRestr;
				List<string> st = new List<string>();
				while (dR.Read())
				{
					label1.Text = dR["Title"].ToString();
					label15.Text = dR["Genre"].ToString();
					label14.MaximumSize = new Size(200, 0);
					label14.AutoSize = true;
					label14.Text = dR["Cast"].ToString();
					label13.Text = dR["Creator"].ToString();
					label12.Text = dR["Duration"].ToString();
					ageRestr = dR["Restrictions"].ToString();
					label11.Text = ageRestr + "+";
					check_Premiere = dR["Premiere"].ToString();
					if (check_Premiere == "0")
					{
						label16.Text = "No";
					}
					else
					{
						label16.Text = "Yes";
					}
					label17.Text = dR["RatingIMDB"].ToString();
					label18.MaximumSize = new Size(400, 0);
					label18.AutoSize = true;
					label18.Text = dR["Descriprion"].ToString();
					label19.Text = dR["Price"].ToString();
					st.Add(dR["Id"].ToString());
				}
				command0.Dispose();
				dR.Close();

				string date1;
				DateTime date0;
				string time1;
				DateTime time0;
				foreach (var movie in st)
				{
					SqlCommand command1 = new SqlCommand("Select * from ScheduledMovies where MovieID = @mId", cnn);
					SqlParameter mId = new SqlParameter();
					mId.ParameterName = "@mId";
					command1.Parameters.AddWithValue("@mId", movie);
					SqlDataReader dataR = command1.ExecuteReader();
					while (dataR.Read())
					{
						date0 = DateTime.Parse(dataR["Date"].ToString());
						date1 = date0.ToString("dddd dd, MMMM yyyy");
						time0 = DateTime.Parse(dataR["Time"].ToString());
						time1 = time0.ToString("hh:mm tt");
						comboBox1.Items.Add(date1+" - "+time1);
					}

					// Close the connection and dispose of the commands:
					command1.Dispose();
					dataR.Close();
				}
				cnn.Close();
			}
		}

		public static string Book = "";

		private void button1_Click(object sender, System.EventArgs e)
		{
			Book = comboBox1.Text;

			this.ParentForm.DialogResult = DialogResult.OK;

			BookingSeatsScreen bk = new BookingSeatsScreen();
			bk.ShowDialog();
		}
	}
}
