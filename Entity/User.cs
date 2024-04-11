using BuddyBudget.Database;
using BuddyBudget.Database.Repositories;
using System.Diagnostics;

namespace BuddyBudget.Entity
{
	public class User
	{
		public int UserId { get; set; }
		public string UserName { get; set; }


		public void CreateUser(string connectionString, User user)
		{
			UserRepository userRepository = new UserRepository(connectionString);
			userRepository.InsertUser(user);
		}


		public static User CreateUserIfNotExists(string connectionString, string userName)
		{
			DatabaseManager databaseManager = new DatabaseManager();

			try
			{

				int userId = databaseManager.GetUserIdFromDatabase(userName);

				if (userId == 0)
				{

					User newUser = new User { UserName = userName };
					newUser.CreateUser(connectionString, newUser);
					userId = databaseManager.GetUserIdFromDatabase(userName);
					newUser.UserId = userId;

					Debug.WriteLine($"User '{userName}' created with UserId: {userId}");

					return newUser;
				}
				else
				{
					Debug.WriteLine($"User '{userName}' already exists with UserId: {userId}");

					return new User { UserId = userId, UserName = userName };
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Error creating or retrieving user '{userName}': {ex.Message}");
				throw;
			}
		}
	}
}
