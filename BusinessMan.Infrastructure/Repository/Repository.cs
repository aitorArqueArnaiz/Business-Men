using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace BusinessMan.Infrastructure.Repository
{
    public class Repository : IRepository
    {
        #region Class variables/properties

        private string connectionString;

        #endregion

        public Repository() : base()
        {
            if (File.Exists(Directory.GetCurrentDirectory() + "ConnectionString.txt"))
            {
                this.connectionString = File.ReadAllText(Directory.GetCurrentDirectory() + "ConnectionString.txt");
            }
            else
            {
                this.connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=BusinessMan;"
                    + "Integrated Security=true;";
            }
        }

        public void AddAsync(string sqlQuery)
        {
            this.ExecuteSQlCommand(sqlQuery);
        }

        public void DeleteAsync(string sqlQuery)
        {
            this.ExecuteSQlCommand(sqlQuery);
        }

        public string GetAsync(string sqlQuery)
        {
            var result = this.ReadFromDataBAse(sqlQuery);
            return result == null ? string.Empty : result[0];
        }

        public List<string> ListAsync(string sqlQuery)
        {
            return this.ReadFromDataBAse(sqlQuery);
        }

        public void UpdateAsync(string sqlQuery)
        {
            this.ExecuteSQlCommand(sqlQuery);
        }

        #region Helper Methods

        /// <summary>This method gets a employee row from the data base Employee table.</summary>
        /// <param name="record">The row reader record.</param>
        private string ReadSingleRow(IDataRecord record)
        {
            return string.Format("{0}, '{1}', '{2}''", record.GetInt32(0), record.GetString(1), record.GetString(2));
        }

        /// <summary>This method executes an sql query.</summary>
        /// <param name="sqlQuery">The sql query to be executed.</param>
        private void ExecuteSQlCommand(string sqlQuery)
        {
            SqlConnection connection;
            try
            {
                connection = new SqlConnection(connectionString);
            }
            catch (Exception sqlConnectionError)
            {
                throw new Exception($"Error connecting to data base {sqlConnectionError.Message}");

            }
            using (connection)
            {
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Close();
                }
                catch (Exception sqlEx)
                {
                    throw new Exception($"Error executing sql query {sqlEx.Message}. Stack trace {sqlEx.StackTrace}");
                }
            }
        }

        /// <summary>This method return a single or a multiple rows from data base.</summary>
        /// <param name="sqlQuery">The sql query to be executed.</param>
        private List<string> ReadFromDataBAse(string sqlQuery)
        {
            SqlConnection connection = null;
            List<string> result = new List<string>() { };
            try
            {
                connection = new SqlConnection(connectionString);
            }
            catch (Exception sqlConnectionError)
            {
                throw new Exception($"Error openning connection against the data base {sqlConnectionError.Message}");
            }
            using (connection)
            {
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(this.ReadSingleRow(reader));
                    }
                    reader.NextResult();
                }
            }
            return result;
        }

        #endregion
    }
}
