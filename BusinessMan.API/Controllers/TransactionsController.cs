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
    public class TransactionsController : ControllerBase
    {
        /// <summary>
        /// Logger object for controller errors and exceptions.
        /// </summary>
        private readonly ILogger<TransactionsController> _logger;

        /// <summary>
        /// Service object for transactions.
        /// </summary>
        private readonly ITransactionService _transactionService;

        public TransactionsController(ILogger<TransactionsController> logger,
                                     ITransactionService transactionService)
        {
            _logger = logger;
            _transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
        }

        [HttpGet("all-transactions")]
        public async Task<IActionResult> GetAllTransactions([FromHeader] GetAllTransactionsRequest request)
        {
            if (request == null) return NotFound("Empty request");
            try
            {
                // Execute the service operation
                var response = await Task.Run(() => _transactionService.GetAllTransactions());

                // Convert domain response into GetAllRatesResponse DTO.
                GetAllTransactionsResponse responseDto = new GetAllTransactionsResponse()
                {
                    Transactions = response
                };
                return Ok(responseDto);
            }
            catch (Exception error)
            {
                _logger.LogError($"Exception occured during get all transactions operation. Message {error.Message}");
                return BadRequest();
            }
        }

        [HttpPost("sku")]
        public async Task<IActionResult> GetProductTransactions([FromBody] GetProductTransactionRequest request)
        {
            if (request == null) return NotFound("Empty request");
            try
            {
                // Execute the service operation
                var response = await Task.Run(() => _transactionService.GetProductTransactions(request.SKU));

                // Convert domain response into GetAllRatesResponse DTO.
                GetProductTransactionResponse responseDto = new GetProductTransactionResponse()
                {
                    Transactions = response.Item1,
                    Total = response.Item2
                };
                return Ok(responseDto);
            }
            catch (Exception error)
            {
                _logger.LogError($"Exception occured during get product transactions operation. Message {error.Message}");
                return BadRequest();
            }
        }

    }
}
