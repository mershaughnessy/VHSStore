using System.Collections.Generic;
using System.Threading.Tasks;
using VHSStore.Domain.Models.InvoiceModels;

namespace VHSStore.Application.Interfaces.Repos_Interfaces
{
    public interface IInvoiceRepository
    {
        public Task<int> AddAsync(InvoiceModel entity);
        public Task<IEnumerable<InvoiceModel>> GetAllAsync();
        public Task<InvoiceModel> GetByIdAsync(string id);
        public Task<int> UpdateShippingStatusAsync(string invoiceNumber, bool shipped);
        public Task<IEnumerable<InvoiceModel>> GetInvoicesPendingShipmentAsync();
    }
}
