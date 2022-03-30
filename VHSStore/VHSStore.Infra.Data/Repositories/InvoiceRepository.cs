using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using VHSStore.Application.Interfaces.Repos_Interfaces;
using VHSStore.Domain.Models.InvoiceModels;

namespace VHSStore.Infra.Data.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly IConfiguration _configuration;

        public InvoiceRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> AddAsync(InvoiceModel entity)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("VHSStoreDBConnection")))
            {
                await connection.OpenAsync();
                var result = await connection.ExecuteAsync(
                    @"INSERT INTO [Invoices] 
                        (IndexId, InvoiceNumber, CustomerId, TotalInvoicePrice, PurchaseDate, ADDR1, ADDR2, PostCode, Shipped)
                    VALUES
                        (newId(), @InvoiceNumber, @CustomerId, @TotalInvoicePrice, @PurchaseDate, @ADDR1, @ADDR2, @PostCode, 0)",
                    entity);
                return result;
            }
        }

        public Task<int> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<InvoiceModel>> GetAllAsync()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("VHSStoreDBConnection")))
            {
                await connection.OpenAsync();
                var result = await connection.QueryAsync<InvoiceModel>(
                    @"SELECT * FROM [Invoices]");
                return result.AsList();
            }
        }

        public async Task<InvoiceModel> GetByIdAsync(string id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("VHSStoreDBConnection")))
            {
                await connection.OpenAsync();
                var result = await connection.QuerySingleAsync<InvoiceModel>(
                    @"SELECT * FROM [Invoices] WHERE [IndexId] = @IndexId", new { IndexId = id });
                return result;
            }
        }

        public async Task<int> UpdateShippingStatusAsync(string invoiceNumber, bool shipped)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("VHSStoreDBConnection")))
            {
                await connection.OpenAsync();
                var result = await connection.ExecuteAsync(
                    @"UPDATE [Invoices] SET [Shipped] = @Shipped WHERE [InvoiceNumber] = @InvoiceNumber",
                    new 
                    {
                        InvoiceNumber = invoiceNumber,
                        Shipped = shipped
                    });
                return result;
            }
        }

        public async Task<IEnumerable<InvoiceModel>> GetInvoicesPendingShipmentAsync()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("VHSStoreDBConnection")))
            {
                await connection.OpenAsync();
                var result = await connection.QueryAsync<InvoiceModel>(
                    @"SELECT * FROM [Invoices] WHERE [Shipped] = 0");
                return result;
            }
        }

        public Task<int> UpdateAsync(InvoiceModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
