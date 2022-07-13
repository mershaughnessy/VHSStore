using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using VHSStore.Application.Interfaces.Repos_Interfaces;
using VHSStore.Domain.Models;
using VHSStore.Utility.Sql;

namespace VHSStore.Infra.Data.Repositories
{
    public class InvoiceDetailRepository : IInvoiceDetailRepository
    {
        private readonly DapperWrap _dapperWrap;

        public InvoiceDetailRepository(IConfiguration configuration)
        {
            _dapperWrap = new DapperWrap(configuration.GetConnectionString("VHSStoreDBConnection"));
        }

        public async Task<int> AddAsync(InvoiceDetailModel entity)
        {
            var result = await _dapperWrap.ExecuteAsync(@"INSERT INTO [InvoiceDetails]
                        (IndexId, MovieId, Quantity, UnitPrice, TotalPrice, InvoiceNumber)
                    VALUES
                        (newId(), @MovieId, @Quantity, @UnitPrice, @TotalPrice, @InvoiceNumber)",
                        entity);
            return result;
        }
    }
}
