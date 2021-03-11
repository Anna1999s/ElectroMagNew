using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using ParserApp.Interfaces;

namespace ParserApp.Tools
{
    public class ParserWorker<T> where T : class
    {
        IParserService<T> parser;
        IParserSettingsService parserSettings; //настройки для загрузчика кода страниц
        HtmlLoader loader; //загрузчик кода страницы
        bool isActive; //активность парсера

        public IParserService<T> Parser
        {
            get { return parser; }
            set { parser = value; }
        }

        public IParserSettingsService Settings
        {
            get { return parserSettings; }
            set
            {
                parserSettings = value; //Новые настройки парсера
                loader = new HtmlLoader(value); //сюда помещаются настройки для загрузчика кода страницы
            }
        }

        public bool IsActive //проверяем активность парсера.
        {
            get { return isActive; }
        }

        //Это событие отвечает информирование при завершении работы парсера.
        public event Action<object> OnComplited;

        //1-й конструктор, в качестве аргумента будет передеваться класс реализующий интерфейс IParser
        public ParserWorker(IParserService<T> parser)
        {
            this.parser = parser;
        }

        public async Task Start(string wwwroot) //Запускаем парсер
        {
            isActive = true;
            await Worker(wwwroot);
        }

        public void Stop() //Останавливаем парсер
        {
            isActive = false;
        }

        public async Task Worker(string wwwroot)
        {
            for (int i = parserSettings.StartPoint; i <= parserSettings.EndPoint; i++)
            {
                if (IsActive)
                {
                    HtmlParser domParser = new HtmlParser();

                    string source = await loader.GetSourceByPage(i);
                    IHtmlDocument document = await domParser.ParseDocumentAsync(source);
                    T idList = parser.GetCarIdsList(document);

                    List<IHtmlDocument> documents = new List<IHtmlDocument>();
                    string[] sourceArray = await loader.GetSourcesByCarIds((string[])(object)idList); // очень долго отрабатывает
                    foreach (var item in sourceArray)
                    {
                        documents.Add(await domParser.ParseDocumentAsync(item));
                    }
                    await parser.ParseCarDetailsAsync(documents, (string[])(object)idList);

                    List<IHtmlDocument> photoDocuments = new List<IHtmlDocument>();
                    string[] sourcePhotoArray = await loader.GetPhotoSourcesByCarIds((string[])(object)idList);
                    foreach (var item in sourcePhotoArray)
                    {
                        photoDocuments.Add(await domParser.ParseDocumentAsync(item));
                    }
                    await parser.ParseCarPhotosAsync(photoDocuments, (string[])(object)idList, wwwroot);

                    //OnNewData?.Invoke(this, result);
                }
            }

            OnComplited?.Invoke(this);
            isActive = false;
        }
    }
}
