using BuddyBudget.Entity;
using Dapper;
using Microsoft.Data.Sqlite;
using System.Diagnostics;

namespace BuddyBudget.Database.Repositories
{
	public class UserRepository
	{
		private readonly string _connectionString;

		public UserRepository(string connectionString)
		{
			_connectionString = connectionString;
		}

		public void InsertUser(User user)
		{
			try
			{
				using (var connection = new SqliteConnection(_connectionString))
				{
					connection.Open();

					string insert = @"
						INSERT INTO Users (Username)
						VALUES (@Username)";

					using (var command = connection.CreateCommand())
					{
						command.CommandText = insert;
						command.Parameters.AddWithValue("@Username", user.UserName);

						command.ExecuteNonQuery();

						Debug.WriteLine($"Budget inserted for user: {user.UserName} with ID: {user.UserId}");
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Error inserting user: {ex.Message}");
				throw;
			}
		}

		public void DeleteUser(User user)
		{
			using (var connection = new SqliteConnection(_connectionString))
			{
				connection.Open();

				string delete = @"
					DELETE FROM Users
					WHERE Id = @Id";

				connection.Execute(delete, new { Id = user.UserId });
			}
		}
	}
}
