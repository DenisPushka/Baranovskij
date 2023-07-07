using Baranovskij.WebApi.Models;
using LinqToDB;
using LinqToDB.Data;

namespace Baranovskij
{
    /// <summary>
    /// База данны: Игра.
    /// </summary>
    public class DbGame: DataConnection
    {
        /// <summary>
        /// Инициализация.
        /// </summary>
        /// <param name="connectionString">Строка подключения.</param>
        public DbGame(string connectionString)
            : base(ProviderName.SqlServer2019, connectionString)
        {
        }

        /// <summary>
        /// Таблица пользователей.
        /// </summary>
        public ITable<User> Users => this.GetTable<User>();

        /// <summary>
        /// Таблица оружия.
        /// </summary>
        public ITable<Weapon> Weapons => this.GetTable<Weapon>();

        /// <summary>
        /// Таблица городов.
        /// </summary>
        public ITable<City> Cities => this.GetTable<City>();

        /// <summary>
        /// Таблица N:N - Пользователь:Оружие.
        /// </summary>
        public ITable<UserWeaponMap> UserWeaponMap => this.GetTable<UserWeaponMap>();

        /// <summary>
        /// Таблица N:N - Пользователь:Еда.
        /// </summary>
        public ITable<UserFoodMap> UserFoodMap => this.GetTable<UserFoodMap>();
    }
}