using System.Collections.Generic;
using Vueling.Domain.Entities;

namespace Vueling.API.DTOs
{
    public class GetAllTransactionsResponse
    {
        /// <summary>
        /// List of all existing transactions.
        /// </summary>
        public List<Transaction> Transactions { get; set; }
    }
}
