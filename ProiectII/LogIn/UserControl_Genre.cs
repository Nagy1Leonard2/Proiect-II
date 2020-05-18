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
			foreach (var title in UserControl_Search_Genre.st)
			{
				MessageBox.Show(title);
			}
		}
	}
}
