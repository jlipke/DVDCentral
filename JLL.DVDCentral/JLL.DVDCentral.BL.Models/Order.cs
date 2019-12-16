using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JLL.DVDCentral.BL.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public string UserId { get; set; }
        public string PaymentReceipt { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}
