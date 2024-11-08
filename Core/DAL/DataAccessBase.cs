using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace TkwEF.Core.DAL
{
    public abstract class DataAccessBase
    {
        #region Fields

        /// <summary>
        /// Строка подключения к БД
        /// </summary>
        public abstract string ConnectionString { get; }
        ///// <summary>
        ///// Возвращает соединение для работы с базой
        ///// </summary>
        //public static DbConnection DataConnection { get; private set; }

        #endregion

        #region Protected Methods

        protected int ExecuteNonQuery(DbCommand cmd)
        {
            return cmd.ExecuteNonQuery();
        }
        protected IDataReader ExecuteReader(DbCommand cmd)
        {
            return ExecuteReader(cmd, CommandBehavior.Default);
        }
        protected IDataReader ExecuteReader(DbCommand cmd, CommandBehavior behavior)
        {
            return cmd.ExecuteReader(behavior);
        }

        protected object ExecuteScalar(DbCommand cmd)
        {
            return cmd.ExecuteScalar();
        }

        protected static DateTime? ReturnDateSql(IDataReader reader)
        {
            DateTime? dateAccept;
            if (reader["Date_accept"] != DBNull.Value)
                dateAccept = (DateTime?)reader["Date_accept"] == GetDateMinSql ? null : (DateTime?)reader["Date_accept"];
            else
                dateAccept = null;
            return dateAccept;
        }

        /// <summary>
        /// Возвращает строковое представление всех параметров в формате NameParametr = ValueParametr (например для отображения в обработчиках ощибок)
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        protected static string GetParamAndValueFromCommand(SqlCommand cmd)
        {
            StringBuilder sb = new StringBuilder();
            foreach (SqlParameter par in cmd.Parameters)
                sb.AppendFormat("{0} = {1}", par.ParameterName, par.Value);
            return sb.ToString();
        }

        #endregion

        #region Static Methods

        public static DateTime GetDateMinSql { get { return new DateTime(1900, 1, 1); } }
        /// <summary>
        /// Возвращает строку для параметра IN
        /// </summary>
        /// <param name="inData"></param>
        /// <param name="byOper"></param>
        /// <returns></returns>
        public static string GetInParam(int[] inData)
        {
            string byOper = null;
            if (inData != null && inData.Length != 0)
            {
                byOper = string.Empty;
                foreach (int val in inData)
                    byOper += val.ToString() + ", ";
                byOper = byOper.Trim().TrimEnd(',');
            }
            return byOper;
        }
        /// <summary>
        /// Возвращает строку для параметра IN
        /// </summary>
        /// <param name="inData"></param>
        /// <param name="byOper"></param>
        /// <returns></returns>
        public static string GetInParam(short[] inData)
        {
            string byOper = null;
            if (inData != null && inData.Length != 0)
            {
                byOper = string.Empty;
                foreach (short val in inData)
                    byOper += val.ToString() + ", ";
                byOper = byOper.Trim().TrimEnd(',');
            }
            return byOper;
        }
        /// <summary>
        /// Возвращает строку для параметра IN
        /// </summary>
        /// <param name="inData"></param>
        /// <param name="byOper"></param>
        /// <returns></returns>
        public static string GetInParam(byte[] inData)
        {
            string byOper = null;
            if (inData != null && inData.Length != 0)
            {
                byOper = string.Empty;
                foreach (byte val in inData)
                    byOper += val.ToString() + ", ";
                byOper = byOper.Trim().TrimEnd(',');
            }
            return byOper;
        }
        /// <summary>
        /// Возвращает порядковый номер столбца по его названию. Если не найден, то возвращает -1.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static int GetOrdinalX(IDataReader reader, string fieldName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i) == fieldName)
                    return i;
            }
            return -1;
        }

        #endregion

        #region Common Protected Methods

        /// <summary>
        /// Записать ошибку в БД
        /// </summary>
        /// <param name="ex"></param>
        protected virtual void ErrRecord(Exception ex)
        {
            // Делать запись ошибки в БД
        }

        #endregion
    }
}
