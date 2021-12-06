using System.Collections.Generic;
using BusinessMan.Domain.Entities;

namespace BusinessMan.API.DTOs
{
    public class GetAllTransactionsResponse
    {
        /// <summary>
        /// List of all existing transactions.
        /// </summary>
        public List<Transaction> Transactions { get; set; }
    }
}
