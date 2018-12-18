using CT.Service.Repository;
using Dapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using static Dapper.SqlMapper;

namespace CT.Service.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {

        #region Global varialbles
        //Declare an instance for log4net
        public readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public long RoleId { get; set; }
        public long UserId { get; set; }
        public string Language { get; set; }
        public int SPReturnId { get; set; }
        public string SPReturnMessage { get; set; }
        #endregion


        public IEnumerable<T> GetItems<T>(CommandType commandType, string sql, object parameters = null)
        {
            using (IDbConnection connection = GetOpenConnection())
            {
                return connection.Query<T>(sql, parameters, commandType: commandType);
            }
        }

        public T GetSingleItem<T>(CommandType commandType, string sql, object parameters = null)
        {
            using (IDbConnection connection = GetOpenConnection())
            {
                return connection.Query<T>(sql, parameters, commandType: commandType).FirstOrDefault();
            }
        }
        public int Execute(CommandType commandType, string sql, object parameters = null)
        {
            using (IDbConnection connection = GetOpenConnection())
            {
                return connection.Execute(sql, parameters, commandType: commandType);
            }
        }

        public IDbConnection GetOpenConnection()
        {
            IDbConnection connection = new ConnectionFactory().GetConnection;
            if (connection.State == ConnectionState.Closed) connection.Open();
            return connection;
        }

        public IEnumerable<T> Select<T>(object criteria = null)
        {
            PropertyContainer properties = ParseProperties(criteria);
            string sqlPairs = GetSqlPairs(properties.AllNames, " AND ");
            string sql = string.Format("SELECT * FROM [{0}] WHERE {1}", typeof(T).Name, sqlPairs);
            return GetItems<T>(CommandType.Text, sql, properties.AllPairs);
        }

        private PropertyContainer ParseProperties<T>(T obj)
        {
            PropertyContainer propertyContainer = new PropertyContainer();

            string typeName = typeof(T).Name;
            string[] validKeyNames = new[] { "Id",
            string.Format("{0}Id", typeName), string.Format("{0}_Id", typeName) };

            System.Reflection.PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (System.Reflection.PropertyInfo property in properties)
            {
                // Skip reference types (but still include string!)
                if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                    continue;

                // Skip methods without a public setter
                if (property.GetSetMethod() == null)
                    continue;

                // Skip methods specifically ignored
                if (property.IsDefined(typeof(DapperIgnore), false))
                    continue;

                string name = property.Name;
                object value = typeof(T).GetProperty(property.Name).GetValue(obj, null);

                if (property.IsDefined(typeof(DapperKey), false) || validKeyNames.Contains(name))
                {
                    propertyContainer.AddId(name, value);
                }
                else
                {
                    propertyContainer.AddValue(name, value);
                }
            }

            return propertyContainer;
        }

        private static string GetSqlPairs(IEnumerable<string> keys, string separator = ", ")
        {
            List<string> pairs = keys.Select(key => string.Format("{0}=@{0}", key)).ToList();
            return string.Join(separator, pairs);
        }

        private void SetId<T>(T obj, int id, IDictionary<string, object> propertyPairs)
        {
            if (propertyPairs.Count == 1)
            {
                string propertyName = propertyPairs.Keys.First();
                System.Reflection.PropertyInfo propertyInfo = obj.GetType().GetProperty(propertyName);
                if (propertyInfo.PropertyType == typeof(int))
                {
                    propertyInfo.SetValue(obj, id, null);
                }
            }
        }

        public List<object> GetMultipleList<T1, T2>(CommandType commandType, string sql, object parameters = null)
        {
            using (IDbConnection connection = GetOpenConnection())
            {
                GridReader gridResult = connection.QueryMultiple(sql, parameters, commandType: commandType);
                IEnumerable<T1> table1Result = gridResult.Read<T1>();
                IEnumerable<T2> table2Result = gridResult.Read<T2>();
                List<object> objList = new List<object>();
                objList.Add(table1Result);
                objList.Add(table2Result);
                return objList;
            }
        }

        public List<object> GetMultipleList(CommandType commandType, string sql, object parameters = null, params Func<GridReader, object>[] funcs)
        {
            var returnResults = new List<object>();
            using (IDbConnection connection = GetOpenConnection())
            {
                GridReader gridResult = connection.QueryMultiple(sql, parameters, commandType: commandType);
                foreach (var fun in funcs)
                {
                    var obj = fun(gridResult);
                    returnResults.Add(obj);
                }
                return returnResults;
            }
        }
    }

    public class GenericEntity
    {

    }
}
