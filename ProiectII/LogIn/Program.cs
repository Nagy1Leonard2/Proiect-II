using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LogIn
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Login());
		}

		public static string DB_ConnectionString_Users()
		{
			return "Data Source = LPTVIVIANACSA\\SQLSERVER01; Initial Catalog = UsersDB; Integrated Security = True";
		}
		public static string DB_ConnectionString_Booking()
		{
			return "Data Source = LPTVIVIANACSA\\SQLSERVER01; Initial Catalog = BookingDB; Integrated Security = True";
		}
	}
}
