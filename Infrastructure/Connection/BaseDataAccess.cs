using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Connection
{
    public class BaseDataAccess
    {
        protected string ConnectionString { get; set; }
        private static DataTable dataTable;

        public BaseDataAccess()
        {
        }

        public BaseDataAccess(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(this.ConnectionString);
            if (connection.State != ConnectionState.Open)
                connection.Open();
            return connection;
        }

        protected DbCommand GetCommand(DbConnection connection, string commandText, CommandType commandType)
        {
            SqlCommand command = new SqlCommand(commandText, connection as SqlConnection);
            command.CommandType = commandType;
            return command;
        }

        protected SqlParameter GetParameter(string parameter, object value)
        {
            SqlParameter parameterObject = new SqlParameter(parameter, value != null ? value : DBNull.Value);
            parameterObject.Direction = ParameterDirection.Input;
            return parameterObject;
        }

        protected SqlParameter GetParameterOut(string parameter, SqlDbType type, object value = null, ParameterDirection parameterDirection = ParameterDirection.InputOutput)
        {
            SqlParameter parameterObject = new SqlParameter(parameter, type); ;

            if (type == SqlDbType.NVarChar || type == SqlDbType.VarChar || type == SqlDbType.NText || type == SqlDbType.Text)
            {
                parameterObject.Size = -1;
            }

            parameterObject.Direction = parameterDirection;

            if (value != null)
            {
                parameterObject.Value = value;
            }
            else
            {
                parameterObject.Value = DBNull.Value;
            }

            return parameterObject;
        }

        protected int ExecuteNonQuery(string procedureName, List<DbParameter> parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            int returnValue = -1;

            try
            {
                using (SqlConnection connection = this.GetConnection())
                {
                    DbCommand cmd = this.GetCommand(connection, procedureName, commandType);

                    if (parameters != null && parameters.Count > 0)
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());
                    }
                    returnValue = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return returnValue;
        }

        protected object ExecuteScalar(string procedureName, List<SqlParameter> parameters)
        {
            object returnValue = null;

            try
            {
                using (DbConnection connection = this.GetConnection())
                {
                    DbCommand cmd = this.GetCommand(connection, procedureName, CommandType.StoredProcedure);
                    if (parameters != null && parameters.Count > 0)
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());
                    }
                    returnValue = cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return returnValue;
        }

        public DbDataReader GetDataReader(string procedureName, List<DbParameter> parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            DbDataReader ds;

            try
            {
                DbConnection connection = this.GetConnection();
                {
                    DbCommand cmd = this.GetCommand(connection, procedureName, commandType);
                    if (parameters != null && parameters.Count > 0)
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());
                    }

                    ds = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return ds;
        }

        public async Task<int> ConnectionExecute(string connectionString, string StoreProcedure)
        {
            int Rowsaffected = 0;
            dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(StoreProcedure, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Connection.Open();
                Rowsaffected = await command.ExecuteNonQueryAsync();
                connection.Close();
            }

            return Rowsaffected;
        }

        public async Task<int> ConnectionExecuteWithParameters(string StoreProcedure, List<Parameters> parameters)
        {
            int Rowsaffected = 1;
            dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                SqlCommand command = new SqlCommand(StoreProcedure, connection);
                foreach (var Iparameter in parameters)
                {
                    command.Parameters.Add(Iparameter.parameter, Iparameter.type).Value = Iparameter.value;
                }
                command.CommandType = CommandType.StoredProcedure;
                command.Connection.Open();
                await command.ExecuteNonQueryAsync();
                connection.Close();
            }

            return Rowsaffected;
        }
        public async Task<DataTable> ConnectionWithParameters(string StoreProcedure, List<Parameters> parameters)
        {
            dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                SqlCommand command = new SqlCommand(StoreProcedure, connection);
                foreach (var Iparameter in parameters)
                {
                    command.Parameters.Add(Iparameter.parameter, Iparameter.type).Value = Iparameter.value;
                }
                command.CommandType = CommandType.StoredProcedure;
                await command.Connection.OpenAsync();

                using (SqlDataAdapter da = new SqlDataAdapter(command))
                {
                    da.Fill(dataTable);
                }
                connection.Close();
            }

            return dataTable;
        }

    }
}
