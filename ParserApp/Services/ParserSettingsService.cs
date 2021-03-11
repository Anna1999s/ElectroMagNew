using System;
using System.Collections.Generic;
using System.Text;
using ParserApp.Interfaces;

namespace ParserApp.Services
{
    public class ParserSettingsService : IParserSettingsService
    {
        public ParserSettingsService(int start, int end)
        {
            StartPoint = start;
            EndPoint = end;
        }

        public string BaseUrl { get; set; } = "https://www.happycarservice.com/nHome/html/02_insurance_list.html";
        public string Postfix { get; set; } = "page={CurrentId}";

        public string CarDetailsUrl { get; set; } = "https://www.happycarservice.com/nHome/html/02_insurance_view.html";
        public string CarDetailsId { get; set; } = "idx={CurrentId}";

        public string CarPhotosUrl { get; set; } = "https://www.happycarservice.com/nHome/html/commonCarBigView.php";
        public string CarPhotosId { get; set; } = "idx={CurrentId}";

        public int StartPoint { get; set; }
        public int EndPoint { get; set; }
    }
}
