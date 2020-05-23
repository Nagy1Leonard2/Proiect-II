using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace LogIn
{
	public partial class UserControl_DateTime : UserControl
	{
		public UserControl_DateTime()
		{
			InitializeComponent();
		}

		public void DisplayMovies()
		{
			List<string> st = new List<string>();

			// Database Connection:
			SqlConnection cnn = new SqlConnection(Program.DB_ConnectionString_Booking());
			cnn.Open();


			// Select statement: 
			SqlCommand command0 = new SqlCommand("Select m.Title from Movies m, ScheduledMovies s where s.Date like @today and s.MovieID = m.Id", cnn);
			SqlParameter today = new SqlParameter();
			today.ParameterName = "@today";
			command0.Parameters.AddWithValue("@today", UserControl_Search_DateTime.dt.ToString("yyyy-MM-dd"));

			//MessageBox.Show(DateTime.Now.ToString("yyyy-MM-dd"));
			using (command0)
			{
				SqlDataReader dR = command0.ExecuteReader();
				using (dR)
				{
					while (dR.Read())
					{
						st.Add(dR["Title"].ToString());
					}
				}
			}

			//Close connections and dispose commands:
			command0.Dispose();
			cnn.Close();



			// Display Movies logic:

			List<Label> labels = new List<Label>();
			// Movie title:
			int x = 11;
			int y = 200;
			int pos = 1;

			// For page number:
			int total = st.Count;
			int nrPages;
			int nr_fullPages = total / 8;
			int nr_notFullPages = total % 8;
			if (nr_notFullPages != 0)
				nrPages = nr_fullPages + 1;
			else
				nrPages = nr_fullPages;

			label11.Text = nrPages.ToString();
			foreach (var title in st)
			{
				switch (pos)
				{
					// 1st row:
					case 1:
						ShowTitle(labels, title, x, y);
						pos++;
						break;
					case 2:
						ShowTitle(labels, title, x + 125, y);
						pos++;
						break;
					case 3:
						ShowTitle(labels, title, x + 252, y);
						pos++;
						break;
					case 4:
						ShowTitle(labels, title, x + 378, y);
						pos++;
						break;

					// 2nd row:
					case 5:
						ShowTitle(labels, title, x, y + 186);
						pos++;
						break;
					case 6:
						ShowTitle(labels, title, x + 125, y + 186);
						pos++;
						break;
					case 7:
						ShowTitle(labels, title, x + 252, y + 186);
						pos++;
						break;
					case 8:
						ShowTitle(labels, title, x + 378, y + 186);
						pos++;
						break;
					default:
						pos = 1;
						break;
				}
			}
		}

		public void ShowTitle(List<Label> labels, string title, int x, int y)
		{
			var temp = new Label
			{
				Location = new Point(x, y),
				Text = title,
				BackColor = Color.Black,
				ForeColor = Color.White,
				Font = new Font("Calibri", 12),
				Height = 24,
				Width = 47,
				MaximumSize = new Size(125, 0),
				AutoSize = true
			};
			this.Controls.Add(temp);
			temp.Show();
			labels.Add(temp);
		}
	}
}
