using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Baranovskij
{
    /// <summary>
    /// Контроллер пользователя.
    /// </summary>
    public class UserController: ApiController
    {
        /// <summary>
        /// Провайдер.
        /// </summary>
        private static readonly UserLinq2DbProvider Db = Service.GetProviderUser();

        /// <summary>
        /// Добавление пользователя.
        /// </summary>
        /// <param name="user">Добавляемый пользователь.</param>
        /// <returns>Статус код.</returns>
        [HttpPost]
        [ActionName("api/User/Add")]
        public HttpResponseMessage InsertUser([FromBody] User user) =>
            Request.CreateResponse(Db.AddUser(user)
                ? HttpStatusCode.OK
                : HttpStatusCode.BadRequest);

        /// <summary>
        /// Получение всех пользователей.
        /// </summary>
        /// <returns>Пользователи.</returns>
        [HttpGet]
        [ActionName("api/User/GetAll")]
        public HttpResponseMessage GetUsers() => Request.CreateResponse(Db.GetUsers());

        /// <summary>
        /// Поиск пользователей по <paramref name="city"/>.
        /// </summary>
        /// <param name="city">Фильтр поиска.</param>
        /// <returns>Пользователи.</returns>
        [HttpGet]
        [Route("api/User/GetFromCity")]
        public HttpResponseMessage GetFromCity(string city) => Request.CreateResponse(Db.GetUsersFromCity(city));

        /// <summary>
        /// Обновление здоровья у пользователя по <paramref name="nick"/>.
        /// </summary>
        /// <param name="nick">Ник пользователя.</param>
        /// <param name="newHealth">Новое здоровье пользователя.</param>
        /// <returns>Статус код.</returns>
        [HttpPost]
        [ActionName("api/User/UpdateHealth")]
        public HttpResponseMessage UpdateHealth(string nick, int newHealth) =>
            Request.CreateResponse(Db.UpdateHealth(nick, newHealth)
                ? HttpStatusCode.OK
                : HttpStatusCode.BadRequest);

        /// <summary>
        /// Удаление пользователя с ником <paramref name="nick"/>.
        /// </summary>
        /// <param name="nick">Ник пользователя.</param>
        /// <returns>Статус код.</returns>
        [HttpDelete]
        public HttpResponseMessage DeleteUser(string nick) =>
            Request.CreateResponse(Db.DeleteUser(nick)
                ? HttpStatusCode.OK
                : HttpStatusCode.BadRequest);
    }
}