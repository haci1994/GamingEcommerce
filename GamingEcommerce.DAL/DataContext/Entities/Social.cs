namespace GamingEcommerce.DAL.DataContext.Entities
{
    public class Social : BaseEntity
    {
        public required string Name { get; set; }
        public required string Class { get; set; }
        public string? Url { get; set; }
    }
}