using System.Collections.Generic;
using BusinessMan.Domain.Entities;

namespace BusinessMan.Domain.Interfaces
{
    public interface ITransactionService
    {
        /// <summary>
        /// Method that gets all the existing transactions of all products.
        /// </summary>
        /// <returns>The list of transactions.</returns>
        List<Transaction> GetAllTransactions();

        /// <summary>
        /// Method that gets all the transactions for a given product.
        /// </summary>
        /// <param name="sku"></param>
        /// <param name="transactions"></param>
        /// <returns>The list of product transactions and the total cost.</returns>
        (List<Transaction>, decimal) GetProductTransactions(string sku, List<Transaction> transactions = null);
    }
}
