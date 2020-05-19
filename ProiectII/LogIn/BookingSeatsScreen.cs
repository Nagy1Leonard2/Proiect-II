using System;
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
	}
}
