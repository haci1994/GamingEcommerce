namespace GamingEcommerce.DAL.DataContext.Entities
{
    public class Currency : BaseEntity
    {
        public required string Name { get; set; }
        public required string FlagName { get; set; }
        public double ExchangeRate { get; set; }
    }
}