using AdminPanel.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
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
	/// Logika interakcji dla klasy BenefitEdit.xaml
	/// </summary>
	public partial class BenefitEdit : UserControl
	{
		public BenefitEdit(ContentControl contentControl, int ID)
		{
			InitializeComponent();
			AdminName.Content = Application.Current.Properties["Name"].ToString();
			this.contentControl = contentControl;
			this.ID = ID;
			DataGridView();
			OnLoad();
		}
		ContentControl contentControl;
		string info = "";
		int ID;
		IList<Musers> users = new List<Musers>();
		bool IfAll = true;
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
		private void OnLoad()
		{
			MySqlConnection conn = new MySqlConnection(ConnectionString);
			conn.Open();
			MySqlCommand cmd = conn.CreateCommand();
			cmd.CommandText = $"SELECT * FROM Benefits WHERE ID = {ID}";
			cmd.ExecuteNonQuery();
			MySqlDataReader reader = cmd.ExecuteReader();
			Mbenefits benf = new Mbenefits();
			if (reader.Read())
			{
				benf = new Mbenefits() { ID = reader.GetInt32("ID"), Name = reader.GetString("Name"), Description = reader.GetString("Description"), QRkey = reader.GetString("QRkey"), EndDate = reader.GetString("EndDate") };
				NameForm.Text = benf.Name;
				DescriptionForm.Text = benf.Description;
				QRForm.Text = benf.QRkey;
				EndDateForm.Text = benf.EndDate;
				EndDateForm.SelectedDate = Convert.ToDateTime(benf.EndDate);
				IfAllCheck.IsChecked = IfAll;
			}
			else
			{
				contentControl.Content = new Benefits(contentControl);
			}
			conn.Close();
		}
		private void Delete(object sender, RoutedEventArgs e)
		{
			MessageBoxResult messageBoxResult = MessageBox.Show("Czy na pewno chcesz usunąć ten benefit", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
			if (messageBoxResult == MessageBoxResult.Yes)
			{
				MySqlConnection conn = new MySqlConnection(ConnectionString);
				conn.Open();
				MySqlCommand cmd = conn.CreateCommand();
				cmd.CommandText = $"DELETE FROM benefitstouser WHERE BenefitID = {ID}";
				cmd.ExecuteNonQuery();
				cmd = conn.CreateCommand();
				cmd.CommandText = $"DELETE FROM Benefits WHERE ID = {ID}";
				cmd.ExecuteNonQuery();
				conn.Close();
				contentControl.Content = new Benefits(contentControl);
			}
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
			reader.Close();
			foreach (Musers user in users)
			{
				MySqlCommand cmd2 = conn.CreateCommand();
				cmd2.CommandText = $"SELECT * FROM benefitstouser WHERE UserID={user.ID}";
				cmd2.ExecuteNonQuery();
				bool IfBen = true;
				MySqlDataReader reader2 = cmd2.ExecuteReader();
				if (!reader2.Read()) { IfBen = false; IfAll = false; }
				user.IfBenefit = IfBen;
				reader2.Close();
			}
			DGusers.ItemsSource= users;
			conn.Close();
		}
		private void OnSubmit(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrEmpty(NameForm.Text) && !string.IsNullOrEmpty(DescriptionForm.Text) && !string.IsNullOrEmpty(QRForm.Text) && EndDateForm.SelectedDate != null)
			{
				MySqlConnection conn = new MySqlConnection(ConnectionString);
				conn.Open();
				MySqlCommand cmd = conn.CreateCommand();
				cmd.CommandText = $"UPDATE benefits SET Name='{NameForm.Text}',Description='{DescriptionForm.Text}',QRkey='{QRForm.Text}',EndDate='{EndDateForm.SelectedDate}'";
				cmd.ExecuteNonQuery();
				foreach(Musers user in DGusers.Items)
				{
					MySqlCommand cmd2 = conn.CreateCommand();
					cmd2.CommandText = $"SELECT * FROM benefitstouser WHERE UserID={user.ID} AND BenefitID={ID}";
					cmd2.ExecuteNonQuery();
					MySqlDataReader reader2 = cmd2.ExecuteReader();
					if (reader2.Read())
					{
						reader2.Close();
						if(user.IfBenefit == true)
						{
							continue;
						}
						else
						{
							MySqlCommand cmd3 = conn.CreateCommand();
							cmd3.CommandText = $"DELETE FROM benefitstouser WHERE UserID={user.ID} AND BenefitID={ID}";
							cmd3.ExecuteNonQuery();
						}
					}
					else
					{
						reader2.Close();
						if (user.IfBenefit == true)
						{
							MySqlCommand cmd3 = conn.CreateCommand();
							cmd3.CommandText = $"INSERT INTO benefitstouser VALUES(null, {ID}, {user.ID})";
							cmd3.ExecuteNonQuery();
						}
						else
						{
							continue;
						}
					}
				}
				conn.Close();
				contentControl.Content = new Benefits(contentControl);
			}
			else
			{
				info = "Proszę uzupełnić wszytkie pola";
			}
			InfoLabel.Content = info;
		}
		private void CheckBoxChanged(object sender, RoutedEventArgs e)
		{
			bool? check = ((CheckBox)sender).IsChecked;
			foreach (Musers user in users)
			{
				user.IfBenefit = (bool)check;
			}
			DGusers.ItemsSource = users;
			DGusers.Items.Refresh();
		}
		private void OnSearch(object sender, RoutedEventArgs e)
		{
			var Searched = users.Where(user => user.FirstName.Contains(SearchBar.Text) || user.LastName.Contains(SearchBar.Text) || user.Email.Contains(SearchBar.Text));
			DGusers.ItemsSource = Searched;
			DGusers.Items.Refresh();
		}
	}
}
