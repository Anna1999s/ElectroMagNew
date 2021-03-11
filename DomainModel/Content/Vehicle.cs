using System.Collections.Generic;
using DomainModel.Catalog;

namespace DomainModel.Content
{
    public class Vehicle : BaseEntity
    {
        /// <summary>
        ///     VIN
        /// </summary>
        public string VinCode { get; set; }

        /// <summary>
        ///     Пробег
        /// </summary>
        public int Odometer { get; set; }

        /// <summary>
        ///     Наличие ключей
        /// </summary>
        public bool Keys { get; set; }

        /// <summary>
        ///     Идентификатор модели
        /// </summary>
        public int? VehicleModelId { get; set; }

        /// <summary>
        ///     Модель
        /// </summary>
        public VehicleModel VehicleModel { get; set; }

        /// <summary>
        ///     Идентификатор типа кузова
        /// </summary>
        public int? BodyTypeId { get; set; }

        /// <summary>
        ///     Типа кузова
        /// </summary>
        public BodyType BodyType { get; set; }

        /// <summary>
        ///     Идентификатор типа привода
        /// </summary>
        public int? DriveTypeId { get; set; }

        /// <summary>
        ///     Тип привода
        /// </summary>
        public DriveType DriveType { get; set; }

        /// <summary>
        ///     Идентификатор типа топлива
        /// </summary>
        public int? FuelTypeId { get; set; }

        /// <summary>
        ///     Тип топлива
        /// </summary>
        public FuelType FuelType { get; set; }

        /// <summary>
        ///     Идентификатор страны-производителя
        /// </summary>
        public int? ManufacturerCountryId { get; set; }

        /// <summary>
        ///     Страна-производитель
        /// </summary>
        public Country ManufacturerCountry { get; set; }

        /// <summary>
        ///     Идентификатор типа КПП
        /// </summary>
        public int? TransmissionTypeId { get; set; }

        /// <summary>
        ///     Тип КПП
        /// </summary>
        public TransmissionType TransmissionType { get; set; }

        /// <summary>
        ///     Идентификатор цвета
        /// </summary>
        public int? VehicleColorId { get; set; }

        /// <summary>
        ///     Цвет
        /// </summary>
        public VehicleColor VehicleColor { get; set; }

        /// <summary>
        ///     Опции
        /// </summary>
        public virtual List<VehicleOption> VehicleOptions { get; set; } = new List<VehicleOption>();

        /// <summary>
        ///     Идентификатор типа документа
        /// </summary>
        public int? DocumentTypeId { get; set; }

        /// <summary>
        ///     Тип документа
        /// </summary>
        public DocumentType DocumentType { get; set; }

        /// <summary>
        ///     Идентификатор типа причиненного ушерба
        /// </summary>
        public int? DamageTypeId { get; set; }

        /// <summary>
        ///     Причиненный ущерб
        /// </summary>
        public DamageType DamageType { get; set; }

        /// <summary>
        ///     Год
        /// </summary>
        public string Year { get; set; }

        /// <summary>
        ///     Объем двигателя
        /// </summary>
        public string EngineDisplacement { get; set; }

        /// <summary>
        ///     Фото
        /// </summary>
        public List<Photo> Photos { get; set; } = new List<Photo>();
        public string ExternalId { get; set; }
    }
}