using AutoMapper;
using DomainModel.Catalog;
using DomainModel.Framework;
using DomainModel.Framework.Models;
using DomainModel.Localization;
using Interfaces.Catalog;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace WebSite.Areas.Admin.Controllers
{
    public class ContentController : BaseAdminController
    {
        private readonly IVehicleTypeService _vehicleTypeService;
        private readonly IVehicleMarkService _vehicleMarkService;
        private readonly IVehicleModelService _vehicleModelService;
        private readonly ITransmissionTypeService _transmissionTypeService;
        private readonly IFuelTypeService _fuelTypeService;
        private readonly IVehicleColorService _vehicleColorService;
        private readonly IVehicleOptionService _vehicleOptionService;
        private readonly ICountryService _manufacturerСountryService;
        private readonly IDriveTypeService _driveTypeService;
        private readonly IBodyTypeService _bodyTypeService;
        private readonly IOptions<GoogleTranslateConfig> _googleTranslateConfig;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ContentController(IMapper mapper, IVehicleTypeService vehicleTypeService, IVehicleMarkService vehicleMarkService, IVehicleModelService vehicleModelService, ITransmissionTypeService transmissionTypeService, IFuelTypeService fuelTypeService, IVehicleColorService vehicleColorService, IVehicleOptionService vehicleOptionService, ICountryService manufacturerСountryService, IDriveTypeService driveTypeService, IBodyTypeService bodyTypeService, IOptions<GoogleTranslateConfig> googleTranslateConfig, IWebHostEnvironment webHostEnvironment) : base(mapper)
        {
            _vehicleTypeService = vehicleTypeService;
            _vehicleMarkService = vehicleMarkService;
            _vehicleModelService = vehicleModelService;
            _transmissionTypeService = transmissionTypeService;
            _fuelTypeService = fuelTypeService;
            _vehicleColorService = vehicleColorService;
            _vehicleOptionService = vehicleOptionService;
            _manufacturerСountryService = manufacturerСountryService;
            _driveTypeService = driveTypeService;
            _bodyTypeService = bodyTypeService;
            _googleTranslateConfig = googleTranslateConfig;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //AddorUpdateTypeAndLocalize();
            //AddorUpdateMarkAndLocalize();
            
            
            AddorUpdateModel();
            //AddorUpdateTransmissionTypeAndLocalize();
            //AddorUpdateFuelTypeAndLocalize();
            //AddorUpdateOptionsAndLocalize();
            //AddorUpdateColorAndLocalize();
            //AddorUpdateCountriesAndLocalize();
            //AddorUpdateDriveTypeAndLocalize();
            //AddorUpdateBodyTypeAndLocalize();

            return View();
        }

        /// <summary>
        /// получение и обновление типов кузова в зависимости от категории
        /// </summary>
        private void AddorUpdateBodyTypeAndLocalize()
        {
            
            var translateClient = new GoogleTranslateClient(_googleTranslateConfig);

            var autoRiaClient = new AutoRiaClient();

            var vehicleTypes = _vehicleTypeService.GetVehicleTypes();

            foreach (var vehicleType in vehicleTypes)
            {
                var metadataList = autoRiaClient.GetBodyStyles(vehicleType.ExternalId);

                foreach (var metaData in metadataList)
                {
                    if (!_bodyTypeService.GetByExternalId(metaData.ExternalId))
                    {
                        var localizations = new List<Localization>();
                        localizations.Add(Heplers.Localization(metaData.Name, Culture.RuCode));
                        localizations.Add(Heplers.Localization(translateClient.TranslateToEnglish(metaData.Name), Culture.EnCode));
                        localizations.Add(Heplers.Localization(translateClient.TranslateToKorean(metaData.Name), Culture.KoCode));

                        var entity = new BodyType();
                        entity.Name = new LocalizationSet();
                        entity.Name.Localizations = localizations;
                        entity.ExternalId = metaData.ExternalId;
                        entity.VehicleType = _vehicleTypeService.GetById(vehicleType.Id);

                        _bodyTypeService.Add(entity);
                    }
                }
            }
        }

        /// <summary>
        /// получение и обновление типов привода в зависимости от категории
        /// </summary>
        private void AddorUpdateDriveTypeAndLocalize()
        {
            var translateClient = new GoogleTranslateClient(_googleTranslateConfig);

            var autoRiaClient = new AutoRiaClient();

            var vehicleTypes = _vehicleTypeService.GetVehicleTypes();

            foreach (var vehicleType in vehicleTypes)
            {
                var metadataList = autoRiaClient.GetDriverTypes(vehicleType.ExternalId);

                foreach (var metaData in metadataList)
                {
                    if (!_driveTypeService.GetByExternalId(metaData.ExternalId))
                    {
                        var localizations = new List<Localization>();
                        localizations.Add(Heplers.Localization(metaData.Name, Culture.RuCode));
                        localizations.Add(Heplers.Localization(translateClient.TranslateToEnglish(metaData.Name), Culture.EnCode));
                        localizations.Add(Heplers.Localization(translateClient.TranslateToKorean(metaData.Name), Culture.KoCode));

                        var entity = new DriveType();
                        entity.Name = new LocalizationSet();
                        entity.Name.Localizations = localizations;
                        entity.ExternalId = metaData.ExternalId;
                        entity.VehicleType = _vehicleTypeService.GetById(vehicleType.Id);

                        _driveTypeService.Add(entity);
                    }
                }
            }
        }

        /// <summary>
        /// получение / обновление страны и сохранение в с учетом локализаций
        /// </summary>
        private void AddorUpdateCountriesAndLocalize()
        {
            var translateClient = new GoogleTranslateClient(_googleTranslateConfig);

            var client = new AutoRiaClient();

            var metadataList = client.GetCountries();

            foreach (var metaData in metadataList)
            {
                if (!_manufacturerСountryService.GetByExternalId(metaData.ExternalId))
                {
                    var localizations = new List<Localization>();
                    localizations.Add(Heplers.Localization(metaData.Name, Culture.RuCode));
                    localizations.Add(Heplers.Localization(translateClient.TranslateToEnglish(metaData.Name), Culture.EnCode));
                    localizations.Add(Heplers.Localization(translateClient.TranslateToKorean(metaData.Name), Culture.KoCode));

                    var entity = new Country();
                    entity.Name = new LocalizationSet();
                    entity.Name.Localizations = localizations;
                    entity.ExternalId = metaData.ExternalId;

                    _manufacturerСountryService.Add(entity);
                }
            }
        }

        /// <summary>
        /// получение / обновление цветов и сохранение в с учетом локализаций
        /// </summary>
        private void AddorUpdateColorAndLocalize()
        {
            var translateClient = new GoogleTranslateClient(_googleTranslateConfig);

            var client = new AutoRiaClient();

            var metadataList = client.GetColors();

            foreach (var metaData in metadataList)
            {
                if (!_vehicleColorService.GetByExternalId(metaData.ExternalId))
                {
                    var localizations = new List<Localization>();
                    localizations.Add(Heplers.Localization(metaData.Name, Culture.RuCode));
                    localizations.Add(Heplers.Localization(translateClient.TranslateToEnglish(metaData.Name), Culture.EnCode));
                    localizations.Add(Heplers.Localization(translateClient.TranslateToKorean(metaData.Name), Culture.KoCode));

                    var entity = new VehicleColor();
                    entity.Name = new LocalizationSet();
                    entity.Name.Localizations = localizations;
                    entity.ExternalId = metaData.ExternalId;

                    _vehicleColorService.Add(entity);
                }
            }
        }

        /// <summary>
        /// получение и обновление опций в зависимости от категории
        /// </summary>
        private void AddorUpdateOptionsAndLocalize()
        {
            var translateClient = new GoogleTranslateClient(_googleTranslateConfig);

            var autoRiaClient = new AutoRiaClient();

            var vehicleTypes = _vehicleTypeService.GetVehicleTypes();

            foreach (var vehicleType in vehicleTypes)
            {
                var metadataList = autoRiaClient.GetOptions(vehicleType.ExternalId);

                foreach (var metaData in metadataList)
                {
                    if (!_vehicleOptionService.GetByExternalId(metaData.ExternalId))
                    {
                        var localizations = new List<Localization>();
                        localizations.Add(Heplers.Localization(metaData.Name, Culture.RuCode));
                        localizations.Add(Heplers.Localization(translateClient.TranslateToEnglish(metaData.Name), Culture.EnCode));
                        localizations.Add(Heplers.Localization(translateClient.TranslateToKorean(metaData.Name), Culture.KoCode));

                        var entity = new VehicleOption();
                        entity.Name = new LocalizationSet();
                        entity.Name.Localizations = localizations;
                        entity.ExternalId = metaData.ExternalId;
                        entity.VehicleType = _vehicleTypeService.GetById(vehicleType.Id);

                        _vehicleOptionService.Add(entity);
                    }
                }
            }
        }

        /// <summary>
        /// получение / обновление типов топлива и сохранение в с учетом локализаций
        /// </summary>
        private void AddorUpdateFuelTypeAndLocalize()
        {
            var translateClient = new GoogleTranslateClient(_googleTranslateConfig);

            var client = new AutoRiaClient();

            var metadataList = client.GetFuelTypes();

            foreach (var metaData in metadataList)
            {
                if (!_fuelTypeService.GetByExternalId(metaData.ExternalId))
                {
                    var localizations = new List<Localization>();
                    localizations.Add(Heplers.Localization(metaData.Name, Culture.RuCode));
                    localizations.Add(Heplers.Localization(translateClient.TranslateToEnglish(metaData.Name), Culture.EnCode));
                    localizations.Add(Heplers.Localization(translateClient.TranslateToKorean(metaData.Name), Culture.KoCode));

                    var entity = new FuelType();
                    entity.Name = new LocalizationSet();
                    entity.Name.Localizations = localizations;
                    entity.ExternalId = metaData.ExternalId;

                    _fuelTypeService.Add(entity);
                }
            }
        }

        /// <summary>
        /// получение и обновление типов трансмисии в зависимости от категории
        /// </summary>
        private void AddorUpdateTransmissionTypeAndLocalize()
        {
            var translateClient = new GoogleTranslateClient(_googleTranslateConfig);

            var autoRiaClient = new AutoRiaClient();

            var vehicleTypes = _vehicleTypeService.GetVehicleTypes();

            foreach (var vehicleType in vehicleTypes)
            {
                var metadataList = autoRiaClient.GetTransmissionTypes(vehicleType.ExternalId);

                foreach (var metaData in metadataList)
                {
                    if (!_transmissionTypeService.GetByExternalId(metaData.ExternalId))
                    {
                        var localizations = new List<Localization>();
                        localizations.Add(Heplers.Localization(metaData.Name, Culture.RuCode));
                        localizations.Add(Heplers.Localization(translateClient.TranslateToEnglish(metaData.Name), Culture.EnCode));
                        localizations.Add(Heplers.Localization(translateClient.TranslateToKorean(metaData.Name), Culture.KoCode));

                        var entity = new TransmissionType();
                        entity.Name = new LocalizationSet();
                        entity.Name.Localizations = localizations;
                        entity.ExternalId = metaData.ExternalId;
                        entity.VehicleType = _vehicleTypeService.GetById(vehicleType.Id);

                        _transmissionTypeService.Add(entity);
                    }
                }
            }
        }
        
        /// <summary>
        /// получение / обновление типов ТС и сохранение в с учетом локализаций
        /// </summary>
        private void AddorUpdateTypeAndLocalize()
        {
            var translateClient = new GoogleTranslateClient(_googleTranslateConfig);

            var client = new AutoRiaClient();
            
            var metadataList = client.GetVehicleTypes();

            foreach (var metaData in metadataList)
            {
                if (!_vehicleTypeService.GetByExternalId(metaData.ExternalId))
                {
                    var localizations = new List<Localization>();
                    localizations.Add(Heplers.Localization(metaData.Name, Culture.RuCode));
                    localizations.Add(Heplers.Localization(translateClient.TranslateToEnglish(metaData.Name), Culture.EnCode));
                    localizations.Add(Heplers.Localization(translateClient.TranslateToKorean(metaData.Name), Culture.KoCode));

                    var entity = new VehicleType();
                    entity.Name = new LocalizationSet();
                    entity.Name.Localizations = localizations;
                    entity.ExternalId = metaData.ExternalId;

                    _vehicleTypeService.AddVehicleType(entity);
                }
            }
        }
        
        /// <summary>
        /// получение / обновление марок автомобилей и сохранение в с учетом локализаций
        /// </summary>
        private void AddorUpdateMarkAndLocalize()
        {
            var translateClient = new GoogleTranslateClient(_googleTranslateConfig);

            var autoRiaClient = new AutoRiaClient();

            var vehicleTypes = _vehicleTypeService.GetVehicleTypes();

            foreach (var vehicleType in vehicleTypes)
            {
                var metadataList = autoRiaClient.GetVehicleMarks(vehicleType.ExternalId);

                foreach (var metaData in metadataList)
                {
                    if (!_vehicleMarkService.GetByExternalId(metaData.ExternalId))
                    {
                        var localizations = new List<Localization>();
                        localizations.Add(Heplers.Localization(metaData.Name, Culture.RuCode));
                        localizations.Add(Heplers.Localization(metaData.Name, Culture.EnCode));
                        localizations.Add(Heplers.Localization(translateClient.TranslateToKorean(metaData.Name), Culture.KoCode));

                        var entity = new VehicleMark();
                        entity.Name = new LocalizationSet();
                        entity.Name.Localizations = localizations;
                        entity.ExternalId = metaData.ExternalId;
                        entity.VehicleType = _vehicleTypeService.GetById(vehicleType.Id);

                        _vehicleMarkService.AddVehicleMark(entity);
                    }
                }
            }
        }

        /// <summary>
        /// получение / обновление моделей автомобилей
        /// </summary>
        private void AddorUpdateModel()
        {
            var client = new AutoRiaClient();

            var vehicleMarks = _vehicleMarkService.GeVehicleMarks();

            foreach (var vehicleMark in vehicleMarks)
            {
                var metadataList = client.GetVehicleModels(vehicleMark.VehicleType.ExternalId, vehicleMark.ExternalId);

                foreach (var metaData in metadataList)
                {
                    if (!_vehicleModelService.GetByExternalId(metaData.ExternalId))
                    {
                        var entity = new VehicleModel();
                        entity.Name = metaData.Name;
                        entity.ExternalId = metaData.ExternalId;
                        entity.VehicleMark = _vehicleMarkService.GetById(vehicleMark.Id);
                        _vehicleModelService.AddVehicleModel(entity);
                    }
                }
            }
        }
    }
}
