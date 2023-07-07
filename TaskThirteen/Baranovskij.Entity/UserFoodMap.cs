using System;
using LinqToDB.Mapping;

namespace Baranovskij
{
    /// <summary>
    /// Промежуточная таблица N:N - Пользователь:Еда.
    /// </summary>
    [Table(Name = "UserFoodMap")]
    public class UserFoodMap
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        [PrimaryKey]
        public Guid UserFoodMapId { get; set; }

        /// <summary>
        /// Ключ к записи из таблицы User.
        /// </summary>
        [Column(Name = "UserId"), NotNull]
        public Guid UserId { get; set; }

        /// <summary>
        /// Ключ к записи из таблицы Food.
        /// </summary>
        [Column(Name = "FoodId"), NotNull]
        public Guid FoodId { get; set; }
    }
}