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
				SqlConnection cnn = new SqlConnection(@"Data Source = ALDWYCH; Initial Catalog = BookingDB; Integrated Security = True");
				cnn.Open();


				// Select statement: 
				SqlCommand command0 = new SqlCommand("Select * from Movies where Title = @title", cnn);
				SqlParameter title = new SqlParameter();
				title.ParameterName = "@title";
				command0.Parameters.AddWithValue("@title", UserControl_Search_Title.Movie);
				SqlDataReader dR = command0.ExecuteReader();
				string check_Premiere;
				while (dR.Read())
				{
					label1.Text = dR["Title"].ToString();
					label15.Text = dR["Genre"].ToString();
					label14.MaximumSize = new Size(200, 0);
					label14.AutoSize = true;
					label14.Text = dR["Cast"].ToString();
					label13.Text = dR["Creator"].ToString();
					label12.Text = dR["Duration"].ToString();
					label11.Text = dR["Restrictions"].ToString();
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
				}

				// Close the connection and dispose of the commands:
				command0.Dispose();
				dR.Close();
				cnn.Close();
			}
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			this.ParentForm.DialogResult = DialogResult.OK;

			BookingSeatsScreen bk = new BookingSeatsScreen();
			bk.ShowDialog();
		}

		private void label15_Click(object sender, System.EventArgs e)
		{

		}
	}
}
