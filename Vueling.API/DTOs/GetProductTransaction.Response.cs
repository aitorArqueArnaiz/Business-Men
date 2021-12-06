using System.Collections.Generic;
using Vueling.Domain.Entities;

namespace Vueling.API.DTOs
{
    public class GetProductTransactionResponse
    {
        /// <summary>
        /// List of all product transactions.
        /// </summary>
        public List<Transaction> Transactions { get; set; }

        /// <summary>
        /// Total summ of all transactions.
        /// </summary>
        public decimal Total { get; set; }
    }
}
