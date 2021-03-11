using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ParserApp.Interfaces;

namespace ParserApp.Tools
{
    public class HtmlLoader
    {
        readonly HttpClient client; //для отправки HTTP запросов и получения HTTP ответов.
        readonly string url; //сюда будем передовать адрес.
        readonly string carDetailsUrl; //сюда будем передовать адрес.
        readonly string carPhotosUrl; //сюда будем передовать адрес.
        string phpScript = "/member_proc.php";

        public HtmlLoader(IParserSettingsService settings)
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "C# App"); //Это для индентификации на сайте-жертве.
            //url = settings.BaseUrl;
            url = $"{settings.BaseUrl}{phpScript}?{settings.Postfix}"; //test
            carDetailsUrl = settings.CarDetailsUrl; //test
            carPhotosUrl = settings.CarPhotosUrl; //test
        }

        public async Task<string> GetSourceByPage(int id) // id - это id страницы
        {
            string currentUrl = url.Replace("{CurrentId}", id.ToString());//Подменяем {CurrentId} на номер страницы
            var content = GetContainerOfEncodedLoginData();
            
                var response = await client.PostAsync(currentUrl /*+ phpScript*/, content);
            

            string source = default;

            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                source = await response.Content.ReadAsStringAsync(); //На это моменте ломаются символы из-за кодировки
            }

            return source;
        }

        public async Task<string[]> GetSourcesByCarIds(string[] carIds)
        {
            List<string> list = new List<string>();
            var content = GetContainerOfEncodedLoginData();

            foreach (var carId in carIds)
            {
                var response = await client.PostAsync(carDetailsUrl + phpScript + "?idx=" + carId, content);

                string source = default;

                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    source = await response.Content.ReadAsStringAsync();
                }
                list.Add(source);
            }

            return list.ToArray();
        }

        public async Task<string[]> GetPhotoSourcesByCarIds(string[] carIds)
        {
            List<string> list = new List<string>();
            var content = GetContainerOfEncodedLoginData();

            foreach (var carId in carIds)
            {
                var response = await client.PostAsync(carPhotosUrl + "?idx=" + carId, content);

                string source = default;

                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    source = await response.Content.ReadAsStringAsync();
                }
                list.Add(source);
            }

            return list.ToArray();
        }

        public FormUrlEncodedContent GetContainerOfEncodedLoginData()
        {
            var values = new Dictionary<string, string>
            {
                { "tname", "nb2_member"},
                { "mode", "login"},
                { "user_id", "ABC" },
                { "user_pwd", "123456"}
            };
            var content = new FormUrlEncodedContent(values);
            return content;
        }
    }
}
