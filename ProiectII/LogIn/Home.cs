using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LogIn
{
	public partial class Home : Form
	{
		public Home()
		{
			InitializeComponent();
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
		//to do: panel backColor black
		//to do: logo in the top left corner
		//to do: left panel to be black
		//to do: design left panel buttons
		//to do: search icon transparent background
		//to do: logout function
		//to do: background black
		//to do: input box on search button press
		//to do: display data from db in userControls
		//to do: pagination (length from db)

		private void OnClick(object sender, EventArgs e)
		{
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
				}
			}
		}

		public static string qt = "";
		public static string inde = "";
		private void Home_Load(object sender, EventArgs e)
		{
			string connetionString;
			SqlConnection cnn;
			connetionString = @"Data Source =DESKTOP-CJRBB7E; Initial Catalog = BookingDB; Integrated Security = True";
			cnn = new SqlConnection(connetionString);
			cnn.Open();

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


			inde = Login.myString;
			MessageBox.Show(inde);
			if(inde == "1")
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
		
		private void Label1_Click(object sender, EventArgs e)
		{
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
		}

		private void button1_Click(object sender, EventArgs e)
		{
			userControl_DateTime1.Hide();
			userControl_Genre1.Hide();
			userControl_Rating1.Hide();
			userControl_Title1.Hide();
			userControl_Search_Rating1.Hide();
			userControl_Search_Title1.Hide();
			userControl_Search_Genre1.Hide();
			userControl_Search_DateTime1.Hide();

			userControl_Main1.Show();
			userControl_Main1.BringToFront();
		}

		private void panel2_Paint(object sender, PaintEventArgs e)
		{

		}
	}
}
