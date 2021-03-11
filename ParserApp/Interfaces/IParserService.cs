using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;

namespace ParserApp.Interfaces
{
    public interface IParserService<T> where T : class //класс реализующие этот интерфейс смогут возвращаться данные любого ссылочного типа
    {
        T GetCarIdsList(IHtmlDocument document); // тип T при реализации будет заменяться на любой другой тип

        Task ParseCarDetailsAsync(List<IHtmlDocument> documents, string[] carIds);
        Task ParseCarPhotosAsync(List<IHtmlDocument> documents, string[] carIds, string wwwroot);
    }
}
