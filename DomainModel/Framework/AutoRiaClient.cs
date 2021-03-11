using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DomainModel.Framework
{
    public class AutoRiaClient
    {
        //документация по интеграции https://github.com/ria-com/auto-ria-rest-api/tree/master/AUTO_RIA_API
        private static string ApiKey => "ipMO51dUUNNz9oe9av67DBcgQAoU8fmBzbEGqNJK";
        public static string BaseUrl => "https://developers.ria.com/auto/";
        public static string BaseUrlCategory => BaseUrl + "categories/";

        public static string Atribut = "?api_key=";
        public static string Mark = "/marks";
        public static string Model = "/models";
        public static string Gearboxe = "/gearboxes";
        public static string Type = "/type";
        public static string Option = "/auto_options";
        public static string Color = "/colors";
        public static string Country = "/countries";
        public static string BodyStyles = "/bodystyles";
        public static string DriverTypes = "/driverTypes";

        public class MetaData
        {
            [JsonProperty("name")]
            public string Name { get; set; }
            [JsonProperty("value")]
            public int ExternalId { get; set; }
        }

        /// <summary>
        /// Типы транспорта
        /// Что бы получить список типов транспорта нужно отправив GET запрос на адрес https://developers.ria.com/auto/categories/?api_key=YOUR_API_KEY
        /// <returns></returns>
        public List<MetaData> GetVehicleTypes()
        {
            using (var client = new HttpClient())
            {
                var url = BaseUrlCategory + Atribut + ApiKey;
                var response = client.GetAsync(url);

                if (response.Result.StatusCode == HttpStatusCode.TooManyRequests)
                {
                    return new List<MetaData>();
                }
                if (response.Result.StatusCode == HttpStatusCode.OK)
                {
                    var result = response.Result.Content.ReadAsStringAsync().Result;

                    var list = JsonConvert.DeserializeObject<List<MetaData>>(result);

                    return list;
                }
                return new List<MetaData>();
            }
        }

        /// <summary>
        /// Типы кузова
        /// Типы кузова зависят от типов транспорта.Поэтому для того, чтобы получить список типов кузова необходимо отправить GET запрос на адрес
        /// https://developers.ria.com/auto/categories/:categoryId/bodystyles?api_key=YOUR_API_KEY
        /// где categoryId - идентификатор типа транспорта, api_key- Ваш ключ
        /// </summary>
        /// <param name="vehicleTypeId"></param>
        /// <returns></returns>
        public List<MetaData> GetBodyStyles(int vehicleTypeId)
        {
            using (var client = new HttpClient())
            {
                var url = BaseUrlCategory + vehicleTypeId + BodyStyles + Atribut + ApiKey;

                var response = client.GetAsync(url);

                if (response.Result.StatusCode == HttpStatusCode.TooManyRequests)
                {
                    return new List<MetaData>();
                }
                if (response.Result.StatusCode == HttpStatusCode.OK)
                {
                    var result = response.Result.Content.ReadAsStringAsync().Result;

                    var list = JsonConvert.DeserializeObject<List<MetaData>>(result);

                    return list;
                }
                return new List<MetaData>();
            }
        }

        /// <summary>
        /// Получение марок
        /// Марки зависят от типов транспорта. Поэтому для того, чтобы получить список марок необходимо отправить GET запрос по адресу
        /// https://developers.ria.com/auto/categories/:categoryId/marks?api_key=YOUR_API_KEY,
        /// где categoryId - идентификатор типа транспорта, api_key- Ваш ключ
        /// </summary>
        /// <param name="vehicleTypeId"></param>
        /// <returns></returns>
        public List<MetaData> GetVehicleMarks(int vehicleTypeId)
        {
            using (var client = new HttpClient())
            {
                var url = BaseUrlCategory + vehicleTypeId + Mark + Atribut + ApiKey;

                var response = client.GetAsync(url);

                if (response.Result.StatusCode == HttpStatusCode.TooManyRequests)
                {
                    return new List<MetaData>();
                }
                if (response.Result.StatusCode == HttpStatusCode.OK)
                {
                    var result = response.Result.Content.ReadAsStringAsync().Result;

                    var list = JsonConvert.DeserializeObject<List<MetaData>>(result);

                    return list;
                }
                return new List<MetaData>();
            }
        }

        /// <summary>
        /// Получение моделей
        /// Модели зависят от типов транспорта и марок. Следовательно список марок можно получить по адресу
        /// http://api.auto.ria.com/categories/:categoryId/marks/:markId/models?api_key=YOUR_API_KEY,
        /// где categoryId - идентификатор типа транспорта а markId - идентификатор марки, api_key- Ваш ключ.
        /// </summary>
        /// <param name="vehicleTypeId"></param>
        /// <param name="vehicleMarkId"></param>
        /// <returns></returns>
        public List<MetaData> GetVehicleModels(int vehicleTypeId, int vehicleMarkId)
        {
            using (var client = new HttpClient())
            {
                var url = BaseUrlCategory + vehicleTypeId + Mark + "/" + vehicleMarkId + Model + Atribut + ApiKey;
                var response = client.GetAsync(url);

                if (response.Result.StatusCode == HttpStatusCode.TooManyRequests)
                {
                    return new List<MetaData>();
                }
                if (response.Result.StatusCode == HttpStatusCode.OK)
                {
                    var result = response.Result.Content.ReadAsStringAsync().Result;

                    var list = JsonConvert.DeserializeObject<List<MetaData>>(result);

                    return list;
                }

                return new List<MetaData>();
            }
        }

        /// <summary>
        /// Получение коробок передач
        /// Коробки передач зависят от типа транспорта, поэтому, чтобы получить их список, необходимо послать GET запрос по адресу
        /// https://developers.ria.com/auto/categories/:categoryId/gearboxes?api_key=YOUR_API_KEY,
        /// где categoryId - идентификатор типа транспорта, api_key- Ваш ключ
        /// </summary>
        /// <param name="vehicleTypeId"></param>
        /// <returns></returns>
        public List<MetaData> GetTransmissionTypes(int vehicleTypeId)
        {
            using (var client = new HttpClient())
            {
                var url = BaseUrlCategory + vehicleTypeId + Gearboxe + Atribut + ApiKey;
                var response = client.GetAsync(url);

                if (response.Result.StatusCode == HttpStatusCode.TooManyRequests)
                {
                    return new List<MetaData>();
                }
                if (response.Result.StatusCode == HttpStatusCode.OK)
                {
                    var result = response.Result.Content.ReadAsStringAsync().Result;

                    var list = JsonConvert.DeserializeObject<List<MetaData>>(result);

                    return list;
                }
                return new List<MetaData>();
            }
        }

        /// <summary>
        /// Получение типов топлива
        /// Типы топлива можно получить отправив GET запрос по адресу https://developers.ria.com/auto/type?api_key=YOUR_API_KEY
        /// </summary>
        /// <returns></returns>
        public List<MetaData> GetFuelTypes()
        {
            using (var client = new HttpClient())
            {
                var url = BaseUrl + Type + Atribut + ApiKey;
                var response = client.GetAsync(url);

                if (response.Result.StatusCode == HttpStatusCode.TooManyRequests)
                {
                    return new List<MetaData>();
                }
                if (response.Result.StatusCode == HttpStatusCode.OK)
                {
                    var result = response.Result.Content.ReadAsStringAsync().Result;

                    var list = JsonConvert.DeserializeObject<List<MetaData>>(result);

                    return list;
                }
                return new List<MetaData>();
            }
        }

        /// <summary>
        /// получение опций
        /// Опции зависят от типа транспорта. Получить их список можно отправив GET запрос по адресу
        /// https://developers.ria.com/auto/categories/:categoryId/auto_options?api_key=YOUR_API_KEY,
        /// где categoryId - идентификатор типа транспорта, api_key- Ваш ключ.
        /// </summary>
        /// <param name="vehicleTypeId"></param>
        /// <returns></returns>
        public List<MetaData> GetOptions(int vehicleTypeId)
        {
            using (var client = new HttpClient())
            {
                var url = BaseUrlCategory + vehicleTypeId + Option + Atribut + ApiKey;

                var response = client.GetAsync(url);

                if (response.Result.StatusCode == HttpStatusCode.TooManyRequests)
                {
                    return new List<MetaData>();
                }
                if (response.Result.StatusCode == HttpStatusCode.OK)
                {
                    var result = response.Result.Content.ReadAsStringAsync().Result;

                    var list = JsonConvert.DeserializeObject<List<MetaData>>(result);

                    return list;
                }
                return new List<MetaData>();
            }
        }

        /// <summary>
        /// Получить список всех цветов можно, если отправить GET запрос по адресу https://developers.ria.com/auto/colors?api_key=YOUR_API_KEY.
        /// </summary>
        /// <returns></returns>
        public List<MetaData> GetColors()
        {
            using (var client = new HttpClient())
            {
                var url = BaseUrl + Color + Atribut + ApiKey;

                var response = client.GetAsync(url);

                if (response.Result.StatusCode == HttpStatusCode.TooManyRequests)
                {
                    return new List<MetaData>();
                }
                if (response.Result.StatusCode == HttpStatusCode.OK)
                {
                    var result = response.Result.Content.ReadAsStringAsync().Result;

                    var list = JsonConvert.DeserializeObject<List<MetaData>>(result);

                    return list;
                }
                return new List<MetaData>();
            }
        }

        /// <summary>
        /// Получить список стран производителей можно, если отправить GET запрос по адресу https://developers.ria.com/auto/countries?api_key=YOUR_API_KEY. 
        /// </summary>
        /// <returns></returns>
        public List<MetaData> GetCountries()
        {
            using (var client = new HttpClient())
            {
                var url = BaseUrl + Country + Atribut + ApiKey;

                var response = client.GetAsync(url);

                if (response.Result.StatusCode == HttpStatusCode.TooManyRequests)
                {
                    return new List<MetaData>();
                }
                if (response.Result.StatusCode == HttpStatusCode.OK)
                {
                    var result = response.Result.Content.ReadAsStringAsync().Result;

                    var list = JsonConvert.DeserializeObject<List<MetaData>>(result);

                    return list;
                }
                return new List<MetaData>();
            }
        }

        /// <summary>
        /// Типы привода
        /// Типы привода также зависят от тиа транспорта, поэтому, чтобы получить их список, необходимо плсать GET запрос по адресу
        /// https://developers.ria.com/auto/categories/:categoryId/driverTypes?api_key=YOUR_API_KEY, где categoryId - идентификатор типа транспорта, api_key- Ваш ключ.
        /// </summary>
        /// <param name="vehicleTypeId"></param>
        /// <returns></returns>
        public List<MetaData> GetDriverTypes(int vehicleTypeId)
        {
            using (var client = new HttpClient())
            {
                var url = BaseUrlCategory + vehicleTypeId + DriverTypes + Atribut + ApiKey;

                var response = client.GetAsync(url);

                if (response.Result.StatusCode == HttpStatusCode.TooManyRequests)
                {
                    return new List<MetaData>();
                }
                if (response.Result.StatusCode == HttpStatusCode.OK)
                {
                    var result = response.Result.Content.ReadAsStringAsync().Result;

                    var list = JsonConvert.DeserializeObject<List<MetaData>>(result);

                    return list;
                }
                return new List<MetaData>();
            }
        }
    }
}
