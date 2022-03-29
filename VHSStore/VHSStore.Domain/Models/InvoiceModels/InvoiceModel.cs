using System;

namespace VHSStore.Domain.Models.InvoiceModels
{
    public class InvoiceModel
    {
        public string IndexId { get; set; }
        public string InvoiceNumber { get; set; }
        public string CustomerId { get; set; }
        public decimal TotalInvoicePrice { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string ADDR1 { get; set; }
        public string ADDR2 { get; set; }
        public string PostCode { get; set; }
        public bool Shipped { get; set; }
    }
}
