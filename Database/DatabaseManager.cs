using BuddyBudget.Entity;
using Dapper;
using Microsoft.Data.Sqlite;
using System.Diagnostics;


namespace BuddyBudget.Database
{
	public class DatabaseManager
	{
		private string _connectionString;
		public DatabaseManager()
		{

			string databasePath = Path.Combine(FileSystem.AppDataDirectory, "BuddyBudget.db");
			_connectionString = $"Data Source={databasePath}";
			Debug.WriteLine($"Connection String: {_connectionString}");

		}
		public string ConnectionString => _connectionString;

		public void InitializeDatabase()
		{
			Debug.Write($"Connection String: {_connectionString}");
			try
			{
				using (var connection = new SqliteConnection(_connectionString))
				{
					connection.Open();
					Debug.WriteLine("Connection made");

					if (!DoesTableExist("Budgets"))
					{
						connection.Execute(@"
                                CREATE TABLE IF NOT EXISTS Budgets (
                                    BudgetId INTEGER PRIMARY KEY AUTOINCREMENT,
                                    UserName TEXT NOT NULL,
									RemainingBudget DOUBLE NOT NULL,
                                    HousingBudget DOUBLE NULL,
                                    UtilitiesBudget DOUBLE NULL,
                                    GroceriesBudget DOUBLE NULL,
                                    TransportationBudget DOUBLE NULL,
                                    EntertainmentBudget DOUBLE NULL,
                                    MiscellaneousBudget DOUBLE NULL,
                                    SavingsBudget DOUBLE NULL,
                                    DebtBudget DOULBE NULL,
									BudgetGraphScale DOUBLE NULL
                                );
                            ");
						Debug.WriteLine("Budgets table created successfully");
					}
					else
					{
						Debug.WriteLine("Tables already exist");
					}
					if (!DoesTableExist("Users"))
					{
						connection.Execute(@"
                                CREATE TABLE IF NOT EXISTS Users (
                                    UserId INTEGER PRIMARY KEY AUTOINCREMENT,
                                    Username TEXT NOT NULL
                                );
                            ");
						Debug.WriteLine("Users table created successfully");
					}
					else
					{
						Debug.WriteLine("Tables already exist");
					}
					if (!DoesTableExist("OriginalBudget"))
					{
						connection.Execute(@"
								CREATE TABLE IF NOT EXISTS OriginalBudget (
									OriginalBudgetId INTEGER PRIMARY KEY AUTOINCREMENT,
									UserName TEXT NOT NULL,
									MonthlyBudget DOUBLE NOT NULL,
									HousingBudget DOUBLE NULL,
									UtilitiesBudget DOUBLE NULL,
									GroceriesBudget DOUBLE NULL,
									TransportationBudget DOUBLE NULL,
									EntertainmentBudget DOUBLE NULL,
									MiscellaneousBudget DOUBLE NULL,
									SavingsBudget DOUBLE NULL,
									DebtBudget DOUBLE NULL
								);
							");
						Debug.WriteLine("OriginalBudget table created successfully");
					}
					else
					{
						Debug.WriteLine("Tables already exist");
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Error initializing database: {ex.Message}");
			}

		}

		public string GetConnectionString()
		{
			return _connectionString;
		}

		public int GetUserIdFromDatabase(string userName)
		{
			try
			{
				using (var connection = new SqliteConnection(_connectionString))
				{
					connection.Open();

					Debug.WriteLine($"Attempting to retrieve UserId for UserName: {userName}");

					string query = "SELECT UserId FROM Users WHERE Username = @UserName";

					using (var command = new SqliteCommand(query, connection))
					{
						command.Parameters.AddWithValue("@UserName", userName);

						Debug.WriteLine($"Executing SQL query: {query}");

						object result = command.ExecuteScalar();

						if (result != null && int.TryParse(result.ToString(), out int userId))
						{
							Debug.WriteLine($"UserId retrieved successfully: {userId}");
							return userId;
						}
						else
						{
							Debug.WriteLine("User does not exist");
							return 0;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"An error occurred while retrieving UserId: {ex.Message}");
				throw;
			}
		}

		public bool DoesTableExist(string tableName)
		{
			using (var connection = new SqliteConnection(_connectionString))
			{
				connection.Open();

				var command = connection.CreateCommand();
				command.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name=@TableName";
				command.Parameters.AddWithValue("@TableName", tableName);

				using (var reader = command.ExecuteReader())
				{
					return reader.HasRows;
				}
			}
		}
	}
}

