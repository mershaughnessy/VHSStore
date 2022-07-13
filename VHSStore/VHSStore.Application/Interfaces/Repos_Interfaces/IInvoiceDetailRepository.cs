using System.Threading.Tasks;
using VHSStore.Domain.Models;

namespace VHSStore.Application.Interfaces.Repos_Interfaces
{
    public interface IInvoiceDetailRepository
    {
        public Task<int> AddAsync(InvoiceDetailModel entity);
    }
}
