
using System.Collections.Generic;
using BusinessMan.Domain.Entities;

namespace BusinessMan.API.DTOs
{
    public class GetAllRatesResponse
    {
        /// <summary>
        /// List of all existing rates.
        /// </summary>
        public List<Rate> Rates { get; set; }
    }
}
