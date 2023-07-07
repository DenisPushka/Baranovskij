using System.Collections.Generic;
using System.Linq;
using LinqToDB;

namespace Baranovskij
{
    /// <summary>
    /// Провайдер Linq2Db.
    /// </summary>
    public class Linq2DbProvider : DataBaseProvider
    {
        /// <summary>
        /// Добавление пользователя.
        /// </summary>
        /// <param name="user">Добавляемый пользователь.</param>
        public override void AddUser(User user)
        {
            Validation.CheckObjectForNull(user);

            using (var db = new DbGame(ConnectionString))
            {
                db.Users
                    .DataContext
                    .Insert(user);
            }
        }

        /// <summary>
        /// Получение всех пользователей.
        /// </summary>
        /// <returns>Пользователи.</returns>
        public override IEnumerable<User> GetUsers()
        {
            using (var db = new DbGame(ConnectionString))
            {
                var users =
                    from user in db.Users
                    select user;

                return users
                    .LoadWith(request => request.City)
                    .ToList();
            }
        }

        /// <summary>
        /// Поиск пользователей по <paramref name="city"/>.
        /// </summary>
        /// <param name="city">Фильтр поиска.</param>
        /// <returns>Пользователи.</returns>
        public override IEnumerable<User> GetUsersFromCity(string city)
        {
            Validation.CheckNullOrEmptyString(city);

            using (var db = new DbGame(ConnectionString))
            {
                var users =
                    from user in db.Users
                    where user
                        .City
                        .NameCity
                        .Equals(city)
                    select user;

                return users
                    .LoadWith(request => request.City)
                    .ToList();
            }
        }

        /// <summary>
        /// Обновление здоровья у пользователя по <paramref name="nick"/>.
        /// </summary>
        /// <param name="nick">Ник пользователя.</param>
        /// <param name="newHealth">Новое здоровье пользователя.</param>
        public override void UpdateHealth(string nick, int newHealth)
        {
            Validation.CheckNullOrEmptyString(nick);
            Validation.CheckPositiveNumber(newHealth);

            using (var db = new DbGame(ConnectionString))
            {
                db.Users
                    .Where(user => user.Nick.Equals(nick))
                    .Set(user => user.Health, newHealth)
                    .Update();
            }
        }

        /// <summary>
        /// Удаление пользователя по <paramref name="nick"/>.
        /// </summary>
        /// <param name="nick">Ник пользователя.</param>
        public override void DeleteUser(string nick)
        {
            Validation.CheckNullOrEmptyString(nick);

            using (var db = new DbGame(ConnectionString))
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
    }
}