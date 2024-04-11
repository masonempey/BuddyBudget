using BuddyBudget.Entity;
using Microsoft.Data.Sqlite;
using System.Diagnostics;
using System.Text;

namespace BuddyBudget.Database.Repositories
{
	public class BudgetRepository
	{
		private readonly string _connectionString;
		private readonly string _userName;

		public BudgetRepository(string connectionString, string userName)
		{
			_connectionString = connectionString;
			_userName = userName;
		}

		public bool DoesTableExist(string tableName)
		{
			try
			{
				using (var connection = new SqliteConnection(_connectionString))
				{
					connection.Open();

					string query = $"SELECT name FROM sqlite_master WHERE type='table' AND name='{tableName}'";

					using (var command = new SqliteCommand(query, connection))
					{
						using (var reader = command.ExecuteReader())
						{
							return reader.Read();
						}
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Error checking table existence: {ex.Message}");
				throw;
			}
		}

		public Budget ReturnUsersOriginalBudget(string userName)
		{
			Budget budget = null;

			try
			{
				using (var connection = new SqliteConnection(_connectionString))
				{
					connection.Open();

					Debug.WriteLine($"Attempting to retrieve original Budget for Username: {userName}");

					string query = "SELECT * FROM OriginalBudget WHERE Username = @UserName";

					Debug.WriteLine($"Executing SQL query: {query}");

					using (var command = new SqliteCommand(query, connection))
					{
						command.Parameters.AddWithValue("@UserName", userName);

						using (var reader = command.ExecuteReader())
						{
							if (reader.Read())
							{
								budget = new Budget
								{
									UserName = userName,
									MonthlyBudget = reader.GetDouble(reader.GetOrdinal("MonthlyBudget")),
									HousingBudget = reader.GetDouble(reader.GetOrdinal("HousingBudget")),
									UtilitiesBudget = reader.GetDouble(reader.GetOrdinal("UtilitiesBudget")),
									GroceriesBudget = reader.GetDouble(reader.GetOrdinal("GroceriesBudget")),
									TransportationBudget = reader.GetDouble(reader.GetOrdinal("TransportationBudget")),
									EntertainmentBudget = reader.GetDouble(reader.GetOrdinal("EntertainmentBudget")),
									MiscellaneousBudget = reader.GetDouble(reader.GetOrdinal("MiscellaneousBudget")),
									SavingsBudget = reader.GetDouble(reader.GetOrdinal("SavingsBudget")),
									DebtBudget = reader.GetDouble(reader.GetOrdinal("DebtBudget"))
								};
							}
							else
							{
								Debug.WriteLine($"Budget not found for user: {userName}");
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Error retrieving original budget: {ex.Message}");
				throw;
			}
			if (budget != null)
			{
				return budget;
			}
			else
			{
				budget = new Budget
				{
					UserName = userName,
					RemainingBudget = 0,
					HousingBudget = 0,
					UtilitiesBudget = 0,
					GroceriesBudget = 0,
					TransportationBudget = 0,
					EntertainmentBudget = 0,
					MiscellaneousBudget = 0,
					SavingsBudget = 0,
					DebtBudget = 0,
					BudgetGraphScale = 0
				};
				return budget;
			}
		}

		public Budget ReturnUsersBudgetByName(string userName)
		{
			try
			{
				Budget budget = null;

				using (var connection = new SqliteConnection(_connectionString))
				{
					Debug.WriteLine(_connectionString);
					connection.Open();

					Debug.WriteLine($"Attempting to retrieve Budget for Username: {userName}");

					string query = "SELECT * FROM Budgets WHERE Username = @UserName";


					Debug.WriteLine($"Executing SQL query: {query}");

					using (var command = new SqliteCommand(query, connection))
					{
						command.Parameters.AddWithValue("@UserName", userName);

						using (var reader = command.ExecuteReader())
						{
							if (reader.Read())
							{
								budget = new Budget
								{
									UserName = userName,
									RemainingBudget = reader.GetDouble(reader.GetOrdinal("RemainingBudget")),
									HousingBudget = reader.GetDouble(reader.GetOrdinal("HousingBudget")),
									UtilitiesBudget = reader.GetDouble(reader.GetOrdinal("UtilitiesBudget")),
									GroceriesBudget = reader.GetDouble(reader.GetOrdinal("GroceriesBudget")),
									TransportationBudget = reader.GetDouble(reader.GetOrdinal("TransportationBudget")),
									EntertainmentBudget = reader.GetDouble(reader.GetOrdinal("EntertainmentBudget")),
									MiscellaneousBudget = reader.GetDouble(reader.GetOrdinal("MiscellaneousBudget")),
									SavingsBudget = reader.GetDouble(reader.GetOrdinal("SavingsBudget")),
									DebtBudget = reader.GetDouble(reader.GetOrdinal("DebtBudget")),
									BudgetGraphScale = reader.GetDouble(reader.GetOrdinal("BudgetGraphScale"))
								};
								Debug.WriteLine($"Budget found for user: {userName}");
								Debug.WriteLine($"UserName: {budget.UserName}");
								Debug.WriteLine($"RemainingBudget: {budget.RemainingBudget}");
								Debug.WriteLine($"HousingBudget: {budget.HousingBudget}");
								Debug.WriteLine($"UtilitiesBudget: {budget.UtilitiesBudget}");
								Debug.WriteLine($"GroceriesBudget: {budget.GroceriesBudget}");
								Debug.WriteLine($"TransportationBudget: {budget.TransportationBudget}");
								Debug.WriteLine($"EntertainmentBudget: {budget.EntertainmentBudget}");
								Debug.WriteLine($"MiscellaneousBudget: {budget.MiscellaneousBudget}");
								Debug.WriteLine($"SavingsBudget: {budget.SavingsBudget}");
								Debug.WriteLine($"DebtBudget: {budget.DebtBudget}");
								Debug.WriteLine($"BudgetGraphScale: {budget.BudgetGraphScale}");
							}
							else
							{
								Debug.WriteLine($"Budget not found for user: {userName}");
							}
						}
					}
				}
				return budget;
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Error retrieving budget: {ex.Message}");
				throw;
			}
		}

		public double RetrieveBudgetGraphScale(string userName)
		{
			double budgetGraphScale = 0;

			using (var connection = new SqliteConnection(_connectionString))
			{
				connection.Open();

				Debug.WriteLine($"Attempting to retrieve Budget Scale for Username: {userName}");

				string query = "SELECT BudgetGraphScale FROM Budgets WHERE Username = @UserName";

				Debug.WriteLine($"Executing SQL query: {query}");

				using (var command = new SqliteCommand(query, connection))
				{
					command.Parameters.AddWithValue("@UserName", userName);

					using (var reader = command.ExecuteReader())
					{
						if (reader.Read())
						{
							budgetGraphScale = reader.GetDouble(reader.GetOrdinal("BudgetGraphScale"));
							Debug.WriteLine($"Budget Scale found for user: {userName}");
							Debug.WriteLine($"Budget Scale: {budgetGraphScale}");
						}
						else
						{
							Debug.WriteLine($"Budget Scale not found for user: {userName}");
						}
					}
				}
			}

			return budgetGraphScale;
		}

		public long CheckIfUserHasOriginalBudget(string userName)
		{
			bool tableExists = DoesTableExist("OriginalBudget");
			Debug.WriteLine($"Does YourActualTableNameHere table exist? {tableExists}");

			try
			{
				using (var connection = new SqliteConnection(_connectionString))
				{
					connection.Open();

					string select = "SELECT COUNT (*) FROM OriginalBudget WHERE UserName = @UserName";

					using (var command = connection.CreateCommand())
					{
						command.CommandText = select;
						command.Parameters.AddWithValue("@UserName", userName);

						Object result = command.ExecuteScalar();

						Debug.WriteLine($"Checking if user has original budget: {userName}");

						if (result != null && result != DBNull.Value)
						{
							Debug.WriteLine($"User has original budget: {userName}");
							return Convert.ToInt64(result);
						}
						else
						{
							Debug.WriteLine($"User does not have original budget: {userName}");
							return 0;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Error checking if user has original budget: {ex.Message}");
				throw;
			}
		}

		public void InsertOriginalBudget(Budget budget)
		{
			try
			{
				using (var connection = new SqliteConnection(_connectionString))
				{
					connection.Open();

					string insert = @"
				INSERT INTO OriginalBudget
				(UserName, MonthlyBudget, HousingBudget, UtilitiesBudget, GroceriesBudget, TransportationBudget, EntertainmentBudget, MiscellaneousBudget, SavingsBudget, DebtBudget) 
				VALUES (@UserName, @MonthlyBudget, @HousingBudget, @UtilitiesBudget, @GroceriesBudget, @TransportationBudget, @EntertainmentBudget, @MiscellaneousBudget, @SavingsBudget, @DebtBudget)";


					using (var command = connection.CreateCommand())
					{
						command.CommandText = insert;
						command.Parameters.AddWithValue("@UserName", budget.UserName);
						command.Parameters.AddWithValue("@MonthlyBudget", budget.MonthlyBudget);
						command.Parameters.AddWithValue("@HousingBudget", budget.HousingBudget);
						command.Parameters.AddWithValue("@UtilitiesBudget", budget.UtilitiesBudget);
						command.Parameters.AddWithValue("@GroceriesBudget", budget.GroceriesBudget);
						command.Parameters.AddWithValue("@TransportationBudget", budget.TransportationBudget);
						command.Parameters.AddWithValue("@EntertainmentBudget", budget.EntertainmentBudget);
						command.Parameters.AddWithValue("@MiscellaneousBudget", budget.MiscellaneousBudget);
						command.Parameters.AddWithValue("@SavingsBudget", budget.SavingsBudget);
						command.Parameters.AddWithValue("@DebtBudget", budget.DebtBudget);

						command.ExecuteNonQuery();

						Debug.WriteLine($"Original Budget inserted for user: {budget.UserName} with ID: {budget.UserId}");
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Error inserting Original budget: {ex.Message}");
				throw;
			}
		}

		public void UpdateOriginalBudget(Budget originalBudget)
		{
			try
			{
				using (var connection = new SqliteConnection(_connectionString))
				{
					connection.Open();

					string updateQuery = @"
                UPDATE OriginalBudget 
                SET MonthlyBudget = @MonthlyBudget,
                    HousingBudget = @HousingBudget,
                    UtilitiesBudget = @UtilitiesBudget,
                    GroceriesBudget = @GroceriesBudget,
                    TransportationBudget = @TransportationBudget,
                    EntertainmentBudget = @EntertainmentBudget,
                    MiscellaneousBudget = @MiscellaneousBudget,
                    SavingsBudget = @SavingsBudget,
                    DebtBudget = @DebtBudget 
                WHERE UserName = @UserName";

					using (var command = connection.CreateCommand())
					{
						command.CommandText = updateQuery;
						command.Parameters.AddWithValue("@MonthlyBudget", originalBudget.MonthlyBudget);
						command.Parameters.AddWithValue("@HousingBudget", originalBudget.HousingBudget);
						command.Parameters.AddWithValue("@UtilitiesBudget", originalBudget.UtilitiesBudget);
						command.Parameters.AddWithValue("@GroceriesBudget", originalBudget.GroceriesBudget);
						command.Parameters.AddWithValue("@TransportationBudget", originalBudget.TransportationBudget);
						command.Parameters.AddWithValue("@EntertainmentBudget", originalBudget.EntertainmentBudget);
						command.Parameters.AddWithValue("@MiscellaneousBudget", originalBudget.MiscellaneousBudget);
						command.Parameters.AddWithValue("@SavingsBudget", originalBudget.SavingsBudget);
						command.Parameters.AddWithValue("@DebtBudget", originalBudget.DebtBudget);
						command.Parameters.AddWithValue("@UserName", originalBudget.UserName);

						command.ExecuteNonQuery();

						Debug.WriteLine($"Original budget updated for user: {originalBudget.UserName}");
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Error updating original budget: {ex.Message}");
				throw;
			}
		}

		public void InsertBudget(Budget budget)
		{
			try
			{
				using (var connection = new SqliteConnection(_connectionString))
				{
					connection.Open();
					InsertBudgetRecord(connection, budget);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Error inserting budget: {ex.Message}");
				throw;
			}
		}


		private void InsertBudgetRecord(SqliteConnection connection, Budget budget)
		{
			string insertQuery = @"
        INSERT INTO Budgets 
        (UserName, RemainingBudget, HousingBudget, UtilitiesBudget, GroceriesBudget, 
        TransportationBudget, EntertainmentBudget, MiscellaneousBudget, SavingsBudget, 
        DebtBudget, BudgetGraphScale) 
        VALUES 
        (@UserName, @RemainingBudget, @HousingBudget, @UtilitiesBudget, @GroceriesBudget, 
        @TransportationBudget, @EntertainmentBudget, @MiscellaneousBudget, @SavingsBudget, 
        @DebtBudget, @BudgetGraphScale)";

			using (var command = connection.CreateCommand())
			{
				command.CommandText = insertQuery;
				command.Parameters.AddWithValue("@UserName", budget.UserName);
				command.Parameters.AddWithValue("@RemainingBudget", budget.RemainingBudget);
				command.Parameters.AddWithValue("@HousingBudget", budget.HousingBudget);
				command.Parameters.AddWithValue("@UtilitiesBudget", budget.UtilitiesBudget);
				command.Parameters.AddWithValue("@GroceriesBudget", budget.GroceriesBudget);
				command.Parameters.AddWithValue("@TransportationBudget", budget.TransportationBudget);
				command.Parameters.AddWithValue("@EntertainmentBudget", budget.EntertainmentBudget);
				command.Parameters.AddWithValue("@MiscellaneousBudget", budget.MiscellaneousBudget);
				command.Parameters.AddWithValue("@SavingsBudget", budget.SavingsBudget);
				command.Parameters.AddWithValue("@DebtBudget", budget.DebtBudget);
				command.Parameters.AddWithValue("@BudgetGraphScale", budget.BudgetGraphScale);

				command.ExecuteNonQuery();

				Debug.WriteLine($"Budget inserted for user: {budget.UserName} with ID: {budget.UserId}");
			}
		}

		public void UpdateBudget(Budget budget)
		{
			try
			{
				using (var connection = new SqliteConnection(_connectionString))
				{
					connection.Open();
					
					string updateQuery = @"
                UPDATE Budgets 
                SET RemainingBudget = @RemainingBudget,
					HousingBudget = @HousingBudget,
                    UtilitiesBudget = @UtilitiesBudget,
                    GroceriesBudget = @GroceriesBudget,
                    TransportationBudget = @TransportationBudget,
                    EntertainmentBudget = @EntertainmentBudget,
                    MiscellaneousBudget = @MiscellaneousBudget,
                    SavingsBudget = @SavingsBudget,
                    DebtBudget = @DebtBudget,
					BudgetGraphScale = @BudgetGraphScale 
                WHERE UserName = @UserName";

					using (var command = connection.CreateCommand())
					{
						command.CommandText = updateQuery;
						command.Parameters.AddWithValue("@RemainingBudget", budget.RemainingBudget);
						command.Parameters.AddWithValue("@HousingBudget", budget.HousingBudget);
						command.Parameters.AddWithValue("@UtilitiesBudget", budget.UtilitiesBudget);
						command.Parameters.AddWithValue("@GroceriesBudget", budget.GroceriesBudget);
						command.Parameters.AddWithValue("@TransportationBudget", budget.TransportationBudget);
						command.Parameters.AddWithValue("@EntertainmentBudget", budget.EntertainmentBudget);
						command.Parameters.AddWithValue("@MiscellaneousBudget", budget.MiscellaneousBudget);
						command.Parameters.AddWithValue("@SavingsBudget", budget.SavingsBudget);
						command.Parameters.AddWithValue("@DebtBudget", budget.DebtBudget);
						command.Parameters.AddWithValue("@BudgetGraphScale", budget.BudgetGraphScale);
						command.Parameters.AddWithValue("@UserName", budget.UserName);

						command.ExecuteNonQuery();

						Debug.WriteLine($"Budget updated for user with UserId: {budget.UserId}");
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Error updating budget: {ex.Message}");
				throw;
			}
		}
	}
}
