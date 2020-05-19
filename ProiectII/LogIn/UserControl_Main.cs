using System.Data.SqlClient;
using System.Windows.Forms;

namespace LogIn
{
	public partial class UserControl_Main : UserControl
	{
		public UserControl_Main()
		{
			InitializeComponent();
			// Database Connection:
			SqlConnection cnn = new SqlConnection(@"Data Source = ALDWYCH; Initial Catalog = BookingDB; Integrated Security = True");
			cnn.Open();
		}
	}
}
