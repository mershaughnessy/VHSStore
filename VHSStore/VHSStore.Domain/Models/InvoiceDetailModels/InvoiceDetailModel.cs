using System;
using System.Collections.Generic;
using System.Text;

namespace VHSStore.Domain.Models
{
    public class InvoiceDetailModel
    {
        public string IndexId { get; set; }
        public string MovieId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string InvoiceNumber { get; set; }
    }
}
