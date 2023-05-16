using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System.Security.Cryptography;
using UserApp.Models;
using System.Text;
using System.Text.RegularExpressions;

namespace UserApp;

public partial class RegistrationPage : ContentPage
{
	private Database db;
	string ConnectionString;
	string Salt;
	string pattern = @"^(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*\(\)\-\=\+_])(?=.{8,})";
	public RegistrationPage(Database db)
	{
		this.db = db;
		ConnectionString = $"Server={db.Server};Database={db.Name};Uid={db.User};Pwd={db.Password};";
		InitializeComponent();
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
	private void OnRegister(object sender, EventArgs e)
	{
		if (EmailForm.Text != "" && PassForm.Text != "" && ConfPassForm.Text != "") {
			if (Regex.IsMatch(PassForm.Text, pattern))
			{
				if (ConfPassForm.Text == PassForm.Text)
				{
					MySqlConnection conn = new MySqlConnection(ConnectionString);
					conn.Open();
					MySqlCommand cmd = conn.CreateCommand();
					cmd.CommandText = $"SELECT * FROM users WHERE email ='{EmailForm.Text}'";
					cmd.ExecuteNonQuery();
					MySqlDataReader reader = cmd.ExecuteReader();
					if (reader.Read())
					{
						cmd = conn.CreateCommand();
						cmd.CommandText = $"UPDATE users SET Password='{HashPasword(PassForm.Text)}', Salt='{Salt}'";
						cmd.ExecuteNonQuery();
					}
					else
					{
						info.Text = "U¿yj adresu email poddanego we wniosku o cz³onkostwo";
					}

					conn.Close();
				}
				else
				{
					info.Text = "SprawdŸ czy has³o jest takie samow  obu polach";
				}
			}
			else
			{
				info.Text = "Has³o powinno zawiraæ minimum 8 znaków,\n znaki specjalne, cyfry i du¿e litery";
			}
		}
		else
		{
			info.Text = "Wype³nij wszytkie pola";
		}
	}
	private void GoToLogin(object sender, EventArgs e)
	{
		Navigation.PushAsync(new LoginPage(db));
	}
}