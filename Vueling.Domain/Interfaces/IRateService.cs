using System.Collections.Generic;
using BusinessMan.Domain.Entities;

namespace BusinessMan.Domain.Interfaces
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
