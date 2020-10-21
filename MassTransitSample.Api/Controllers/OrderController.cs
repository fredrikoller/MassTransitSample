using System;
using System.Threading.Tasks;
using MassTransit;
using MassTransitSample.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MassTransitSample.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IRequestClient<SubmitOrder> _submitOrderRequestClient;

        public OrderController(ILogger<OrderController> logger, IRequestClient<SubmitOrder> submitOrderRequestClient)
        {
            _logger = logger;
            _submitOrderRequestClient = submitOrderRequestClient;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(Guid id, string customerNumber)
        {
            var (accepted, rejected) = await _submitOrderRequestClient.GetResponse<OrderSubmissionAccepted, OrderSubmissionRejected>(new
            {
                OrderId = id,
                InVar.Timestamp,
                CustomerNumber = customerNumber

            });

            if (accepted.IsCompletedSuccessfully)
            {
                var response = await accepted;
                
                return Ok(response);
            }
            else
            {
                var response = await rejected;

                return BadRequest(response.Message);
            }
        }

    }
}
