using MomAndBaby.BusinessObject.Entity;

namespace MomAndBaby.BusinessObject.Models
{
  public class OrderResponseModel
  {
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public string StatusName { get; set; }
    public string CustomerName { get; set; }
    public decimal TotalAmount { get; set; }
    public string? PaymentMethod { get; set; }

    public string? ShippingAddress { get; set; }

  }
}
