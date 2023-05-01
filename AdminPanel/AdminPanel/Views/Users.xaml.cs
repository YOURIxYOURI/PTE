using AdminPanel.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
	/// Logika interakcji dla klasy Users.xaml
	/// </summary>
	public partial class Users : UserControl
	{
		public Users(ContentControl contentControl)
		{
			InitializeComponent();
			DataGridView();
			AdminName.Content = Application.Current.Properties["Name"].ToString();
			this.contentControl = contentControl;
		}
		ContentControl contentControl;
		string info = "";
		IList<Musers> users = new List<Musers>();
		string ConnectionString = $"Server={ConfigurationManager.AppSettings["Server"]};Database={ConfigurationManager.AppSettings["Database"]};Uid={ConfigurationManager.AppSettings["User"]};Pwd={ConfigurationManager.AppSettings["Password"]}";
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
		private void DataGridView()
		{
			MySqlConnection conn = new MySqlConnection(ConnectionString);
			conn.Open();
			MySqlCommand cmd = conn.CreateCommand();
			cmd.CommandText = $"SELECT * FROM Users";
			cmd.ExecuteNonQuery();
			MySqlDataReader reader = cmd.ExecuteReader();
			while (reader.Read())
			{
				users.Add(new Musers() { ID = reader.GetInt32("ID"), FirstName = reader.GetString("FirstName"), LastName = reader.GetString("LastName"), Email = reader.GetString("Email")});
			}
			conn.Close();
			DGusers.ItemsSource = users;
		}
		private void Delete(object sender, RoutedEventArgs e)
		{
			MySqlConnection conn = new MySqlConnection(ConnectionString);
			Musers row = (Musers)((Button)e.Source).DataContext;
			int id = row.ID;
			conn.Open();
			if (Application.Current.Properties["IfMain"].ToString() == "True")
			{
				MySqlCommand cmd = conn.CreateCommand();
				cmd.CommandText = $"DELETE FROM benefitstouser WHERE UserID = {id}";
				cmd.ExecuteNonQuery();
				cmd = conn.CreateCommand();
				cmd.CommandText = $"DELETE FROM Users WHERE ID = {id}";
				cmd.ExecuteNonQuery();
				conn.Close();
				DataGridView();
			}
			else
			{
				MessageBox.Show("Operacją która próbujesz wykonać może zostać przeprowadzona tylko przez Administratora Nadrzednego", "Uwaga!", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
		private void Edit(object sender, RoutedEventArgs e)
		{
			Musers row = (Musers)((Button)e.Source).DataContext;
			int id = row.ID;
			contentControl.Content = new UserEdit(contentControl, id);
		}
		private void OnSubmit(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrEmpty(FnameForm.Text) && !string.IsNullOrEmpty(LnameForm.Text) && !string.IsNullOrEmpty(EmailForm.Text) && !string.IsNullOrEmpty(AdressForm.Text) && !string.IsNullOrEmpty(GradeForm.Text) && !string.IsNullOrEmpty(SchoolNameForm.Text) && !string.IsNullOrEmpty(GradeYearForm.Text) && !string.IsNullOrEmpty(PhoneForm.Text) && !string.IsNullOrEmpty(WorkForm.Text) && !string.IsNullOrEmpty(BirthPlaceForm.Text) && BirthDateForm.SelectedDate != null)
			{
				MySqlConnection conn = new MySqlConnection(ConnectionString);
				conn.Open();
				MySqlCommand cmd = conn.CreateCommand();
				cmd.CommandText = $"SELECT * FROM Users WHERE Email = '{EmailForm.Text}'";
				cmd.ExecuteNonQuery();
				MySqlDataReader reader = cmd.ExecuteReader();
				if (!reader.Read())
				{
					reader.Close();
					cmd = conn.CreateCommand();
					cmd.CommandText = $"INSERT INTO users VALUES(null,'{FnameForm.Text}','{LnameForm.Text}','{BirthDateForm.SelectedDate}','{BirthPlaceForm.Text}','{PhoneForm.Text}','{EmailForm.Text}','{AdressForm.Text}','{GradeForm.Text}','{SchoolNameForm.Text}','{GradeYearForm.Text}','{WorkForm.Text}','',0,'')";
					cmd.ExecuteNonQuery();
					info = "";
					FnameForm.Text = "";
					LnameForm.Text = "";
					BirthDateForm.Text = "Wybierz datę";
					BirthDateForm.SelectedDate= null;
					BirthPlaceForm.Text = "";
					PhoneForm.Text = "";
					EmailForm.Text = "";
					AdressForm.Text = "";
					GradeForm.Text = "";
					SchoolNameForm.Text = "";
					GradeYearForm.Text = "";
					WorkForm.Text = "";
					DataGridView();
				}
				else
				{
					info = "Użytkownik z takim adresem email już istnieje";
				}
				conn.Close();
			}
			else
			{
				info = "Proszę uzupełnić wszytkie pola";
			}
			InfoLabel.Content = info;
		}
		private void OnSearch(object sender, RoutedEventArgs e)
		{
			var Searched = users.Where(user => user.FirstName.Contains(SearchBar.Text) || user.LastName.Contains(SearchBar.Text) || user.Email.Contains(SearchBar.Text));
			DGusers.ItemsSource = Searched;
			DGusers.Items.Refresh();
		}
	}
}
