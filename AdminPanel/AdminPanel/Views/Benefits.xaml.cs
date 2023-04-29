using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AdminPanel.Views
{
	/// <summary>
	/// Logika interakcji dla klasy Benefits.xaml
	/// </summary>
	public partial class Benefits : UserControl
	{
		public Benefits(ContentControl contentControl)
		{
			InitializeComponent();
			this.contentControl = contentControl;
		}
		ContentControl contentControl;
		public void GoTo(object sender, RoutedEventArgs e)
		{
			switch (((Button)sender).Tag)
			{
				case "Admins":
					this.contentControl.Content = new Admins(contentControl);
					break;
				case "Users":
					this.contentControl.Content = new Users(contentControl);
					break;
				case "Benefits":
					this.contentControl.Content = new Benefits(contentControl);
					break;
			}
		}
		private void LogOut(object sender, RoutedEventArgs e)
		{
			Application.Current.Properties["Name"] = "";
			Application.Current.Properties["ID"] = "";
			Application.Current.Properties["IfMain"] = "";
			this.contentControl.Content = new Login(contentControl);
		}
	}
}
