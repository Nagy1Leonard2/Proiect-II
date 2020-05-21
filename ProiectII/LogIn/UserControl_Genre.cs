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
			// Movie title:
			int x = 11;
			int y = 203;
			int pos = 1;

			// For page number:
			int total = UserControl_Search_Genre.st.Count;
			int nrPages;
			int nr_fullPages = total / 8;
			int nr_notFullPages = total % 8;
			if (nr_notFullPages != 0)
				nrPages = nr_fullPages + 1;
			else
				nrPages = nr_fullPages;
		
			label11.Text = nrPages.ToString();
			foreach (var title in UserControl_Search_Genre.st)
			{
				
				switch (pos)
				{
					// 1st row:
					case 1:
						ShowTitle(labels, title, x, y);
						pos++;
						break;
					case 2:
						ShowTitle(labels, title, x+125, y);
						pos++;
						break;
					case 3:
						ShowTitle(labels, title, x+252, y);
						pos++;
						break;
					case 4:
						ShowTitle(labels, title, x+378, y);
						pos++;
						break;

					// 2nd row:
					case 5:
						ShowTitle(labels, title, x, y+186);
						pos++;
						break;
					case 6:
						ShowTitle(labels, title, x+125, y+186);
						pos++;
						break;
					case 7:
						ShowTitle(labels, title, x+252, y+186);
						pos++;
						break;
					case 8:
						ShowTitle(labels, title, x +378, y+186);
						pos++;
						break;
					default:
						pos = 1;
						break;
				}
			}
		}

		public void ShowTitle(List<Label> labels, string title, int x, int y)
		{
			var temp = new Label
			{
				Location = new Point(x, y),
				Text = title,
				BackColor = Color.Black,
				ForeColor = Color.White,
				Font = new Font("Calibri", 12),
				Height = 24,
				Width = 47,
				MaximumSize = new Size(125, 0),
				AutoSize = true
			};
			this.Controls.Add(temp);
			temp.Show();
			labels.Add(temp);
		}
	}
}
