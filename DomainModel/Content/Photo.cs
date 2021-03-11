namespace DomainModel.Content
{
    public class Photo : BaseEntity
    {
        public string Path { get; set; }
        public int? VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}