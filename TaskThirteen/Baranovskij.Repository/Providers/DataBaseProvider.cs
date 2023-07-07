using System;
using System.Collections.Generic;
using System.Configuration;

namespace Baranovskij
{
    /// <summary>
    /// Абстрактный провайдер.
    /// </summary>
    public abstract class DataBaseProvider
    {
        /// <summary>
        /// Строка подключения.
        /// </summary>
        protected readonly string ConnectionString = ConfigurationManager.AppSettings.Get("ConnectionStringToDbGame");

        /// <summary>
        /// Добавление пользователя.
        /// </summary>
        /// <param name="user">Добавляемый пользователь.</param>
        public abstract void AddUser(User user);

        #region Get

        /// <summary>
        /// Получение всех пользователей.
        /// </summary>
        /// <returns>Пользователи.</returns>
        public abstract IEnumerable<User> GetUsers();

        /// <summary>
        /// Поиск пользователей по <paramref name="city"/>.
        /// </summary>
        /// <param name="city">Фильтр поиска.</param>
        /// <returns>Пользователи.</returns>
        public abstract IEnumerable<User> GetUsersFromCity(string city);

        #endregion

        /// <summary>
        /// Обновление здоровья у пользователя по <paramref name="nick"/>.
        /// </summary>
        /// <param name="nick">Ник пользователя.</param>
        /// <param name="newHealth">Новое здоровье пользователя.</param>
        public abstract void UpdateHealth(string nick, int newHealth);

        /// <summary>
        /// Удаление пользователя.
        /// </summary>
        /// <param name="nick">Ник пользователя.</param>
        public abstract void DeleteUser(string nick);
    }
}