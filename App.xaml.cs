using BuddyBudget.Database;
using BuddyBudget.Entity;
using System.Diagnostics;
using System.Reflection;

namespace BuddyBudget
{ 
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			try
			{

				MainPage = new MainPage();

			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}

		}
	}
}
