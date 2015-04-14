using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace OwinIdentitySqlServerRepository.DataAccess
{
    public class SqlServerDatabase
    {
        public SqlServerDatabase()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString;
        }
        public string ConnectionString { get; set; }

        /// <summary>
        /// Create an SqlConnection to the data source.
        /// </summary>
        /// <returns>Returns an SqlConnection created using ConnectionString.</returns>
        public SqlConnection CreateConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        /// <summary>
        /// Create an SqlConnection to the data source and open that connection.
        /// </summary>
        /// <returns>Returns an open SqlConnection created using ConnectionString.</returns>
        public SqlConnection CreateOpenConnection()
        {
            var connection = CreateConnection();

            connection.Open();

            return connection;
        }

        /// <summary>
        /// Creates an SqlCommand using the specified parameters and sets the command type to CommandType.Text.
        /// </summary>
        /// <param name="commandText">The Transact-SQL statement to execute at the data source.</param>
        /// <param name="connection">The SqlConnection used by this instance of the SqlCommand.</param>
        /// <returns>Returns an SqlCommand.</returns>
        public SqlCommand CreateCommand(string commandText, SqlConnection connection)
        {
            var command = new SqlCommand
            {
                CommandText = commandText,
                Connection = connection,
                CommandType = CommandType.Text
            };

            return command;
        }

        /// <summary>
        /// Creates an SqlCommand using the specified parameters and sets the command type to CommandType.StoredProcedure.
        /// </summary>
        /// <param name="procName">The stored procedure to execute at the data source.</param>
        /// <param name="connection">The SqlConnection used by this instance of the SqlCommand.</param>
        /// <returns>Returns an SqlCommand.</returns>
        public SqlCommand CreateStoredProcCommand(string procName, SqlConnection connection)
        {
            var command = new SqlCommand
            {
                CommandText = procName,
                Connection = connection,
                CommandType = CommandType.StoredProcedure
            };

            return command;
        }

        /// <summary>
        /// Creates an SqlParameter using the specified parameters.
        /// </summary>
        /// <param name="parameterName">The name of the parameter to map.</param>
        /// <param name="parameterValue">An Object that is the value of the SqlParameter.</param>
        /// <returns>Returns an SqlParameter.</returns>
        public SqlParameter CreateParameter(string parameterName, object parameterValue)
        {
            return new SqlParameter(parameterName, parameterValue);
        }
    }
}
