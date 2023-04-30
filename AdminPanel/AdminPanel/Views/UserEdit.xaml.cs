using AdminPanel.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection.PortableExecutable;
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
    /// Logika interakcji dla klasy UserEdit.xaml
    /// </summary>
    public partial class UserEdit : UserControl
    {
        public UserEdit(ContentControl contentControl, int ID)
        {
            InitializeComponent();
            this.contentControl = contentControl;
            this.ID = ID;
			OnLoad();
			AdminName.Content = Application.Current.Properties["Name"].ToString();
		}
        ContentControl contentControl;
		Musers user;
		string info = "";
        int ID;
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
		private void Delete(object sender, RoutedEventArgs e)
		{
			MySqlConnection conn = new MySqlConnection(ConnectionString);
			conn.Open();
			if (Application.Current.Properties["IfMain"].ToString() == "True")
			{
				MySqlCommand cmd = conn.CreateCommand();
				cmd.CommandText = $"DELETE FROM Users WHERE ID = {ID}";
				cmd.ExecuteNonQuery();
				conn.Close();
				contentControl.Content = new Users(contentControl);
			}
			else
			{
				MessageBox.Show("Operacją która próbujesz wykonać może zostać przeprowadzona tylko przez Administratora Nadrzednego", "Uwaga!", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
		private void OnLoad()
		{
			MySqlConnection conn = new MySqlConnection(ConnectionString);
			conn.Open();
			MySqlCommand cmd = conn.CreateCommand();
			cmd.CommandText = $"SELECT * FROM Users WHERE ID = {ID}";
			cmd.ExecuteNonQuery();
			MySqlDataReader reader = cmd.ExecuteReader();
			if(reader.Read())
			{
				user = new Musers() { ID = reader.GetInt32("ID"), FirstName = reader.GetString("FirstName"), LastName = reader.GetString("LastName"), Email = reader.GetString("Email"), Adress = reader.GetString("Adress"), BirthDate = reader.GetString("BirthDate"), BirthPlace = reader.GetString("BirthPlace"), Grade = reader.GetString("Grade"), GraduationYear= reader.GetString("GraduationYear"), Phone = reader.GetString("Phone"), SchoolName = reader.GetString("SchoolName"), WorkPlace = reader.GetString("WorkPlace") };
				FnameForm.Text = user.FirstName;
				LnameForm.Text = user.LastName;
				BirthDateForm.Text = user.BirthDate;
				BirthDateForm.SelectedDate = Convert.ToDateTime(user.BirthDate);
				BirthPlaceForm.Text = user.BirthPlace;
				PhoneForm.Text = user.Phone;
				EmailForm.Text = user.Email;
				AdressForm.Text = user.Adress;
				GradeForm.Text = user.Grade;
				SchoolNameForm.Text = user.SchoolName;
				GradeYearForm.Text = user.GraduationYear;
				WorkForm.Text = user.WorkPlace;
			}
			else
			{
				contentControl.Content = new Users(contentControl);
			}
			conn.Close();
		}
		private void OnSubmit(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrEmpty(FnameForm.Text) && !string.IsNullOrEmpty(LnameForm.Text) && !string.IsNullOrEmpty(EmailForm.Text) && !string.IsNullOrEmpty(AdressForm.Text) && !string.IsNullOrEmpty(GradeForm.Text) && !string.IsNullOrEmpty(SchoolNameForm.Text) && !string.IsNullOrEmpty(GradeYearForm.Text) && !string.IsNullOrEmpty(PhoneForm.Text) && !string.IsNullOrEmpty(WorkForm.Text) && !string.IsNullOrEmpty(BirthPlaceForm.Text) && BirthDateForm.SelectedDate != null)
			{
				MySqlConnection conn = new MySqlConnection(ConnectionString);
				if (EmailForm.Text != user.Email)
				{
					conn.Open();
					MySqlCommand cmd = conn.CreateCommand();
					cmd.CommandText = $"SELECT * FROM Users WHERE Email = '{EmailForm.Text}'";
					cmd.ExecuteNonQuery();
					MySqlDataReader reader = cmd.ExecuteReader();
					if (!reader.Read())
					{
						reader.Close();
						cmd = conn.CreateCommand();
						cmd.CommandText = $"UPDATE users SET FirstName='{FnameForm.Text}', LastName='{LnameForm.Text}',BirthDate='{BirthDateForm.SelectedDate}',BirthPlace = '{BirthPlaceForm.Text}',Phone='{PhoneForm.Text}',Email='{EmailForm.Text}',Adress='{AdressForm.Text}',Grade='{GradeForm.Text}',SchoolName='{SchoolNameForm.Text}',GraduationYear='{GradeYearForm.Text}',WorkPlace='{WorkForm.Text}' WHERE ID={ID}";
						cmd.ExecuteNonQuery();
						conn.Close();
						contentControl.Content = new Users(contentControl);
					}
					else
					{
						info = "Użytkownik z takim adresem email już istnieje";
					}
				}
				else
				{
					conn.Open();
					MySqlCommand cmd = conn.CreateCommand();
					cmd.CommandText = $"UPDATE users SET FirstName='{FnameForm.Text}', LastName='{LnameForm.Text}',BirthDate='{BirthDateForm.SelectedDate}',BirthPlace = '{BirthPlaceForm.Text}',Phone='{PhoneForm.Text}',Email='{EmailForm.Text}',Adress='{AdressForm.Text}',Grade='{GradeForm.Text}',SchoolName='{SchoolNameForm.Text}',GraduationYear='{GradeYearForm.Text}',WorkPlace='{WorkForm.Text}' WHERE ID={ID}";
					cmd.ExecuteNonQuery();
					conn.Close();
					contentControl.Content = new Users(contentControl);
				}
			}
			else
			{
				info = "Proszę uzupełnić wszytkie pola";
			}
			InfoLabel.Content = info;
		}
	}
}
