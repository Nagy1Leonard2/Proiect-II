using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace LogIn
{
	public partial class Add_Movie : UserControl
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

		public Add_Movie()
        {
            InitializeComponent();
			button2.Click += OnClick;

			// Add the default values:
			comboBox2.Items.Add("Action");
			comboBox2.Items.Add("Anime");
			comboBox2.Items.Add("Comedy");
			comboBox2.Items.Add("Crime");
			comboBox2.Items.Add("Documentary");
			comboBox2.Items.Add("Drama");
			comboBox2.Items.Add("Horror");
			comboBox2.Items.Add("Thriler");
			comboBox2.Items.Add("Sci-Fi");
			comboBox2.Items.Add("Romance");
		}

		// Variable used in the next Form:
		public static string id = "";

		// Add movie button:
		private void button2_Click(object sender, EventArgs e)
        {
			if (pictureBox1.Image != null)
			{
				// Database Connection:
				SqlConnection cnn = new SqlConnection(Program.DB_ConnectionString_Booking());
				cnn.Open();

				// Insert statement: 
				SqlCommand command1 = new SqlCommand("Insert into Movies "+
					"(Title, Descriprion, Genre, Duration, RatingIMDB, Creator, Cast, Premiere, Restrictions, Price) values "+
					"(@title, @desc, @genre, @duration, @rating, @creator, @cast, @premiere, @restr, @price)", cnn);
				SqlParameter title = new SqlParameter();
				SqlParameter desc = new SqlParameter();
				SqlParameter genre = new SqlParameter();
				SqlParameter duration = new SqlParameter();
				SqlParameter rating = new SqlParameter();
				SqlParameter creator = new SqlParameter();
				SqlParameter cast = new SqlParameter();
				SqlParameter premiere = new SqlParameter();
				SqlParameter restr = new SqlParameter();
				SqlParameter price = new SqlParameter();
				title.ParameterName = "@title";
				desc.ParameterName = "@desc";
				genre.ParameterName = "@genre";
				duration.ParameterName = "@duration";
				rating.ParameterName = "@rating";
				creator.ParameterName = "@creator";
				cast.ParameterName = "@cast";
				premiere.ParameterName = "@premiere";
				restr.ParameterName = "@restr";
				price.ParameterName = "@price";
				command1.Parameters.AddWithValue("@title", textBox9.Text);
				command1.Parameters.AddWithValue("@desc", textBox8.Text);
				command1.Parameters.AddWithValue("@genre", comboBox2.Text);
				command1.Parameters.AddWithValue("@duration", textBox4.Text);
				command1.Parameters.AddWithValue("@rating", textBox7.Text+"."+textBox2.Text);
				command1.Parameters.AddWithValue("@creator", textBox3.Text);
				command1.Parameters.AddWithValue("@cast", textBox6.Text);
				if (comboBox1.Text == "Yes")
				{
					command1.Parameters.AddWithValue("@premiere", "1");
				}
				else if (comboBox1.Text == "No")
				{
					command1.Parameters.AddWithValue("@premiere", "0");
				}
				command1.Parameters.AddWithValue("@restr", textBox5.Text);
				command1.Parameters.AddWithValue("@price", textBox1.Text);

				using (command1)
				{
					SqlDataAdapter dA = new SqlDataAdapter();
					dA.InsertCommand = command1;
					dA.InsertCommand.ExecuteNonQuery();

					MessageBox.Show(textBox9.Text + " has been added.");

					dA.Dispose();
				}
				command1.Parameters.Clear();
				command1.Dispose();

				// Select statement: 
				SqlCommand command0 = new SqlCommand("Select * from Movies where Title = @tl", cnn);
				SqlParameter tl = new SqlParameter();
				tl.ParameterName = "@tl";
				command0.Parameters.AddWithValue("@tl", textBox9.Text);
				SqlDataReader dR = command0.ExecuteReader();
				
				while (dR.Read())
				{
					id = dR["Id"].ToString();
				}
				command0.Dispose();
				dR.Close();
				cnn.Close();

				string fname = "img" + id + ".jpg";
				string folder = Program.ImagesFolder();
				string pathstring = System.IO.Path.Combine(folder, fname);
				Image a = pictureBox1.Image;
				a.Save(pathstring);
			}
			else
			{
				MessageBox.Show("Please add a photo.");
			}
		}

		// Add photo:
		private void button1_Click(object sender, EventArgs e)
		{
			// Open file dialog:
			OpenFileDialog open = new OpenFileDialog();
			// Image filters:
			open.Title = "Select a Image";
			open.Filter = "jpg files (*.jpg)|*.jpg|All files (*.*)|*.*";
			if (open.ShowDialog() == DialogResult.OK)
			{
				// Display image in picture box:
				pictureBox1.Image = Image.FromFile(open.FileName);
				pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
			}
		}
	}
}
