using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Xml;
using Vueling.Domain.Entities;
using Transaction = Vueling.Domain.Entities.Transaction;

namespace Vueling.Domain.ApiRest_Client
{
    public static class ApiRestClient
    {
        /// <summary>
        /// API REST path's.
        /// </summary>
        private const string _ratesJsonPath = "http://quiet-stone-2094.herokuapp.com/rates.json";
        private const string _ratesXmlPath = "http://quiet-stone-2094.herokuapp.com/rates.xml";
        private const string _transactionsJsonPath = "http://quiet-stone-2094.herokuapp.com/transactions.json";
        private const string _transactionsXmlPath = "http://quiet-stone-2094.herokuapp.com/transactions.xml";

        private static WebClient _apiRestClient;

        public static void Initialize()
        {
            _apiRestClient = new WebClient();
        }

        /// <summary>
        /// Method that returns the rates in JSON format.
        /// </summary>
        /// <returns></returns>
        public static List<Rate> GetRatesJSON()
        {
            try
            {
                var response = _apiRestClient.DownloadString(_ratesJsonPath);

                dynamic jToken = JToken.Parse(response);
                var rates = jToken.ToObject<List<Rate>>();

                return rates;
            }
            catch(ArgumentNullException error)
            {
                throw new ArgumentNullException(error.Message);
            }
            catch (WebException error)
            {
                throw new WebException(error.Message);
            }
            catch (NotSupportedException error)
            {
                throw new NotSupportedException(error.Message);
            }

        }

        /// <summary>
        /// Method that returns the rates in XML format.
        /// </summary>
        /// <returns></returns>
        public static XmlDocument GetRatesXML()
        {
            try
            {
                var response = _apiRestClient.DownloadString(_ratesXmlPath);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(response);
                return doc;
            }
            catch (ArgumentNullException error)
            {
                throw new ArgumentNullException(error.Message);
            }
            catch (WebException error)
            {
                throw new WebException(error.Message);
            }
            catch (NotSupportedException error)
            {
                throw new NotSupportedException(error.Message);
            }
        }

        /// <summary>
        ///  Method that returns the transactions in JSON format.
        /// </summary>
        /// <returns></returns>
        public static List<Transaction> GetTransactionsJSON()
        {
            try
            {
                var response = _apiRestClient.DownloadString(_transactionsJsonPath);

                dynamic jToken = JToken.Parse(response);
                var transactions = jToken.ToObject<List<Transaction>>();

                return transactions;
            }
            catch (ArgumentNullException error)
            {
                throw new ArgumentNullException(error.Message);
            }
            catch (WebException error)
            {
                throw new WebException(error.Message);
            }
            catch (NotSupportedException error)
            {
                throw new NotSupportedException(error.Message);
            }
        }

        /// <summary>
        ///  Method that returns the transactions in XML format.
        /// </summary>
        /// <returns></returns>
        public static XmlDocument GetTransactionsXML()
        {
            try
            {
                var response = _apiRestClient.DownloadString(_transactionsXmlPath);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(response);
                return doc;
            }
            catch (ArgumentNullException error)
            {
                throw new ArgumentNullException(error.Message);
            }
            catch (WebException error)
            {
                throw new WebException(error.Message);
            }
            catch (NotSupportedException error)
            {
                throw new NotSupportedException(error.Message);
            }
        }
    }
}
