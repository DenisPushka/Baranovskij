using System.Net.Http;
using System.Net;
using System.Web.Http;

namespace Baranovskij
{
    /// <summary>
    /// Контроллер для таблицы "Weapon".
    /// </summary>
    public class WeaponController : ApiController
    {
        /// <summary>
        /// Провайдер.
        /// </summary>
        private static readonly WeaponLinq2DbProvider Db = Service.GetProviderWeapon();

        /// <summary>
        /// Добавление оружия.
        /// </summary>
        /// <param name="weapon">Добавляемое оружие.</param>
        /// <returns>Статус код.</returns>
        [HttpPost]
        [ActionName("api/Weapon/Add")]
        public HttpResponseMessage InsertWeapon([FromBody] Weapon weapon) =>
            Request.CreateResponse(Db.AddWeapon(weapon)
                ? HttpStatusCode.OK
                : HttpStatusCode.BadRequest);

        /// <summary>
        /// Получение всего оружия.
        /// </summary>
        /// <returns>Оружие.</returns>
        [HttpGet]
        [ActionName("api/Weapon/GetAll")]
        public HttpResponseMessage GetWeapons() => Request.CreateResponse(Db.GetWeapons());

        /// <summary>
        /// Получение оружия дешевле <paramref name="price"/>.
        /// </summary>
        /// <param name="price">Фильтр поиска.</param>
        /// <returns>Оружие.</returns>
        [HttpGet]
        [Route("api/Weapon/GetWeaponToPrice")]
        public HttpResponseMessage GetWeaponToPrice(int price) => Request.CreateResponse(Db.GetWeaponToPrice(price));

        /// <summary>
        /// Удаление оружия по <paramref name="name"/>.
        /// </summary>
        /// <param name="name">Название оружия.</param>
        /// <returns>Статус код.</returns>
        [HttpDelete]
        public HttpResponseMessage DeleteWeapon(string name) =>
            Request.CreateResponse(Db.DeleteWeapon(name)
                ? HttpStatusCode.OK
                : HttpStatusCode.BadRequest);
    }
}