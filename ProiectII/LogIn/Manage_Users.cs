using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace LogIn
{
	public partial class Manage_Users : UserControl
    {
		[NonSerialized]
		private EventHandler fClick;
		public event EventHandler Click
		{
			add { fClick += value; }
			remove { fClick -= value; }
		}
		protected void OnClick(object sender, EventArgs e)
		{
			EventHandler handler = fClick;
			if (fClick != null)
				handler(sender, e);
		}

		public Manage_Users()
        {
            InitializeComponent();
			button1.Click += OnClick;
			button2.Click += OnClick;
			button3.Click += OnClick;
			General();

			// Visual settings for the Add User button:
			button1.Cursor = Cursors.Hand;
			button1.ForeColor = Color.FromArgb(210, 54, 65);
			button1.Enabled = true;

		}

		// If New User is checked or not:
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
			if (checkBox1.Checked)
			{
				// Existing User label:
				label3.Font = new Font(label3.Font, FontStyle.Strikeout);
				label3.Cursor = Cursors.No;

				// Existing User comboBox:
				comboBox1.Items.Clear();
				comboBox1.ResetText();
				comboBox1.Enabled = false;

				// Username:
				textBox1.Text = "";

				// Password:
				textBox2.Text = "";

				// Rights:
				comboBox2.ResetText();

				// Visuals for the butttons:
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

		// When an existing user is selected:
		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			GetUsernameData();
		}

		// Visuals before any user action:
		public void General()
		{
			// Existing user:
			comboBox1.Items.Clear();
			comboBox1.ResetText();
			comboBox1.Enabled = true;
			label3.Font = new Font(label3.Font, FontStyle.Regular);
			label3.Cursor = Cursors.Default;

			// Delete User:
			button2.Enabled = true;
			button2.Cursor = Cursors.Hand;
			button2.ForeColor = Color.FromArgb(210, 54, 65);

			// Save Changes:
			button3.Enabled = true;
			button3.Cursor = Cursors.Hand;
			button3.ForeColor = Color.FromArgb(210, 54, 65);

			// Add User:
			button1.ForeColor = Color.FromArgb(129, 137, 150);
			button1.Enabled = false;

			GetUsernames();
		}

		// Show existing users:
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

			// Close the connection and dispose afetr use:
			command1.Dispose();
			dataR.Close();
			cnn.Close();
		}

		// Show data for existing users:
		public void GetUsernameData()
		{
			// Variables:
			string rght;

			//Sql Conenction:
			SqlConnection cnn = new SqlConnection(Program.DB_ConnectionString_Users());
			cnn.Open();

			//Select statement to retrieve if the user is Admin or not:
			SqlCommand command0 = new SqlCommand("Select * from Users where Username = @use", cnn);
			SqlParameter use = new SqlParameter();
			use.ParameterName = "@use";
			command0.Parameters.AddWithValue("@use", comboBox1.Text);

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
			command0.Parameters.Clear();
			command0.Dispose();
			da.Close();
			cnn.Close();
		}

		// Visuals before any user action:
		private void Manage_Users_VisibleChanged(object sender, EventArgs e)
		{
			if (Visible == true)
			{
				General();

				// AddUuser:
				button1.Cursor = Cursors.Hand;
				button1.ForeColor = Color.FromArgb(210, 54, 65);
				button1.Enabled = true;

				// Username:
				textBox1.Text = "";

				// Password:
				textBox2.Text = "";

				// Rights:
				comboBox2.ResetText();

				// New User:
				checkBox1.Checked = false;
			}
		}

		// Add New User:
		private void button1_Click(object sender, EventArgs e)
		{
			// Variables:
			string right = comboBox2.Text;

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

			// Dispose and close the connection:
			command1.Parameters.Clear();
			command1.Dispose();
			cnn.Close();

			// Set visuals to default:
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
			// Variables:
			string right = comboBox2.Text;

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

			// Close connection and Dispose:
			command1.Parameters.Clear();
			command1.Dispose();
			cnn.Close();

			// Default visuals:
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

			// Close connection and Dispose:
			command1.Parameters.Clear();
			command1.Dispose();
			cnn.Close();

			// Show default visuals:
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
