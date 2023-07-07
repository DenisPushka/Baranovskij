using System;
using System.Collections.Generic;

namespace Baranovskij
{
    /// <summary>
    /// Точка входа.
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            Start();
        }

        /// <summary>
        /// Демонстрация.
        /// </summary>
        private static void Start()
        {
            var provider = UserService.GetProvider();
            provider.AddUser(
                new User
                {
                    UserId = Guid.NewGuid(),
                    Nick = "Grisha_3",
                    CityId = new Guid("786780d4-effa-40f7-8075-aa721f522df9"),
                    Price = 35,
                    Health = 40,
                    RegistrationDate = DateTime.Now
                });

            ShowCollection(provider.GetUsers());

            Console.WriteLine("Пользователи из города: Samara");
            ShowCollection(provider.GetUsersFromCity("Samara"));

            Console.WriteLine("Изменение у пользователя с ником Alex_15 Health равные 33 единиц");
            provider.UpdateHealth("Alex_15", 33);

            Console.WriteLine("Удаление пользователя с ником Grisha_48");
            provider.DeleteUser("Grisha_48");

            Console.WriteLine("Таблица пользователей после всех изменений:");
            ShowCollection(provider.GetUsers());

            Console.ReadLine();
        }

        /// <summary>
        /// Отображение коллекции пользователей.
        /// </summary>
        /// <param name="users">Передаваемая коллекция.</param>
        private static void ShowCollection(IEnumerable<User> users)
        {
            foreach (var user in users)
            {
                Console.WriteLine(
                    $" Id: {user.UserId}\n Ник: {user.Nick}\n Дата регистрации пользователя: {user.RegistrationDate.ToSqlString()}\n" +
                    $" Город: {user.City.NameCity},\n Количество денег: {user.Price},\n Здоровье: {user.Health}.\n" +
                    new string('-', 20) + "\n \n \n ");
            }
        }
    }
}