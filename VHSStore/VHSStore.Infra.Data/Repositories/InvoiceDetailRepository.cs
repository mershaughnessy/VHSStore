using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using VHSStore.Application.Interfaces.Repos_Interfaces;
using VHSStore.Domain.Models;

namespace VHSStore.Infra.Data.Repositories
{
    public class InvoiceDetailRepository : IInvoiceDetailRepository
    {
        private readonly IConfiguration _configuration;

        public InvoiceDetailRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<int> AddAsync(InvoiceDetailModel entity)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("VHSStoreDBConnection")))
            {
                await connection.OpenAsync();
                var result = await connection.ExecuteAsync(
                    @"INSERT INTO [InvoiceDetails]
                        (IndexId, MovieId, Quantity, UnitPrice, TotalPrice, InvoiceNumber)
                    VALUES
                        (newId(), @MovieId, @Quantity, @UnitPrice, @TotalPrice, @InvoiceNumber)",
                    entity);
                return result;
            }
        }

        public Task<int> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<InvoiceDetailModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<InvoiceDetailModel> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(InvoiceDetailModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
