using System;
using System.Windows.Forms;

namespace LogIn
{
	public partial class UserControl_Search_Title : UserControl
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
		public UserControl_Search_Title()
		{
			InitializeComponent();
			button1.Click += OnClick;
		}
	}
}
