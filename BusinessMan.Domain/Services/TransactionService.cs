using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using BusinessMan.Domain.ApiRest_Client;
using BusinessMan.Domain.Entities;
using BusinessMan.Domain.Interfaces;
using BusinessMan.Infrastructure.Repository;
using static BusinessMan.Domain.Shared.Enums;

namespace BusinessMan.Domain.Services
{
    public class TransactionService : ITransactionService
    {
        private IRepository _transactionrepository;

        public TransactionService(IRepository transactionrepository)
        {
            _transactionrepository = transactionrepository ?? throw new ArgumentNullException(nameof(transactionrepository));
        }

        public List<Transaction> GetAllTransactions()
        {
            try
            {
                var transactions = ApiRestClient.GetTransactionsJSON();

                // Update data base for transactions
                this._transactionrepository.DeleteAsync(this.DeleteTransactionsSQLQuery());
                this.InsertTransactionsByBatches(transactions);

                return transactions;
            }
            catch(Exception error)
            {
                throw new TimeoutException($"Error occured during getting the list of transactions. Message {error.Message}");
            }
        }

        public (List<Transaction>, decimal) GetProductTransactions(string sku, List<Transaction> transactions = null)
        {
            try
            {
                if (transactions == null)
                {
                    transactions = ApiRestClient.GetTransactionsJSON().Where(t => t.SKU == sku).ToList();
                }
                else
                {
                    transactions = transactions.Where(t => t.SKU == sku).ToList();
                }
                return (transactions, this.RoundTotalCost(this.CalculateTotalTransactionProductCost(transactions)));
            }
            catch (Exception error)
            {
                throw new TimeoutException($"Error occured during getting the list of product transactions and total cost. Message {error.Message}");
            }
        }

        /// <summary>
        /// Method that calculates the total cost of a product transactions.
        /// </summary>
        /// <returns></returns>
        private decimal CalculateTotalTransactionProductCost(List<Transaction> productTransactions)
        {
            if (productTransactions == null || !productTransactions.Any()) return 0.0M;

            decimal total = 0.0M;
            foreach(Transaction trans in productTransactions)
            {
                if (trans.Currency == Enum.GetName(typeof(Currency) ,Currency.EUR))
                {
                    total += ConvertToEUR(trans);
                }
                else
                {
                    total += trans.Amount;
                }
            }
            return total;
        }

        /// <summary>
        /// Method to convert transaction amount to EUR.
        /// </summary>
        /// <param name="transactionValue"></param>
        /// <returns></returns>
        private decimal ConvertToEUR(Transaction transaction)
        {
            // Get all rates for currencies transform.
            decimal rate = ApiRestClient.GetRatesJSON().Where(r => r.From == transaction.Currency).Select( elem => elem.Value).LastOrDefault();
            return transaction.Amount * rate;
        }

        /// <summary>
        /// Method that rounds the total applying Banker's Rounding algorithm.
        /// </summary>
        /// <param name="totalCost"></param>
        /// <returns></returns>
        private decimal RoundTotalCost(decimal totalCost)
        {
            return Convert.ToDecimal(Math.Round(totalCost, 2, MidpointRounding.ToEven));
        }

        /// <summary>
        /// Method that created the SQL query for inserting the transactions.
        /// </summary>
        /// <param name="rates"></param>
        /// <returns></returns>
        private void InsertTransactionsByBatches(List<Transaction> transactions)
        {
            List<List<Transaction>> ListOfChunks = SplitIntoChunks(transactions, 1000);
            foreach (List<Transaction> transactionsOfThousend in ListOfChunks)
            {
                string sSql = @"INSERT INTO [BusinessMan].[dbo].[Transactions] " +
                "([sku],[currency],[amount]) " +
                "VALUES ";

                foreach (Transaction transaction in transactionsOfThousend)
                {
                    sSql += string.Format(string.Concat("('{0}', '{1}', '{2}') ,"), transaction.SKU, transaction.Currency, transaction.Amount);
                }
                this._transactionrepository.AddAsync(sSql.Substring(0, sSql.Length - 1));
            }
        }

        /// <summary>
        /// Method that creates the SQL query to delete the Transactions table.
        /// </summary>
        /// <returns></returns>
        private string DeleteTransactionsSQLQuery()
        {
            return "DELETE FROM [BusinessMan].[dbo].[Transactions]";
        }

        /// <summary>
        /// Splits a List<T> into multiple chunks
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list to be chunked</param>
        /// <param name="chunkSize">The size of each chunk</param>
        /// <returns>A list of chunks</returns>
        public List<List<T>> SplitIntoChunks<T>(List<T> list, int chunkSize)
        {
            if (chunkSize <= 0)
            {
                throw new ArgumentException("Chunk size must be greater than 0");
            }
            List<List<T>> retVal = new List<List<T>>();
            int index = 0;
            while (index < list.Count)
            {
                int count = list.Count - index > chunkSize ? chunkSize : list.Count - index;
                retVal.Add(list.GetRange(index, count));
                index += chunkSize;
            }
            return retVal;
        }
    }
}
