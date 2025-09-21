namespace GamingEcommerce.DAL.DataContext.Entities
{
    public class WebsiteInfo : BaseEntity
    {
        public required string HelpPhone { get; set; }
        public required string SupportPhone { get; set; }
        public required string Address { get; set; }
        public required string Email { get; set; }
        public required string Copyright { get; set; }
    }
}