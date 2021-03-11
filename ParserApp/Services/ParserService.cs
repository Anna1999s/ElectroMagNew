using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using DomainModel.Content;
using DomainModel.Framework;
using Interfaces.Content;
using Newtonsoft.Json;
using ParserApp.Interfaces;
using Services;

namespace ParserApp.Services
{
    public class ParserService : IParserService<string[]>
    {
        private const string CarNameKey = "CarName";
        private const string AuctionDateKey = "AuctionDate";
        private const string RegistrationYearKey = "Registration year";
        private const string TheCarsNumberKey = "External lot number";
        private const string FuelKey = "Fuel";
        private const string TransmissionKey = "Transmission";
        private const string DistanceDrivenKey = "Odometer";
        private const string DisplacementKey = "Engine Displacement";
        private const string StorageAreaKey = "Storage area";
        private const string IncurredCostTreatmentKey = "Incurred Cost Treatment";

        private readonly ILotService _lotService;
        private readonly IPhotoService _photoService;
        private readonly IVehicleService _vehicleService;

        public ParserService(ILotService lotService, IPhotoService photoService, IVehicleService vehicleService)
        {
            _lotService = lotService;
            _photoService = photoService;
            _vehicleService = vehicleService;
        }

        public string[] GetCarIdsList(IHtmlDocument document)
        {
            var list = new List<string>();
            IEnumerable<IElement> items = document.QuerySelectorAll("dl.board_list_box dt a:first-child");

            foreach (var item in items)
            {
                var carId = item.OuterHtml.Substring(36, 6);
                list.Add(carId);
            }

            return list.ToArray();
        }

        public async Task ParseCarDetailsAsync(List<IHtmlDocument> documents, string[] carIds)
        {
            var rootDirectory = "D:\\ParserTests";
            if (!Directory.Exists(rootDirectory)) Directory.CreateDirectory(rootDirectory);

            var i = 0;
            foreach (var document in documents)
            {
                var dictionary = new Dictionary<string, string>();
                //var auctionDateDictionary = new Dictionary<string, DateTime>();

                var nameKey = CarNameKey;
                var nameValue = document.QuerySelector("div.sub_con_box p.view_title span").TextContent;
                dictionary.Add(nameKey, nameValue);


                var auctionDateKey = AuctionDateKey;
                var auctionDateValue = document
                    .QuerySelector("div.sub_con_box div.view_box div.view_right ul li:nth-child(4) dd").TextContent;
                dictionary.Add(auctionDateKey, auctionDateValue);

                //var registrationYearKey = document.QuerySelector("table.table_style01 tbody tr th:nth-child(1)").TextContent;
                var registrationYearKey = RegistrationYearKey;
                var registrationYearValue =
                    document.QuerySelector("table.table_style01 tbody tr td:nth-child(2)").TextContent;
                dictionary.Add(registrationYearKey, registrationYearValue);

                //var theCarsNumberKey = document.QuerySelector("table.table_style01 tbody tr th:nth-child(3)").TextContent;
                var theCarsNumberKey = TheCarsNumberKey;
                var theCarsNumberValue =
                    document.QuerySelector("table.table_style01 tbody tr td:nth-child(4)").TextContent;
                dictionary.Add(theCarsNumberKey, theCarsNumberValue);

                //var fuelKey = document.QuerySelector("table.table_style01 tbody tr:nth-child(2) th:nth-child(1)").TextContent;
                var fuelKey = FuelKey;
                var fuelValue = document.QuerySelector("table.table_style01 tbody tr:nth-child(2) td:nth-child(2)")
                    .TextContent;
                dictionary.Add(fuelKey, fuelValue);

                //var transmissionKey = document.QuerySelector("table.table_style01 tbody tr:nth-child(2) th:nth-child(3)").TextContent;
                var transmissionKey = TransmissionKey;
                var transmissionValue = document
                    .QuerySelector("table.table_style01 tbody tr:nth-child(2) td:nth-child(4)").TextContent;
                dictionary.Add(transmissionKey, transmissionValue);

                //var distanceDrivenKey = document.QuerySelector("table.table_style01 tbody tr:nth-child(3) th:nth-child(1)").TextContent;
                var distanceDrivenKey = DistanceDrivenKey;
                var distanceDrivenValue = document
                    .QuerySelector("table.table_style01 tbody tr:nth-child(3) td:nth-child(2)").TextContent;
                dictionary.Add(distanceDrivenKey, distanceDrivenValue);

                //var displacementKey = document.QuerySelector("table.table_style01 tbody tr:nth-child(3) th:nth-child(3)").TextContent;
                var displacementKey = DisplacementKey;
                var displacementValue = document
                    .QuerySelector("table.table_style01 tbody tr:nth-child(3) td:nth-child(4)").TextContent;
                dictionary.Add(displacementKey, displacementValue);

                //var storageAreaKey = document.QuerySelector("table.table_style01 tbody tr:nth-child(4) th:nth-child(1)").TextContent;
                var storageAreaKey = StorageAreaKey;
                var storageAreaValue = document
                    .QuerySelector("table.table_style01 tbody tr:nth-child(4) td:nth-child(2)").TextContent;
                dictionary.Add(storageAreaKey, storageAreaValue);

                //var incurredCostTreatmentKey = document.QuerySelector("table.table_style01 tbody tr:nth-child(5) th").TextContent;
                var incurredCostTreatmentKey = IncurredCostTreatmentKey;
                var incurredCostTreatmentValue =
                    document.QuerySelector("table.table_style01 tbody tr:nth-child(5) td").TextContent;
                dictionary.Add(incurredCostTreatmentKey, incurredCostTreatmentValue);

                //ConvertDictionaryToJson(dictionary, carIds[i]);
                await AddLot(dictionary, carIds[i]);
                i++;
            }
        }

