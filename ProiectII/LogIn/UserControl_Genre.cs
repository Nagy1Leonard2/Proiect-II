using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LogIn
{
	public partial class UserControl_Genre : UserControl
	{
		public UserControl_Genre()
		{
			InitializeComponent();
		}

		public void DisplayMovies()
		{
			List<Label> labels = new List<Label>();
			Rectangle r = this.ClientRectangle;
			foreach (var title in UserControl_Search_Genre.st)
			{
				//MessageBox.Show(title);

				var temp = new Label();
				temp.Location = new Point(59, 217);
				temp.Text = title;
				temp.BackColor = Color.Black;
				temp.ForeColor = Color.Coral;
				temp.Font = new Font("Calibri", 12);
				temp.Height = 24;
				temp.Width = 47;
				this.Controls.Add(temp);
				temp.Show();
				labels.Add(temp);
			}
		}
	}
}
