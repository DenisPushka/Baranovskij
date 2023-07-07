namespace Baranovskij
{
    /// <summary>
    /// Сервис.
    /// </summary>
    public static class Service
    {
        /// <summary>
        /// Получение провайдера для таблицы "пользователь".
        /// </summary>
        /// <returns>Провайдер для таблицы "пользователь".</returns>
        public static UserLinq2DbProvider GetProviderUser() => new UserLinq2DbProvider();

        /// <summary>
        /// Получение провайдера для таблицы "оружие".
        /// </summary>
        /// <returns>Провайдер для таблицы "оружие".</returns>
        public static WeaponLinq2DbProvider GetProviderWeapon() => new WeaponLinq2DbProvider();
    }
}