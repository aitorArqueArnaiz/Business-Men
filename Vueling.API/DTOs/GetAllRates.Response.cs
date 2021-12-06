
using System.Collections.Generic;
using Vueling.Domain.Entities;

namespace Vueling.API.DTOs
{
    public class GetAllRatesResponse
    {
        /// <summary>
        /// List of all existing rates.
        /// </summary>
        public List<Rate> Rates { get; set; }
    }
}
