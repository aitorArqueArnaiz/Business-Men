using System.Collections.Generic;
using BusinessMan.Domain.Entities;

namespace BusinessMan.API.DTOs
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
