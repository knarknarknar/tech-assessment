
namespace CSharp.models
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
