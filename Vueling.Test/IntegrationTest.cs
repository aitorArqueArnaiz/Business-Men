using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Net;
using Vueling.API.Controllers;
using Vueling.API.DTOs;
using Vueling.Domain.ApiRest_Client;
using Vueling.Domain.Interfaces;
using Vueling.Domain.Services;
using Vueling.Infrastructure.Repository;

namespace Vueling.IntegrationTest
{
    public class IntegrationTest
    {
        /// <summary>
        /// The transaction controller object.
        /// </summary>
        private TransactionsController _transactionController;

        /// <summary>
        /// The rate controller object.
        /// </summary>
        private RatesController _ratesController;


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
        private IRepository _repository;

        /// <summary>
        /// The number of rates in the list.
        /// </summary>
        private const int NumberOfRates = 6;

        [SetUp]
        public void Setup()
        {
            //Repository initialization
            this._repository = new Repository();

            // Service intialization
            this._transactionService = new TransactionService(this._repository);
            this._rateService = new RateService(this._repository);

            // Api Rest client initialization
            ApiRestClient.Initialize();

            // Controllers initialization
            var loggerRates = Mock.Of<ILogger<RatesController>>();
            this._ratesController = new RatesController(loggerRates, this._rateService);
            var loggerTraansactions = Mock.Of<ILogger<TransactionsController>>();
            this._transactionController = new TransactionsController(loggerTraansactions, this._transactionService);
        }

        [TearDown]
        public void TearDown()
        {
            this._transactionService = null;
            this._rateService = null;
            this._transactionController = null;
            this._ratesController = null;
        }

        [Test]
        [Author("Aitor Arqué Arnaiz")]
        [Description("Test intended to get all the existing transactions.")]
        public void Get_All_Transactions_Test()
        {
            // Arrange
            GetAllTransactionsRequest request = new GetAllTransactionsRequest()
            {
                IsXml = false
            };

            // Act
            dynamic response = this._transactionController.GetAllTransactions(request).Result;

            // Assert
            Assert.NotNull(response);
            Assert.Greater(response.Value.Transactions.Count, 0);
            Assert.AreEqual((HttpStatusCode)response.StatusCode, HttpStatusCode.OK);

        }

        [Test]
        [Author("Aitor Arqué Arnaiz")]
        [Description("Test intended to get all SKU product transactions and it total cost.")]
        public void Get_Product_Transactions_And_Total_Cost_Test()
        {
            // Arrange
            GetProductTransactionRequest request = new GetProductTransactionRequest()
            {
               SKU = "TS787"
            };

            // Act
            dynamic response = this._transactionController.GetProductTransactions(request).Result;

            // Assert
            Assert.NotNull(response);
            Assert.AreEqual((HttpStatusCode)response.StatusCode, HttpStatusCode.OK);

        }

        [Test]
        [Author("Aitor Arqué Arnaiz")]
        [Description("Test intended to get all the existing rates.")]
        public void Get_All_Rates_Test()
        {
            // Arrange
            GetAllRatesRequest request = new GetAllRatesRequest()
            {
                IsXml = false
            };

            // Act
            dynamic response = this._ratesController.GetAllRates(request).Result;

            // Assert
            Assert.NotNull(response);
            Assert.AreEqual(response.Value.Rates.Count, 6);
            Assert.AreEqual((HttpStatusCode)response.StatusCode, HttpStatusCode.OK);

        }

        [Test]
        [Author("Aitor Arqué Arnaiz")]
        [Description("Test intended to get all the existing rates.")]
        public void Get_All_Rates_Null_Request_Test()
        {
            // Arrange

            // Act
            dynamic response = this._ratesController.GetAllRates(null).Result;

            // Assert
            Assert.NotNull(response);
            Assert.AreEqual((HttpStatusCode)response.StatusCode, HttpStatusCode.NotFound);

        }

        [Test]
        [Author("Aitor Arqué Arnaiz")]
        [Description("Test intended to get all the existing transactions.")]
        public void Get_All_Transactions_Null_Request_Test()
        {
            // Arrange

            // Act
            dynamic response = this._transactionController.GetAllTransactions(null).Result;

            // Assert
            Assert.NotNull(response);
            Assert.AreEqual((HttpStatusCode)response.StatusCode, HttpStatusCode.NotFound);

        }
    }
}