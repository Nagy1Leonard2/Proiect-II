using System;
using System.Windows.Forms;

namespace LogIn
{
	public partial class Home : Form
	{
		public Home()
		{
			InitializeComponent();

			// Logic for the Admin version:
			inde = Login.myString;
			if (inde == "1")
			{
				label4.Show();
				label5.Show();
			}
			else
			{
				label4.Hide();
				label5.Hide();
			}
		}

		private void OnClick(object sender, EventArgs e)
		{
			// Display userControls logic:
			if (sender is Button)
			{
				Button btn = sender as Button;
				if (btn.Parent == userControl_Search_Title1)
				{
					userControl_Title1.Show();
					userControl_Title1.BringToFront();

					userControl_Rating1.Hide();
					userControl_Main1.Hide();
					userControl_Genre1.Hide();
					userControl_DateTime1.Hide();
					userControl_Search_Rating1.Hide();
					userControl_Search_Title1.Hide();
					userControl_Search_Genre1.Hide();
					userControl_Search_DateTime1.Hide();
					add_Movie1.Hide();
					manage_Users1.Hide();
				}
				else if (btn.Parent == userControl_Search_DateTime1)
				{
					userControl_DateTime1.Show();
					userControl_DateTime1.BringToFront();

					userControl_Rating1.Hide();
					userControl_Main1.Hide();
					userControl_Genre1.Hide();
					userControl_Title1.Hide();
					userControl_Search_Rating1.Hide();
					userControl_Search_Title1.Hide();
					userControl_Search_Genre1.Hide();
					userControl_Search_DateTime1.Hide();
					add_Movie1.Hide();
					manage_Users1.Hide();
				}
				else if (btn.Parent == userControl_Search_Genre1)
				{
					userControl_Genre1.Show();
					userControl_Genre1.BringToFront();

					userControl_Rating1.Hide();
					userControl_Main1.Hide();
					userControl_DateTime1.Hide();
					userControl_Title1.Hide();
					userControl_Search_Rating1.Hide();
					userControl_Search_Title1.Hide();
					userControl_Search_Genre1.Hide();
					userControl_Search_DateTime1.Hide();
					add_Movie1.Hide();
					manage_Users1.Hide();

					userControl_Genre1.DisplayMovies();
				}
				else if (btn.Parent == userControl_Search_Rating1)
				{
					userControl_Rating1.Show();
					userControl_Rating1.BringToFront();

					userControl_DateTime1.Hide();
					userControl_Main1.Hide();
					userControl_Genre1.Hide();
					userControl_Title1.Hide();
					userControl_Search_Rating1.Hide();
					userControl_Search_Title1.Hide();
					userControl_Search_Genre1.Hide();
					userControl_Search_DateTime1.Hide();
					add_Movie1.Hide();
					manage_Users1.Hide();

					userControl_Rating1.DisplayMovies();
				}
			}
		}

		public static string qt = "";
		public static string inde = "";

		private void Home_Load(object sender, EventArgs e)
		{
			// Show the Main userControl:
			userControl_Main1.Show();
			userControl_Main1.BringToFront();

			userControl_DateTime1.Hide();
			userControl_Genre1.Hide();
			userControl_Rating1.Hide();
			userControl_Title1.Hide();
			userControl_Search_Rating1.Hide();
			userControl_Search_Title1.Hide();
			userControl_Search_Genre1.Hide();
			userControl_Search_DateTime1.Hide();
			add_Movie1.Hide();
			manage_Users1.Hide();
		}
		
		private void Label1_Click(object sender, EventArgs e)
		{
			// Close the form on X_label click:
			Close();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			// Search by Title Screen:
			qt = "title";

			userControl_Search_Title1.Show();
			userControl_Search_Title1.BringToFront();

			userControl_Search_DateTime1.Hide();
			userControl_Search_Genre1.Hide();
			userControl_Search_Rating1.Hide();
			userControl_Main1.Hide();
			userControl_DateTime1.Hide();
			userControl_Genre1.Hide();
			userControl_Rating1.Hide();
			userControl_Title1.Hide();
			add_Movie1.Hide();
			manage_Users1.Hide();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			// Search by Date and Time Screen:
			qt = "date";

			userControl_Search_DateTime1.Show();
			userControl_Search_DateTime1.BringToFront();
			userControl_Main1.Hide();
			userControl_DateTime1.Hide();
			userControl_Genre1.Hide();
			userControl_Rating1.Hide();
			userControl_Title1.Hide();
			add_Movie1.Hide();
			manage_Users1.Hide();
		}

		private void button4_Click(object sender, EventArgs e)
		{
			// Search by Genre Screen:
			qt = "genre";

			userControl_Search_Genre1.Show();
			userControl_Search_Genre1.BringToFront();
			userControl_Main1.Hide();
			userControl_DateTime1.Hide();
			userControl_Genre1.Hide();
			userControl_Rating1.Hide();
			userControl_Title1.Hide();
			add_Movie1.Hide();
			manage_Users1.Hide();
		}

		private void button5_Click(object sender, EventArgs e)
		{
			// Search by Rating Screen:
			qt = "rating";

			userControl_Search_Rating1.Show();
			userControl_Search_Rating1.BringToFront();

			userControl_Search_Title1.Hide();
			userControl_Search_Genre1.Hide();
			userControl_Search_DateTime1.Hide();
			userControl_Main1.Hide();
			userControl_DateTime1.Hide();
			userControl_Genre1.Hide();
			userControl_Rating1.Hide();
			userControl_Title1.Hide();
			add_Movie1.Hide();
			manage_Users1.Hide();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			// Show Home again:
			userControl_DateTime1.Hide();
			userControl_Genre1.Hide();
			userControl_Rating1.Hide();
			userControl_Title1.Hide();
			userControl_Search_Rating1.Hide();
			userControl_Search_Title1.Hide();
			userControl_Search_Genre1.Hide();
			userControl_Search_DateTime1.Hide();
			add_Movie1.Hide();
			manage_Users1.Hide();

			userControl_Main1.Show();
			userControl_Main1.BringToFront();
		}

		private void label2_Click(object sender, EventArgs e)
		{
			// Logout:
			Login x = new Login();
			this.Hide();
			x.ShowDialog();
			this.Close();
		}

		private void label4_Click(object sender, EventArgs e)
		{
			//Show Add Movies control
			userControl_DateTime1.Hide();
			userControl_Genre1.Hide();
			userControl_Rating1.Hide();
			userControl_Title1.Hide();
			userControl_Search_Rating1.Hide();
			userControl_Search_Title1.Hide();
			userControl_Search_Genre1.Hide();
			userControl_Search_DateTime1.Hide();
			userControl_Main1.Hide();
			manage_Users1.Hide();

			add_Movie1.Show();
			add_Movie1.BringToFront();
		}

		private void label5_Click(object sender, EventArgs e)
		{
			//Show Manage Users control
			userControl_DateTime1.Hide();
			userControl_Genre1.Hide();
			userControl_Rating1.Hide();
			userControl_Title1.Hide();
			userControl_Search_Rating1.Hide();
			userControl_Search_Title1.Hide();
			userControl_Search_Genre1.Hide();
			userControl_Search_DateTime1.Hide();
			userControl_Main1.Hide();
			add_Movie1.Hide();

			manage_Users1.Show();
			manage_Users1.BringToFront();
		}
	}
}
