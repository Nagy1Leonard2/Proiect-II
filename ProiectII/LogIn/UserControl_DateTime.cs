using System.Data.SqlClient;
using System.Windows.Forms;

namespace LogIn
{
	public partial class UserControl_DateTime : UserControl
	{
		public UserControl_DateTime()
		{
			InitializeComponent();
			// Database Connection:
			SqlConnection cnn = new SqlConnection(@"Data Source = ALDWYCH; Initial Catalog = BookingDB; Integrated Security = True");
			cnn.Open();
		}
	}
}
