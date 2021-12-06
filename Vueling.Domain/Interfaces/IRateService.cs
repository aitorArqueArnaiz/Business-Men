using System.Collections.Generic;
using Vueling.Domain.Entities;

namespace Vueling.Domain.Interfaces
{
    public interface IRateService
    {
        /// <summary>
        /// Method that gets all the existing rates.
        /// </summary>
        /// <returns>The list of rates.</returns>
        List<Rate> GetAllRates();
    }
}
