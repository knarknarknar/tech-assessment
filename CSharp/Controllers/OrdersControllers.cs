// Controllers/OrdersController.cs
using Microsoft.AspNetCore.Mvc;
using OrderApi.Models;
using System.Linq;

namespace OrderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        // A simple in-memory list to simulate a database
        private static List<Order> Orders = new List<Order>();

        // POST: api/orders
        [HttpPost]
        public IActionResult CreateOrder([FromBody] Order newOrder)
        {
            if (newOrder == null)
            {
                return BadRequest("Order data is null");
            }

            // Generate a new ID (in a real application, this would be done by a database)
            newOrder.Id = Orders.Count + 1;

            // Save the new order (in-memory in this case)
            Orders.Add(newOrder);

            // Return a response indicating that the order was created
            return CreatedAtAction(nameof(GetOrder), new { id = newOrder.Id }, newOrder);
        }

        // GET: api/orders/{id}
        [HttpGet("{id}")]
        public IActionResult GetOrder(int id)
        {
            var order = Orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        // GET: api/orders/customer/{customerName}
        [HttpGet("customer/{customerName}")]
        public IActionResult GetOrdersByCustomer(string customerName)
        {
            var customerOrders = Orders.Where(o => o.CustomerName.Equals(customerName, StringComparison.OrdinalIgnoreCase)).ToList();
            
            if (!customerOrders.Any())
            {
                return NotFound($"No orders found for customer {customerName}");
            }

            return Ok(customerOrders);
        }
    }
}
