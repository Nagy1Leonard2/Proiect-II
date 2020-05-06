using System;
using System.Windows.Forms;

namespace LogIn
{
	public partial class Search : Form
	{
		
		public Search()
		{
			InitializeComponent();
		}
		public static string sr = "";
		private void Search_Load(object sender, EventArgs e)
		{
			
		}
		
		private void button1_Click(object sender, EventArgs e)
		{
			this.Hide();

			Home hm = new Home();
			hm.Show();
		}

		private void label1_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
