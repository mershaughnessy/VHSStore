using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VHSStore.Domain.Models.InvoiceDetailModels;

namespace VHSStore.Domain.Models.PurchaseModels
{
    public class CompletePurchaseModel
    {
        [Required(ErrorMessage = "Missing: CustomerId")]
        public string CustomerId { get; set; }
        [Required(ErrorMessage = "Missing: ADDR1")]
        public string ADDR1 { get; set; }
        public string ADDR2 { get; set; }
        [Required(ErrorMessage = "Missing: PostCode")]
        public string PostCode { get; set; }
        [Required(ErrorMessage = "Missing: TotalInvoicePrice")]
        public decimal TotalInvoicePrice { get; set; }
        [Required(ErrorMessage = "Missing: InvoiceDetails")]
        public IReadOnlyList<AddInvoiceDetailModel> InvoiceDetails { get; set; }
    }
}
