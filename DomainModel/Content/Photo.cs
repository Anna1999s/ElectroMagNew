namespace DomainModel.Content
{
    public class Photo : BaseEntity
    {
        public string Path { get; set; }
        public int? ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}