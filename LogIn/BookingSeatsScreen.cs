using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LogIn
{
	public partial class BookingSeatsScreen : Form
	{
		public BookingSeatsScreen()
		{
			InitializeComponent();
		}

		public string conString = "Data Source=.;Initial Catalog=BookingsDB;Integrated Security=True";
		private void BookingSeatsScreen_Load(object sender, EventArgs e)
		{
			panel1.BackColor = Color.FromArgb(200, 0, 0, 0);

			SqlConnection con = new SqlConnection(conString);
			con.Open();

			s1.BackColor = Color.Red;

		}
		private void Label1_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
