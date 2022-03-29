using System;
using System.Collections.Generic;
using System.Text;

namespace VHSStore.Domain.Models.InvoiceDetailModels
{
    public class AddInvoiceDetailModel
    {
        public string MovieId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
