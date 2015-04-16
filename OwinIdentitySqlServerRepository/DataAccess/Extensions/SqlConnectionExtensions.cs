using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using AutoMapper;

namespace OwinIdentitySqlServerRepository.DataAccess.Extensions
{
    public static class SqlConnectionExtensions
    {
        public static void ExecuteNonQuery(this SqlConnection connection, string commandText,
            params SqlParameter[] parameters)
        {
            try
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = commandText;
                            command.Transaction = transaction;

                            foreach (var parameter in parameters)
                                command.Parameters.Add(parameter);

                            command.ExecuteNonQuery();

                            transaction.Commit();
                        }
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                }
            }
            finally
            {
                connection.Close();
            }
        }

        public static void ExecuteNonQuery(this SqlConnection connection, string commandText, Object obj)
        {
            var props = new List<PropertyInfo>(obj.GetType().GetProperties());

            var parameters = (from prop in props
                              let value = prop.GetValue(obj)
                              let param = value.AsParameter("@" + prop.Name)
                              select param).ToArray();

            ExecuteNonQuery(connection, commandText, parameters);
        }

        public static IEnumerable<T> ExecuteQuery<T>(this SqlConnection connection, string commandText,
            params SqlParameter[] parameters)
        {
            try
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = commandText;
                    foreach (var parameter in parameters)
                        command.Parameters.Add(parameter);

                    using (var reader = command.ExecuteReader())
                    {
                        Mapper.CreateMap<IDataReader, T>();
                        return Mapper.Map<IDataReader, IEnumerable<T>>(reader);
                    }
                }
            }
            finally
            {
                connection.Close();
            }
        }

        public static IEnumerable<T> ExecuteQuery<T>(this SqlConnection connection, string commandText, Object obj)
        {
            var props = new List<PropertyInfo>(obj.GetType().GetProperties());

            var parameters = (from prop in props
                              let value = prop.GetValue(obj)
                              let param = value.AsParameter("@" + prop.Name)
                              select param).ToArray();

            return ExecuteQuery<T>(connection, commandText, parameters);
        }

        public static void Insert(this SqlConnection connection, object obj, string tableName)
        {
            if (obj == null) throw new ArgumentNullException("obj");

            var props = new List<PropertyInfo>(obj.GetType().GetProperties());

            var data = (from prop in props
                        let value = prop.GetValue(obj)
                        let param = value.AsParameter("@" + prop.Name)
                        select new
                        {
                            prop.Name,
                            param
                        }).ToArray();

            var columns = data.Select(x => "[" + x.Name + "]").Aggregate((a, b) => a + ", " + b);
            var values = data.Select(x => x.param.ParameterName).Aggregate((a, b) => a + ", " + b);

            var sql = string.Format("INSERT INTO [{0}] ({1}) VALUES ({2})", tableName, columns, values);

            var parameters = data.Select(x => x.param).ToArray();

            connection.ExecuteNonQuery(sql, parameters);
        }

        public static IEnumerable<T> Select<T>(this SqlConnection connection, object obj, string tableName)
        {
            if (obj == null) throw new ArgumentNullException("obj");

            var props = new List<PropertyInfo>(obj.GetType().GetProperties());

            var data = (from prop in props
                        let value = prop.GetValue(obj)
                        let param = value.AsParameter("@" + prop.Name)
                        select new
                        {
                            prop.Name,
                            param
                        }).ToArray();

            var predicate = data
                .Select(x => string.Format("[{0}] = {1}", x.Name, "@" + x.Name.ToLower()))
                .Aggregate((a, b) => a + " AND " + b);

            var sql = new StringBuilder()
                .AppendFormat("SELECT * FROM [{0}]", tableName);

            if (!String.IsNullOrWhiteSpace(predicate))
                sql.AppendFormat(" WHERE {0}", predicate);

            var parameters = data.Select(x => x.param).ToArray();
            return connection.ExecuteQuery<T>(sql.ToString(), parameters);
        }

        public static void Update(this SqlConnection connection, object obj, string tableName)
        {
            if (obj == null) throw new ArgumentNullException("obj");

            var props = new List<PropertyInfo>(obj.GetType().GetProperties());

            var data = (from prop in props
                        let value = prop.GetValue(obj)
                        let param = value.AsParameter("@" + prop.Name)
                        select new
                        {
                            prop.Name,
                            param
                        }).ToArray();

            var values = data
                .Select(x => string.Format("[{0}] = {1}", x.Name, x.param.ParameterName))
                .Aggregate((a, b) => a + ", " + b);

            var predicate = data
                .Select(x => string.Format("[{0}] = {1}", x.Name, x.param.ParameterName))
                .Aggregate((a, b) => a + " AND " + b);

            var sql = new StringBuilder()
                .AppendFormat("UPDATE [{0}] SET {1}", tableName, values);

            if (!String.IsNullOrWhiteSpace(predicate))
                sql.AppendFormat(" WHERE {0}", predicate);

            var parameters = data.Select(x => x.param).ToArray();
            connection.ExecuteNonQuery(sql.ToString(), parameters);
        }

        public static void Delete(this SqlConnection connection, object obj, string tableName)
        {
            if (obj == null) throw new ArgumentNullException("obj");

            var props = new List<PropertyInfo>(obj.GetType().GetProperties());

            var data = (from prop in props
                        let value = prop.GetValue(obj)
                        let param = value.AsParameter("@" + prop.Name)
                        select new
                        {
                            prop.Name,
                            param
                        }).ToArray();

            var sql = new StringBuilder()
                .AppendFormat("DELETE FROM [{0}]", tableName);
            var predicate = data
                .Select(x => string.Format("[{0}] = {1}", x.Name, "@" + x.Name.ToLower()))
                .Aggregate((a, b) => a + " AND " + b);

            if (!String.IsNullOrWhiteSpace(predicate))
                sql.AppendFormat(" WHERE {0}", predicate);

            var parameters = data.Select(x => x.param).ToArray();
            connection.ExecuteNonQuery(sql.ToString(), parameters);
        }

        public static SqlParameter AsParameter(this object obj, string parameterName)
        {
            if (obj is DateTime)
                obj = ((DateTime)obj).ToString("o");
            if (obj is DateTimeOffset)
                obj = ((DateTimeOffset)obj).ToString("o");

            return new SqlParameter(parameterName, obj ?? DBNull.Value);
        }

        public static SqlParameter[] AsParameters(this object obj)
        {
            var props = new List<PropertyInfo>(obj.GetType().GetProperties());

            var parameters = new List<SqlParameter>();
            foreach (var prop in props)
            {
                var propValue = prop.GetValue(obj, null);

                if (propValue is DateTime)
                    propValue = ((DateTime)propValue).ToString("o");
                if (propValue is DateTimeOffset)
                    propValue = ((DateTimeOffset)propValue).ToString("o");

                parameters.Add(new SqlParameter("@" + prop.Name.ToLower(), propValue ?? DBNull.Value));

            }
            return parameters.ToArray();
        }
    }
}