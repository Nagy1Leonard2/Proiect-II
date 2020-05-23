using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LogIn
{
    public partial class Manage_Users : UserControl
    {
        public Manage_Users()
        {
            InitializeComponent();
			General();

			button1.Cursor = Cursors.Hand;
			button1.ForeColor = Color.FromArgb(210, 54, 65);
			button1.Enabled = true;

		}

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
			if (checkBox1.Checked)
			{
				label3.Font = new Font(label3.Font, FontStyle.Strikeout);
				label3.Cursor = Cursors.No;
				comboBox1.Items.Clear();
				comboBox1.ResetText();
				comboBox1.Enabled = false;
				textBox1.Text = "";
				textBox2.Text = "";
				comboBox2.ResetText();
				button2.ForeColor = Color.FromArgb(129, 137, 150);
				button2.Enabled = false;
				button3.ForeColor = Color.FromArgb(129, 137, 150);
				button3.Enabled = false;

				button1.Cursor = Cursors.Hand;
				button1.ForeColor = Color.FromArgb(210, 54, 65);
				button1.Enabled = true;
			}
			else
			{
				General();				
			}
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			GetUsernameData();
		}

		public void General()
		{
			comboBox1.Items.Clear();
			comboBox1.ResetText();
			label3.Font = new Font(label3.Font, FontStyle.Regular);
			comboBox1.Enabled = true;
			button2.Enabled = true;
			button3.Enabled = true;
			label3.Cursor = Cursors.Default;
			button2.Cursor = Cursors.Hand;
			button3.Cursor = Cursors.Hand;
			button2.ForeColor = Color.FromArgb(210, 54, 65);
			button3.ForeColor = Color.FromArgb(210, 54, 65);

			button1.ForeColor = Color.FromArgb(129, 137, 150);
			button1.Enabled = false;

			GetUsernames();
		}

		public void GetUsernames()
		{
			//Sql Conenction:
			SqlConnection cnn = new SqlConnection(Program.DB_ConnectionString_Users());
			cnn.Open();

			// Select statement: 
			SqlCommand command1 = new SqlCommand("Select Username from Users order by Username", cnn);
			SqlDataReader dataR = command1.ExecuteReader();
			while (dataR.Read())
			{
				comboBox1.Items.Add(dataR["Username"]);
			}

			command1.Dispose();
			dataR.Close();
			cnn.Close();
		}

		public void GetUsernameData()
		{
			
			//Sql Conenction:
			SqlConnection cnn = new SqlConnection(Program.DB_ConnectionString_Users());
			cnn.Open();

			//Select statement to retrieve if the user is Admin or not:
			SqlCommand command0 = new SqlCommand("Select * from Users where Username = @use", cnn);
			SqlParameter use = new SqlParameter();
			use.ParameterName = "@use";
			command0.Parameters.AddWithValue("@use", comboBox1.Text);

			string rght;

			//SqlDataReader:
			SqlDataReader da = command0.ExecuteReader();
			while (da.Read())
			{
				textBox1.Text = da["Username"].ToString();
				textBox2.Text = da["Password"].ToString();
				rght = da["Rights"].ToString();
				if (rght == "1")
				{
					comboBox2.Text = "Admin";
				}
				else if (rght == "0")
				{
					comboBox2.Text = "User";
				}
			}

			//Close the connection and dispose of the commands:
			command0.Dispose();
			da.Close();
			cnn.Close();
		}

		private void Manage_Users_VisibleChanged(object sender, EventArgs e)
		{
			if (Visible == true)
			{
				General();

				button1.Cursor = Cursors.Hand;
				button1.ForeColor = Color.FromArgb(210, 54, 65);
				button1.Enabled = true;

				textBox1.Text = "";
				textBox2.Text = "";
				comboBox2.ResetText();
				checkBox1.Checked = false;
			}
		}

		// Add New User:
		private void button1_Click(object sender, EventArgs e)
		{
			// Database Connection:
			SqlConnection cnn = new SqlConnection(Program.DB_ConnectionString_Users());
			cnn.Open();

			// Insert statement: 
			SqlCommand command1 = new SqlCommand("Insert into Users (Username, Password, Rights) values (@usr, @pass, @rght)", cnn);
			SqlParameter usr = new SqlParameter();
			SqlParameter pass = new SqlParameter();
			SqlParameter rght = new SqlParameter();
			usr.ParameterName = "@usr";
			pass.ParameterName = "@pass";
			rght.ParameterName = "@rght";
			command1.Parameters.AddWithValue("@usr", textBox1.Text);
			command1.Parameters.AddWithValue("@pass", textBox2.Text);
			string right = comboBox2.Text;
			if (right == "Admin")
			{
				command1.Parameters.AddWithValue("@rght", "1");
			}
			else if (right == "User")
			{
				command1.Parameters.AddWithValue("@rght", "0");
			}
			
			using (command1)
			{
				
				SqlDataAdapter dA = new SqlDataAdapter();
				dA.InsertCommand = command1;
				dA.InsertCommand.ExecuteNonQuery();

				MessageBox.Show(textBox1.Text + " has been added.");

				dA.Dispose();
			}
			command1.Parameters.Clear();
			command1.Dispose();
			cnn.Close();

			General();
			button1.Cursor = Cursors.Hand;
			button1.ForeColor = Color.FromArgb(210, 54, 65);
			button1.Enabled = true;
			

			textBox1.Text = "";
			textBox2.Text = "";
			comboBox2.ResetText();
			checkBox1.Checked = false;
		}

		// Change user details:
		private void button3_Click(object sender, EventArgs e)
		{
			// Database Connection:
			SqlConnection cnn = new SqlConnection(Program.DB_ConnectionString_Users());
			cnn.Open();

			// Insert statement: 
			SqlCommand command1 = new SqlCommand("Update Users set Username = @usr, Password = @pass, Rights = @rght where Username = @initUsr", cnn);
			SqlParameter usr = new SqlParameter();
			SqlParameter pass = new SqlParameter();
			SqlParameter rght = new SqlParameter();
			SqlParameter initUsr = new SqlParameter();
			usr.ParameterName = "@usr";
			pass.ParameterName = "@pass";
			rght.ParameterName = "@rght";
			initUsr.ParameterName = "@initUsr";
			command1.Parameters.AddWithValue("@initUsr", comboBox1.Text);
			command1.Parameters.AddWithValue("@usr", textBox1.Text);
			command1.Parameters.AddWithValue("@pass", textBox2.Text);
			string right = comboBox2.Text;
			if (right == "Admin")
			{
				command1.Parameters.AddWithValue("@rght", "1");
			}
			else if (right == "User")
			{
				command1.Parameters.AddWithValue("@rght", "0");
			}

			using (command1)
			{

				SqlDataAdapter dA = new SqlDataAdapter();
				dA.UpdateCommand = command1;
				dA.UpdateCommand.ExecuteNonQuery();

				MessageBox.Show(textBox1.Text + " has been updated.");

				dA.Dispose();
			}
			command1.Parameters.Clear();
			command1.Dispose();
			cnn.Close();

			General();
			button1.Cursor = Cursors.Hand;
			button1.ForeColor = Color.FromArgb(210, 54, 65);
			button1.Enabled = true;

			textBox1.Text = "";
			textBox2.Text = "";
			comboBox2.ResetText();
			checkBox1.Checked = false;
		}

		// Delete User:
		private void button2_Click(object sender, EventArgs e)
		{
			// Database Connection:
			SqlConnection cnn = new SqlConnection(Program.DB_ConnectionString_Users());
			cnn.Open();

			// Insert statement: 
			SqlCommand command1 = new SqlCommand("Delete Users where Username = @usr", cnn);
			SqlParameter usr = new SqlParameter();
			usr.ParameterName = "@usr";
			command1.Parameters.AddWithValue("@usr", comboBox1.Text);

			using (command1)
			{

				SqlDataAdapter dA = new SqlDataAdapter();
				dA.DeleteCommand = command1;
				dA.DeleteCommand.ExecuteNonQuery();

				MessageBox.Show(textBox1.Text + " is no longer with us.");

				dA.Dispose();
			}
			command1.Parameters.Clear();
			command1.Dispose();
			cnn.Close();

			General();
			button1.Cursor = Cursors.Hand;
			button1.ForeColor = Color.FromArgb(210, 54, 65);
			button1.Enabled = true;

			textBox1.Text = "";
			textBox2.Text = "";
			comboBox2.ResetText();
			checkBox1.Checked = false;
		}
	}
}
