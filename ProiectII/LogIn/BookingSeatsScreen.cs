using System;
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

			s1.BackColor = Color.Red;
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

		public void BookedSeats()
		{
			// Database Connection:
			SqlConnection cnn = new SqlConnection(Program.DB_ConnectionString_Booking());
			cnn.Open();


			// Select statement: 
			SqlCommand command0 = new SqlCommand("Select Title from Movies where RatingIMDB > @min and RatingIMDB < @max order by RatingIMDB", cnn);
			SqlParameter min = new SqlParameter();
			SqlParameter max = new SqlParameter();
			min.ParameterName = "@min";
			max.ParameterName = "@max";
			command0.Parameters.AddWithValue("@min", UserControl_Search_Rating.Min);
			command0.Parameters.AddWithValue("@max", UserControl_Search_Rating.Max);
			using (command0)
			{
				SqlDataReader dR = command0.ExecuteReader();
				using (dR)
				{
					while (dR.Read())
					{
						MessageBox.Show(dR["Title"].ToString());
					}
				}
			}

			//Close connections and dispose commands:
			command0.Dispose();
			cnn.Close();
		}
	}
}
