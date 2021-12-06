using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using Vueling.Domain.ApiRest_Client;
using Vueling.Domain.Entities;
using Vueling.Domain.Interfaces;
using Vueling.Domain.Services;
using Vueling.Infrastructure.Repository;

namespace Vueling.Test
{
    public class UnitTests
    {
        /// <summary>
        /// The transaction service object.
        /// </summary>
        private ITransactionService _transactionService;

        /// <summary>
        /// The rate service object.
        /// </summary>
        private IRateService _rateService;
        /// <summary>
        /// File system Repository.
        /// </summary>
        private Mock<IRepository> _repository;


        /// <summary>
        /// The number of rates in the list.
        /// </summary>
        private const int NumberOfRates = 6;

        [SetUp]
        public void Setup()
        {
            //Repository initialization
            this._repository = new Mock<IRepository>();

            // Service intialization
            this._transactionService = new TransactionService(this._repository.Object);
            this._rateService = new RateService(this._repository.Object);

            // API REST client services initialization
            ApiRestClient.Initialize();
        }

        [TearDown]
        public void TearDown()
        {
            this._transactionService = null;
        }

        [Test]
        [Author("Aitor Arqué Arnaiz")]
        [Description("Test intended to get all rates.")]
        public void Get_All_Rates_Test()
        {
            // Arrange

            // Act
            var response = this._rateService.GetAllRates();

            // Assert
            Assert.NotNull(response);
            Assert.AreEqual(response.Count, NumberOfRates);
        }

        [Test]
        [Author("Aitor Arqué Arnaiz")]
        [Description("Test intended to get all existing transactions.")]
        public void Get_All_Transactions_Test()
        {
            // Arrange

            // Act
            var response = this._transactionService.GetAllTransactions();

            // Assert
            Assert.NotNull(response);
            Assert.Greater(response.Count, 0);
        }

        [Test]
        [Author("Aitor Arqué Arnaiz")]
        [Description("Test intended to check the calculation of a product transaction with an empty sku.")]
        public void Calculate_Product_Transactions_Empty_Sku_Test()
        {
            // Arrange
            string testSku = "";

            // Act
            var response = this._transactionService.GetProductTransactions(testSku);

            // Assert
            Assert.NotNull(response);
            Assert.AreEqual(response.Item1.Count, 0);
        }

        [Test]
        [Author("Aitor Arqué Arnaiz")]
        [Description("Test intended to check the calculation of a product transaction.")]
        public void Calculate_Product_Transactions_Test()
        {
            // Arrange
            Transaction transaction_cad = new Transaction()
            {
                Currency = "CAD",
                Amount = 1.366M,
                SKU = "TS787"
            };

            Transaction transaction_usd = new Transaction()
            {
                Currency = "USD",
                Amount = 1.359M,
                SKU = "TS787"
            };

            Transaction transaction_aud = new Transaction()
            {
                Currency = "AUD",
                Amount = 0.732M,
                SKU = "TS787"
            };

            Transaction transaction_eur = new Transaction()
            {
                Currency = "EUR",
                Amount = 0.736M,
                SKU = "TS787"
            };

            List<Transaction> transactions = new List<Transaction>() { transaction_cad, transaction_usd, transaction_aud, transaction_eur };

            // Act
            var response = this._transactionService.GetProductTransactions("TS787", transactions);

            // Assert
            Assert.NotNull(response);
            Assert.AreEqual(response.Item1.Count, transactions.Count);
            Assert.AreEqual(response.Item2.ToString().Substring(response.Item2.ToString().IndexOf(",") + 1).Length, 2);
        }

        [Test]
        [Author("Aitor Arqué Arnaiz")]
        [Description("Test intended to check the calculation of a basic product transaction.")]
        public void Calculate_Product_Basic_Transactions_Test()
        {
            // Arrange
            Transaction transaction_cad = new Transaction()
            {
                Currency = "USD",
                Amount = 10.00M,
                SKU = "TS787"
            };

            Transaction transaction_usd = new Transaction()
            {
                Currency = "EUR",
                Amount = 7.63M,
                SKU = "TS787"
            };
            List<Transaction> transactions = new List<Transaction>() { transaction_cad, transaction_usd };

            // Act
            var response = this._transactionService.GetProductTransactions("TS787", transactions);

            // Assert
            Assert.NotNull(response);
            Assert.AreEqual(response.Item1.Count, transactions.Count);
            Assert.AreEqual(response.Item2.ToString().Substring(response.Item2.ToString().IndexOf(",") + 1).Length, 2);
        }
    }
}