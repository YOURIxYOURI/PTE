namespace UserApp;

public partial class LoginPage : ContentPage
{

	public LoginPage()
	{
		InitializeComponent();

	}
	private void OnLogin(object sender, EventArgs e)
	{
		
	}
	private void GoToRegister(object sender, EventArgs e)
	{
		Navigation.PushAsync(new RegistrationPage());
	}
}