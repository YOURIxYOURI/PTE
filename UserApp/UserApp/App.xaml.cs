using UserApp.Models;

namespace UserApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		
		MainPage = new NavigationPage(new LoginPage(new Database { Name="pte", Password="",Server="localhost",User="root"}));
	}
}
