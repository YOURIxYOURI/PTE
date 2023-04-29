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
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;

namespace AdminPanel.Views
{
	/// <summary>
	/// Logika interakcji dla klasy Login.xaml
	/// </summary>
	public partial class Login : UserControl
	{
		public Login(ContentControl contentControl)
		{
			InitializeComponent();
			this.contentControl = contentControl;
		}
		ContentControl contentControl;
		string ConnectionString = $"Server={ConfigurationManager.AppSettings["Server"]};Database={ConfigurationManager.AppSettings["Database"]};Uid={ConfigurationManager.AppSettings["User"]};Pwd={ConfigurationManager.AppSettings["Password"]}";
		public string info = "";
		

		bool VerifyPassword(string password, string hash, byte[] salt)
		{
			const int keySize = 64;
			const int iterations = 350000;
			HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
			if (hash.Length % 2 != 0)
			{
				hash = "0" + hash;
			}
			byte [] hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);
			byte []hsc  = Convert.FromHexString(hash);
			List<byte> bytes= new List<byte>();
			bytes.AddRange(hsc);
			bytes.RemoveAt(0);
			hsc = bytes.ToArray();
			return hashToCompare.SequenceEqual(hsc);
		}

		public void OnLogin(object sender, RoutedEventArgs e)
		{

			if (!string.IsNullOrEmpty(PassForm.Password) && !string.IsNullOrEmpty(LogForm.Text))
			{
				MySqlConnection conn = new MySqlConnection(ConnectionString);
				conn.Open();
				MySqlCommand cmd = conn.CreateCommand();
				cmd.CommandText = $"SELECT * FROM admins WHERE email='{LogForm.Text}'";
				cmd.ExecuteNonQuery();
				MySqlDataReader reader = cmd.ExecuteReader();
				if (reader.Read())
				{
					byte[] salt = Convert.FromBase64String(reader.GetString("Salt"));
					if (VerifyPassword(PassForm.Password, reader.GetString("Password"), salt))
					{
						Application.Current.Properties["Name"] = reader.GetString("FirstName") + " " + reader.GetString("LastName");
						Application.Current.Properties["ID"] = reader.GetInt32("ID");
						Application.Current.Properties["IfMain"] = reader.GetBoolean("IfMain");
						contentControl.Content = new Users(contentControl);
					}
					else
					{
						info = "Email lub hasło nie poprawne";
					}
				}
				else
				{
					info = "Email lub hasło nie poprawne";
				}
			}
			else
			{
				info = "Wszytkie pola sa wymagane";
			}
			InfoLabel.Content = info;
		}
	}
}
