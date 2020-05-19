using System.Data.SqlClient;
using System.Windows.Forms;

namespace LogIn
{
	public partial class UserControl_Rating : UserControl
	{
		public UserControl_Rating()
		{
			InitializeComponent();
		}

		public void DisplayMovies()
		{
			// Database Connection:
			SqlConnection cnn = new SqlConnection(Program.DB_ConnectionString_Booking());
			cnn.Open();


			// Select statement: 
			SqlCommand command0 = new SqlCommand("Select Title from Movies where RatingIMDB > @min and RatingIMDB < @max order by RatingIMDB", cnn);
			SqlParameter min = new SqlParameter();
			SqlParameter max = new SqlParameter();
			min.ParameterName = "@min";
			max.ParameterName = "@max";
			command0.Parameters.AddWithValue("@min", UserControl_Search_Rating.Min);
			command0.Parameters.AddWithValue("@max", UserControl_Search_Rating.Max);
			using (command0)
			{
				SqlDataReader dR = command0.ExecuteReader();
				using (dR)
				{
					while (dR.Read())
					{
						MessageBox.Show(dR["Title"].ToString());
					}
				}
			}
		}
	}
}
