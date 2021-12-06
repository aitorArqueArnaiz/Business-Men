
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vueling.Infrastructure.Repository
{
    public interface IRepository
    {
        /// <summary>This method adds a new data row into data base, given a connection string and an sql query string.</summary>
        /// <param name="sqlQuery">the SQL query to be executed.</param>
        void AddAsync(string sqlQuery);

        /// <summary>This method deletes data row from data base</summary>
        /// <param name="sqlQuery">the SQL query to be executed in order to delete a single row.</param>
        void DeleteAsync(string sqlQuery);

        /// <summary>This method gets a single row given a query string.</summary>
        /// <param name="sqlQuery">the SQL query to be executed.</param>
        /// <returns>A data base row with the corresponding data.</returns>
        string GetAsync(string sqlQuery);

        /// <summary>This method adds a new data row into data base, given a connection string and an sql query string.</summary>
        /// <param name="sqlQuery">the SQL query to be executed.</param>
        /// <returns>A list of data rows with the corresponding data.</returns>
        List<string> ListAsync(string sqlQuery);

        /// <summary>This method updates an existing data row into data base, given a connection string and an sql query string.</summary>
        /// <param name="sqlQuery">the SQL query to be executed.</param>
        void UpdateAsync(string sqlQuery);
    }
}
