using System.Security.Cryptography;
using UserApp.Models;

namespace UserApp;

public partial class LoginPage : ContentPage
{
	Database db;
	public LoginPage(Database db)
	{
		this.db = db;
		InitializeComponent();

	}
	private void OnLogin(object sender, EventArgs e)
	{
		
	}
	private void GoToRegister(object sender, EventArgs e)
	{
		Navigation.PushAsync(new RegistrationPage(db));
	}
	bool VerifyPassword(string password, string hash, byte[] salt)
	{
		const int keySize = 64;
		const int iterations = 350000;
		HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
		byte[] hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);
		byte[] hsc;
		if (hash.Length % 2 != 0)
		{
			hash = "0" + hash;
			hsc = Convert.FromHexString(hash);
			List<byte> bytes = new List<byte>();
			bytes.AddRange(hsc);
			bytes.RemoveAt(0);
			hsc = bytes.ToArray();
		}
		else
		{
			hsc = Convert.FromHexString(hash);
		}
		return hashToCompare.SequenceEqual(hsc);
	}

}