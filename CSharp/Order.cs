// Models/Order.cs
namespace OrderApi.Models
{
    public class Order
    {
        public int Id { get; set; } // Typically auto-generated
        public string CustomerName { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime OrderDate { get; set; }
    }
}