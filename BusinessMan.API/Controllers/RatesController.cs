using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using BusinessMan.API.DTOs;
using BusinessMan.Domain.Interfaces;

namespace BusinessMan.API.Controllers
{
    [ApiController]
    [Route("api/v1/vueling/")]
    public class RatesController : ControllerBase
    {
        /// <summary>
        /// Logger object for controller errors and exceptions.
        /// </summary>
        private readonly ILogger<RatesController> _logger;

        /// <summary>
        /// Service object for transactions.
        /// </summary>
        private readonly IRateService _rateService;

        public RatesController(ILogger<RatesController> logger,
                                     IRateService transactionService)
        {
            _logger = logger;
            _rateService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
        }

        [HttpGet("all-rates")]
        public async Task<IActionResult> GetAllRates([FromHeader] GetAllRatesRequest request)
        {
            if (request == null) return NotFound("Empty request");
            try
            {
                // Execute the service operation
                var response = await Task.Run(() => _rateService.GetAllRates());

                // Convert domain response into GetAllRatesResponse DTO.
                GetAllRatesResponse responseDto = new GetAllRatesResponse()
                {
                    Rates = response
                };
                return Ok(responseDto);
            }
            catch (Exception error)
            {
                _logger.LogError($"Exception occured during get all rates operation. Message {error.Message}");
                return BadRequest();
            }
        }

    }
}
