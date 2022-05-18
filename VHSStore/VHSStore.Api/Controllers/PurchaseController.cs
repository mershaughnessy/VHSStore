using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using VHSStore.Application.Interfaces.Repos_Interfaces;
using VHSStore.Domain.Models;
using VHSStore.Domain.Models.InvoiceModels;
using VHSStore.Domain.Models.PurchaseModels;

namespace VHSStore.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInvoiceDetailRepository _invoiceDetailRepository;
        private readonly IMovieRepository _movieRepository;

        public PurchaseController(IInvoiceRepository invoiceRepository,
            IInvoiceDetailRepository invoiceDetailRepository,
            IMovieRepository movieRepository)
        {
            _invoiceRepository = invoiceRepository;
            _invoiceDetailRepository = invoiceDetailRepository;
            _movieRepository = movieRepository;
        }

        [HttpPost("CompletePurchase")]
        public async Task<IActionResult> CompletePurchase(CompletePurchaseModel completePurchase)
        {
            var invoiceNumber = Guid.NewGuid().ToString();
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _invoiceRepository.AddAsync(
                    new InvoiceModel
                    {
                        InvoiceNumber = invoiceNumber,
                        CustomerId = completePurchase.CustomerId,
                        TotalInvoicePrice = completePurchase.TotalInvoicePrice,
                        PurchaseDate = DateTime.Now,
                        ADDR1 = completePurchase.ADDR1,
                        ADDR2 = completePurchase.ADDR2,
                        PostCode = completePurchase.PostCode
                    });

                for (int i = 0; i < completePurchase.InvoiceDetails.Count(); i++)
                {
                    await _invoiceDetailRepository.AddAsync(
                        new InvoiceDetailModel
                        {
                            MovieId = completePurchase.InvoiceDetails[i].MovieId,
                            Quantity = completePurchase.InvoiceDetails[i].Quantity,
                            UnitPrice = completePurchase.InvoiceDetails[i].UnitPrice,
                            TotalPrice = completePurchase.InvoiceDetails[i].Quantity * completePurchase.InvoiceDetails[i].UnitPrice,
                            InvoiceNumber = invoiceNumber
                        });

                    await _movieRepository.StockChangeAsync(completePurchase.InvoiceDetails[i].MovieId, -completePurchase.InvoiceDetails[i].Quantity);
                }

                transaction.Complete();
            }

            return Ok(new BaseResponse<string>()
            {
                Body = "Purchase Successfully completed."
            });
        }
    }
}
