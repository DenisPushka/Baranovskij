using System;
using LinqToDB.Mapping;

namespace Baranovskij
{
    /// <summary>
    /// Город.
    /// </summary>
    [Table(Name = "City")]
    public class City
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        [PrimaryKey]
        public Guid CityId { get; set; }

        /// <summary>
        /// Название города.
        /// </summary>
        [Column(Name = "NameCity"), NotNull]
        public string NameCity { get; set; }
    }
}