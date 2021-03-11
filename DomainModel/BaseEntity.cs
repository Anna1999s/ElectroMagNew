using System;

namespace DomainModel
{
    public class BaseEntity
    {
        protected BaseEntity()
        {
            Created = DateTime.UtcNow;
        }

        public const int NameLength = 256;
        public const int HandleLength = 256;

        public int Id { get; set; }

        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public bool IsDelete { get; set; }
    }
}