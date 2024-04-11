using BuddyBudget.Database;
using BuddyBudget.Database.Repositories;
using BuddyBudget.Drawables;
using BuddyBudget.Entity;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Windows.Input;

namespace BuddyBudget
{
	public partial class BudgetHome : ContentPage
	{
		private readonly string _connectionString;
		private readonly GraphDrawViewModel graphDrawViewModel;
		private readonly BudgetRepository budgetRepository;
		private readonly BudgetViewModel viewModel;

		public bool IsButtonClicked = false;
		public bool CreateColorChanged = false;
		public bool ViewColorChanged = false;
		public bool UserExists = false;
		public bool IsGenerateBudgetClicked = false;
		private readonly string _userName;


		public BudgetHome(string userName, string connectionString)
		{
			InitializeComponent();

			graphDrawViewModel = new GraphDrawViewModel(userName);
			budgetRepository = new BudgetRepository(connectionString, userName);

			viewModel = new BudgetViewModel(connectionString, userName, this);

			_connectionString = connectionString;
			BindingContext = viewModel;

			DisableAllViews();

			_userName = userName;

			VerifyUserBudget(userName);
		}

		public void DisableAllViews()
		{
			viewModel.IsBudgetViewVisible = false;
			viewModel.IsBudgetOptionScreenVisible = false;
			viewModel.IsViewLayoutVisible = false;
		}

		public Budget RetrieveBudget()
		{
			try
			{
				
				Budget budget = budgetRepository.ReturnUsersBudgetByName(_userName);

				return budget;
			}
			catch
			{
				Debug.WriteLine("Error in GetBudget()");
				return null;
			}
		}

