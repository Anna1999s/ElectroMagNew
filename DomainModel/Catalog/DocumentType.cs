using System.Collections.Generic;
using DomainModel.Content;
using DomainModel.Localization;

namespace DomainModel.Catalog
{
    /// <summary>
    /// Тип документа
    /// </summary>
    public class DocumentType : BaseEntity
    {
        public DocumentType()
        {
            Name = new LocalizationSet();
        }

        public int? NameId { get; set; }
        public LocalizationSet Name { get; set; }
        public List<Vehicle> Vehicles { get; set; }
    }
}