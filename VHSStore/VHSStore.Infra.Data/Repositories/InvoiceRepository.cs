using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using VHSStore.Application.Interfaces.Repos_Interfaces;
using VHSStore.Domain.Models.InvoiceModels;
using VHSStore.Utility.Sql;

namespace VHSStore.Infra.Data.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly DapperWrap _dapperWrap;

        public InvoiceRepository(IConfiguration configuration)
        {
            _dapperWrap = new DapperWrap(configuration.GetConnectionString("VHSStoreDBConnection"));
        }

        public async Task<int> AddAsync(InvoiceModel entity)
        {
            var result = await _dapperWrap.ExecuteAsync(@"INSERT INTO [Invoices] 
                        (IndexId, InvoiceNumber, CustomerId, TotalInvoicePrice, PurchaseDate, ADDR1, ADDR2, PostCode, Shipped)
                    VALUES
                        (newId(), @InvoiceNumber, @CustomerId, @TotalInvoicePrice, @PurchaseDate, @ADDR1, @ADDR2, @PostCode, 0)",
                        entity);
            return result;
        }

        public async Task<IEnumerable<InvoiceModel>> GetAllAsync()
        {
            var result = await _dapperWrap.QueryAsync<InvoiceModel>(@"SELECT * FROM [Invoices]");
            return result;
        }

        public async Task<InvoiceModel> GetByIdAsync(string id)
        {
            var result = await _dapperWrap.QuerySingleAsync<InvoiceModel>(@"SELECT * FROM [Invoices] WHERE [IndexId] = @IndexId", new { IndexId = id });
            return result;
        }

        public async Task<int> UpdateShippingStatusAsync(string invoiceNumber, bool shipped)
        {
            var result = await _dapperWrap.ExecuteAsync(@"UPDATE [Invoices] SET [Shipped] = @Shipped WHERE [InvoiceNumber] = @InvoiceNumber",
                new
                {
                    InvoiceNumber = invoiceNumber,
                    Shipped = shipped
                });
            return result;
        }

        public async Task<IEnumerable<InvoiceModel>> GetInvoicesPendingShipmentAsync()
        {
            var result = await _dapperWrap.QueryAsync<InvoiceModel>(@"SELECT * FROM [Invoices] WHERE [Shipped] = 0");
            return result;
        }
    }
}
