using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Baranovskij
{
    /// <summary>
    /// Провайдер ADO.NET.
    /// </summary>
    public class AdonetProvider : DataBaseProvider
    {
        /// <summary>
        /// Добавление пользователя.
        /// </summary>
        /// <param name="user">Добавляемый пользователь.</param>
        public override void AddUser(User user)
        {
            Validation.CheckObjectForNull(user);

            const string queryString =
                "INSERT [dbo].[User] " +
                "values (" +
                "    @userId" +
                "    @nick" +
                "    @registrationDate" +
                "    @cityId" +
                "    @price" +
                "    @health" +
                ")";

            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@userId", Guid.NewGuid());
                command.Parameters.AddWithValue("@nick", user.Nick);
                command.Parameters.AddWithValue("@registrationDate", user.RegistrationDate);
                command.Parameters.AddWithValue("@cityId", user.CityId);
                command.Parameters.AddWithValue("@price", user.Price);
                command.Parameters.AddWithValue("@health", user.Health);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e);
                    Debug.WriteLine(e);
                    throw;
                }
            }
        }

        #region Get

        /// <summary>
        /// Получение всех пользователей.
        /// </summary>
        /// <returns>Пользователи.</returns>
        public override IEnumerable<User> GetUsers()
        {
            const string queryString =
                "SELECT TOP (1000) " +
                "    [UserId]" +
                "    , [Nick]" +
                "    , [RegistrationDate]" +
                "    , U.[CityId]" +
                "    , [Price]" +
                "    , [Health]" +
                "    , [NameCity]" +
                "FROM [dbo].[User] U" +
                "    LEFT JOIN [dbo].[City] C ON" +
                "        C.CityId = U.CityId";

            var users = new List<User>();

            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            UserId = (Guid)reader[0],
                            Nick = (string)reader[1],
                            RegistrationDate = (DateTime)reader[2],
                            CityId = (Guid)reader[3],
                            Price = (double)Convert.ToDecimal(reader[4]),
                            Health = (int)reader[5],
                            City = new City
                            {
                                CityId = (Guid)reader[3],
                                NameCity = (string)reader[6]
                            }
                        });
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e);
                    Debug.WriteLine(e);
                    throw;
                }
            }

            return users;
        }

        /// <summary>
        /// Поиск пользователей по  <paramref name="city"/>.
        /// </summary>
        /// <param name="city">Фильтр поиска.</param>
        /// <returns>Пользователи.</returns>
        public override IEnumerable<User> GetUsersFromCity(string city)
        {
            Validation.CheckNullOrEmptyString(city);

            const string queryString =
                "SELECT TOP (1000) " +
                "    [UserId]" +
                "    , [Nick]" +
                "    , [RegistrationDate]" +
                "    , U.[CityId]" +
                "    , [Price]" +
                "    , [Health]" +
                "    , [NameCity]" +
                "FROM [dbo].[User] U" +
                "    LEFT JOIN [dbo].[City] C ON" +
                "        C.CityId = U.CityId " +
                "WHERE " +
                "    U.[CityId] = (SELECT" +
                "                     [CityId] " +
                "                  FROM [dbo].[City]" +
                "                  WHERE [NameCity] = @city) ";

            var users = new List<User>();

            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@city", city);

                try
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            UserId = (Guid)reader[0],
                            Nick = (string)reader[1],
                            RegistrationDate = (DateTime)reader[2],
                            CityId = (Guid)reader[3],
                            Price = (double)Convert.ToDecimal(reader[4]),
                            Health = (int)reader[5],
                            City = new City
                            {
                                CityId = (Guid)reader[3],
                                NameCity = (string)reader[6]
                            }
                        });
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e);
                    Debug.WriteLine(e);
                    throw;
                }
            }

            return users;
        }

        #endregion

        /// <summary>
        /// Обновление здоровья у пользователя по <paramref name="nick"/>.
        /// </summary>
        /// <param name="nick">Ник пользователя.</param>
        /// <param name="newHealth">Новое здоровье пользователя.</param>
        public override void UpdateHealth(string nick, int newHealth)
        {
            Validation.CheckNullOrEmptyString(nick);
            Validation.CheckPositiveNumber(newHealth);

            const string queryString =
                "UPDATE [dbo].[User] SET " +
                "    Health = @newHealth " +
                "WHERE Nick = @nick";

            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@nick", nick);
                command.Parameters.AddWithValue("@newHealth", newHealth);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e);
                    Debug.WriteLine(e);
                    throw;
                }
            }
        }

        /// <summary>
        /// Удаление пользователя по <paramref name="nick"/>.
        /// </summary>
        /// <param name="nick">Ник пользователя.</param>
        public override void DeleteUser(string nick)
        {
            Validation.CheckNullOrEmptyString(nick);

            const string queryString =
                "DELETE FROM [dbo].[UserFoodMap] " +
                "WHERE UserId = (SELECT UserId FROM [dbo].[User]" +
                "                WHERE Nick = @nick);" +
                "DELETE FROM [dbo].[UserWeaponMap] " +
                "WHERE UserId = (SELECT UserId FROM [dbo].[User]" +
                "                WHERE Nick = @nick);" +
                "DELETE FROM [dbo].[User] " +
                "WHERE UserId = (SELECT UserId FROM [dbo].[User]" +
                "                WHERE Nick = @nick);";

            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@nick", nick);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e);
                    Debug.WriteLine(e);
                    throw;
                }
            }
        }
    }
}