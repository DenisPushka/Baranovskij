using System;
using System.Data.SqlTypes;
using LinqToDB.Mapping;

namespace Baranovskij
{
    /// <summary>
    /// Пользователь.
    /// </summary>
    [Table(Name = "User")]
    public class User
    {
        /// <summary>
        /// Ключ идентификатор.
        /// </summary>
        [PrimaryKey]
        public Guid UserId { get; set; }

        /// <summary>
        /// Ник пользователя.
        /// </summary>
        [Column(Name = "Nick"), NotNull]
        public string Nick { get; set; }

        /// <summary>
        /// Дата регистрации.
        /// </summary>
        [Column(Name = "RegistrationDate")]
        public SqlDateTime RegistrationDate { get; set; }

        /// <summary>
        /// Вторичный ключ к таблице City.
        /// </summary>
        [Column(Name = "CityId")]
        public Guid CityId { get; set; }

        /// <summary>
        /// Город, который в котором проживает пользователь.
        /// </summary>
        [Association(ThisKey = "CityId", OtherKey = "CityId", CanBeNull = true)]
        public City City { get; set; }

        /// <summary>
        /// Количество денег у пользователя.
        /// </summary>
        [Column(Name = "Price")]
        public double Price { get; set; }

        /// <summary>
        /// Количество здоровья у пользователя.
        /// </summary>
        [Column(Name = "Health")]
        public int Health { get; set; }
    }
}