using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VHSStore.Application.Interfaces.Repos_Interfaces;
using VHSStore.Domain.Models;
using VHSStore.Domain.Models.InvoiceModels;

namespace VHSStore.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingController : ControllerBase
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public ShippingController(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        [HttpPost("ShipPurchase")]
        public async Task<IActionResult> ShipPurchase(string invoiceNumber)
        {
            try
            {
                var data = await _invoiceRepository.UpdateShippingStatusAsync(invoiceNumber, true);

                return Ok(new BaseResponse<string>()
                {
                    Body = $"Affected Rows: {data}"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse<string>()
                { 
                    HasError = true,
                    Error = ex.Message
                });
            }
        }

        [HttpGet("OutstandingShipments")]
        public async Task<IActionResult> OutstandingShipments()
        {
            try
            {
                var data = await _invoiceRepository.GetInvoicesPendingShipmentAsync();

                return Ok(new BaseResponse<IEnumerable<InvoiceModel>>()
                { 
                    Body = data
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse<string>()
                { 
                    HasError = true,
                    Error = ex.Message
                });
            }
        }
    }
}
