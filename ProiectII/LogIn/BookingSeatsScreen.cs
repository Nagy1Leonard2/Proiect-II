using DotLiquid.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace LogIn
{
	public partial class BookingSeatsScreen : Form
	{
		public BookingSeatsScreen()
		{
			InitializeComponent();

			label43.Text = UserControl_Search_Title.Movie;
			label44.Text = UserControl_Title.Book;

			Show_BookedSeats();
		}

		private void BookingSeatsScreen_Load(object sender, EventArgs e)
		{
			//panel1.BackColor = Color.FromArgb(200, 0, 0, 0);
			string seatId;
			for (int i = 1; i <= 200; i++)
			{
				seatId = "s" + i.ToString();
				((PictureBox)this.panel1.Controls[seatId]).Tag = "UnChecked";
			}
		}

		private void Label1_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void label2_Click(object sender, EventArgs e)
		{
			Home y = new Home();
			this.Hide();
			y.ShowDialog();
			this.Close();
		}

		public void Show_BookedSeats()
		{
			string temp = label44.Text;
			DateTime myDate = DateTime.ParseExact(temp, "dddd dd, MMMM yyyy - hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);

			string str = myDate.ToString("yyyy-MM-dd HH:mm");

			string dt = str.Substring(0, 10);
			string tm = str.Substring(11);

			// Database Connection:
			SqlConnection cnn = new SqlConnection(Program.DB_ConnectionString_Booking());
			cnn.Open();

			
			// Select statement: 
			SqlCommand command0 = new SqlCommand("Select Id from ScheduledMovies where Date = @date and Time = @time", cnn);
			SqlParameter date = new SqlParameter();
			SqlParameter time = new SqlParameter();
			date.ParameterName = "@date";
			time.ParameterName = "@time";
			command0.Parameters.AddWithValue("@date", dt);
			command0.Parameters.AddWithValue("@time", tm);
			string sm_id = "";
			using (command0)
			{
				SqlDataReader dR = command0.ExecuteReader();
				using (dR)
				{
					while (dR.Read())
					{
						sm_id = dR["Id"].ToString();
					}
				}
			}

			List<string> seats = new List<string>();

			SqlCommand command1 = new SqlCommand("Select * from SeatBooking where ScheduledMovieID = @smId", cnn);
			SqlParameter smId = new SqlParameter();
			smId.ParameterName = "@smId";
			command1.Parameters.AddWithValue("@smId", sm_id);
			using (command1)
			{
				SqlDataReader dataR = command1.ExecuteReader();
				using (dataR)
				{
					while (dataR.Read())
					{
						seats.Add(dataR["RoomSeatID"].ToString());
					}
				}
			}

			// Default the seats to empty:
			string sId;
			for (int i = 1; i <= 200; i++)
			{
				sId = "s" + i.ToString();
				((PictureBox)this.panel1.Controls[sId]).Image = Properties.Resources.icon8_user_male_52_grey;
				((PictureBox)this.panel1.Controls[sId]).SizeMode = PictureBoxSizeMode.StretchImage;
			}

			// Put the booked seats:
			string seatId;
			foreach (var nr in seats)
			{
				seatId = "s" + nr;
				((PictureBox)this.panel1.Controls[seatId]).Image = Properties.Resources.icons8_user_male_52_red;
				((PictureBox)this.panel1.Controls[seatId]).SizeMode = PictureBoxSizeMode.StretchImage;
			}
			
			//Close connections and dispose commands:
			command0.Dispose();
			command1.Dispose();
			cnn.Close();
		}


		private void button1_Click(object sender, EventArgs e)
		{
			Book();

			Home y = new Home();
			this.Hide();
			y.ShowDialog();
			this.Close();
		}

		
		public void Book()
		{

			List<string> str = new List<string>();
			string seatId;
			for (int i = 1; i <= 200; i++)
			{
				seatId = "s" + i.ToString();
				if (((PictureBox)this.panel1.Controls[seatId]).Tag.ToString() == "Checked")
				{
					str.Add(seatId);
				}
			}

			

			// Get the Scheduled Movie Id:
			string temp = label44.Text;
			DateTime myDate = DateTime.ParseExact(temp, "dddd dd, MMMM yyyy - hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);

			string stri = myDate.ToString("yyyy-MM-dd HH:mm");

			string dt = stri.Substring(0, 10);
			string tm = stri.Substring(11);




			// Database Connection:
			SqlConnection cnn = new SqlConnection(Program.DB_ConnectionString_Booking());
			cnn.Open();


			// Select statement: 
			SqlCommand command0 = new SqlCommand("Select Id from ScheduledMovies where Date = @date and Time = @time", cnn);
			SqlParameter date = new SqlParameter();
			SqlParameter time = new SqlParameter();
			date.ParameterName = "@date";
			time.ParameterName = "@time";
			command0.Parameters.AddWithValue("@date", dt);
			command0.Parameters.AddWithValue("@time", tm);
			string sm_id = "";
			using (command0)
			{
				SqlDataReader dR = command0.ExecuteReader();
				using (dR)
				{
					while (dR.Read())
					{
						sm_id = dR["Id"].ToString();
					}
				}
			}



			// Book the selected seats:

			// Insert statement: 
			SqlCommand command1 = new SqlCommand("Insert into SeatBooking (CustomerID, ScheduledMovieID, RoomSeatID) values (1, @sId, @rsId)", cnn);
			SqlParameter rsId = new SqlParameter();
			SqlParameter sId = new SqlParameter();
			rsId.ParameterName = "@rsId";
			sId.ParameterName = "@sId";
			string roSeatId;
			using (command1)
			{
				foreach (string roomSeatId in str)
				{
					//MessageBox.Show(roomSeatId);
					roSeatId = roomSeatId.Substring(1);
					command1.Parameters.AddWithValue("@rsId", roSeatId);
					command1.Parameters.AddWithValue("@sId", sm_id);
					SqlDataAdapter dA = new SqlDataAdapter();
					dA.InsertCommand = command1;
					dA.InsertCommand.ExecuteNonQuery();

					dA.Dispose();
					command1.Parameters.Clear();
				}
			}

			//Close connections and dispose commands:
			command0.Dispose();
			command1.Dispose();
			cnn.Close();

		}



		// Mouse events for booking and unbooking (green <-> grey):
		
		private void s1_MouseClick(object sender, MouseEventArgs e)
		{
			s1.Image = Properties.Resources.icons8_user_male_52_green;
			s1.SizeMode = PictureBoxSizeMode.StretchImage;
			s1.Tag = "Checked";
		}

		private void s1_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s1.Image = Properties.Resources.icon8_user_male_52_grey;
			s1.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s2_MouseClick(object sender, MouseEventArgs e)
		{
			s2.Image = Properties.Resources.icons8_user_male_52_green;
			s2.SizeMode = PictureBoxSizeMode.StretchImage;
			s2.Tag = "Checked";
		}

		private void s2_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s1.Image = Properties.Resources.icon8_user_male_52_grey;
			s1.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s3_MouseClick(object sender, MouseEventArgs e)
		{
			s3.Image = Properties.Resources.icons8_user_male_52_green;
			s3.SizeMode = PictureBoxSizeMode.StretchImage;
			s3.Tag = "Checked";
		}

		private void s3_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s3.Image = Properties.Resources.icon8_user_male_52_grey;
			s3.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s4_MouseClick(object sender, MouseEventArgs e)
		{
			s4.Image = Properties.Resources.icons8_user_male_52_green;
			s4.SizeMode = PictureBoxSizeMode.StretchImage;
			s4.Tag = "Checked";
		}

		private void s4_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s4.Image = Properties.Resources.icon8_user_male_52_grey;
			s4.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s5_MouseClick(object sender, MouseEventArgs e)
		{
			s5.Image = Properties.Resources.icons8_user_male_52_green;
			s5.SizeMode = PictureBoxSizeMode.StretchImage;
			s5.Tag = "Checked";
		}

		private void s5_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s5.Image = Properties.Resources.icon8_user_male_52_grey;
			s5.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s6_MouseClick(object sender, MouseEventArgs e)
		{
			s6.Image = Properties.Resources.icons8_user_male_52_green;
			s6.SizeMode = PictureBoxSizeMode.StretchImage;
			s6.Tag = "Checked";
		}

		private void s6_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s6.Image = Properties.Resources.icon8_user_male_52_grey;
			s6.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s7_MouseClick(object sender, MouseEventArgs e)
		{
			s7.Image = Properties.Resources.icons8_user_male_52_green;
			s7.SizeMode = PictureBoxSizeMode.StretchImage;
			s7.Tag = "Checked";
		}

		private void s7_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s7.Image = Properties.Resources.icon8_user_male_52_grey;
			s7.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s8_MouseClick(object sender, MouseEventArgs e)
		{
			s8.Image = Properties.Resources.icons8_user_male_52_green;
			s8.SizeMode = PictureBoxSizeMode.StretchImage;
			s8.Tag = "Checked";
		}

		private void s8_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s8.Image = Properties.Resources.icon8_user_male_52_grey;
			s8.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s9_MouseClick(object sender, MouseEventArgs e)
		{
			s9.Image = Properties.Resources.icons8_user_male_52_green;
			s9.SizeMode = PictureBoxSizeMode.StretchImage;
			s9.Tag = "Checked";
		}

		private void s9_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s9.Image = Properties.Resources.icon8_user_male_52_grey;
			s9.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s10_MouseClick(object sender, MouseEventArgs e)
		{
			s10.Image = Properties.Resources.icons8_user_male_52_green;
			s10.SizeMode = PictureBoxSizeMode.StretchImage;
			s10.Tag = "Checked";
		}

		private void s10_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s10.Image = Properties.Resources.icon8_user_male_52_grey;
			s10.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s11_MouseClick(object sender, MouseEventArgs e)
		{
			s11.Image = Properties.Resources.icons8_user_male_52_green;
			s11.SizeMode = PictureBoxSizeMode.StretchImage;
			s11.Tag = "Checked";
		}

		private void s11_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s11.Image = Properties.Resources.icon8_user_male_52_grey;
			s11.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s12_MouseClick(object sender, MouseEventArgs e)
		{
			s12.Image = Properties.Resources.icons8_user_male_52_green;
			s12.SizeMode = PictureBoxSizeMode.StretchImage;
			s12.Tag = "Checked";
		}

		private void s12_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s12.Image = Properties.Resources.icon8_user_male_52_grey;
			s12.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s13_MouseClick(object sender, MouseEventArgs e)
		{
			s13.Image = Properties.Resources.icons8_user_male_52_green;
			s13.SizeMode = PictureBoxSizeMode.StretchImage;
			s13.Tag = "Checked";
		}

		private void s13_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s13.Image = Properties.Resources.icon8_user_male_52_grey;
			s13.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s14_MouseClick(object sender, MouseEventArgs e)
		{
			s14.Image = Properties.Resources.icons8_user_male_52_green;
			s14.SizeMode = PictureBoxSizeMode.StretchImage;
			s14.Tag = "Checked";
		}

		private void s14_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s14.Image = Properties.Resources.icon8_user_male_52_grey;
			s14.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s15_MouseClick(object sender, MouseEventArgs e)
		{
			s15.Image = Properties.Resources.icons8_user_male_52_green;
			s15.SizeMode = PictureBoxSizeMode.StretchImage;
			s15.Tag = "Checked";
		}

		private void s15_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s15.Image = Properties.Resources.icon8_user_male_52_grey;
			s15.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s16_MouseClick(object sender, MouseEventArgs e)
		{
			s16.Image = Properties.Resources.icons8_user_male_52_green;
			s16.SizeMode = PictureBoxSizeMode.StretchImage;
			s16.Tag = "Checked";
		}

		private void s16_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s16.Image = Properties.Resources.icon8_user_male_52_grey;
			s16.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s17_MouseClick(object sender, MouseEventArgs e)
		{
			s17.Image = Properties.Resources.icons8_user_male_52_green;
			s17.SizeMode = PictureBoxSizeMode.StretchImage;
			s17.Tag = "Checked";
		}

		private void s17_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s17.Image = Properties.Resources.icon8_user_male_52_grey;
			s17.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s18_MouseClick(object sender, MouseEventArgs e)
		{
			s18.Image = Properties.Resources.icons8_user_male_52_green;
			s18.SizeMode = PictureBoxSizeMode.StretchImage;
			s18.Tag = "Checked";
		}

		private void s18_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s18.Image = Properties.Resources.icon8_user_male_52_grey;
			s18.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s19_MouseClick(object sender, MouseEventArgs e)
		{
			s18.Image = Properties.Resources.icons8_user_male_52_green;
			s18.SizeMode = PictureBoxSizeMode.StretchImage;
			s19.Tag = "Checked";
		}

		private void s19_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s19.Image = Properties.Resources.icon8_user_male_52_grey;
			s19.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s20_MouseClick(object sender, MouseEventArgs e)
		{
			s20.Image = Properties.Resources.icons8_user_male_52_green;
			s20.SizeMode = PictureBoxSizeMode.StretchImage;
			s20.Tag = "Checked";
		}

		private void s20_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s20.Image = Properties.Resources.icon8_user_male_52_grey;
			s20.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s21_MouseClick(object sender, MouseEventArgs e)
		{
			s21.Image = Properties.Resources.icons8_user_male_52_green;
			s21.SizeMode = PictureBoxSizeMode.StretchImage;
			s21.Tag = "Checked";
		}

		private void s21_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s21.Image = Properties.Resources.icon8_user_male_52_grey;
			s21.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s22_MouseClick(object sender, MouseEventArgs e)
		{
			s22.Image = Properties.Resources.icons8_user_male_52_green;
			s22.SizeMode = PictureBoxSizeMode.StretchImage;
			s22.Tag = "Checked";
		}

		private void s22_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s22.Image = Properties.Resources.icon8_user_male_52_grey;
			s22.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s23_MouseClick(object sender, MouseEventArgs e)
		{
			s23.Image = Properties.Resources.icons8_user_male_52_green;
			s23.SizeMode = PictureBoxSizeMode.StretchImage;
			s23.Tag = "Checked";
		}

		private void s23_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s23.Image = Properties.Resources.icon8_user_male_52_grey;
			s23.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s24_MouseClick(object sender, MouseEventArgs e)
		{
			s24.Image = Properties.Resources.icons8_user_male_52_green;
			s24.SizeMode = PictureBoxSizeMode.StretchImage;
			s24.Tag = "Checked";
		}

		private void s24_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s24.Image = Properties.Resources.icon8_user_male_52_grey;
			s24.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s25_MouseClick(object sender, MouseEventArgs e)
		{
			s25.Image = Properties.Resources.icons8_user_male_52_green;
			s25.SizeMode = PictureBoxSizeMode.StretchImage;
			s25.Tag = "Checked";
		}

		private void s25_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s24.Image = Properties.Resources.icon8_user_male_52_grey;
			s24.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s26_MouseClick(object sender, MouseEventArgs e)
		{
			s26.Image = Properties.Resources.icons8_user_male_52_green;
			s26.SizeMode = PictureBoxSizeMode.StretchImage;
			s26.Tag = "Checked";
		}

		private void s26_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s26.Image = Properties.Resources.icon8_user_male_52_grey;
			s26.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s27_MouseClick(object sender, MouseEventArgs e)
		{
			s27.Image = Properties.Resources.icons8_user_male_52_green;
			s27.SizeMode = PictureBoxSizeMode.StretchImage;
			s27.Tag = "Checked";
		}

		private void s27_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s27.Image = Properties.Resources.icon8_user_male_52_grey;
			s27.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s28_MouseClick(object sender, MouseEventArgs e)
		{
			s28.Image = Properties.Resources.icons8_user_male_52_green;
			s28.SizeMode = PictureBoxSizeMode.StretchImage;
			s28.Tag = "Checked";
		}

		private void s28_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s28.Image = Properties.Resources.icon8_user_male_52_grey;
			s28.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s29_MouseClick(object sender, MouseEventArgs e)
		{
			s29.Image = Properties.Resources.icons8_user_male_52_green;
			s29.SizeMode = PictureBoxSizeMode.StretchImage;
			s29.Tag = "Checked";
		}

		private void s29_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s29.Image = Properties.Resources.icon8_user_male_52_grey;
			s29.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s30_MouseClick(object sender, MouseEventArgs e)
		{
			s30.Image = Properties.Resources.icons8_user_male_52_green;
			s30.SizeMode = PictureBoxSizeMode.StretchImage;
			s30.Tag = "Checked";
		}

		private void s30_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s30.Image = Properties.Resources.icon8_user_male_52_grey;
			s30.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s31_MouseClick(object sender, MouseEventArgs e)
		{
			s31.Image = Properties.Resources.icons8_user_male_52_green;
			s31.SizeMode = PictureBoxSizeMode.StretchImage;
			s31.Tag = "Checked";
		}

		private void s31_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s31.Image = Properties.Resources.icon8_user_male_52_grey;
			s31.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s32_MouseClick(object sender, MouseEventArgs e)
		{
			s32.Image = Properties.Resources.icons8_user_male_52_green;
			s32.SizeMode = PictureBoxSizeMode.StretchImage;
			s32.Tag = "Checked";
		}

		private void s32_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s32.Image = Properties.Resources.icon8_user_male_52_grey;
			s32.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s33_MouseClick(object sender, MouseEventArgs e)
		{
			s33.Image = Properties.Resources.icons8_user_male_52_green;
			s33.SizeMode = PictureBoxSizeMode.StretchImage;
			s33.Tag = "Checked";
		}

		private void s33_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s33.Image = Properties.Resources.icon8_user_male_52_grey;
			s33.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s34_MouseClick(object sender, MouseEventArgs e)
		{
			s34.Image = Properties.Resources.icons8_user_male_52_green;
			s34.SizeMode = PictureBoxSizeMode.StretchImage;
			s34.Tag = "Checked";
		}

		private void s34_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s34.Image = Properties.Resources.icon8_user_male_52_grey;
			s34.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s35_MouseClick(object sender, MouseEventArgs e)
		{
			s35.Image = Properties.Resources.icons8_user_male_52_green;
			s35.SizeMode = PictureBoxSizeMode.StretchImage;
			s35.Tag = "Checked";
		}

		private void s35_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s35.Image = Properties.Resources.icon8_user_male_52_grey;
			s35.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s36_MouseClick(object sender, MouseEventArgs e)
		{
			s36.Image = Properties.Resources.icons8_user_male_52_green;
			s36.SizeMode = PictureBoxSizeMode.StretchImage;
			s36.Tag = "Checked";
		}

		private void s36_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s36.Image = Properties.Resources.icon8_user_male_52_grey;
			s36.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s37_MouseClick(object sender, MouseEventArgs e)
		{
			s37.Image = Properties.Resources.icons8_user_male_52_green;
			s37.SizeMode = PictureBoxSizeMode.StretchImage;
			s37.Tag = "Checked";
		}

		private void s37_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s37.Image = Properties.Resources.icon8_user_male_52_grey;
			s37.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s38_MouseClick(object sender, MouseEventArgs e)
		{
			s38.Image = Properties.Resources.icons8_user_male_52_green;
			s38.SizeMode = PictureBoxSizeMode.StretchImage;
			s38.Tag = "Checked";
		}

		private void s38_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s38.Image = Properties.Resources.icon8_user_male_52_grey;
			s38.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s39_MouseClick(object sender, MouseEventArgs e)
		{
			s39.Image = Properties.Resources.icons8_user_male_52_green;
			s39.SizeMode = PictureBoxSizeMode.StretchImage;
			s39.Tag = "Checked";
		}

		private void s39_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s39.Image = Properties.Resources.icon8_user_male_52_grey;
			s39.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s40_MouseClick(object sender, MouseEventArgs e)
		{
			s40.Image = Properties.Resources.icons8_user_male_52_green;
			s40.SizeMode = PictureBoxSizeMode.StretchImage;
			s40.Tag = "Checked";
		}

		private void s40_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s40.Image = Properties.Resources.icon8_user_male_52_grey;
			s40.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s41_MouseClick(object sender, MouseEventArgs e)
		{
			s41.Image = Properties.Resources.icons8_user_male_52_green;
			s41.SizeMode = PictureBoxSizeMode.StretchImage;
			s41.Tag = "Checked";
		}

		private void s41_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s41.Image = Properties.Resources.icon8_user_male_52_grey;
			s41.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s42_MouseClick(object sender, MouseEventArgs e)
		{
			s42.Image = Properties.Resources.icons8_user_male_52_green;
			s42.SizeMode = PictureBoxSizeMode.StretchImage;
			s42.Tag = "Checked";
		}

		private void s42_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s42.Image = Properties.Resources.icon8_user_male_52_grey;
			s42.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s43_MouseClick(object sender, MouseEventArgs e)
		{
			s43.Image = Properties.Resources.icons8_user_male_52_green;
			s43.SizeMode = PictureBoxSizeMode.StretchImage;
			s43.Tag = "Checked";
		}

		private void s43_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s43.Image = Properties.Resources.icon8_user_male_52_grey;
			s43.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s44_MouseClick(object sender, MouseEventArgs e)
		{
			s44.Image = Properties.Resources.icons8_user_male_52_green;
			s44.SizeMode = PictureBoxSizeMode.StretchImage;
			s44.Tag = "Checked";
		}

		private void s44_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s44.Image = Properties.Resources.icon8_user_male_52_grey;
			s44.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s45_MouseClick(object sender, MouseEventArgs e)
		{
			s45.Image = Properties.Resources.icons8_user_male_52_green;
			s45.SizeMode = PictureBoxSizeMode.StretchImage;
			s45.Tag = "Checked";
		}

		private void s45_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s45.Image = Properties.Resources.icon8_user_male_52_grey;
			s45.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s46_MouseClick(object sender, MouseEventArgs e)
		{
			s46.Image = Properties.Resources.icons8_user_male_52_green;
			s46.SizeMode = PictureBoxSizeMode.StretchImage;
			s46.Tag = "Checked";
		}

		private void s46_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s46.Image = Properties.Resources.icon8_user_male_52_grey;
			s46.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s47_MouseClick(object sender, MouseEventArgs e)
		{
			s47.Image = Properties.Resources.icons8_user_male_52_green;
			s47.SizeMode = PictureBoxSizeMode.StretchImage;
			s47.Tag = "Checked";
		}

		private void s47_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s47.Image = Properties.Resources.icon8_user_male_52_grey;
			s47.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s48_MouseClick(object sender, MouseEventArgs e)
		{
			s48.Image = Properties.Resources.icons8_user_male_52_green;
			s48.SizeMode = PictureBoxSizeMode.StretchImage;
			s48.Tag = "Checked";
		}

		private void s48_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s48.Image = Properties.Resources.icon8_user_male_52_grey;
			s48.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s49_MouseClick(object sender, MouseEventArgs e)
		{
			s49.Image = Properties.Resources.icons8_user_male_52_green;
			s49.SizeMode = PictureBoxSizeMode.StretchImage;
			s49.Tag = "Checked";
		}

		private void s49_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s49.Image = Properties.Resources.icon8_user_male_52_grey;
			s49.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s50_MouseClick(object sender, MouseEventArgs e)
		{
			s50.Image = Properties.Resources.icons8_user_male_52_green;
			s50.SizeMode = PictureBoxSizeMode.StretchImage;
			s50.Tag = "Checked";
		}

		private void s50_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s50.Image = Properties.Resources.icon8_user_male_52_grey;
			s50.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s51_MouseClick(object sender, MouseEventArgs e)
		{
			s51.Image = Properties.Resources.icons8_user_male_52_green;
			s51.SizeMode = PictureBoxSizeMode.StretchImage;
			s51.Tag = "Checked";
		}

		private void s51_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s51.Image = Properties.Resources.icon8_user_male_52_grey;
			s51.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s52_MouseClick(object sender, MouseEventArgs e)
		{
			s52.Image = Properties.Resources.icons8_user_male_52_green;
			s52.SizeMode = PictureBoxSizeMode.StretchImage;
			s52.Tag = "Checked";
		}

		private void s52_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s52.Image = Properties.Resources.icon8_user_male_52_grey;
			s52.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s53_MouseClick(object sender, MouseEventArgs e)
		{
			s53.Image = Properties.Resources.icons8_user_male_52_green;
			s53.SizeMode = PictureBoxSizeMode.StretchImage;
			s53.Tag = "Checked";
		}

		private void s53_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s53.Image = Properties.Resources.icon8_user_male_52_grey;
			s53.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s54_MouseClick(object sender, MouseEventArgs e)
		{
			s54.Image = Properties.Resources.icons8_user_male_52_green;
			s54.SizeMode = PictureBoxSizeMode.StretchImage;
			s54.Tag = "Checked";
		}

		private void s54_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s54.Image = Properties.Resources.icon8_user_male_52_grey;
			s54.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s55_MouseClick(object sender, MouseEventArgs e)
		{
			s55.Image = Properties.Resources.icons8_user_male_52_green;
			s55.SizeMode = PictureBoxSizeMode.StretchImage;
			s55.Tag = "Checked";
		}

		private void s55_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s55.Image = Properties.Resources.icon8_user_male_52_grey;
			s55.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s56_MouseClick(object sender, MouseEventArgs e)
		{
			s56.Image = Properties.Resources.icons8_user_male_52_green;
			s56.SizeMode = PictureBoxSizeMode.StretchImage;
			s56.Tag = "Checked";
		}

		private void s56_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s56.Image = Properties.Resources.icon8_user_male_52_grey;
			s56.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s57_MouseClick(object sender, MouseEventArgs e)
		{
			s57.Image = Properties.Resources.icons8_user_male_52_green;
			s57.SizeMode = PictureBoxSizeMode.StretchImage;
			s57.Tag = "Checked";
		}

		private void s57_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s57.Image = Properties.Resources.icon8_user_male_52_grey;
			s57.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s58_MouseClick(object sender, MouseEventArgs e)
		{
			s58.Image = Properties.Resources.icons8_user_male_52_green;
			s58.SizeMode = PictureBoxSizeMode.StretchImage;
			s58.Tag = "Checked";
		}

		private void s58_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s58.Image = Properties.Resources.icon8_user_male_52_grey;
			s58.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s59_MouseClick(object sender, MouseEventArgs e)
		{
			s59.Image = Properties.Resources.icons8_user_male_52_green;
			s59.SizeMode = PictureBoxSizeMode.StretchImage;
			s59.Tag = "Checked";
		}

		private void s59_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s59.Image = Properties.Resources.icon8_user_male_52_grey;
			s59.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s60_MouseClick(object sender, MouseEventArgs e)
		{
			s60.Image = Properties.Resources.icons8_user_male_52_green;
			s60.SizeMode = PictureBoxSizeMode.StretchImage;
			s60.Tag = "Checked";
		}

		private void s60_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s60.Image = Properties.Resources.icon8_user_male_52_grey;
			s60.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s61_MouseClick(object sender, MouseEventArgs e)
		{
			s61.Image = Properties.Resources.icons8_user_male_52_green;
			s61.SizeMode = PictureBoxSizeMode.StretchImage;
			s61.Tag = "Checked";
		}

		private void s61_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s61.Image = Properties.Resources.icon8_user_male_52_grey;
			s61.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s62_MouseClick(object sender, MouseEventArgs e)
		{
			s62.Image = Properties.Resources.icons8_user_male_52_green;
			s62.SizeMode = PictureBoxSizeMode.StretchImage;
			s62.Tag = "Checked";
		}

		private void s62_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s62.Image = Properties.Resources.icon8_user_male_52_grey;
			s62.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s63_MouseClick(object sender, MouseEventArgs e)
		{
			s63.Image = Properties.Resources.icons8_user_male_52_green;
			s63.SizeMode = PictureBoxSizeMode.StretchImage;
			s63.Tag = "Checked";
		}

		private void s63_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s63.Image = Properties.Resources.icon8_user_male_52_grey;
			s63.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s64_MouseClick(object sender, MouseEventArgs e)
		{
			s64.Image = Properties.Resources.icons8_user_male_52_green;
			s64.SizeMode = PictureBoxSizeMode.StretchImage;
			s64.Tag = "Checked";
		}

		private void s64_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s64.Image = Properties.Resources.icon8_user_male_52_grey;
			s64.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s65_MouseClick(object sender, MouseEventArgs e)
		{
			s65.Image = Properties.Resources.icons8_user_male_52_green;
			s65.SizeMode = PictureBoxSizeMode.StretchImage;
			s65.Tag = "Checked";
		}

		private void s65_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s65.Image = Properties.Resources.icon8_user_male_52_grey;
			s65.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s66_MouseClick(object sender, MouseEventArgs e)
		{
			s66.Image = Properties.Resources.icons8_user_male_52_green;
			s66.SizeMode = PictureBoxSizeMode.StretchImage;
			s66.Tag = "Checked";
		}

		private void s66_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s66.Image = Properties.Resources.icon8_user_male_52_grey;
			s66.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s67_MouseClick(object sender, MouseEventArgs e)
		{
			s67.Image = Properties.Resources.icons8_user_male_52_green;
			s67.SizeMode = PictureBoxSizeMode.StretchImage;
			s67.Tag = "Checked";
		}

		private void s67_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s67.Image = Properties.Resources.icon8_user_male_52_grey;
			s67.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s68_MouseClick(object sender, MouseEventArgs e)
		{
			s68.Image = Properties.Resources.icons8_user_male_52_green;
			s68.SizeMode = PictureBoxSizeMode.StretchImage;
			s68.Tag = "Checked";
		}

		private void s68_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s68.Image = Properties.Resources.icon8_user_male_52_grey;
			s68.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s69_MouseClick(object sender, MouseEventArgs e)
		{
			s69.Image = Properties.Resources.icons8_user_male_52_green;
			s69.SizeMode = PictureBoxSizeMode.StretchImage;
			s69.Tag = "Checked";
		}

		private void s69_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s69.Image = Properties.Resources.icon8_user_male_52_grey;
			s69.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s70_MouseClick(object sender, MouseEventArgs e)
		{
			s70.Image = Properties.Resources.icons8_user_male_52_green;
			s70.SizeMode = PictureBoxSizeMode.StretchImage;
			s70.Tag = "Checked";
		}

		private void s70_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s70.Image = Properties.Resources.icon8_user_male_52_grey;
			s70.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s71_MouseClick(object sender, MouseEventArgs e)
		{
			s71.Image = Properties.Resources.icons8_user_male_52_green;
			s71.SizeMode = PictureBoxSizeMode.StretchImage;
			s71.Tag = "Checked";
		}

		private void s71_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s71.Image = Properties.Resources.icon8_user_male_52_grey;
			s71.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s72_MouseClick(object sender, MouseEventArgs e)
		{
			s72.Image = Properties.Resources.icons8_user_male_52_green;
			s72.SizeMode = PictureBoxSizeMode.StretchImage;
			s72.Tag = "Checked";
		}

		private void s72_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s72.Image = Properties.Resources.icon8_user_male_52_grey;
			s72.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s73_MouseClick(object sender, MouseEventArgs e)
		{
			s73.Image = Properties.Resources.icons8_user_male_52_green;
			s73.SizeMode = PictureBoxSizeMode.StretchImage;
			s73.Tag = "Checked";
		}

		private void s73_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s73.Image = Properties.Resources.icon8_user_male_52_grey;
			s73.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s74_MouseClick(object sender, MouseEventArgs e)
		{
			s74.Image = Properties.Resources.icons8_user_male_52_green;
			s74.SizeMode = PictureBoxSizeMode.StretchImage;
			s74.Tag = "Checked";
		}

		private void s74_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s74.Image = Properties.Resources.icon8_user_male_52_grey;
			s74.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s75_MouseClick(object sender, MouseEventArgs e)
		{
			s75.Image = Properties.Resources.icons8_user_male_52_green;
			s75.SizeMode = PictureBoxSizeMode.StretchImage;
			s75.Tag = "Checked";
		}

		private void s75_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s75.Image = Properties.Resources.icon8_user_male_52_grey;
			s75.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s76_MouseClick(object sender, MouseEventArgs e)
		{
			s76.Image = Properties.Resources.icons8_user_male_52_green;
			s76.SizeMode = PictureBoxSizeMode.StretchImage;
			s76.Tag = "Checked";
		}

		private void s76_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s76.Image = Properties.Resources.icon8_user_male_52_grey;
			s76.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s77_MouseClick(object sender, MouseEventArgs e)
		{
			s77.Image = Properties.Resources.icons8_user_male_52_green;
			s77.SizeMode = PictureBoxSizeMode.StretchImage;
			s77.Tag = "Checked";
		}

		private void s77_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s77.Image = Properties.Resources.icon8_user_male_52_grey;
			s77.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s78_MouseClick(object sender, MouseEventArgs e)
		{
			s78.Image = Properties.Resources.icons8_user_male_52_green;
			s78.SizeMode = PictureBoxSizeMode.StretchImage;
			s78.Tag = "Checked";
		}

		private void s78_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s78.Image = Properties.Resources.icon8_user_male_52_grey;
			s78.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s79_MouseClick(object sender, MouseEventArgs e)
		{
			s79.Image = Properties.Resources.icons8_user_male_52_green;
			s79.SizeMode = PictureBoxSizeMode.StretchImage;
			s79.Tag = "Checked";
		}

		private void s79_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s79.Image = Properties.Resources.icon8_user_male_52_grey;
			s79.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s80_MouseClick(object sender, MouseEventArgs e)
		{
			s80.Image = Properties.Resources.icons8_user_male_52_green;
			s80.SizeMode = PictureBoxSizeMode.StretchImage;
			s80.Tag = "Checked";
		}

		private void s80_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s80.Image = Properties.Resources.icon8_user_male_52_grey;
			s80.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s81_MouseClick(object sender, MouseEventArgs e)
		{
			s81.Image = Properties.Resources.icons8_user_male_52_green;
			s81.SizeMode = PictureBoxSizeMode.StretchImage;
			s81.Tag = "Checked";
		}

		private void s81_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s81.Image = Properties.Resources.icon8_user_male_52_grey;
			s81.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s82_MouseClick(object sender, MouseEventArgs e)
		{
			s82.Image = Properties.Resources.icons8_user_male_52_green;
			s82.SizeMode = PictureBoxSizeMode.StretchImage;
			s82.Tag = "Checked";
		}

		private void s82_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s82.Image = Properties.Resources.icon8_user_male_52_grey;
			s82.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s83_MouseClick(object sender, MouseEventArgs e)
		{
			s83.Image = Properties.Resources.icons8_user_male_52_green;
			s83.SizeMode = PictureBoxSizeMode.StretchImage;
			s83.Tag = "Checked";
		}

		private void s83_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s83.Image = Properties.Resources.icon8_user_male_52_grey;
			s83.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s84_MouseClick(object sender, MouseEventArgs e)
		{
			s84.Image = Properties.Resources.icons8_user_male_52_green;
			s84.SizeMode = PictureBoxSizeMode.StretchImage;
			s84.Tag = "Checked";
		}

		private void s84_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s84.Image = Properties.Resources.icon8_user_male_52_grey;
			s84.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s85_MouseClick(object sender, MouseEventArgs e)
		{
			s85.Image = Properties.Resources.icons8_user_male_52_green;
			s85.SizeMode = PictureBoxSizeMode.StretchImage;
			s85.Tag = "Checked";
		}

		private void s85_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s85.Image = Properties.Resources.icon8_user_male_52_grey;
			s85.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s86_MouseClick(object sender, MouseEventArgs e)
		{
			s86.Image = Properties.Resources.icons8_user_male_52_green;
			s86.SizeMode = PictureBoxSizeMode.StretchImage;
			s86.Tag = "Checked";
		}

		private void s86_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s86.Image = Properties.Resources.icon8_user_male_52_grey;
			s86.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s87_MouseClick(object sender, MouseEventArgs e)
		{
			s87.Image = Properties.Resources.icons8_user_male_52_green;
			s87.SizeMode = PictureBoxSizeMode.StretchImage;
			s87.Tag = "Checked";
		}

		private void s87_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s87.Image = Properties.Resources.icon8_user_male_52_grey;
			s87.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s88_MouseClick(object sender, MouseEventArgs e)
		{
			s88.Image = Properties.Resources.icons8_user_male_52_green;
			s88.SizeMode = PictureBoxSizeMode.StretchImage;
			s88.Tag = "Checked";
		}

		private void s88_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s88.Image = Properties.Resources.icon8_user_male_52_grey;
			s88.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s89_MouseClick(object sender, MouseEventArgs e)
		{
			s89.Image = Properties.Resources.icons8_user_male_52_green;
			s89.SizeMode = PictureBoxSizeMode.StretchImage;
			s89.Tag = "Checked";
		}

		private void s89_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s89.Image = Properties.Resources.icon8_user_male_52_grey;
			s89.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s90_MouseClick(object sender, MouseEventArgs e)
		{
			s90.Image = Properties.Resources.icons8_user_male_52_green;
			s90.SizeMode = PictureBoxSizeMode.StretchImage;
			s90.Tag = "Checked";
		}

		private void s90_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s90.Image = Properties.Resources.icon8_user_male_52_grey;
			s90.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s91_MouseClick(object sender, MouseEventArgs e)
		{
			s91.Image = Properties.Resources.icons8_user_male_52_green;
			s91.SizeMode = PictureBoxSizeMode.StretchImage;
			s91.Tag = "Checked";
		}

		private void s91_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s91.Image = Properties.Resources.icon8_user_male_52_grey;
			s91.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s92_MouseClick(object sender, MouseEventArgs e)
		{
			s92.Image = Properties.Resources.icons8_user_male_52_green;
			s92.SizeMode = PictureBoxSizeMode.StretchImage;
			s92.Tag = "Checked";
		}

		private void s92_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s92.Image = Properties.Resources.icon8_user_male_52_grey;
			s92.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s93_MouseClick(object sender, MouseEventArgs e)
		{
			s93.Image = Properties.Resources.icons8_user_male_52_green;
			s93.SizeMode = PictureBoxSizeMode.StretchImage;
			s93.Tag = "Checked";
		}

		private void s93_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s93.Image = Properties.Resources.icon8_user_male_52_grey;
			s93.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s94_MouseClick(object sender, MouseEventArgs e)
		{
			s94.Image = Properties.Resources.icons8_user_male_52_green;
			s94.SizeMode = PictureBoxSizeMode.StretchImage;
			s94.Tag = "Checked";
		}

		private void s94_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s94.Image = Properties.Resources.icon8_user_male_52_grey;
			s94.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s95_MouseClick(object sender, MouseEventArgs e)
		{
			s95.Image = Properties.Resources.icons8_user_male_52_green;
			s95.SizeMode = PictureBoxSizeMode.StretchImage;
			s95.Tag = "Checked";
		}

		private void s95_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s95.Image = Properties.Resources.icon8_user_male_52_grey;
			s95.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s96_MouseClick(object sender, MouseEventArgs e)
		{
			s96.Image = Properties.Resources.icons8_user_male_52_green;
			s96.SizeMode = PictureBoxSizeMode.StretchImage;
			s96.Tag = "Checked";
		}

		private void s96_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s96.Image = Properties.Resources.icon8_user_male_52_grey;
			s96.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s97_MouseClick(object sender, MouseEventArgs e)
		{
			s97.Image = Properties.Resources.icons8_user_male_52_green;
			s97.SizeMode = PictureBoxSizeMode.StretchImage;
			s97.Tag = "Checked";
		}

		private void s97_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s97.Image = Properties.Resources.icon8_user_male_52_grey;
			s97.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s98_MouseClick(object sender, MouseEventArgs e)
		{
			s98.Image = Properties.Resources.icons8_user_male_52_green;
			s98.SizeMode = PictureBoxSizeMode.StretchImage;
			s98.Tag = "Checked";
		}

		private void s98_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s98.Image = Properties.Resources.icon8_user_male_52_grey;
			s98.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s99_MouseClick(object sender, MouseEventArgs e)
		{
			s99.Image = Properties.Resources.icons8_user_male_52_green;
			s99.SizeMode = PictureBoxSizeMode.StretchImage;
			s99.Tag = "Checked";
		}

		private void s99_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s99.Image = Properties.Resources.icon8_user_male_52_grey;
			s99.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s100_MouseClick(object sender, MouseEventArgs e)
		{
			s100.Image = Properties.Resources.icons8_user_male_52_green;
			s100.SizeMode = PictureBoxSizeMode.StretchImage;
			s100.Tag = "Checked";
		}

		private void s100_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s100.Image = Properties.Resources.icon8_user_male_52_grey;
			s100.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s101_MouseClick(object sender, MouseEventArgs e)
		{
			s101.Image = Properties.Resources.icons8_user_male_52_green;
			s101.SizeMode = PictureBoxSizeMode.StretchImage;
			s101.Tag = "Checked";
		}

		private void s101_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s101.Image = Properties.Resources.icon8_user_male_52_grey;
			s101.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s102_MouseClick(object sender, MouseEventArgs e)
		{
			s102.Image = Properties.Resources.icons8_user_male_52_green;
			s102.SizeMode = PictureBoxSizeMode.StretchImage;
			s102.Tag = "Checked";
		}

		private void s102_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s102.Image = Properties.Resources.icon8_user_male_52_grey;
			s102.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s103_MouseClick(object sender, MouseEventArgs e)
		{
			s103.Image = Properties.Resources.icons8_user_male_52_green;
			s103.SizeMode = PictureBoxSizeMode.StretchImage;
			s103.Tag = "Checked";
		}

		private void s103_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s103.Image = Properties.Resources.icon8_user_male_52_grey;
			s103.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s104_MouseClick(object sender, MouseEventArgs e)
		{
			s104.Image = Properties.Resources.icons8_user_male_52_green;
			s104.SizeMode = PictureBoxSizeMode.StretchImage;
			s104.Tag = "Checked";
		}

		private void s104_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s104.Image = Properties.Resources.icon8_user_male_52_grey;
			s104.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s105_MouseClick(object sender, MouseEventArgs e)
		{
			s105.Image = Properties.Resources.icons8_user_male_52_green;
			s105.SizeMode = PictureBoxSizeMode.StretchImage;
			s105.Tag = "Checked";
		}

		private void s105_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s105.Image = Properties.Resources.icon8_user_male_52_grey;
			s105.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s106_MouseClick(object sender, MouseEventArgs e)
		{
			s106.Image = Properties.Resources.icons8_user_male_52_green;
			s106.SizeMode = PictureBoxSizeMode.StretchImage;
			s106.Tag = "Checked";
		}

		private void s106_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s106.Image = Properties.Resources.icon8_user_male_52_grey;
			s106.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s107_MouseClick(object sender, MouseEventArgs e)
		{
			s107.Image = Properties.Resources.icons8_user_male_52_green;
			s107.SizeMode = PictureBoxSizeMode.StretchImage;
			s107.Tag = "Checked";
		}

		private void s107_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s107.Image = Properties.Resources.icon8_user_male_52_grey;
			s107.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s108_MouseClick(object sender, MouseEventArgs e)
		{
			s108.Image = Properties.Resources.icons8_user_male_52_green;
			s108.SizeMode = PictureBoxSizeMode.StretchImage;
			s108.Tag = "Checked";
		}

		private void s108_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s108.Image = Properties.Resources.icon8_user_male_52_grey;
			s108.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s109_MouseClick(object sender, MouseEventArgs e)
		{
			s109.Image = Properties.Resources.icons8_user_male_52_green;
			s109.SizeMode = PictureBoxSizeMode.StretchImage;
			s109.Tag = "Checked";
		}

		private void s109_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s109.Image = Properties.Resources.icon8_user_male_52_grey;
			s109.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s110_MouseClick(object sender, MouseEventArgs e)
		{
			s110.Image = Properties.Resources.icons8_user_male_52_green;
			s110.SizeMode = PictureBoxSizeMode.StretchImage;
			s110.Tag = "Checked";
		}

		private void s110_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s110.Image = Properties.Resources.icon8_user_male_52_grey;
			s110.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s111_MouseClick(object sender, MouseEventArgs e)
		{
			s111.Image = Properties.Resources.icons8_user_male_52_green;
			s111.SizeMode = PictureBoxSizeMode.StretchImage;
			s111.Tag = "Checked";
		}

		private void s111_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s111.Image = Properties.Resources.icon8_user_male_52_grey;
			s111.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s112_MouseClick(object sender, MouseEventArgs e)
		{
			s112.Image = Properties.Resources.icons8_user_male_52_green;
			s112.SizeMode = PictureBoxSizeMode.StretchImage;
			s112.Tag = "Checked";
		}

		private void s112_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s112.Image = Properties.Resources.icon8_user_male_52_grey;
			s112.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s113_MouseClick(object sender, MouseEventArgs e)
		{
			s113.Image = Properties.Resources.icons8_user_male_52_green;
			s113.SizeMode = PictureBoxSizeMode.StretchImage;
			s113.Tag = "Checked";
		}

		private void s113_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s113.Image = Properties.Resources.icon8_user_male_52_grey;
			s113.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s114_MouseClick(object sender, MouseEventArgs e)
		{
			s114.Image = Properties.Resources.icons8_user_male_52_green;
			s114.SizeMode = PictureBoxSizeMode.StretchImage;
			s114.Tag = "Checked";
		}

		private void s114_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s114.Image = Properties.Resources.icon8_user_male_52_grey;
			s114.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s115_MouseClick(object sender, MouseEventArgs e)
		{
			s115.Image = Properties.Resources.icons8_user_male_52_green;
			s115.SizeMode = PictureBoxSizeMode.StretchImage;
			s115.Tag = "Checked";
		}

		private void s115_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s115.Image = Properties.Resources.icon8_user_male_52_grey;
			s115.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s116_MouseClick(object sender, MouseEventArgs e)
		{
			s116.Image = Properties.Resources.icons8_user_male_52_green;
			s116.SizeMode = PictureBoxSizeMode.StretchImage;
			s116.Tag = "Checked";
		}

		private void s116_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s116.Image = Properties.Resources.icon8_user_male_52_grey;
			s116.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s117_MouseClick(object sender, MouseEventArgs e)
		{
			s117.Image = Properties.Resources.icons8_user_male_52_green;
			s117.SizeMode = PictureBoxSizeMode.StretchImage;
			s117.Tag = "Checked";
		}

		private void s117_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s117.Image = Properties.Resources.icon8_user_male_52_grey;
			s117.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s118_MouseClick(object sender, MouseEventArgs e)
		{
			s118.Image = Properties.Resources.icons8_user_male_52_green;
			s118.SizeMode = PictureBoxSizeMode.StretchImage;
			s118.Tag = "Checked";
		}

		private void s118_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s118.Image = Properties.Resources.icon8_user_male_52_grey;
			s118.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s119_MouseClick(object sender, MouseEventArgs e)
		{
			s119.Image = Properties.Resources.icons8_user_male_52_green;
			s119.SizeMode = PictureBoxSizeMode.StretchImage;
			s119.Tag = "Checked";
		}

		private void s119_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s119.Image = Properties.Resources.icon8_user_male_52_grey;
			s119.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s120_MouseClick(object sender, MouseEventArgs e)
		{
			s120.Image = Properties.Resources.icons8_user_male_52_green;
			s120.SizeMode = PictureBoxSizeMode.StretchImage;
			s120.Tag = "Checked";
		}

		private void s120_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s120.Image = Properties.Resources.icon8_user_male_52_grey;
			s120.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s121_MouseClick(object sender, MouseEventArgs e)
		{
			s121.Image = Properties.Resources.icons8_user_male_52_green;
			s121.SizeMode = PictureBoxSizeMode.StretchImage;
			s121.Tag = "Checked";
		}

		private void s121_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s121.Image = Properties.Resources.icon8_user_male_52_grey;
			s121.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s122_MouseClick(object sender, MouseEventArgs e)
		{
			s122.Image = Properties.Resources.icons8_user_male_52_green;
			s122.SizeMode = PictureBoxSizeMode.StretchImage;
			s122.Tag = "Checked";
		}

		private void s122_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s122.Image = Properties.Resources.icon8_user_male_52_grey;
			s122.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s123_MouseClick(object sender, MouseEventArgs e)
		{
			s123.Image = Properties.Resources.icons8_user_male_52_green;
			s123.SizeMode = PictureBoxSizeMode.StretchImage;
			s123.Tag = "Checked";
		}

		private void s123_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s123.Image = Properties.Resources.icon8_user_male_52_grey;
			s123.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s124_MouseClick(object sender, MouseEventArgs e)
		{
			s124.Image = Properties.Resources.icons8_user_male_52_green;
			s124.SizeMode = PictureBoxSizeMode.StretchImage;
			s124.Tag = "Checked";
		}

		private void s124_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s124.Image = Properties.Resources.icon8_user_male_52_grey;
			s124.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s125_MouseClick(object sender, MouseEventArgs e)
		{
			s125.Image = Properties.Resources.icons8_user_male_52_green;
			s125.SizeMode = PictureBoxSizeMode.StretchImage;
			s125.Tag = "Checked";
		}

		private void s125_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s125.Image = Properties.Resources.icon8_user_male_52_grey;
			s125.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s126_MouseClick(object sender, MouseEventArgs e)
		{
			s126.Image = Properties.Resources.icons8_user_male_52_green;
			s126.SizeMode = PictureBoxSizeMode.StretchImage;
			s126.Tag = "Checked";
		}

		private void s126_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s126.Image = Properties.Resources.icon8_user_male_52_grey;
			s126.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s127_MouseClick(object sender, MouseEventArgs e)
		{
			s127.Image = Properties.Resources.icons8_user_male_52_green;
			s127.SizeMode = PictureBoxSizeMode.StretchImage;
			s127.Tag = "Checked";
		}

		private void s127_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s127.Image = Properties.Resources.icon8_user_male_52_grey;
			s127.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s128_MouseClick(object sender, MouseEventArgs e)
		{
			s128.Image = Properties.Resources.icons8_user_male_52_green;
			s128.SizeMode = PictureBoxSizeMode.StretchImage;
			s128.Tag = "Checked";
		}

		private void s128_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s128.Image = Properties.Resources.icon8_user_male_52_grey;
			s128.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s129_MouseClick(object sender, MouseEventArgs e)
		{
			s129.Image = Properties.Resources.icons8_user_male_52_green;
			s129.SizeMode = PictureBoxSizeMode.StretchImage;
			s129.Tag = "Checked";
		}

		private void s129_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s129.Image = Properties.Resources.icon8_user_male_52_grey;
			s129.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s130_MouseClick(object sender, MouseEventArgs e)
		{
			s130.Image = Properties.Resources.icons8_user_male_52_green;
			s130.SizeMode = PictureBoxSizeMode.StretchImage;
			s130.Tag = "Checked";
		}

		private void s130_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s130.Image = Properties.Resources.icon8_user_male_52_grey;
			s130.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s131_MouseClick(object sender, MouseEventArgs e)
		{
			s131.Image = Properties.Resources.icons8_user_male_52_green;
			s131.SizeMode = PictureBoxSizeMode.StretchImage;
			s131.Tag = "Checked";
		}

		private void s131_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s131.Image = Properties.Resources.icon8_user_male_52_grey;
			s131.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s132_MouseClick(object sender, MouseEventArgs e)
		{
			s132.Image = Properties.Resources.icons8_user_male_52_green;
			s132.SizeMode = PictureBoxSizeMode.StretchImage;
			s132.Tag = "Checked";
		}

		private void s132_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s132.Image = Properties.Resources.icon8_user_male_52_grey;
			s132.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s133_MouseClick(object sender, MouseEventArgs e)
		{
			s133.Image = Properties.Resources.icons8_user_male_52_green;
			s133.SizeMode = PictureBoxSizeMode.StretchImage;
			s133.Tag = "Checked";
		}

		private void s133_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s133.Image = Properties.Resources.icon8_user_male_52_grey;
			s133.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s134_MouseClick(object sender, MouseEventArgs e)
		{
			s134.Image = Properties.Resources.icons8_user_male_52_green;
			s134.SizeMode = PictureBoxSizeMode.StretchImage;
			s134.Tag = "Checked";
		}

		private void s134_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s134.Image = Properties.Resources.icon8_user_male_52_grey;
			s134.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s135_MouseClick(object sender, MouseEventArgs e)
		{
			s135.Image = Properties.Resources.icons8_user_male_52_green;
			s135.SizeMode = PictureBoxSizeMode.StretchImage;
			s135.Tag = "Checked";
		}

		private void s135_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s135.Image = Properties.Resources.icon8_user_male_52_grey;
			s135.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s136_MouseClick(object sender, MouseEventArgs e)
		{
			s136.Image = Properties.Resources.icons8_user_male_52_green;
			s136.SizeMode = PictureBoxSizeMode.StretchImage;
			s136.Tag = "Checked";
		}

		private void s136_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s136.Image = Properties.Resources.icon8_user_male_52_grey;
			s136.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s137_MouseClick(object sender, MouseEventArgs e)
		{
			s137.Image = Properties.Resources.icons8_user_male_52_green;
			s137.SizeMode = PictureBoxSizeMode.StretchImage;
			s137.Tag = "Checked";
		}

		private void s137_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s137.Image = Properties.Resources.icon8_user_male_52_grey;
			s137.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s138_MouseClick(object sender, MouseEventArgs e)
		{
			s138.Image = Properties.Resources.icons8_user_male_52_green;
			s138.SizeMode = PictureBoxSizeMode.StretchImage;
			s138.Tag = "Checked";
		}

		private void s138_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s138.Image = Properties.Resources.icon8_user_male_52_grey;
			s138.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s139_MouseClick(object sender, MouseEventArgs e)
		{
			s139.Image = Properties.Resources.icons8_user_male_52_green;
			s139.SizeMode = PictureBoxSizeMode.StretchImage;
			s139.Tag = "Checked";
		}

		private void s139_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s139.Image = Properties.Resources.icon8_user_male_52_grey;
			s139.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s140_MouseClick(object sender, MouseEventArgs e)
		{
			s140.Image = Properties.Resources.icons8_user_male_52_green;
			s140.SizeMode = PictureBoxSizeMode.StretchImage;
			s140.Tag = "Checked";
		}

		private void s140_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s140.Image = Properties.Resources.icon8_user_male_52_grey;
			s140.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s141_MouseClick(object sender, MouseEventArgs e)
		{
			s141.Image = Properties.Resources.icons8_user_male_52_green;
			s141.SizeMode = PictureBoxSizeMode.StretchImage;
			s141.Tag = "Checked";
		}

		private void s141_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s141.Image = Properties.Resources.icon8_user_male_52_grey;
			s141.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s142_MouseClick(object sender, MouseEventArgs e)
		{
			s142.Image = Properties.Resources.icons8_user_male_52_green;
			s142.SizeMode = PictureBoxSizeMode.StretchImage;
			s142.Tag = "Checked";
		}

		private void s142_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s142.Image = Properties.Resources.icon8_user_male_52_grey;
			s142.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s143_MouseClick(object sender, MouseEventArgs e)
		{
			s143.Image = Properties.Resources.icons8_user_male_52_green;
			s143.SizeMode = PictureBoxSizeMode.StretchImage;
			s143.Tag = "Checked";
		}

		private void s143_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s143.Image = Properties.Resources.icon8_user_male_52_grey;
			s143.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s144_MouseClick(object sender, MouseEventArgs e)
		{
			s144.Image = Properties.Resources.icons8_user_male_52_green;
			s144.SizeMode = PictureBoxSizeMode.StretchImage;
			s144.Tag = "Checked";
		}

		private void s144_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s144.Image = Properties.Resources.icon8_user_male_52_grey;
			s144.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s145_MouseClick(object sender, MouseEventArgs e)
		{
			s145.Image = Properties.Resources.icons8_user_male_52_green;
			s145.SizeMode = PictureBoxSizeMode.StretchImage;
			s145.Tag = "Checked";
		}

		private void s145_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s145.Image = Properties.Resources.icon8_user_male_52_grey;
			s145.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s146_MouseClick(object sender, MouseEventArgs e)
		{
			s146.Image = Properties.Resources.icons8_user_male_52_green;
			s146.SizeMode = PictureBoxSizeMode.StretchImage;
			s146.Tag = "Checked";
		}

		private void s146_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s146.Image = Properties.Resources.icon8_user_male_52_grey;
			s146.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s147_MouseClick(object sender, MouseEventArgs e)
		{
			s147.Image = Properties.Resources.icons8_user_male_52_green;
			s147.SizeMode = PictureBoxSizeMode.StretchImage;
			s147.Tag = "Checked";
		}

		private void s147_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s147.Image = Properties.Resources.icon8_user_male_52_grey;
			s147.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s148_MouseClick(object sender, MouseEventArgs e)
		{
			s148.Image = Properties.Resources.icons8_user_male_52_green;
			s148.SizeMode = PictureBoxSizeMode.StretchImage;
			s148.Tag = "Checked";
		}

		private void s148_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s148.Image = Properties.Resources.icon8_user_male_52_grey;
			s148.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s149_MouseClick(object sender, MouseEventArgs e)
		{
			s149.Image = Properties.Resources.icons8_user_male_52_green;
			s149.SizeMode = PictureBoxSizeMode.StretchImage;
			s149.Tag = "Checked";
		}

		private void s149_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s149.Image = Properties.Resources.icon8_user_male_52_grey;
			s149.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s150_MouseClick(object sender, MouseEventArgs e)
		{
			s150.Image = Properties.Resources.icons8_user_male_52_green;
			s150.SizeMode = PictureBoxSizeMode.StretchImage;
			s150.Tag = "Checked";
		}

		private void s150_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s150.Image = Properties.Resources.icon8_user_male_52_grey;
			s150.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s151_MouseClick(object sender, MouseEventArgs e)
		{
			s151.Image = Properties.Resources.icons8_user_male_52_green;
			s151.SizeMode = PictureBoxSizeMode.StretchImage;
			s151.Tag = "Checked";
		}

		private void s151_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s151.Image = Properties.Resources.icon8_user_male_52_grey;
			s151.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s152_MouseClick(object sender, MouseEventArgs e)
		{
			s152.Image = Properties.Resources.icons8_user_male_52_green;
			s152.SizeMode = PictureBoxSizeMode.StretchImage;
			s152.Tag = "Checked";
		}

		private void s152_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s152.Image = Properties.Resources.icon8_user_male_52_grey;
			s152.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s153_MouseClick(object sender, MouseEventArgs e)
		{
			s153.Image = Properties.Resources.icons8_user_male_52_green;
			s153.SizeMode = PictureBoxSizeMode.StretchImage;
			s153.Tag = "Checked";
		}

		private void s153_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s153.Image = Properties.Resources.icon8_user_male_52_grey;
			s153.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s154_MouseClick(object sender, MouseEventArgs e)
		{
			s154.Image = Properties.Resources.icons8_user_male_52_green;
			s154.SizeMode = PictureBoxSizeMode.StretchImage;
			s154.Tag = "Checked";
		}

		private void s154_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s154.Image = Properties.Resources.icon8_user_male_52_grey;
			s154.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s155_MouseClick(object sender, MouseEventArgs e)
		{
			s155.Image = Properties.Resources.icons8_user_male_52_green;
			s155.SizeMode = PictureBoxSizeMode.StretchImage;
			s155.Tag = "Checked";
		}

		private void s155_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s155.Image = Properties.Resources.icon8_user_male_52_grey;
			s155.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s156_MouseClick(object sender, MouseEventArgs e)
		{
			s156.Image = Properties.Resources.icons8_user_male_52_green;
			s156.SizeMode = PictureBoxSizeMode.StretchImage;
			s156.Tag = "Checked";
		}

		private void s156_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s156.Image = Properties.Resources.icon8_user_male_52_grey;
			s156.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s157_MouseClick(object sender, MouseEventArgs e)
		{
			s157.Image = Properties.Resources.icons8_user_male_52_green;
			s157.SizeMode = PictureBoxSizeMode.StretchImage;
			s157.Tag = "Checked";
		}

		private void s157_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s157.Image = Properties.Resources.icon8_user_male_52_grey;
			s157.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s158_MouseClick(object sender, MouseEventArgs e)
		{
			s158.Image = Properties.Resources.icons8_user_male_52_green;
			s158.SizeMode = PictureBoxSizeMode.StretchImage;
			s158.Tag = "Checked";
		}

		private void s158_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s158.Image = Properties.Resources.icon8_user_male_52_grey;
			s158.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s159_MouseClick(object sender, MouseEventArgs e)
		{
			s159.Image = Properties.Resources.icons8_user_male_52_green;
			s159.SizeMode = PictureBoxSizeMode.StretchImage;
			s159.Tag = "Checked";
		}

		private void s159_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s159.Image = Properties.Resources.icon8_user_male_52_grey;
			s159.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s160_MouseClick(object sender, MouseEventArgs e)
		{
			s160.Image = Properties.Resources.icons8_user_male_52_green;
			s160.SizeMode = PictureBoxSizeMode.StretchImage;
			s160.Tag = "Checked";
		}

		private void s160_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s160.Image = Properties.Resources.icon8_user_male_52_grey;
			s160.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s161_MouseClick(object sender, MouseEventArgs e)
		{
			s161.Image = Properties.Resources.icons8_user_male_52_green;
			s161.SizeMode = PictureBoxSizeMode.StretchImage;
			s161.Tag = "Checked";
		}

		private void s161_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s161.Image = Properties.Resources.icon8_user_male_52_grey;
			s161.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s162_MouseClick(object sender, MouseEventArgs e)
		{
			s162.Image = Properties.Resources.icons8_user_male_52_green;
			s162.SizeMode = PictureBoxSizeMode.StretchImage;
			s162.Tag = "Checked";
		}

		private void s162_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s162.Image = Properties.Resources.icon8_user_male_52_grey;
			s162.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s163_MouseClick(object sender, MouseEventArgs e)
		{
			s163.Image = Properties.Resources.icons8_user_male_52_green;
			s163.SizeMode = PictureBoxSizeMode.StretchImage;
			s163.Tag = "Checked";
		}

		private void s163_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s163.Image = Properties.Resources.icon8_user_male_52_grey;
			s163.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s164_MouseClick(object sender, MouseEventArgs e)
		{
			s164.Image = Properties.Resources.icons8_user_male_52_green;
			s164.SizeMode = PictureBoxSizeMode.StretchImage;
			s164.Tag = "Checked";
		}

		private void s164_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s164.Image = Properties.Resources.icon8_user_male_52_grey;
			s164.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s165_MouseClick(object sender, MouseEventArgs e)
		{
			s165.Image = Properties.Resources.icons8_user_male_52_green;
			s165.SizeMode = PictureBoxSizeMode.StretchImage;
			s165.Tag = "Checked";
		}

		private void s165_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s165.Image = Properties.Resources.icon8_user_male_52_grey;
			s165.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s166_MouseClick(object sender, MouseEventArgs e)
		{
			s166.Image = Properties.Resources.icons8_user_male_52_green;
			s166.SizeMode = PictureBoxSizeMode.StretchImage;
			s166.Tag = "Checked";
		}

		private void s166_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s166.Image = Properties.Resources.icon8_user_male_52_grey;
			s166.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s167_MouseClick(object sender, MouseEventArgs e)
		{
			s167.Image = Properties.Resources.icons8_user_male_52_green;
			s167.SizeMode = PictureBoxSizeMode.StretchImage;
			s167.Tag = "Checked";
		}

		private void s167_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s167.Image = Properties.Resources.icon8_user_male_52_grey;
			s167.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s168_MouseClick(object sender, MouseEventArgs e)
		{
			s168.Image = Properties.Resources.icons8_user_male_52_green;
			s168.SizeMode = PictureBoxSizeMode.StretchImage;
			s186.Tag = "Checked";
		}

		private void s168_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s168.Image = Properties.Resources.icon8_user_male_52_grey;
			s168.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s169_MouseClick(object sender, MouseEventArgs e)
		{
			s169.Image = Properties.Resources.icons8_user_male_52_green;
			s169.SizeMode = PictureBoxSizeMode.StretchImage;
			s169.Tag = "Checked";
		}

		private void s169_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s169.Image = Properties.Resources.icon8_user_male_52_grey;
			s169.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s170_MouseClick(object sender, MouseEventArgs e)
		{
			s170.Image = Properties.Resources.icons8_user_male_52_green;
			s170.SizeMode = PictureBoxSizeMode.StretchImage;
			s170.Tag = "Checked";
		}

		private void s170_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s170.Image = Properties.Resources.icon8_user_male_52_grey;
			s170.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s171_MouseClick(object sender, MouseEventArgs e)
		{
			s171.Image = Properties.Resources.icons8_user_male_52_green;
			s171.SizeMode = PictureBoxSizeMode.StretchImage;
			s171.Tag = "Checked";
		}

		private void s171_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s171.Image = Properties.Resources.icon8_user_male_52_grey;
			s171.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s172_MouseClick(object sender, MouseEventArgs e)
		{
			s172.Image = Properties.Resources.icons8_user_male_52_green;
			s172.SizeMode = PictureBoxSizeMode.StretchImage;
			s172.Tag = "Checked";
		}

		private void s172_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s172.Image = Properties.Resources.icon8_user_male_52_grey;
			s172.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s173_MouseClick(object sender, MouseEventArgs e)
		{
			s173.Image = Properties.Resources.icons8_user_male_52_green;
			s173.SizeMode = PictureBoxSizeMode.StretchImage;
			s173.Tag = "Checked";
		}

		private void s173_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s173.Image = Properties.Resources.icon8_user_male_52_grey;
			s173.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s174_MouseClick(object sender, MouseEventArgs e)
		{
			s174.Image = Properties.Resources.icons8_user_male_52_green;
			s174.SizeMode = PictureBoxSizeMode.StretchImage;
			s174.Tag = "Checked";
		}

		private void s174_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s174.Image = Properties.Resources.icon8_user_male_52_grey;
			s174.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s175_MouseClick(object sender, MouseEventArgs e)
		{
			s175.Image = Properties.Resources.icons8_user_male_52_green;
			s175.SizeMode = PictureBoxSizeMode.StretchImage;
			s175.Tag = "Checked";
		}

		private void s175_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s175.Image = Properties.Resources.icon8_user_male_52_grey;
			s175.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s176_MouseClick(object sender, MouseEventArgs e)
		{
			s176.Image = Properties.Resources.icons8_user_male_52_green;
			s176.SizeMode = PictureBoxSizeMode.StretchImage;
			s176.Tag = "Checked";
		}

		private void s176_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s176.Image = Properties.Resources.icon8_user_male_52_grey;
			s176.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s177_MouseClick(object sender, MouseEventArgs e)
		{
			s177.Image = Properties.Resources.icons8_user_male_52_green;
			s177.SizeMode = PictureBoxSizeMode.StretchImage;
			s177.Tag = "Checked";
		}

		private void s177_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s177.Image = Properties.Resources.icon8_user_male_52_grey;
			s177.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s178_MouseClick(object sender, MouseEventArgs e)
		{
			s178.Image = Properties.Resources.icons8_user_male_52_green;
			s178.SizeMode = PictureBoxSizeMode.StretchImage;
			s178.Tag = "Checked";
		}

		private void s178_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s178.Image = Properties.Resources.icon8_user_male_52_grey;
			s178.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s179_MouseClick(object sender, MouseEventArgs e)
		{
			s179.Image = Properties.Resources.icons8_user_male_52_green;
			s179.SizeMode = PictureBoxSizeMode.StretchImage;
			s179.Tag = "Checked";
		}

		private void s179_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s179.Image = Properties.Resources.icon8_user_male_52_grey;
			s179.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s180_MouseClick(object sender, MouseEventArgs e)
		{
			s180.Image = Properties.Resources.icons8_user_male_52_green;
			s180.SizeMode = PictureBoxSizeMode.StretchImage;
			s180.Tag = "Checked";
		}

		private void s180_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s180.Image = Properties.Resources.icon8_user_male_52_grey;
			s180.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s181_MouseClick(object sender, MouseEventArgs e)
		{
			s181.Image = Properties.Resources.icons8_user_male_52_green;
			s181.SizeMode = PictureBoxSizeMode.StretchImage;
			s181.Tag = "Checked";
		}

		private void s181_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s181.Image = Properties.Resources.icon8_user_male_52_grey;
			s181.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s182_MouseClick(object sender, MouseEventArgs e)
		{
			s182.Image = Properties.Resources.icons8_user_male_52_green;
			s182.SizeMode = PictureBoxSizeMode.StretchImage;
			s182.Tag = "Checked";
		}

		private void s182_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s182.Image = Properties.Resources.icon8_user_male_52_grey;
			s182.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s183_MouseClick(object sender, MouseEventArgs e)
		{
			s183.Image = Properties.Resources.icons8_user_male_52_green;
			s183.SizeMode = PictureBoxSizeMode.StretchImage;
			s183.Tag = "Checked";
		}

		private void s183_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s183.Image = Properties.Resources.icon8_user_male_52_grey;
			s183.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s184_MouseClick(object sender, MouseEventArgs e)
		{
			s184.Image = Properties.Resources.icons8_user_male_52_green;
			s184.SizeMode = PictureBoxSizeMode.StretchImage;
			s184.Tag = "Checked";
		}

		private void s184_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s184.Image = Properties.Resources.icon8_user_male_52_grey;
			s184.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s185_MouseClick(object sender, MouseEventArgs e)
		{
			s185.Image = Properties.Resources.icons8_user_male_52_green;
			s185.SizeMode = PictureBoxSizeMode.StretchImage;
			s185.Tag = "Checked";
		}

		private void s185_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s185.Image = Properties.Resources.icon8_user_male_52_grey;
			s185.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s186_MouseClick(object sender, MouseEventArgs e)
		{
			s186.Image = Properties.Resources.icons8_user_male_52_green;
			s186.SizeMode = PictureBoxSizeMode.StretchImage;
			s186.Tag = "Checked";
		}

		private void s186_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s186.Image = Properties.Resources.icon8_user_male_52_grey;
			s186.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s187_MouseClick(object sender, MouseEventArgs e)
		{
			s187.Image = Properties.Resources.icons8_user_male_52_green;
			s187.SizeMode = PictureBoxSizeMode.StretchImage;
			s187.Tag = "Checked";
		}

		private void s187_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s187.Image = Properties.Resources.icon8_user_male_52_grey;
			s187.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s188_MouseClick(object sender, MouseEventArgs e)
		{
			s188.Image = Properties.Resources.icons8_user_male_52_green;
			s188.SizeMode = PictureBoxSizeMode.StretchImage;
			s188.Tag = "Checked";
		}

		private void s188_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s188.Image = Properties.Resources.icon8_user_male_52_grey;
			s188.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s189_MouseClick(object sender, MouseEventArgs e)
		{
			s189.Image = Properties.Resources.icons8_user_male_52_green;
			s189.SizeMode = PictureBoxSizeMode.StretchImage;
			s189.Tag = "Checked";
		}

		private void s189_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s189.Image = Properties.Resources.icon8_user_male_52_grey;
			s189.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s190_MouseClick(object sender, MouseEventArgs e)
		{
			s190.Image = Properties.Resources.icons8_user_male_52_green;
			s190.SizeMode = PictureBoxSizeMode.StretchImage;
			s190.Tag = "Checked";
		}

		private void s190_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s190.Image = Properties.Resources.icon8_user_male_52_grey;
			s190.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s191_MouseClick(object sender, MouseEventArgs e)
		{
			s191.Image = Properties.Resources.icons8_user_male_52_green;
			s191.SizeMode = PictureBoxSizeMode.StretchImage;
			s191.Tag = "Checked";
		}

		private void s191_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s191.Image = Properties.Resources.icon8_user_male_52_grey;
			s191.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s192_MouseClick(object sender, MouseEventArgs e)
		{
			s192.Image = Properties.Resources.icons8_user_male_52_green;
			s192.SizeMode = PictureBoxSizeMode.StretchImage;
			s192.Tag = "Checked";
		}

		private void s192_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s192.Image = Properties.Resources.icon8_user_male_52_grey;
			s192.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s193_MouseClick(object sender, MouseEventArgs e)
		{
			s193.Image = Properties.Resources.icons8_user_male_52_green;
			s193.SizeMode = PictureBoxSizeMode.StretchImage;
			s193.Tag = "Checked";
		}

		private void s193_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s193.Image = Properties.Resources.icon8_user_male_52_grey;
			s193.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s194_MouseClick(object sender, MouseEventArgs e)
		{
			s194.Image = Properties.Resources.icons8_user_male_52_green;
			s194.SizeMode = PictureBoxSizeMode.StretchImage;
			s194.Tag = "Checked";
		}

		private void s194_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s194.Image = Properties.Resources.icon8_user_male_52_grey;
			s194.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s195_MouseClick(object sender, MouseEventArgs e)
		{
			s195.Image = Properties.Resources.icons8_user_male_52_green;
			s195.SizeMode = PictureBoxSizeMode.StretchImage;
			s195.Tag = "Checked";
		}

		private void s195_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s195.Image = Properties.Resources.icon8_user_male_52_grey;
			s195.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s196_MouseClick(object sender, MouseEventArgs e)
		{
			s196.Image = Properties.Resources.icons8_user_male_52_green;
			s196.SizeMode = PictureBoxSizeMode.StretchImage;
			s196.Tag = "Checked";
		}

		private void s196_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s196.Image = Properties.Resources.icon8_user_male_52_grey;
			s196.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s197_MouseClick(object sender, MouseEventArgs e)
		{
			s197.Image = Properties.Resources.icons8_user_male_52_green;
			s197.SizeMode = PictureBoxSizeMode.StretchImage;
			s197.Tag = "Checked";
		}

		private void s197_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s197.Image = Properties.Resources.icon8_user_male_52_grey;
			s197.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s198_MouseClick(object sender, MouseEventArgs e)
		{
			s198.Image = Properties.Resources.icons8_user_male_52_green;
			s198.SizeMode = PictureBoxSizeMode.StretchImage;
			s198.Tag = "Checked";
		}

		private void s198_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s198.Image = Properties.Resources.icon8_user_male_52_grey;
			s198.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s199_MouseClick(object sender, MouseEventArgs e)
		{
			s199.Image = Properties.Resources.icons8_user_male_52_green;
			s199.SizeMode = PictureBoxSizeMode.StretchImage;
			s199.Tag = "Checked";
		}

		private void s199_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s199.Image = Properties.Resources.icon8_user_male_52_grey;
			s199.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void s200_MouseClick(object sender, MouseEventArgs e)
		{
			s200.Image = Properties.Resources.icons8_user_male_52_green;
			s200.SizeMode = PictureBoxSizeMode.StretchImage;
			s200.Tag = "Checked";
		}

		private void s200_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			s200.Image = Properties.Resources.icon8_user_male_52_grey;
			s200.SizeMode = PictureBoxSizeMode.StretchImage;
		}
	}
}
