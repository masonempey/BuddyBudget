using BuddyBudget.Database.Repositories;

namespace BuddyBudget.Entity
{
	public class Budget
	{
		public int BudgetId { get; set; }
		public string UserName { get; set; }
		public double MonthlyBudget { get; set; }
		public double RemainingBudget { get; set; }

		public int? UserId { get; set; }
		public User User { get; set; }

		public int? CategoryId { get; set; }
		public Category? Category { get; set; }

		public double HousingBudget { get; set; }
		public double UtilitiesBudget { get; set; }
		public double GroceriesBudget { get; set; }
		public double TransportationBudget { get; set; }
		public double EntertainmentBudget { get; set; }
		public double MiscellaneousBudget { get; set; }
		public double SavingsBudget { get; set; }
		public double DebtBudget { get; set; }
		public double BudgetGraphScale { get; set; }

		public static void CreateBudget(string connectionString, Budget budget, string UserName)
		{
			try
			{
				BudgetRepository budgetRepository = new BudgetRepository(connectionString, UserName);
				budgetRepository.InsertBudget(budget);
			}
			catch (Exception ex)
			{
				throw new Exception($"Error creating budget from CreateBudget: {ex.Message}");
			}
		}

		public static Budget DeleteBudget(int budgetId)
		{
			return new Budget
			{
				BudgetId = budgetId
			};
		}
	}
}
