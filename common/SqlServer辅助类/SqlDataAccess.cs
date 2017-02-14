using System;
using System.Text.RegularExpressions;
using System.Xml;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace BID.DAL.DataAccess
{
    /// <summary>
    /// 封装了企业库的访问，提供给数据访问层直接调用
    /// </summary>
    public class SqlDataAccess
    {
        internal SqlDatabase sqlDB;

        private SqlDataAccess() { }

        static SqlDataAccess() { }

        private static Regex FilterInjectionRegex;

        public static string FilterInjection(string s)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;
            s = s.Trim().Replace("'", "''");
            if (FilterInjectionRegex == null)
                FilterInjectionRegex = new Regex(@"%3D|=|%27|%2D|--|%3B|;", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            if (FilterInjectionRegex.IsMatch(s))
            {
                return string.Empty;
            }
            else
                return s;
        }

        /// <summary>
        /// Create SqlDataAccess.This is only entry when using SqlDataAccess.
        /// </summary>
        /// <param name="name">Connectionstring section name in configuration file.</param>
        /// <returns></returns>
        public static SqlDataAccess CreateDataAccess(string name)
        {
            SqlDataAccess sqlDataAccess = new SqlDataAccess();
            if (name == string.Empty)
                sqlDataAccess.sqlDB = DatabaseFactory.CreateDatabase() as SqlDatabase;
            else
                sqlDataAccess.sqlDB = DatabaseFactory.CreateDatabase(name) as SqlDatabase;

            if (sqlDataAccess.sqlDB == null)
                throw new NullReferenceException("Don't create database, please check the name.");

            return sqlDataAccess;
        }

        /// <summary>
        /// Create SqlDataAccess.This is only entry when using SqlDataAccess.
        /// </summary>
        /// <returns></returns>
        public static SqlDataAccess CreateDataAccess()
        {
            return CreateDataAccess(string.Empty);
        }

        #region Properties

        /// <summary>
        /// <para>Gets the DbProviderFactory used by the database instance.</para>
        /// </summary>
        public DbProviderFactory DbProviderFactory
        {
            get { return sqlDB.DbProviderFactory; }
        }

        /// <summary>
        /// Gets the connection string without credentials.
        /// </summary>
        /// <value>
        /// The connection string without credentials.
        /// </value>
        public string ConnectionStringWithoutCredentials
        {
            get
            {
                return sqlDB.ConnectionStringWithoutCredentials;
            }
        }

        #endregion

        /// <summary>
        /// Gets the DbDataAdapter with the given update behavior and connection from the proper derived class.
        /// </summary>
        /// <returns>A <see cref="DbDataAdapter"/>.</returns>
        /// <seealso cref="DbDataAdapter"/>
        public DbDataAdapter GetDataAdapter()
        {
            return sqlDB.GetDataAdapter();
        }

        /// <summary>
        /// <para>Creates a connection for this database.</para>
        /// </summary>
        /// <returns>
        /// <para>The <see cref="DbConnection"/> for this database.</para>
        /// </returns>
        public DbConnection CreateConnection()
        {
            return sqlDB.CreateConnection();
        }

        /// <summary>
        /// Clears the parameter cache. Since there is only one parameter cache that is shared by all instances
        /// of this class, this clears all parameters cached for all databases.
        /// </summary>
        public static void ClearParameterCache()
        {
            Database.ClearParameterCache();
        }



        #region Build Parameters

        /// <summary>
        /// <para>Adds a new instance of a <see cref="DbParameter"/> object.</para>
        /// </summary>
        /// <param name="name"><para>The name of the parameter.</para></param>
        /// <param name="dbType"><para>One of the <see cref="DbType"/> values.</para></param>
        /// <param name="size"><para>The maximum size of the data within the column.</para></param>
        /// <param name="direction"><para>One of the <see cref="ParameterDirection"/> values.</para></param>
        /// <param name="nullable"><para>A value indicating whether the parameter accepts <see langword="null"/> (<b>Nothing</b> in Visual Basic) values.</para></param>
        /// <param name="precision"><para>The maximum number of digits used to represent the <paramref name="value"/>.</para></param>
        /// <param name="scale"><para>The number of decimal places to which <paramref name="value"/> is resolved.</para></param>
        /// <param name="sourceColumn"><para>The name of the source column mapped to the DataSet and used for loading or returning the <paramref name="value"/>.</para></param>
        /// <param name="sourceVersion"><para>One of the <see cref="DataRowVersion"/> values.</para></param>
        /// <param name="value"><para>The value of the parameter.</para></param>  
        protected SqlParameter CreateParameter(string name, SqlDbType dbType, int size, ParameterDirection direction, bool nullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            SqlParameter param = sqlDB.DbProviderFactory.CreateParameter() as SqlParameter;
            param.ParameterName = sqlDB.BuildParameterName(name);

            ConfigureParameter(param, name, dbType, size, direction, nullable, precision, scale, sourceColumn, sourceVersion, value);
            return param;
        }

        /// <summary>
        /// Configures a given <see cref="DbParameter"/>.
        /// </summary>
        /// <param name="param">The <see cref="DbParameter"/> to configure.</param>
        /// <param name="name"><para>The name of the parameter.</para></param>
        /// <param name="dbType"><para>One of the <see cref="SqlDbType"/> values.</para></param>
        /// <param name="size"><para>The maximum size of the data within the column.</para></param>
        /// <param name="direction"><para>One of the <see cref="ParameterDirection"/> values.</para></param>
        /// <param name="nullable"><para>A value indicating whether the parameter accepts <see langword="null"/> (<b>Nothing</b> in Visual Basic) values.</para></param>
        /// <param name="precision"><para>The maximum number of digits used to represent the <paramref name="value"/>.</para></param>
        /// <param name="scale"><para>The number of decimal places to which <paramref name="value"/> is resolved.</para></param>
        /// <param name="sourceColumn"><para>The name of the source column mapped to the DataSet and used for loading or returning the <paramref name="value"/>.</para></param>
        /// <param name="sourceVersion"><para>One of the <see cref="DataRowVersion"/> values.</para></param>
        /// <param name="value"><para>The value of the parameter.</para></param>  
        protected virtual void ConfigureParameter(SqlParameter param, string name, SqlDbType dbType, int size, ParameterDirection direction, bool nullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            param.SqlDbType = dbType;
            param.Size = size;
            param.Value = (value == null) ? DBNull.Value : value;
            param.Direction = direction;
            param.IsNullable = nullable;
            param.SourceColumn = sourceColumn;
            param.SourceVersion = sourceVersion;
        }

        /// <summary>
        /// <para>Adds a new instance of a <see cref="DbParameter"/> object to the command.</para>
        /// </summary>
        /// <param name="name"><para>The name of the parameter.</para></param>
        /// <param name="dbType"><para>One of the <see cref="DbType"/> values.</para></param>
        /// <param name="size"><para>The maximum size of the data within the column.</para></param>
        /// <param name="direction"><para>One of the <see cref="ParameterDirection"/> values.</para></param>
        /// <param name="nullable"><para>A value indicating whether the parameter accepts <see langword="null"/> (<b>Nothing</b> in Visual Basic) values.</para></param>
        /// <param name="precision"><para>The maximum number of digits used to represent the <paramref name="value"/>.</para></param>
        /// <param name="scale"><para>The number of decimal places to which <paramref name="value"/> is resolved.</para></param>
        /// <param name="sourceColumn"><para>The name of the source column mapped to the DataSet and used for loading or returning the <paramref name="value"/>.</para></param>
        /// <param name="sourceVersion"><para>One of the <see cref="DataRowVersion"/> values.</para></param>
        /// <param name="value"><para>The value of the parameter.</para></param>       
        public SqlParameter BuildParameter(string name, SqlDbType dbType, int size, ParameterDirection direction, bool nullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            return CreateParameter(name, dbType, size, direction, nullable, precision, scale, sourceColumn, sourceVersion, value);
        }

        /// <summary>
        /// <para>Adds a new instance of a <see cref="DbParameter"/> object to the command.</para>
        /// </summary>
        /// <param name="name"><para>The name of the parameter.</para></param>
        /// <param name="dbType"><para>One of the <see cref="SqlDbType"/> values.</para></param>        
        /// <param name="direction"><para>One of the <see cref="ParameterDirection"/> values.</para></param>                
        /// <param name="sourceColumn"><para>The name of the source column mapped to the DataSet and used for loading or returning the <paramref name="value"/>.</para></param>
        /// <param name="sourceVersion"><para>One of the <see cref="DataRowVersion"/> values.</para></param>
        /// <param name="value"><para>The value of the parameter.</para></param>    
        public SqlParameter BuildParameter(string name, SqlDbType dbType, ParameterDirection direction, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            return BuildParameter(name, dbType, 0, direction, false, 0, 0, sourceColumn, sourceVersion, value);
        }

        /// <summary>
        /// Adds a new Out <see cref="DbParameter"/> object to the given <paramref name="command"/>.
        /// </summary>
        /// <param name="name"><para>The name of the parameter.</para></param>
        /// <param name="dbType"><para>One of the <see cref="SqlDbType"/> values.</para></param>        
        /// <param name="size"><para>The maximum size of the data within the column.</para></param>        
        public SqlParameter BuildOutParameter(string name, SqlDbType dbType, int size)
        {
            return BuildParameter(name, dbType, size, ParameterDirection.Output, true, 0, 0, String.Empty, DataRowVersion.Default, DBNull.Value);
        }

        /// <summary>
        /// Adds a new Out <see cref="DbParameter"/> object to the given <paramref name="command"/>.
        /// </summary>
        /// <param name="name"><para>The name of the parameter.</para></param>
        /// <param name="dbType"><para>One of the <see cref="SqlDbType"/> values.</para></param>        
        /// <param name="size"><para>The maximum size of the data within the column.</para></param>        
        public SqlParameter BuildInOutParameter(string name, SqlDbType dbType, int size, object value)
        {
            return BuildParameter(name, dbType, size, ParameterDirection.InputOutput, true, 0, 0, String.Empty, DataRowVersion.Default, value);
        }

        /// <summary>
        /// Adds a new In <see cref="DbParameter"/> object to the given <paramref name="command"/>.
        /// </summary>
        /// <param name="name"><para>The name of the parameter.</para></param>
        /// <param name="dbType"><para>One of the <see cref="SqlDbType"/> values.</para></param>                
        /// <remarks>
        /// <para>This version of the method is used when you can have the same parameter object multiple times with different values.</para>
        /// </remarks>        
        public SqlParameter BuildInParameter(string name, SqlDbType dbType)
        {
            return BuildParameter(name, dbType, ParameterDirection.Input, String.Empty, DataRowVersion.Default, null);
        }

        /// <summary>
        /// Adds a new In <see cref="DbParameter"/> object to the given <paramref name="command"/>.
        /// </summary>
        /// <param name="name"><para>The name of the parameter.</para></param>
        /// <param name="dbType"><para>One of the <see cref="SqlDbType"/> values.</para></param>                
        /// <param name="value"><para>The value of the parameter.</para></param>      
        public SqlParameter BuildInParameter(string name, SqlDbType dbType, object value)
        {
            return BuildParameter(name, dbType, ParameterDirection.Input, String.Empty, DataRowVersion.Default, value);
        }

        /// <summary>
        /// Adds a new In <see cref="DbParameter"/> object to the given <paramref name="command"/>.
        /// </summary>
        /// <param name="name"><para>The name of the parameter.</para></param>
        /// <param name="dbType"><para>One of the <see cref="SqlDbType"/> values.</para></param> 
        /// <param name="size">The length of the parameter</param>
        /// <param name="value"><para>The value of the parameter.</para></param>      
        public SqlParameter BuildInParameter(string name, SqlDbType dbType, int size, object value)
        {
            return BuildParameter(name, dbType, size, ParameterDirection.Input, true, 0, 0, String.Empty, DataRowVersion.Default, value);
        }

        /// <summary>
        /// Adds a new In <see cref="DbParameter"/> object to the given <paramref name="command"/>.
        /// </summary>
        /// <param name="name"><para>The name of the parameter.</para></param>
        /// <param name="dbType"><para>One of the <see cref="SqlDbType"/> values.</para></param>                
        /// <param name="sourceColumn"><para>The name of the source column mapped to the DataSet and used for loading or returning the value.</para></param>
        /// <param name="sourceVersion"><para>One of the <see cref="DataRowVersion"/> values.</para></param>
        public SqlParameter BuildInParameter(string name, SqlDbType dbType, string sourceColumn, DataRowVersion sourceVersion)
        {
            return BuildParameter(name, dbType, 0, ParameterDirection.Input, true, 0, 0, sourceColumn, sourceVersion, null);
        }

        #endregion

        protected bool IsStoredProc(string sqlText)
        {
            if (sqlText.Trim().IndexOf(' ') > 0) //query {only consider it like this at present}?!
                return false;
            else
                return true;
        }

        protected DbCommand GetCommand(string sqlText, params SqlParameter[] parameters)
        {
            DbCommand cmd;
            if (!IsStoredProc(sqlText))
                cmd = sqlDB.GetSqlStringCommand(sqlText);
            else
                cmd = sqlDB.GetStoredProcCommand(sqlText);

            if (parameters != null && parameters.Length != 0)
                foreach (SqlParameter p in parameters)
                {
                    if ((p.Direction == ParameterDirection.InputOutput ||
                            p.Direction == ParameterDirection.Input) &&
                            (p.Value == null))
                    {
                        p.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(p);
                }
            //cmd.Parameters.AddRange(parameters);
            return cmd;
        }

        #region ExecuteNoQuery

        public int ExecuteNonQuery(string sqlText)
        {
            return sqlDB.ExecuteNonQuery(GetCommand(sqlText));
        }

        public int ExecuteNonQuery(string sqlText, params SqlParameter[] parameters)
        {
            return sqlDB.ExecuteNonQuery(GetCommand(sqlText, parameters));
        }

        public int ExecuteNonQuery(DbTransaction transaction, string sqlText, params SqlParameter[] parameters)
        {
            return sqlDB.ExecuteNonQuery(GetCommand(sqlText, parameters), transaction);
        }

        public int ExecuteNonQuery(string storedProcName, params object[] parameterValues)
        {
            return sqlDB.ExecuteNonQuery(storedProcName, parameterValues);
        }

        public int ExecuteNonQuery(DbTransaction transaction, string storedProcName, params object[] parameterValues)
        {
            return sqlDB.ExecuteNonQuery(transaction, storedProcName, parameterValues);
        }

        #endregion

        #region ExecuteDataSet

        public DataSet ExecuteDataSet(string sqlText)
        {
            return sqlDB.ExecuteDataSet(GetCommand(sqlText));
        }

        public DataSet ExecuteDataSet(string sqlText, params SqlParameter[] parameterValues)
        {
            return sqlDB.ExecuteDataSet(GetCommand(sqlText, parameterValues));
        }

        public DataSet ExecuteDataSet(DbTransaction transaction, string sqlText, params SqlParameter[] parameterValues)
        {
            return sqlDB.ExecuteDataSet(GetCommand(sqlText, parameterValues), transaction);
        }

        public DataSet ExecuteDataSet(string storedProcName, params object[] parameterValues)
        {
            return sqlDB.ExecuteDataSet(storedProcName, parameterValues);
        }

        public DataSet ExecuteDataSet(DbTransaction transaction, string storedProcName, params object[] parameterValues)
        {
            return sqlDB.ExecuteDataSet(transaction, storedProcName, parameterValues);
        }

        /// <summary>
        /// 批量插入,liujj Added 
        /// </summary>
        /// <param name="transaction">事务</param>
        /// <param name="commandText">存储过程名称</param>
        /// <param name="ds">数据表结构</param>
        /// <param name="commandParameters">数据命令参数集</param>
        /// <returns></returns>
        public int BatchInsert(DbTransaction transaction, string commandText, DataSet ds, params SqlParameter[] commandParameters)
        {
            DbCommand cmd = GetCommand(commandText, commandParameters);
            return sqlDB.UpdateDataSet(ds, ds.Tables[0].TableName, cmd, cmd, cmd, transaction);
        }

        /// <summary>
        /// 批量插入,liujj Added 
        /// </summary>
        /// <param name="transaction">事务</param>
        /// <param name="strInsert">插入存储过程名称</param>
        /// <param name="strUpdate">更新存储过程</param>
        /// <param name="strDelete">删除存储过程</param>
        /// <param name="ds">数据表结构</param>
        /// <param name="commandParameters">数据命令参数集</param>
        /// <returns></returns>
        public int BatchInsert(DbTransaction transaction, string strInsert, string strUpdate, string strDelete, DataSet ds, SqlParameter[] parasInsert, SqlParameter[] parasUpdate, SqlParameter[] parasDelete)
        {
            DbCommand cmdInsert = (strInsert == "") ? null : GetCommand(strInsert, parasInsert);
            DbCommand cmdUpdate = (strUpdate == "") ? null : GetCommand(strUpdate, parasUpdate);
            DbCommand cmdDelete = (strDelete == "") ? null : GetCommand(strDelete, parasDelete);

            return sqlDB.UpdateDataSet(ds, ds.Tables[0].TableName, cmdInsert, cmdUpdate, cmdUpdate, transaction);
        }
        #endregion

        #region ExecuteScalar

        public object ExecuteScalar(string sqlText)
        {
            return sqlDB.ExecuteScalar(GetCommand(sqlText));
        }

        public object ExecuteScalar(string sqlText, params SqlParameter[] parameterValues)
        {
            return sqlDB.ExecuteScalar(GetCommand(sqlText, parameterValues));
        }

        public object ExecuteScalar(DbTransaction transaction, string sqlText, params SqlParameter[] parameterValues)
        {
            return sqlDB.ExecuteScalar(GetCommand(sqlText, parameterValues), transaction);
        }

        public object ExecuteScalar(string storedProcName, params object[] parameterValues)
        {
            return sqlDB.ExecuteScalar(storedProcName, parameterValues);
        }

        public object ExecuteScalar(DbTransaction transaction, string storedProcName, params object[] parameterValues)
        {
            return sqlDB.ExecuteScalar(transaction, storedProcName, parameterValues);
        }

        #endregion

        #region ExecuteReader
        public IDataReader ExecuteReader(string sqlText)
        {
            return sqlDB.ExecuteReader(GetCommand(sqlText));
        }

        public IDataReader ExecuteReader(string sqlText, params SqlParameter[] parameterValues)
        {
            return sqlDB.ExecuteReader(GetCommand(sqlText, parameterValues));
        }

        public IDataReader ExecuteReader(DbTransaction transaction, string sqlText, params SqlParameter[] parameterValues)
        {
            return sqlDB.ExecuteReader(GetCommand(sqlText, parameterValues), transaction);
        }

        public IDataReader ExecuteReader(string storedProcName, params object[] parameterValues)
        {
            return sqlDB.ExecuteReader(storedProcName, parameterValues);
        }

        public IDataReader ExecuteReader(DbTransaction transaction, string storedProcName, params object[] parameterValues)
        {
            return sqlDB.ExecuteReader(transaction, storedProcName, parameterValues);
        }


        #endregion

        #region ExecuteXmlReader

        public XmlReader ExecuteXmlReader(string sqlText, params SqlParameter[] parameterValues)
        {
            return sqlDB.ExecuteXmlReader(GetCommand(sqlText, parameterValues));
        }

        public XmlReader ExecuteXmlReader(DbTransaction transaction, string sqlText, params SqlParameter[] parameterValues)
        {
            return sqlDB.ExecuteXmlReader(GetCommand(sqlText, parameterValues), transaction);
        }
        #endregion
    }
}
