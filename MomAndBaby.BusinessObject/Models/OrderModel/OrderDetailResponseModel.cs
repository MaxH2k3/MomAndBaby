namespace MomAndBaby.BusinessObject.Models;

public class OrderDetailResponseModel
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductImage { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}