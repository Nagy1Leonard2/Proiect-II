using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace LogIn
{
	public partial class Login : Form
	{
		public Login()
		{
			InitializeComponent();
		}

		public static string myString = "";

		private void Login_Load(object sender, EventArgs e)
		{
			//Opaque background for the panel:
			panel1.BackColor = Color.FromArgb(200, 0, 0, 0);
		}

		private void Label4_Click(object sender, EventArgs e)
		{
			//Close the App on X_label click:
			Close();
		}

		// LogIn Function
		private void Button1_Click(object sender, EventArgs e)
		{
			//Sql Conenction:
			SqlConnection cnn = new SqlConnection(@"Data Source = LPTVIVIANACSA\SQLSERVER01; Initial Catalog = UsersDB; Integrated Security = True");
			cnn.Open();

			//Select statement to retrieve if the user is Admin or not:
			SqlCommand command0 = new SqlCommand("Select Rights from Users where Username = @use  and Password = @pas", cnn);
			SqlParameter use = new SqlParameter();
			SqlParameter pas = new SqlParameter();
			use.ParameterName = "@use";
			pas.ParameterName = "@pas";
			command0.Parameters.AddWithValue("@use",(textBox1.Text));
			command0.Parameters.AddWithValue("@pas",(textBox2.Text));

			//SqlDataReader:
			SqlDataReader da = command0.ExecuteReader();
			while(da.Read())
			{
				myString = da.GetInt32(0).ToString();
			}
			if ((textBox1.Text == "") && (textBox2.Text == ""))
			{
				MessageBox.Show("Username & password fields cannot be empty.");
			}
			else if (textBox1.Text == "")
			{
				MessageBox.Show("Please enter your username.");
			}
			else if (textBox2.Text == "")
			{
				MessageBox.Show("Please enter your password.");
			}
			//============================================
			else if ((myString == "0") || (myString == "1"))
			{
				Home y = new Home();
				this.Hide();
				y.ShowDialog();
				this.Close();

				// Go back to Home from the BookingSeatsScreen method:
				using (var form2 = y)
				{
					if (form2.ShowDialog() == DialogResult.OK)
					{
						y.Hide();
					}
				}
			}
			//============================================
			else
			{
				MessageBox.Show("Wrong credentials.");
			}

			//Close the connection and dispose of the commands:
			command0.Dispose();
			cnn.Close();
		}
	}
}
