using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
			// Variables:
			List<Label> labels = new List<Label>();
			// Movie title:
			int x = 11;
			int y = 203;
			int pos = 1;
			// Movie picture:
			int a = 19;
			int b = 58;

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

			// Show titles and images:
			Panel pn = new Panel();
			int Pages = 0;
			var combined = UserControl_Search_Genre.st.Zip(UserControl_Search_Genre.id, (t, p) => new { title = t, pic = p });
			foreach (var obj in combined)
			{
				switch (pos)
				{
					// 1st row:
					case 1:
						pn = NewPanel();
						pn.BringToFront();
						ShowImage(obj.pic, a, b);
						ShowTitle(labels, obj.title, x, y);
						pos++;
						break;
					case 2:
						ShowImage(obj.pic, a + 125, b);
						ShowTitle(labels, obj.title, x + 125, y);
						pos++;
						break;
					case 3:
						ShowImage(obj.pic, a + 252, b);
						ShowTitle(labels, obj.title, x + 252, y);
						pos++;
						break;
					case 4:
						ShowImage(obj.pic, a + 378, b);
						ShowTitle(labels, obj.title, x + 378, y);
						pos++;
						break;

					// 2nd row:
					case 5:
						ShowImage(obj.pic, a, b + 186);
						ShowTitle(labels, obj.title, x, y + 186);
						pos++;
						break;
					case 6:
						ShowImage(obj.pic, a + 125, b + 186);
						ShowTitle(labels, obj.title, x + 125, y + 186);
						pos++;
						break;
					case 7:
						ShowImage(obj.pic, a + 252, b + 186);
						ShowTitle(labels, obj.title, x + 252, y + 186);
						pos++;
						break;
					case 8:
						ShowImage(obj.pic, a + 378, b + 186);
						ShowTitle(labels, obj.title, x + 378, y + 186);
						pos++;
						break;
					default:
						pos = 1;
						Pages += 1;
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
			temp.BringToFront();
		}

		public void ShowImage(string id, int a, int b)
		{
			var tmp = new PictureBox
			{
				Location = new Point(a, b),
				Size = new Size(90, 140),
				Image = Image.FromFile(Program.ImagesFolder() + "img" + id + ".jpg"),
				SizeMode = PictureBoxSizeMode.StretchImage
			};
			this.Controls.Add(tmp);
			tmp.BringToFront();
		}

		public Panel NewPanel()
		{
			Panel pn = new Panel();
			pn.Location = new Point(3, 32);
			pn.Size = new Size(496, 378);
			this.Controls.Add(pn);

			return pn;
		}
	}
}
