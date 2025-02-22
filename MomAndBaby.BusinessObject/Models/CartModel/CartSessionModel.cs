namespace MomAndBaby.BusinessObject.Models.CartSessionModel
{
    public class CartSessionModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int NumberOfProduct { get; set; } = 0;
        public int? Stock { get; set; }
        public string? Image { get; set; }
        public decimal UnitPrice { get; set; } = 0;

    }
}
