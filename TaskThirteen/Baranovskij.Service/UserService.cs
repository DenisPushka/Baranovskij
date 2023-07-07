namespace Baranovskij
{
    /// <summary>
    /// Сервис для пользователя.
    /// </summary>
    public static class UserService
    {
        /// <summary>
        /// Получение провайдера.
        /// </summary>
        /// <returns>Провайдер.</returns>
        public static DataBaseProvider GetProvider()
        {
            return new Linq2DbProvider();
        }
    }
}
