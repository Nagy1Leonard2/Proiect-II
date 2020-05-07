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

		private void Login_Load(object sender, EventArgs e)
		{
			

			panel1.BackColor = Color.FromArgb(200, 0, 0, 0);
		}

		private void Label4_Click(object sender, EventArgs e)
		{
			Close();
		}
		// LogIn Function
		private void Button1_Click(object sender, EventArgs e)
		{
			//to do: custom error messageBoxes
			//to do: connection to the users db
			//to do: add logo icon on the top left corner
			//to do: this.Close after Home is opened (not only this.hide)

			string connetionString;
			SqlConnection cnn;
			connetionString = @"Data Source =DESKTOP-CJRBB7E; Initial Catalog = UsersDB; Integrated Security = True";
			cnn = new SqlConnection(connetionString);
			cnn.Open();

			SqlCommand command;
			//SqlDataReader dataReader;
			String sql;
			SqlParameter use = new SqlParameter();
			SqlParameter pas = new SqlParameter();

			use.ParameterName = "@use";
			pas.ParameterName = "@pas";

			

			sql = "Select Rights from Users where Username=@use  and Password=@pas";


				 command = new SqlCommand(sql,cnn);
			command.Parameters.AddWithValue("@use",(textBox1.Text));
			command.Parameters.AddWithValue("@pas",(textBox2.Text));
			SqlDataReader da = command.ExecuteReader();

			var myString = "";
			
			while(da.Read())
			{
			
				myString = da.GetInt32(0).ToString();

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
					if (myString == "0")
					{
						MessageBox.Show("User");
					}
					else if(myString == "1")
					{
						MessageBox.Show("Admin");

					}
					Home y = new Home();
					y.Show();
					this.Hide();
				}
				//============================================
				else
				{
					MessageBox.Show("Wrong credentials.");
				}

			}


			
		}
	}
}
