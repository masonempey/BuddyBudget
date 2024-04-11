using BuddyBudget.Database;
using BuddyBudget.Entity;
using System.Diagnostics;

namespace BuddyBudget
{
	public partial class MainPage : ContentPage
	{
		public bool ValidNameEntry = false;

		public MainPage()
		{
			InitializeComponent();
		}

		private void GoToBudgetHome_Clicked(object sender, EventArgs e)
		{
			try
			{
				if (CheckNameEntry())
				{

					Debug.WriteLine("Initializing database...");
					DatabaseManager databaseManager = new DatabaseManager();
					databaseManager.InitializeDatabase();
					Debug.WriteLine("Database initialized.");

					string connectionString = databaseManager.GetConnectionString();

					string userName = RetrieveNameEntry();
					if (userName != null)
					{
						try
						{
							App.Current.MainPage = new BudgetHome(userName, connectionString);
						}
						catch (Exception ex)
						{
							Debug.WriteLine(ex.Message);
						}
					}
					else
					{
						Debug.WriteLine("Error retrieving user from entry.");
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}

		private bool CheckNameEntry()
		{
			try
			{
				string firstName = FirstName.Text;
				string lastName = LastName.Text;

				if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
				{
					DisplayAlert("Error", "Please enter a first and last name", "OK");
					ValidNameEntry = false;
					return ValidNameEntry;
				}
				else
				{
					ValidNameEntry = true;
					return ValidNameEntry;
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				ValidNameEntry = false;
				return ValidNameEntry;
			}
		}
		public string RetrieveNameEntry()
		{
			try
			{
				string firstName = FirstName.Text;
				string lastName = LastName.Text;
				string userName = firstName + " " + lastName;
				return userName;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				return null;
			}

		}
	}

}
