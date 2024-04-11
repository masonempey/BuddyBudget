using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuddyBudget.Entity;
using BuddyBudget.Database;
using System.Diagnostics;
using BuddyBudget.Database.Repositories;

namespace BuddyBudget.Drawables
{
	public class GraphDrawViewModel
	{ 
		public string UserName { get; set; }

		private readonly DatabaseManager databaseManager;
		private readonly BudgetRepository budgetRepository;
		public GraphDrawViewModel(string userName)
		{
			UserName = userName;
			databaseManager = new DatabaseManager();
			budgetRepository = new BudgetRepository(databaseManager.GetConnectionString(), userName);
		}

		public double RemainingMonthlyBudget;
		public double OriginalMonthlyBudget;
		public double OriginalHousingBudget;
		public double RemainingHousingBudget;
		public double OriginalUtilitiesBudget;
		public double RemainingUtilitiesBudget;
		public double OriginalGroceriesBudget;
		public double RemainingGroceriesBudget;
		public double OriginalTransportationBudget;
		public double RemainingTransportationBudget;
		public double OriginalEntertainmentBudget;
		public double RemainingEntertainmentBudget;
		public double OriginalMiscellaneousBudget;
		public double RemainingMiscellaneousBudget;
		public double OriginalSavingsBudget;
		public double RemainingSavingsBudget;
		public double OriginalDebtBudget;
		public double RemainingDebtBudget;
		public double GraphScaleValue;
		public void RetrieveOriginalBudgetForGraph()
		{
			try
			{
				string connectionString = databaseManager.ConnectionString;

				Budget budget = budgetRepository.ReturnUsersOriginalBudget(UserName);

				GraphScaleValue = budgetRepository.RetrieveBudgetGraphScale(UserName);

				UserName = budget.UserName;
				OriginalMonthlyBudget = budget.MonthlyBudget;
				OriginalHousingBudget = budget.HousingBudget;
				OriginalUtilitiesBudget = budget.UtilitiesBudget;
				OriginalGroceriesBudget = budget.GroceriesBudget;
				OriginalTransportationBudget = budget.TransportationBudget;
				OriginalEntertainmentBudget = budget.EntertainmentBudget;
				OriginalMiscellaneousBudget = budget.MiscellaneousBudget;
				OriginalSavingsBudget = budget.SavingsBudget;
				OriginalDebtBudget = budget.DebtBudget;
			}
			catch
			{
				Debug.WriteLine("Error in CalculateGraph()");
			}
		}

		public void RetrieveBudgetForGraph()
		{
			try
			{
				string connectionString = databaseManager.ConnectionString;
				BudgetRepository budgetRepository = new BudgetRepository(connectionString, UserName);
				Budget budget = budgetRepository.ReturnUsersBudgetByName(UserName);

				RemainingMonthlyBudget = budget.RemainingBudget;
				RemainingHousingBudget = budget.HousingBudget;
				RemainingUtilitiesBudget = budget.UtilitiesBudget;
				RemainingGroceriesBudget = budget.GroceriesBudget;
				RemainingTransportationBudget = budget.TransportationBudget;
				RemainingEntertainmentBudget = budget.EntertainmentBudget;
				RemainingMiscellaneousBudget = budget.MiscellaneousBudget;
				RemainingSavingsBudget = budget.SavingsBudget;
				RemainingDebtBudget = budget.DebtBudget;
				GraphScaleValue = budget.BudgetGraphScale;

				Debug.WriteLine(RemainingMonthlyBudget);
			}
			catch
			{
				Debug.WriteLine("Error in CalculateGraph()");
			}
		}
	}
}
