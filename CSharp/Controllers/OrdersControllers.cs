
using Microsoft.AspNetCore.Mvc;
using OrderApi.Models;
using System.Linq;

namespace Controllers
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

        // PUT: api/orders/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, [FromBody] Order updatedOrder)
        {
            if (updatedOrder == null)
            {
                return BadRequest("Order data is null");
            }

            // Find the existing order
            var existingOrder = Orders.FirstOrDefault(o => o.Id == id);
            if (existingOrder == null)
            {
                return NotFound($"Order with ID {id} not found");
            }

            // Update the order details
            existingOrder.CustomerName = updatedOrder.CustomerName ?? existingOrder.CustomerName;
            existingOrder.Product = updatedOrder.Product ?? existingOrder.Product;
            existingOrder.Quantity = updatedOrder.Quantity > 0 ? updatedOrder.Quantity : existingOrder.Quantity;
            existingOrder.Price = updatedOrder.Price > 0 ? updatedOrder.Price : existingOrder.Price;
            existingOrder.OrderDate = updatedOrder.OrderDate != default ? updatedOrder.OrderDate : existingOrder.OrderDate;

            return Ok(existingOrder); // Return the updated order
        }

        // DELETE: api/orders/{id}
        [HttpDelete("{id}")]
        public IActionResult CancelOrder(int id)
        {
            // Find the order to cancel
            var order = Orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound($"Order with ID {id} not found");
            }

            // Remove the order from the list (simulating cancellation)
            Orders.Remove(order);

            // Return a 200 OK response confirming the cancellation
            return Ok($"Order with ID {id} has been cancelled");
        }
    }
}
