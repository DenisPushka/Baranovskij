using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using LinqToDB;

namespace Baranovskij
{
    /// <summary>
    /// Провайдер linq2DB для таблицы "User".
    /// </summary>
    public class UserLinq2DbProvider
    {
        /// <summary>
        /// Строка подключения.
        /// </summary>
        private readonly string _connectionString = ConfigurationManager.AppSettings.Get("ConnectionStringToDbGame");

        /// <summary>
        /// Добавление пользователя.
        /// </summary>
        /// <param name="user">Добавляемый пользователь.</param>
        /// <returns><see langword="true" /> - если элемент был успешно добавлен.</returns>
        public bool AddUser(User user)
        {
            Validation.CheckObjectForNull(user);

            user.UserId = Guid.NewGuid();
            user.RegistrationDate = DateTime.Now;

            try
            {
                using (var db = new DbGame(_connectionString))
                {
                    db.Users
                        .DataContext
                        .Insert(user);
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e);
                throw;
            }

            return true;
        }

        /// <summary>
        /// Получение всех пользователей.
        /// </summary>
        /// <returns>Пользователи.</returns>
        public IEnumerable<User> GetUsers()
        {
            using (var db = new DbGame(_connectionString))
            {
                return
                    (
                        from user in db.Users
                        select user
                    )
                    .LoadWith(request => request.City)
                    .ToList();
            }
        }

        /// <summary>
        /// Поиск пользователей по <paramref name="city"/>.
        /// </summary>
        /// <param name="city">Город.</param>
        /// <returns>Пользователи.</returns>
        public IEnumerable<User> GetUsersFromCity(string city)
        {
            Validation.CheckNullOrEmptyString(city);

            using (var db = new DbGame(_connectionString))
            {
                return
                    (
                        from user in db.Users
                        where user
                            .City
                            .NameCity
                            .Equals(city)
                        select user
                    )
                    .LoadWith(request => request.City)
                    .ToList();
            }
        }

        /// <summary>
        /// Обновление здоровья у пользователя по <paramref name="nick"/>.
        /// </summary>
        /// <param name="nick">Ник пользователя.</param>
        /// <param name="newHealth">Новое здоровье пользователя.</param>
        /// <returns><see langword="true" /> - если элемент был успешно обновлен.</returns>
        public bool UpdateHealth(string nick, int newHealth)
        {
            Validation.CheckNullOrEmptyString(nick);
            Validation.CheckPositiveNumber(newHealth);

            try
            {
                using (var db = new DbGame(_connectionString))
                {
                    db.Users
                        .Where(user => user.Nick.Equals(nick))
                        .Set(user => user.Health, newHealth)
                        .Update();
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e);
                throw;
            }

            return true;
        }

        /// <summary>
        /// Удаление пользователя по <paramref name="nick"/>.
        /// </summary>
        /// <param name="nick">Ник пользователя.</param>
        /// <returns><see langword="true" /> - если элемент был успешно удален.</returns>
        public bool DeleteUser(string nick)
        {
            Validation.CheckNullOrEmptyString(nick);

            try
            {
                using (var db = new DbGame(_connectionString))
                {
                    var userSearch = db.Users
                        .FirstOrDefault(userEnumerable => userEnumerable.Nick.Equals(nick));

                    db.UserFoodMap
                        .Where(user => user.UserId.Equals(userSearch.UserId))
                        .Delete();

                    db.UserWeaponMap
                        .Where(user => user.UserId.Equals(userSearch.UserId))
                        .Delete();

                    db.Users
                        .Where(user => user.Nick.Equals(nick))
                        .Delete();
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e);
                throw;
            }

            return true;
        }
    }
}