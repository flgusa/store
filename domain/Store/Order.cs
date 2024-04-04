using System;
using System.Collections.Generic;
using System.Linq;

namespace Store
{
    public class Order
    {
        public int Id { get; }

        private readonly List<OrderItem> items;

        public OrderItemCollection Items { get; }

        public string CellPhone { get; set; }

        public OrderDelivery Delivery { get; set; }

        public OrderPayment Payment { get; set; }

        /*public int TotalCount
        {
            get { return items.Sum(item => item.Count); }
        }*/

        public int TotalCount => Items.Sum(item => item.Count);

        /*public decimal TotalPrice
        {
            get { return items.Sum(item => item.Price * item.Count); }
        }*/

        public decimal TotalPrice => Items.Sum(item => item.Price * item.Count) + (Delivery?.Amount ?? 0m);

        public Order(int id, IEnumerable<OrderItem> items)
        {
            Id = id;

            Items = new OrderItemCollection(items);
        }

        private void ThrowBookException(string message, int bookId)
        {
            var exception = new InvalidOperationException(message);

            exception.Data["BookId"] = bookId;

            throw exception;
        }
    }
}
