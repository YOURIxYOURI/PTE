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
	/// Logika interakcji dla klasy Benefits.xaml
	/// </summary>
	public partial class Benefits : UserControl
	{
		public Benefits(ContentControl contentControl)
		{
			InitializeComponent();
			AdminName.Content = Application.Current.Properties["Name"].ToString();
			this.contentControl = contentControl;
			DataGridView();
		}
		ContentControl contentControl;
		
		string info = "";
		IList<Mbenefits> benefits = new List<Mbenefits>();
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
			MessageBoxResult messageBoxResult = MessageBox.Show("Czy na pewno chcesz się wylogować", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
			if (messageBoxResult == MessageBoxResult.Yes)
			{
				Application.Current.Properties["Name"] = "";
				Application.Current.Properties["ID"] = "";
				Application.Current.Properties["IfMain"] = "";
				this.contentControl.Content = new Login(contentControl);
			}
		}
		private void DataGridView()
		{
			MySqlConnection conn = new MySqlConnection(ConnectionString);
			conn.Open();
			MySqlCommand cmd = conn.CreateCommand();
			cmd.CommandText = $"SELECT * FROM Benefits";
			cmd.ExecuteNonQuery();
			MySqlDataReader reader = cmd.ExecuteReader();
			while (reader.Read())
			{
				benefits.Add(new Mbenefits() { ID = reader.GetInt32("ID"), Name = reader.GetString("Name"), QRkey = reader.GetString("QRkey"), EndDate = reader.GetString("EndDate") });
			}
			conn.Close();
			DGbenefits.ItemsSource = benefits;
			DGbenefits.Items.Refresh();
		}
		private void Delete(object sender, RoutedEventArgs e)
		{
			MessageBoxResult messageBoxResult = MessageBox.Show("Czy na pewno chcesz usunąć ten benefit", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
			if (messageBoxResult == MessageBoxResult.Yes)
			{
				MySqlConnection conn = new MySqlConnection(ConnectionString);
				Mbenefits row = (Mbenefits)((Button)e.Source).DataContext;
				int id = row.ID;
				conn.Open();
				MySqlCommand cmd = conn.CreateCommand();
				cmd.CommandText = $"DELETE FROM benefitstouser WHERE BenefitID = {id}";
				cmd.ExecuteNonQuery();
				cmd = conn.CreateCommand();
				cmd.CommandText = $"DELETE FROM Benefits WHERE ID = {id}";
				cmd.ExecuteNonQuery();
				conn.Close();
				DataGridView();
			}
		}
		private void Edit(object sender, RoutedEventArgs e)
		{
			Mbenefits row = (Mbenefits)((Button)e.Source).DataContext;
			int id = row.ID;
			contentControl.Content = new BenefitEdit(contentControl, id);
		}
		private void OnSubmit(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrEmpty(NameForm.Text) && !string.IsNullOrEmpty(DescriptionForm.Text) && !string.IsNullOrEmpty(QRForm.Text) && EndDateForm.SelectedDate != null)
			{
				MySqlConnection conn = new MySqlConnection(ConnectionString);
				conn.Open();
				MySqlCommand cmd = conn.CreateCommand();
				cmd = conn.CreateCommand();
				cmd.CommandText = $"INSERT INTO benefits VALUES(null,'{NameForm.Text}','{DescriptionForm.Text}','{QRForm.Text}','{EndDateForm.SelectedDate}')";
				cmd.ExecuteNonQuery();
				info = "";
				NameForm.Text = "";
				DescriptionForm.Text = "";
				QRForm.Text = "";
				EndDateForm.Text = "Wybierz date";
				EndDateForm.SelectedDate = null;
				DataGridView();
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
			var Searched = benefits.Where(benf => benf.Name.Contains(SearchBar.Text) || benf.EndDate.Contains(SearchBar.Text));
			DGbenefits.ItemsSource = Searched;
			DGbenefits.Items.Refresh();
		}
	}
}
