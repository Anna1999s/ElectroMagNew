using System;
using System.Collections.Generic;
using System.Text;

namespace ParserApp.Interfaces
{
    public interface IParserSettingsService
    {
        string BaseUrl { get; set; } //url сайта
        string Postfix { get; set; } //в постфикс будет передаваться id страницы

        string CarDetailsUrl { get; set; }
        string CarDetailsId { get; set; }

        string CarPhotosUrl { get; set; }
        string CarPhotosId { get; set; }

        int StartPoint { get; set; } //c какой страницы парсим данные
        int EndPoint { get; set; } //по какую страницу парсим данные
    }
}
