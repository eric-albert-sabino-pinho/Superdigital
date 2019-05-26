using System;
using System.Data;
using System.Data.SqlClient;

namespace Superdigital.Repository.Repository.Parameters
{
    public static class DataHelperParameters
    {
        public static SqlParameter CreateParameter(string nome, object parametro)
        {
            var sqlparam = new SqlParameter
            {
                ParameterName = nome,
                Value = parametro ?? DBNull.Value
            };
            return sqlparam;
        }

        public static SqlParameter CreateParameter(string nome, dynamic parametro, dynamic valorNulo)
        {
            var sqlparam = new SqlParameter { ParameterName = nome };

            if (parametro == null || parametro == valorNulo)
            {
                sqlparam.Value = DBNull.Value;
            }
            else
            {
                sqlparam.Value = (object)parametro;
            }
            return sqlparam;
        }

        public static SqlParameter CreateParameter(string nome, dynamic parametro, SqlDbType tipoParametro, int tamanho)
        {
            var sqlparam = new SqlParameter { ParameterName = nome, SqlDbType = tipoParametro, Size = tamanho };

            if (parametro == null)
                sqlparam.Value = DBNull.Value;
            else
                sqlparam.Value = (object)parametro;

            return sqlparam;
        }
    }
}
