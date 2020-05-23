using System;
using System.Windows.Forms;

namespace LogIn
{
	public partial class UserControl_Search_DateTime : UserControl
	{
		[NonSerialized]
		private EventHandler fClick;
		public event EventHandler Click
		{
			add { fClick += value; }
			remove { fClick -= value; }
		}

		protected void OnClick(object sender, EventArgs e)
		{
			EventHandler handler = fClick;
			if (fClick != null)
				handler(sender, e);
		}
		public UserControl_Search_DateTime()
		{
			InitializeComponent();
			button1.Click += OnClick;

		}

		public static DateTime dt;
		private void button1_Click(object sender, EventArgs e)
		{
			dt = dateTimePicker1.Value;
		}
	}
}
