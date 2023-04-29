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
using AdminPanel.Models;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace AdminPanel.Views
{
	/// <summary>
	/// Logika interakcji dla klasy Admins.xaml
	/// </summary>
	public partial class Admins : UserControl
	{
		public Admins(ContentControl contentControl)
		{
			InitializeComponent();
			DataGridView();
			AdminName.Content = Application.Current.Properties["Name"].ToString();
			this.contentControl = contentControl;
		}
		public string Salt { get; set; }
		ContentControl contentControl;
		string info= "";
		string pattern = @"^(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*\(\)\-\=\+_])(?=.{8,})";
		string ConnectionString = $"Server={ConfigurationManager.AppSettings["Server"]};Database={ConfigurationManager.AppSettings["Database"]};Uid={ConfigurationManager.AppSettings["User"]};Pwd={ConfigurationManager.AppSettings["Password"]}";

		private void OnChecked(bool value, int id)
		{
			MySqlConnection conn = new MySqlConnection(ConnectionString);
			int data = 1;
			if (!value) { data = 0; }
			conn.Open();
			MySqlCommand cmd = conn.CreateCommand();
			cmd.CommandText = $"UPDATE admins SET IfMain = {data} WHERE ID ={id}";
			cmd.ExecuteNonQuery();
			conn.Close();
			DataGridView();
		}
		private void GoTo(object sender, RoutedEventArgs e)
		{
			switch(((Button)sender).Tag)
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
			cmd.CommandText = $"SELECT * FROM admins";
			cmd.ExecuteNonQuery();
			IList<Madmins> admins= new List<Madmins>();
			MySqlDataReader reader = cmd.ExecuteReader();
			while (reader.Read())
			{
				admins.Add(new Madmins() { ID= reader.GetInt32("ID"), FirstName = reader.GetString("FirstName"), LastName = reader.GetString("LastName"), Email = reader.GetString("Email"), IfMain = reader.GetBoolean("IfMain") });
			}
			conn.Close();
			DGadmins.ItemsSource = admins;
		}
		private void Delete(object sender, RoutedEventArgs e)
		{
				MySqlConnection conn = new MySqlConnection(ConnectionString);
				Madmins row = (Madmins)((Button)e.Source).DataContext;
				int id = row.ID;
				conn.Open();
				if(Application.Current.Properties["IfMain"].ToString() == "True")
				{
					MySqlCommand cmd = conn.CreateCommand();
					cmd.CommandText = $"DELETE FROM Admins WHERE ID = {id}";
					cmd.ExecuteNonQuery();
					conn.Close();
					DataGridView();
				}
				else
				{
					MessageBox.Show("Operacją która próbujesz wykonać może zostać przeprowadzona tylko przez Administratora Nadrzednego", "Uwaga!", MessageBoxButton.OK, MessageBoxImage.Error);
				}
		
		}
		private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
		{
			if (e.Column is DataGridCheckBoxColumn)
			{
				var item = e.Row.Item as Madmins;

				bool? newValue = ((CheckBox)e.EditingElement).IsChecked;
				if (Application.Current.Properties["IfMain"].ToString() == "True")
				{
					var id = item.ID;
					OnChecked((bool)newValue, id);
				}
				else
				{
					MessageBox.Show("Operacją która próbujesz wykonać może zostać przeprowadzona tylko przez Administratora Nadrzednego", "Uwaga!", MessageBoxButton.OK, MessageBoxImage.Error);
					if(newValue == true)
						((CheckBox)e.EditingElement).IsChecked = false;
					else
						((CheckBox)e.EditingElement).IsChecked = true;
				}
			}
		}
		string HashPasword(string password)
		{
			const int keySize = 64;
			const int iterations = 350000;
			HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
			byte[] salt = RandomNumberGenerator.GetBytes(keySize);
			Salt = Convert.ToBase64String(salt);
			var hash = Rfc2898DeriveBytes.Pbkdf2(
				Encoding.UTF8.GetBytes(password),
				salt,
				iterations,
				hashAlgorithm,
				keySize);
			return Convert.ToHexString(hash);
		}
		private void OnSubmit(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrEmpty(FnameForm.Text) && !string.IsNullOrEmpty(LnameForm.Text) && !string.IsNullOrEmpty(EmailForm.Text) && !string.IsNullOrEmpty(PassForm.Password) && !string.IsNullOrEmpty(PassConfForm.Password))
			{
				MySqlConnection conn = new MySqlConnection(ConnectionString);
				conn.Open();
				MySqlCommand cmd = conn.CreateCommand();
				cmd.CommandText = $"SELECT * FROM admins WHERE Email = '{EmailForm.Text}'";
				cmd.ExecuteNonQuery();
				MySqlDataReader reader = cmd.ExecuteReader();
				if (!reader.Read())
				{
					reader.Close();
					if (PassForm.Password == PassConfForm.Password)
					{
						if (Regex.IsMatch(PassForm.Password,pattern))
						{
							cmd = conn.CreateCommand();
							cmd.CommandText = $"INSERT INTO admins VALUES(null,'{FnameForm.Text}','{LnameForm.Text}','{EmailForm.Text}','{HashPasword(PassForm.Password)}',0,'{Salt}')";
							cmd.ExecuteNonQuery();
							FnameForm.Text = "";
							LnameForm.Text = "";
							EmailForm.Text = "";
							PassForm.Password = "";
							PassConfForm.Password = "";
							DataGridView();
						}
						else
						{
							info = "Hasło powinno zawirać minimum 8 znaków,\n znaki specjalne, cyfry i duże litery";
						}
					}
					else
					{
						info = "Prosze się upewnić że podane hasła są takie \n same";
					}
				}
				else
				{
					info = "Administrator z takim adresem email już istnieje";
				}
				conn.Close();
			}
			else
			{
				info = "Proszę uzupełnić wszytkie pola";
			}
			InfoLabel.Content = info;
		}
	}
}
