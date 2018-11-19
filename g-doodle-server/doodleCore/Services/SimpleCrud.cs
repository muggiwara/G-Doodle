using Dapper;
using doodleCore.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace doodleCore.Services
{
    public class SimpleCrud
    {
        public IDbConnection connection;

        private SimpleCrud()
            : this("Data Source=DESKTOP-3B9MPQG;Initial Catalog=GDoodle;Integrated Security=True")
        {
        }

        private SimpleCrud(string connectionStringName)
        {

            try
            {
                connection = new SqlConnection(connectionStringName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static volatile SimpleCrud _instance;

        public static SimpleCrud Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SimpleCrud();
                }
                return _instance;
            }
        }

        public dynamic Get<T>(object param, string cond = null)
        {
            if (param == null)
            {
                return null;
            }
            var name = getName(typeof(T));
            var propNames = GetPropertiesName(typeof(T));
            var paramNames = GetParamsName(param, cond);
            var sql = string.Format("SELECT TOP(1) {0} FROM [{1}] {2}", propNames, name, paramNames);
            var res = this.connection.Query<T>(sql, param);
            return res.FirstOrDefault();
        }

        public IEnumerable<T> GetAll<T>(object param, string cond = null)
        {
            var name = getName(typeof(T));
            var propNames = GetPropertiesName(typeof(T));
            var paramNames = GetParamsName(param, cond);
            var sql = string.Format("SELECT {0} FROM [{1}] {2}", propNames, name, paramNames);
            return this.connection.Query<T>(sql, param);
        }

        public int Count<T>(object param, string cond = null)
        {
            var name = getName(typeof(T));
            var propName = GetPropertiesName(typeof(T));
            var paramNames = GetParamsName(param, cond);
            var sql = string.Format("SELECT {0} FROM [{1}] {2}", propName, name, paramNames);
            var res = this.connection.Query<int>(sql, param);
            return res.FirstOrDefault();
        }

        public int Insert<T>(T item)
        {
            var name = getName(typeof(T));
            var propNames = GetPropertiesName(typeof(T), true);
            var propNameValues = GetPropertiesValue(typeof(T));
            var sql = string.Format("INSERT INTO [{0}] ({1}) VALUES ({2})", name, propNames, propNameValues);
            return this.connection.Execute(sql, item);
        }

        public int Update<T>(T item, object param, string cond = null)
        {
            var name = getName(typeof(T));
            var propNames = GetPropertiesName(typeof(T), true, true);
            var paramNames = GetParamsName(param, cond);
            var sql = string.Format("UPDATE [{0}] SET {1} {2}", name, propNames, paramNames);
            return this.connection.Execute(sql, item);
        }
        //
        public int Delete<T>(object param, string cond = null)
        {
            var name = getName(typeof(T));
            var propNames = GetPropertiesName(typeof(T), false, true);
            var paramNames = GetParamsName(param, cond);
            var sql = string.Format("DELETE FROM [{0}] {1} {2}", name, paramNames);
            return this.connection.Execute(sql, param);
        }

        public IEnumerable<dynamic> Query<dynamic>(string sql, object param = null, SqlTransaction transaction = null, bool buffered = true)
        {
            return this.connection.Query<dynamic>(sql, param, transaction, buffered);
        }

        public dynamic Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return this.connection.Execute(sql, param, transaction, commandTimeout, commandType);
        }

        private string getName(Type type){
            var nameAttribut = type.GetCustomAttribute(typeof(NameAttribute));
            if (nameAttribut != null) {
                return ((NameAttribute)nameAttribut).Name;
            }
            return type.Name;
        }

        private string getName(PropertyInfo property) {
            var nameAttribut = property.GetCustomAttribute(typeof(NameAttribute));
            if (nameAttribut != null) {
                return ((NameAttribute)nameAttribut).Name;
            }
            return property.Name;
        }

        private readonly IEnumerable<Type> IgnoreAttributes = new List<Type>(){
            typeof(PrimaryAttribute),
            typeof(IgnoreAttribute)
        };

        private string GetPropertiesName(Type type, bool withIgnoreAttribut = false, bool withValue = false) {
            var format = withValue ? "{0} = @{0}" : "{0}";
            if (!withIgnoreAttribut) {
                return string.Join(",", type.GetProperties().Select(p => string.Format(format, getName(p))));
            }
            return string.Join(",", type.GetProperties().Where(p => p.GetCustomAttributes(true).All(a => !IgnoreAttributes.Any(ia => ia == a.GetType()))).Select(p => string.Format(format, getName(p))));
        }

        private string GetPropertiesValue(Type type) {
            return string.Join(",", type.GetProperties().Where(p => p.GetCustomAttributes(true).All(a => !IgnoreAttributes.Any(ia => ia == a.GetType()))).Select(p => "@" + p.Name));
        }

        private string GetParamsName(object param = null, string cond = null) {
            return param == null ? "" : "WHERE " + (cond != null ? cond : string.Join(" AND ", param.GetType().GetProperties().Select(p => string.Format("{0} = @{0}", p.Name))));
        }
    }

    
}
