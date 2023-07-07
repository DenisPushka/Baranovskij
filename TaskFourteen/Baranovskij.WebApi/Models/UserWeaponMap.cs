using System;
using LinqToDB.Mapping;

namespace Baranovskij.WebApi.Models
{
    /// <summary>
    /// Промежуточная таблица N:N - Пользователь:Оружие.
    /// </summary>
    [Table(Name = "UserWeaponMap")]
    public class UserWeaponMap
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        [PrimaryKey]
        public Guid UserWeaponId { get; set; }

        /// <summary>
        /// Ключ к записи из таблицы User.
        /// </summary>
        [Column(Name = "UserId"), NotNull]
        public Guid UserId { get; set; }

        /// <summary>
        /// Ключ к записи из таблицы Weapon.
        /// </summary>
        [Column(Name = "WeaponId"), NotNull]
        public Guid WeaponId { get; set; }
    }
}