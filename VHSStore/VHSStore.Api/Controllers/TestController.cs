using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using System;
using System.Threading.Tasks;
using System.Transactions;
using VHSStore.Application.Interfaces;
using VHSStore.Domain.Models.GenreModels;

namespace VHSStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
    }
}
