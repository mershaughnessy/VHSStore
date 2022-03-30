using System.Collections.Generic;
using System.Threading.Tasks;
using VHSStore.Domain.Models.InvoiceModels;

namespace VHSStore.Application.Interfaces.Repos_Interfaces
{
    public interface IInvoiceRepository : IGenericRepository<InvoiceModel>
    {
        public Task<int> UpdateShippingStatusAsync(string invoiceNumber, bool shipped);
        public Task<IEnumerable<InvoiceModel>> GetInvoicesPendingShipmentAsync();
    }
}
