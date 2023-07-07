using LinqToDB;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System;
using System.Data.SqlClient;

namespace Baranovskij
{
    /// <summary>
    /// Провайдер linq2DB для таблицы "Weapon".
    /// </summary>
    public class WeaponLinq2DbProvider
    {
        /// <summary>
        /// Строка подключения.
        /// </summary>
        private readonly string _connectionString = ConfigurationManager.AppSettings.Get("ConnectionStringToDbGame");

        /// <summary>
        /// Добавление оружия.
        /// </summary>
        /// <param name="weapon">Добавляемое оружие.</param>
        /// <returns><see langword="true" /> - если элемент был успешно добавлен.</returns>
        public bool AddWeapon(Weapon weapon)
        {
            Validation.CheckObjectForNull(weapon);

            weapon.WeaponId = Guid.NewGuid();

            try
            {
                using (var db = new DbGame(_connectionString))
                {
                    db.Weapons
                        .DataContext
                        .Insert(weapon);
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
        /// Получение всего оружия.
        /// </summary>
        /// <returns>Оружие.</returns>
        public IEnumerable<Weapon> GetWeapons()
        {
            using (var db = new DbGame(_connectionString))
            {
                return
                    (
                        from weapon in db.Weapons
                        select weapon
                    )
                    .ToList();
            }
        }

        /// <summary>
        /// Получение оружия дешевле <paramref name="price"/>.
        /// </summary>
        /// <param name="price">Цена за оружие.</param>
        /// <returns>Оружие.</returns>
        public IEnumerable<Weapon> GetWeaponToPrice(int price)
        {
            Validation.CheckPositiveNumber(price);

            using (var db = new DbGame(_connectionString))
            {
                return
                    (
                        from weapon in db.Weapons
                        where weapon.Price.ToInt32() < price
                        select weapon
                    )
                    .ToList();
            }
        }

        /// <summary>
        /// Удаление оружия по <paramref name="name"/>.
        /// </summary>
        /// <param name="name">Название оружия.</param>
        /// <returns><see langword="true" /> - если элемент был успешно удален.</returns>
        public bool DeleteWeapon(string name)
        {
            Validation.CheckNullOrEmptyString(name);

            try
            {
                using (var db = new DbGame(_connectionString))
                {
                    var weaponSearch = db.Weapons
                        .FirstOrDefault(weaponEnumerable => weaponEnumerable.Name.Equals(name));

                    db.UserWeaponMap
                        .Where(weapon => weapon.WeaponId.Equals(weaponSearch.WeaponId))
                        .Delete();

                    db.Weapons
                        .Where(weapon => weapon.Name.Equals(name))
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