		public void GenerateBudget(object sender, EventArgs e)
		{
			try
			{

				string userName = _userName;
				double monthlyBudgetValue = ParseDouble(MonthlyBudgetEntry.Text);
				double housingBudgetValue = HousingEntry.Text != null ? ParseDouble(HousingEntry.Text) : 0.0;
				double utilitiesBudgetValue = UtilitiesEntry.Text != null ? ParseDouble(UtilitiesEntry.Text) : 0.0;
				double groceriesBudgetValue = GroceriesEntry.Text != null ? ParseDouble(GroceriesEntry.Text) : 0.0;
				double transportationBudgetValue = TransportationEntry.Text != null ? ParseDouble(TransportationEntry.Text) : 0.0;
				double entertainmentBudgetValue = EntertainmentEntry.Text != null ? ParseDouble(EntertainmentEntry.Text) : 0.0;
				double miscellaneousBudgetValue = MiscellaneousEntry.Text != null ? ParseDouble(MiscellaneousEntry.Text) : 0.0;
				double savingsBudgetValue = SavingsEntry.Text != null ? ParseDouble(SavingsEntry.Text) : 0.0;
				double debtBudgetValue = DebtEntry.Text != null ? ParseDouble(DebtEntry.Text) : 0.0;

				double totalExpenses = housingBudgetValue + utilitiesBudgetValue + groceriesBudgetValue +
					transportationBudgetValue + entertainmentBudgetValue + miscellaneousBudgetValue +
					savingsBudgetValue + debtBudgetValue;

				if (totalExpenses > monthlyBudgetValue)
				{
					DisplayAlert("Invalid Budget", "Total expenses cannot exceed the monthly budget", "OK");
					return;
				}

				User userReturned = User.CreateUserIfNotExists(_connectionString, userName);

				if (userReturned.UserId > 0)
				{
					UserExists = true;
				}
				else if (userReturned.UserId == 0)
				{
					UserExists = false;
				}

				GraphScaleValue = CalculateScaleValue(housingBudgetValue, utilitiesBudgetValue, groceriesBudgetValue, transportationBudgetValue, entertainmentBudgetValue, miscellaneousBudgetValue, savingsBudgetValue, debtBudgetValue);

				Budget Originalbudget = new Budget
				{
					UserName = userName,
					MonthlyBudget = monthlyBudgetValue,
					HousingBudget = housingBudgetValue,
					UtilitiesBudget = utilitiesBudgetValue,
					GroceriesBudget = groceriesBudgetValue,
					TransportationBudget = transportationBudgetValue,
					EntertainmentBudget = entertainmentBudgetValue,
					MiscellaneousBudget = miscellaneousBudgetValue,
					SavingsBudget = savingsBudgetValue,
					DebtBudget = debtBudgetValue,
					UserId = userReturned.UserId,
				};

				Budget budget = new Budget
				{
					UserName = userName,
					RemainingBudget = monthlyBudgetValue,
					HousingBudget = housingBudgetValue,
					UtilitiesBudget = utilitiesBudgetValue,
					GroceriesBudget = groceriesBudgetValue,
					TransportationBudget = transportationBudgetValue,
					EntertainmentBudget = entertainmentBudgetValue,
					MiscellaneousBudget = miscellaneousBudgetValue,
					SavingsBudget = savingsBudgetValue,
					DebtBudget = debtBudgetValue,
					BudgetGraphScale = GraphScaleValue,
					UserId = userReturned.UserId
				};

				if (UserExists)
				{
					try
					{
						long count = budgetRepository.CheckIfUserHasOriginalBudget(userName);
						if (count > 0)
						{
							budgetRepository.UpdateBudget(budget);
							budgetRepository.UpdateOriginalBudget(Originalbudget);
						}
						else
						{
							budgetRepository.InsertBudget(budget);
							budgetRepository.InsertOriginalBudget(Originalbudget);
						}
						Debug.WriteLine("Budget Inserted");

						viewModel.CheckAndDisplayBudget(userName);
					}
					catch (Exception ex)
					{
						Debug.WriteLine("Error updating budget", ex.Message);
					}
				}
				else
				{
					try
					{
						Budget.CreateBudget(_connectionString, budget, userName);
						Debug.WriteLine("Budget Created and inserted into database");
						DisplayAlert("Budget Created", "Your budget has been created", "OK");

						ResetValues();
					}
					catch (Exception ex)
					{
						Debug.WriteLine("Error creating budget", ex.Message);
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Error on generating your Budget", ex.Message);
			}
		}

		public void VerifyUserBudget(string userName)
		{
			Budget budgetFound = budgetRepository.ReturnUsersBudgetByName(userName);

			if (budgetFound != null)
			{
				viewModel.ExecuteLayouts(true, false, false);
			}
			else
			{
				viewModel.ExecuteLayouts(false, true, false);
			}
		}

		double GraphScaleValue;
		public double CalculateScaleValue(double housingExpense, double utilitiesExpense, double groceriesExpense, double transportationExpense, double entertainmentExpense, double miscellaneousExpense, double savingsExpense, double debtExpense)
		{
			double maxGraphHeight = 430;
			double totalBudgetValue = housingExpense + utilitiesExpense + groceriesExpense + transportationExpense + entertainmentExpense + miscellaneousExpense + savingsExpense + debtExpense;

			if (totalBudgetValue > 0)
			{
				// Calculate the magnitude of the total budget value using logarithm base 10
				double magnitude = Math.Log10(totalBudgetValue);

				// Calculate the scale inversely proportional to the magnitude
				double scale = maxGraphHeight / (1 + magnitude);

				// Adjust the scale to fall within the range of 0.6 to 0.9
				GraphScaleValue = (scale - 100) / 450 * 0.3 + 0.6;
			}
			else
			{
				GraphScaleValue = 1.0;
			}

			return GraphScaleValue;
		}

		public void SaveExpense(object sender, EventArgs e)
		{
			try
			{
				if (sender is ImageButton button)
				{
					double HousingExpense = 0.0;
					double UtilitiesExpense = 0.0;
					double GroceriesExpense = 0.0;
					double TransportationExpense = 0.0;
					double EntertainmentExpense = 0.0;
					double MiscellaneousExpense = 0.0;
					double SavingsExpense = 0.0;
					double DebtExpense = 0.0;

					if (!String.IsNullOrWhiteSpace(HousingExpenseEntry.Text))
					{
						HousingExpense = ParseDouble(HousingExpenseEntry.Text);
					}
					if (!String.IsNullOrWhiteSpace(UtilitiesExpenseEntry.Text))
					{
						UtilitiesExpense = ParseDouble(UtilitiesExpenseEntry.Text);
					}
					if (!String.IsNullOrWhiteSpace(GroceriesExpenseEntry.Text))
					{
						GroceriesExpense = ParseDouble(GroceriesExpenseEntry.Text);
					}
					if (!String.IsNullOrWhiteSpace(TransportationExpenseEntry.Text))
					{
						TransportationExpense = ParseDouble(TransportationExpenseEntry.Text);
					}
					if (!String.IsNullOrWhiteSpace(EntertainmentExpenseEntry.Text))
					{
						EntertainmentExpense = ParseDouble(EntertainmentExpenseEntry.Text);
					}
					if (!String.IsNullOrWhiteSpace(MiscellaneousExpenseEntry.Text))
					{
						MiscellaneousExpense = ParseDouble(MiscellaneousExpenseEntry.Text);
					}
					if (!String.IsNullOrWhiteSpace(SavingsExpenseEntry.Text))
					{
						SavingsExpense = ParseDouble(SavingsExpenseEntry.Text);
					}
					if (!String.IsNullOrWhiteSpace(DebtExpenseEntry.Text))
					{
						DebtExpense = ParseDouble(DebtExpenseEntry.Text);
					}

					BudgetViewModel viewModel = (BudgetViewModel)BindingContext;
					viewModel.SaveExpense(HousingExpense, UtilitiesExpense, GroceriesExpense, TransportationExpense,
										  EntertainmentExpense, MiscellaneousExpense, SavingsExpense, DebtExpense);

				}
				else
				{
					Debug.WriteLine("Error saving expense");
					DisplayAlert("Error", "Please enter a valid expense", "OK");
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Error on saving your expense in BudgetHome", ex.Message);
			}
		}

		public void ResetValues()
		{
			HousingExpenseEntry.Text = null;
			UtilitiesExpenseEntry.Text = null;
			GroceriesExpenseEntry.Text = null;
			TransportationExpenseEntry.Text = null;
			EntertainmentExpenseEntry.Text = null;
			MiscellaneousExpenseEntry.Text = null;
			SavingsExpenseEntry.Text = null;
			DebtExpenseEntry.Text = null;

			if (BudgetOptionScreen.IsVisible)
			{
				viewModel.ExecuteLayouts(true, false, false);
				IsGenerateBudgetClicked = true;
				MonthlyBudgetEntry.Text = null;
				HousingEntry.Text = null;
				UtilitiesEntry.Text = null;
				GroceriesEntry.Text = null;
				TransportationEntry.Text = null;
				EntertainmentEntry.Text = null;
				MiscellaneousEntry.Text = null;
				SavingsEntry.Text = null;
				DebtEntry.Text = null;

			}
		}

		public bool SaveExpense_Clicked(object sender, EventArgs e)
		{
			try
			{
				if (sender is ImageButton button)
				{
					//double housingExpense = ParseDouble(housingExpenseEntry.Text);
					//double utilitiesExpense = ParseDouble(utilitiesExpenseEntry.Text);
					//double groceriesExpense = ParseDouble(groceriesExpenseEntry.Text);
					//double transportationExpense = ParseDouble(transportationExpenseEntry.Text);
					//double entertainmentExpense = ParseDouble(entertainmentExpenseEntry.Text);
					//double miscellaneousExpense = ParseDouble(miscellaneousExpenseEntry.Text);
					//double savingsExpense = ParseDouble(savingsExpenseEntry.Text);
					//double debtExpense = ParseDouble(debtExpenseEntry.Text);

					//viewModel.SaveExpense(housingExpense, utilitiesExpense, groceriesExpense, transportationExpense, entertainmentExpense, miscellaneousExpense, savingsExpense, debtExpense);
					return true;
				}
				else
				{
					return false;
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Error on saving your expense: " + ex.Message);
				return false;
			}
		}

		private double ParseDouble(string value)
		{
			if (double.TryParse(value, out double result))
			{
				return result;
			}
			else
			{
				throw new ArgumentException($"Invalid value: {value}");
			}
		}

		public void TestButton(object sender, EventArgs e)
		{
			Debug.WriteLine("Test Button Clicked");
		}

		public bool isBudgetHome = false;
		public void DropDown(object sender, EventArgs e)
		{
			isBudgetHome = !isBudgetHome;

			switch (isBudgetHome)
			{
				case true:
					BackgroundImageSource = "budgethome2.png";
					DashboardButton.IsVisible = true;
					ReportsButton.IsVisible = true;
					AddExpenseButton.IsVisible = true;
					LogoutButton.IsVisible = true;
					OptionsMenuLayout.IsVisible = true;
					break;
				case false:
					BackgroundImageSource = "budgethome.png";
					DashboardButton.IsVisible = false;
					ReportsButton.IsVisible = false;
					AddExpenseButton.IsVisible = false;
					LogoutButton.IsVisible = false;
					OptionsMenuLayout.IsVisible = true;
					break;
			}
		}

		public void DisableBudgetOptions(object sender, EventArgs e)
		{
			viewModel.IsBudgetOptionScreenVisible = !viewModel.IsBudgetOptionScreenVisible;

		}

		private void Button_Clicked(object sender, EventArgs e)
		{
			switch (sender)
			{
				case ImageButton dashboardbutton when dashboardbutton == DashboardButton:
					viewModel.ExecuteLayouts(true, false, false);
					break;
				case ImageButton addexpensebutton when addexpensebutton == AddExpenseButton:
					viewModel.ExecuteLayouts(false, false, true);
					break;
				case ImageButton createbudgetbutton when createbudgetbutton == CreateBudgetButton:
					GenerateBudget(sender, e);
					break;
				case ImageButton logoutbutton when logoutbutton == LogoutButton:
					GoHome(sender, e);
					break;
				case ImageButton addexpensesbutton when addexpensesbutton == AddExpensesButton:
					SaveExpense(sender, e);
					break;
				case ImageButton createnewbudgetbutton when createnewbudgetbutton == CreateNewBudgetButton:
					viewModel.ExecuteLayouts(false, true, false);
					break;
			}
		}
		private async void GoHome(object sender, EventArgs e)
		{
			App.Current.MainPage = new MainPage();
		}

		public void DisplayAlert(string title, string message, string cancel)
		{
			Application.Current.MainPage.DisplayAlert(title, message, cancel);
		}
	}
}
