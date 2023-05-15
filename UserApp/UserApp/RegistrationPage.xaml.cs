namespace UserApp;

public partial class RegistrationPage : ContentPage
{

	public RegistrationPage()
	{
		InitializeComponent();

	}
	private void OnRegister(object sender, EventArgs e)
	{

	}
	private void GoToLogin(object sender, EventArgs e)
	{
		Navigation.PushAsync(new LoginPage());
	}
}