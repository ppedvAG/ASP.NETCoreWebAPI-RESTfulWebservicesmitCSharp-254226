namespace OrderService.WebApi.Models.Domain;

public class Order
{
    public string OrderId { get; set; }

    public string CustomerId { get; set; }

    public string ProductId { get; set; }

    public OrderStatus Status { get; set; }

    public DateTime DeliveryDate { get; set; }
}

public enum OrderStatus
{
    Ordered,
    Delivered,
    Cancelled
}