        public async Task ParseCarPhotosAsync(List<IHtmlDocument> documents, string[] carIds, string wwwroot)
        {
            var url = "https://www.happycarservice.com/";
            if (!Directory.Exists(wwwroot)) Directory.CreateDirectory(wwwroot);
            var i = 0;
            foreach (var document in documents)
            {
                var vehicle = await _vehicleService.GetByExternalId(carIds[i]);

                IEnumerable<IElement> items = document.QuerySelectorAll("div.sliderkit-panel");
                var path = Directory.CreateDirectory(
                    Path.Combine(wwwroot, WorkContext.ImagePath, vehicle.Id.ToString()));
                foreach (var item in items)
                {
                    var carSource = item.OuterHtml.Substring(87, 47);
                    var carUrl = url + carSource;
                    var photoName = carSource.Substring(26);

                    DownloadPhoto(carUrl, path.FullName, photoName);
                    await _photoService.AddByParser(photoName, vehicle.Id, wwwroot);
                }

                i++;
            }
        }

        //TODO: System.Net.WebException: 'Исключение во время запроса WebClient.' IOException: Неожиданный EOF или 0 байт из транспортного потока. 
        private void DownloadPhoto(string url, string path, string carSource)
        {
            var fullPath = Path.Combine(path, carSource);
            using (var webClient = new WebClient())
            {
                webClient.DownloadFile(url, fullPath);
            }
        }


        private void ConvertDictionaryToJson(Dictionary<string, string> dictionary, string carId)
        {
            using (var file = File.CreateText(@"D:\ParserTests\Car_" + carId + ".json"))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(file, dictionary);
            }
        }

        private async Task AddLot(Dictionary<string, string> dictionary, string carId)
        {
            var lot = new Lot
            {
                State = StateEnum.Edit,
                ExternalDescription = dictionary[CarNameKey],
                ExternalLotNumber = dictionary[TheCarsNumberKey],
                AuctionDate = DateTime.Parse(dictionary[AuctionDateKey]),
                Vehicle = new Vehicle
                {
                    Year = dictionary[RegistrationYearKey].Substring(0, 4),
                    EngineDisplacement = dictionary[DisplacementKey].Replace(" cc", ""),
                    Odometer = int.Parse(dictionary[DistanceDrivenKey].Replace(",", "").Replace(" km", "")),
                    ExternalId = carId
                }
            };

            await _lotService.Add(lot);
        }
    }
}