using System;
using System.Data.SqlTypes;
using LinqToDB.Mapping;

namespace Baranovskij
{
    /// <summary>
    /// Оружие.
    /// </summary>
    [Table(Name = "Weapon")]
    public class Weapon
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        [PrimaryKey]
        public Guid WeaponId { get; set; }

        /// <summary>
        /// Название оружия.
        /// </summary>
        [Column(Name = "NameWeapon"), NotNull]
        public string Name { get; set; }

        /// <summary>
        /// Вес оружия.
        /// </summary>
        [Column(Name = "Weight")]
        public float Weight { get; set; }

        /// <summary>
        /// Цена за оружие.
        /// </summary>
        [Column(Name = "Price")]
        public SqlMoney Price { get; set; }
    }
}