using Dapper;
using Newtonsoft.Json;
using Superdigital.Entity.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace Superdigital.Repository
{
    public class DapperUnitOfWork : IUnitOfWork, IDisposable
    {
        private bool _disposed;
        private int _commandTimeout;
        private readonly string _connectionString;
        private bool _ehtransaction { get; set; }
        private SqlConnection _connection { get; set; }
        private SqlTransaction _transaction { get; set; }
        public const string _dataSettingsFilePath = "appsettings.json";

        public string ConnectionString
        {
            get { return _connectionString; }
        }

        public DapperUnitOfWork()
        {
            var connection = JsonConvert.DeserializeObject<ConnectionStrings>(File.ReadAllText(_dataSettingsFilePath));
            _connectionString = connection.Site;
            _commandTimeout = 300;
        }

        public DapperUnitOfWork(int commandTimeout)
        {
            var connection = JsonConvert.DeserializeObject<ConnectionStrings>(File.ReadAllText(_dataSettingsFilePath));
            _connectionString = connection.Site;
            _commandTimeout = commandTimeout;
        }

        public void SetCommandTimeout(int commandTimeout)
        {
            _commandTimeout = commandTimeout;
        }

        public SqlConnection Connection
        {
            get
            {
                return _connection;
            }
        }
        #region "transacoes"
        public SqlTransaction Transaction
        {
            get
            {
                return _transaction;
            }
        }

        public void BeginTransaction()
        {
            _connection = new SqlConnection(this._connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
            _ehtransaction = true;
        }

        public void SaveChanges()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
            }
            finally
            {
                _ehtransaction = false;
                if (_transaction != null)
                    _transaction.Dispose();

                if (_connection != null)
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }

                _transaction = null;
                _connection = null;
            }
        }

        public IEnumerable<T> List<T>(string query, params SqlParameter[] parameter) where T : new()
        {
            if (_ehtransaction == false || _transaction == null)
            {
                return GetWithoutTransaction<T>(query, parameter);
            }
            else
            {
                return GetWithTransaction<T>(query, parameter);
            }
        }

        public T Get<T>(string query, params SqlParameter[] parameter) where T : new()
        {
            return List<T>(query, parameter).AsEnumerable().FirstOrDefault();
        }

        private IEnumerable<T> GetWithTransaction<T>(string query, params SqlParameter[] parameter) where T : new()
        {
            return _connection.Query<T>(sql: query, param: ConvertTo(parameter), transaction: _transaction, commandTimeout: _commandTimeout, commandType: CommandType.StoredProcedure);
        }

        private IEnumerable<T> GetWithoutTransaction<T>(string query, params SqlParameter[] parameter) where T : new()
        {
            using (IDbConnection conn = new SqlConnection(this._connectionString))
            {
                return conn.Query<T>(sql: query, param: ConvertTo(parameter), commandTimeout: _commandTimeout, commandType: CommandType.StoredProcedure);
            }
        }

        public int Execute(string query, params SqlParameter[] parameter)
        {
            if (_ehtransaction == false || _transaction == null)
            {
                using (IDbConnection conn = new SqlConnection(this._connectionString))
                {
                    return conn.Execute(sql: query, param: ConvertTo(parameter), commandTimeout: _commandTimeout, commandType: CommandType.StoredProcedure);
                }
            }
            else
            {
                return _connection.Execute(sql: query, param: ConvertTo(parameter), transaction: _transaction, commandTimeout: _commandTimeout, commandType: CommandType.StoredProcedure);
            }
        }

        private DynamicParameters ConvertTo(SqlParameter[] paramsmysql)
        {
            DynamicParameters paramsdap = null;
            if (paramsmysql != null)
            {
                paramsdap = new DynamicParameters();

                foreach (var item in paramsmysql)
                {
                    if (item.Value.GetType().Name.ToUpper() == typeof(System.DBNull).Name.ToUpper())
                        paramsdap.Add(item.ParameterName, null);
                    else
                        paramsdap.Add(item.ParameterName, item.Value);
                }
            }
            return paramsdap;
        }

        #endregion

        #region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void Dispose(bool disposing)
        {
            _ehtransaction = false;
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_connection != null)
                    {
                        if (_connection.State == ConnectionState.Open)
                        {
                            _connection.Close();
                        }
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }

        ~DapperUnitOfWork()
        {
            Dispose(false);
        }

        #endregion
    }
}
