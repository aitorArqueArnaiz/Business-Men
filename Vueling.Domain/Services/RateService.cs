using System;
using System.Collections.Generic;
using Vueling.Domain.ApiRest_Client;
using Vueling.Domain.Entities;
using Vueling.Domain.Interfaces;
using Vueling.Infrastructure.Repository;
using static Vueling.Domain.Shared.Enums;

namespace Vueling.Domain.Services
{
    public class RateService : IRateService
    {
        private IRepository _rateRepository;

        public RateService(IRepository rateRepository)
        {
            _rateRepository = rateRepository ?? throw new ArgumentNullException(nameof(rateRepository));
        }

        public List<Rate> GetAllRates()
        {
            try
            {
                var rates = ApiRestClient.GetRatesJSON();
                _rateRepository.DeleteAsync(this.DeleteRatesSQLQuery());
                _rateRepository.AddAsync(this.CreateInsertRatesSQlQuery(rates));

                return rates;
            }
            catch (Exception error)
            {
                throw new TimeoutException($"Error occured during getting the list of rates. Message {error.Message}");
            }
        }

        /// <summary>
        /// Method that created the SQL query for inserting the rates.
        /// </summary>
        /// <param name="rates"></param>
        /// <returns></returns>
        private string CreateInsertRatesSQlQuery(List<Rate> rates)
        {
            string result = @"INSERT INTO [Vueling].[dbo].[Rates] " +
                            "([originCurrency],[targetCurrency],[Rate]) " +
                            "VALUES ";
            foreach (Rate rate in rates)
            {
                result += string.Format(string.Concat( "('{0}', '{1}', '{2}') ,"), rate.From, 
                    rate.To, rate.Value.ToString());
            }
            return result.Substring(0, result.Length - 1);
        }

        /// <summary>
        /// Method that creates the SQL query to delete the Rates table.
        /// </summary>
        /// <returns></returns>
        private string DeleteRatesSQLQuery()
        {
            return "DELETE FROM [Vueling].[dbo].[Rates]";
        }
    }
}
