using Microsoft.Data.SqlClient;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Text;

namespace Cookapp_API.DataAccess.DAL
{
    public class MSSQLSERVERDataAccess
    {
        public string m_ConnectionString;

        private int m_commandTimeout;


        public MSSQLSERVERDataAccess(string connectionString, int commandTimeout)
        {
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;

            m_ConnectionString = connectionString;
            m_commandTimeout = commandTimeout;
        }

        public MSSQLSERVERDataAccess(string connectionString)
        {
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;


            m_ConnectionString = connectionString;
            m_commandTimeout = 60;
        }

        public MSSQLSERVERDataAccess()
        {
            m_ConnectionString = string.Empty;
            m_commandTimeout = 0;
        }




        /// <summary>
        /// Chuyen danh sach string sang chuoi dang filter trong cau select ... where .. in
        /// </summary>
        /// <param name="lstString"></param>
        /// <returns></returns>
        public string ArrayStringToStringFilter(List<string> lstString)
        {
            return GlobalFuncs.ArrayStringToStringFilter(lstString);
        }

        /// <summary>
        /// Chuyen danh sach string sang chuoi dang filter trong cau select ... where .. in
        /// </summary>
        /// <param name="lstString"></param>
        /// <returns></returns>
        public string ArrayStringToStringFilter(string[] arrString)
        {
            return GlobalFuncs.ArrayStringToStringFilter(arrString);
        }

        public string ArrayIntToStringFilter(List<int> lstInt)
        {
            return GlobalFuncs.ArrayIntToStringFilter(lstInt);//<@van CODE_20211008>
        }

        public string ArrayInt64ToStringFilter(List<long> lstInt)
        {
            return GlobalFuncs.ArrayIntToStringFilter(lstInt);
        }



        protected object ExecuteScalar(string strSQL, List<string> arrParam, List<object> arrValue)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(m_ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(strSQL, cn);
                    if (m_commandTimeout > 0)
                        cmd.CommandTimeout = m_commandTimeout;
                    for (int i = 0; i < arrParam.Count; i++)
                    {
                        cmd.Parameters.Add(new SqlParameter(arrParam[i], arrValue[i]));
                    }
                    object obj = cmd.ExecuteScalar();
                    return obj;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object ExecuteScalar(string strSQL)
        {
            try
            {

                using (SqlConnection cn = new SqlConnection(m_ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(strSQL, cn);
                    if (m_commandTimeout > 0)
                        cmd.CommandTimeout = m_commandTimeout;

                    object obj = cmd.ExecuteScalar();
                    return obj;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected DataSet ExecuteDataset(string strSQL)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(m_ConnectionString))
                {
                    SqlCommand objCmd = new SqlCommand(strSQL, cn);
                    objCmd.CommandType = CommandType.Text;
                    if (m_commandTimeout > 0)
                        objCmd.CommandTimeout = m_commandTimeout;


                    SqlDataAdapter objDA = new SqlDataAdapter(objCmd);
                    DataSet objDS = new DataSet();
                    objDA.Fill(objDS);
                    cn.Close();
                    return objDS;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ExecuteNonQuery(string strSQL)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(m_ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(strSQL, cn);

                    if (m_commandTimeout > 0)
                        cmd.CommandTimeout = m_commandTimeout;

                    cmd.CommandType = CommandType.Text;

                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected int ExecuteNonQuery(string strSQL, List<string> arrPramName, List<object> arrValue)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(m_ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(strSQL, cn);

                    if (m_commandTimeout > 0)
                        cmd.CommandTimeout = m_commandTimeout;

                    cmd.CommandType = CommandType.Text;
                    for (int i = 0; i < arrPramName.Count; i++)
                    {
                        cmd.Parameters.Add(new SqlParameter(arrPramName[i], arrValue[i]));
                    }
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<object> GetListObject(string strSQL)
        {
            SqlConnection sqlConn = null;
            SqlCommand sqlCmd = null;
            SqlDataReader reader = null;
            List<object> arrObject = new List<object>();
            try
            {
                sqlConn = new SqlConnection(m_ConnectionString);
                sqlConn.Open();

                sqlCmd = new SqlCommand();

                sqlCmd.CommandText = strSQL;
                sqlCmd.Connection = sqlConn;

                if (m_commandTimeout > 0)
                    sqlCmd.CommandTimeout = m_commandTimeout;

                reader = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection);
                object obj;
                while (reader.Read())
                {
                    obj = reader.GetValue(0);
                    arrObject.Add(obj);
                }
                reader.Close();
                reader = null;

                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return arrObject;
            }
            catch (Exception ex)
            {
                if (reader != null)
                {
                    reader.Close();
                    reader = null;
                }
                if (sqlCmd != null)
                    sqlCmd.Dispose();
                if (sqlConn != null)
                {
                    sqlConn.Close();
                    sqlConn.Dispose();
                }
                throw ex;
            }
        }//end

        protected DataSet ExecuteDataset(string strSQL, List<string> arrPramName, List<object> arrValue)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(m_ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(strSQL, cn);
                    cmd.CommandType = CommandType.Text;

                    if (m_commandTimeout > 0)
                        cmd.CommandTimeout = m_commandTimeout;

                    for (int i = 0; i < arrPramName.Count; i++)
                    {
                        cmd.Parameters.Add(new SqlParameter(arrPramName[i], arrValue[i]));
                    }
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    DataSet dst = new DataSet();
                    adap.Fill(dst);
                    return dst;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected DataSet ExecuteDataset(string strSQL, SqlParameter[] sqlParams = null)
        {
            SqlConnection cn = null;
            try
            {
                if (!string.IsNullOrEmpty(strSQL))
                {
                    DataSet dst = new DataSet();
                    using (cn = new SqlConnection(m_ConnectionString))
                    using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = m_commandTimeout;

                        if (sqlParams != null && sqlParams.Length > 0)
                        {
                            foreach (SqlParameter param in sqlParams)
                                cmd.Parameters.Add(param);
                        }

                        cn.Open();

                        using (SqlDataAdapter adap = new SqlDataAdapter(cmd))
                        {
                            adap.Fill(dst);
                        }

                        cmd.Parameters.Clear();
                    }

                    cn.Close();
                    return dst;
                }
            }
            catch (System.Exception ex)
            {
                cn.Close();
                throw ex;
            }

            return null;
        }



        //2012-03-19
        protected Hashtable ExecuteHashtable(string query, List<string> arrParam, List<object> arrValue)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(m_ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.Text;
                    if (m_commandTimeout > 0)
                        cmd.CommandTimeout = m_commandTimeout;

                    for (int i = 0; i < arrParam.Count; i++)
                    {
                        cmd.Parameters.Add(new SqlParameter(arrParam[i], arrValue[i]));
                    }
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    Hashtable hsObjs = null;
                    if (reader.Read())
                    {
                        hsObjs = new Hashtable();
                        string fieldName;
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            fieldName = reader.GetName(i).ToLower();
                            if (!hsObjs.ContainsKey(fieldName))
                                hsObjs.Add(fieldName, reader[i]);
                        }
                    }

                    if (reader != null)
                        reader.Close();
                    if (cmd != null)
                        cmd.Dispose();
                    return hsObjs;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }


        //2012-03-21,van
        public List<Hashtable> ExecuteArrayHastable(string query)
        {
            try
            {
                List<Hashtable> arrHasObj = new List<Hashtable>();

                using (SqlConnection cn = new SqlConnection(m_ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.Text;

                    if (m_commandTimeout > 0)
                        cmd.CommandTimeout = m_commandTimeout;

                    SqlDataReader reader = cmd.ExecuteReader();
                    Hashtable hsObjs = null;
                    while (reader.Read())
                    {
                        hsObjs = new Hashtable();
                        string fieldName;
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            fieldName = reader.GetName(i).ToLower();
                            if (!hsObjs.ContainsKey(fieldName))
                                hsObjs.Add(fieldName, reader[i]);
                        }
                        arrHasObj.Add(hsObjs);
                    }

                    if (reader != null)
                    {
                        reader.Close();
                        reader = null;
                    }
                    if (cmd != null)
                        cmd.Dispose();
                    return arrHasObj;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        //2012-04-11,van
        protected object[] GetObjects(string strSQL)
        {
            using (SqlConnection cn = new SqlConnection(m_ConnectionString))
            {
                SqlDataReader reader = null;
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(strSQL, cn);
                    cmd.CommandType = CommandType.Text;

                    if (m_commandTimeout > 0)
                        cmd.CommandTimeout = m_commandTimeout;

                    reader = cmd.ExecuteReader();

                    object[] objs = null;

                    if (reader.Read())
                    {
                        objs = new object[reader.FieldCount];
                        reader.GetValues(objs);
                    }
                    reader.Close();
                    reader = null;
                    return objs;
                }
                catch (Exception ex)
                {
                    if (reader != null)
                    {
                        reader.Close();
                        reader = null;
                    }
                    throw ex;
                }
            }
        }

        //2012-04-18,van
        protected List<object> GetListObject(string strSQL, List<string> arrayParamName, List<object> arrayValue)
        {
            SqlConnection sqlConn = null;
            SqlCommand sqlCmd = null;
            SqlDataReader reader = null;
            List<object> arrObject = new List<object>();
            try
            {
                sqlConn = new SqlConnection(m_ConnectionString);
                sqlConn.Open();

                sqlCmd = new SqlCommand();

                sqlCmd.CommandText = strSQL;
                sqlCmd.Connection = sqlConn;

                if (m_commandTimeout > 0)
                    sqlCmd.CommandTimeout = m_commandTimeout;

                if (arrayParamName != null && arrayValue != null && arrayParamName.Count == arrayValue.Count)
                {
                    for (int i = 0; i < arrayParamName.Count; i++)
                    {
                        sqlCmd.Parameters.Add(new SqlParameter(arrayParamName[i], arrayValue[i]));
                    }
                }

                reader = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection);
                object obj;
                while (reader.Read())
                {
                    obj = reader.GetValue(0);
                    arrObject.Add(obj);
                }
                reader.Close();
                reader = null;

                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return arrObject;
            }
            catch (Exception ex)
            {
                if (reader != null)
                {
                    reader.Close();
                    reader = null;
                }
                if (sqlCmd != null)
                    sqlCmd.Dispose();
                if (sqlConn != null)
                {
                    sqlConn.Close();
                    sqlConn.Dispose();
                }
                throw ex;
            }
        }//end

        protected List<Hashtable> ExecuteArrayHastable(string query, List<string> arrayParamName, List<object> arrayValue)
        {
            try
            {
                List<Hashtable> arrHasObj = new List<Hashtable>();

                using (SqlConnection cn = new SqlConnection(m_ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.Text;

                    if (m_commandTimeout > 0)
                        cmd.CommandTimeout = m_commandTimeout;
                    if (arrayParamName != null && arrayValue != null && arrayParamName.Count == arrayValue.Count)
                    {
                        for (int i = 0; i < arrayParamName.Count; i++)
                        {
                            cmd.Parameters.Add(new SqlParameter(arrayParamName[i], arrayValue[i]));
                        }
                    }
                    SqlDataReader reader = cmd.ExecuteReader();
                    Hashtable hsObjs = null;
                    while (reader.Read())
                    {
                        hsObjs = new Hashtable();
                        string fieldName;
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            fieldName = reader.GetName(i).ToLower();
                            if (!hsObjs.ContainsKey(fieldName))
                                hsObjs.Add(fieldName, reader[i]);
                        }
                        arrHasObj.Add(hsObjs);
                    }

                    if (reader != null)
                    {
                        reader.Close();
                        reader = null;
                    }
                    if (cmd != null)
                        cmd.Dispose();
                    return arrHasObj;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Return true if table exists. Else return false.
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public bool CheckTableExists(string tableName)
        {
            try
            {
                string query = "select name from sysobjects where name = '" + tableName + "'";

                object result = ExecuteScalar(query);

                return result != null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //<@van CODE_20220819>
        public List<object[]> GetListObjects(string strSQL, List<string> arrayParamName, List<object> arrayValue)
        {
            SqlConnection sqlConn = null;
            SqlCommand sqlCmd = null;
            SqlDataReader reader = null;
            List<object[]> arrObject = new List<object[]>();
            try
            {
                sqlConn = new SqlConnection(m_ConnectionString);
                sqlConn.Open();

                sqlCmd = new SqlCommand();

                sqlCmd.CommandText = strSQL;
                sqlCmd.Connection = sqlConn;

                if (m_commandTimeout > 0)
                    sqlCmd.CommandTimeout = m_commandTimeout;

                if (arrayParamName != null && arrayValue != null && arrayParamName.Count == arrayValue.Count)
                {
                    for (int i = 0; i < arrayParamName.Count; i++)
                    {
                        sqlCmd.Parameters.Add(new SqlParameter(arrayParamName[i], arrayValue[i]));
                    }
                }

                reader = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection);
                object[] objs = null;
                while (reader.Read())
                {
                    objs = new object[reader.FieldCount];
                    reader.GetValues(objs);

                    arrObject.Add(objs);
                }
                reader.Close();
                reader = null;

                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return arrObject;
            }
            catch (Exception ex)
            {
                if (reader != null)
                {
                    reader.Close();
                    reader = null;
                }
                if (sqlCmd != null)
                    sqlCmd.Dispose();
                if (sqlConn != null)
                {
                    sqlConn.Close();
                    sqlConn.Dispose();
                }
                throw ex;
            }
        }//end

        //2013-08-09 @Vannn
        protected int InsertIntoTable(string tableName, List<string> arrayFieldName
            , List<string> arrayParamName, List<object> arrayValue)
        {

            try
            {
                StringBuilder queryBuilder = new StringBuilder();
                queryBuilder.Append("insert into " + GlobalFuncs.FixSqlInjection(tableName) + " (");//<@van CODE_20211013: FORTIFY>
                for (int i = 0; i < arrayFieldName.Count; i++)
                {
                    queryBuilder.Append(arrayFieldName[i]);
                    if (i < arrayFieldName.Count - 1)
                        queryBuilder.Append(",");
                }
                queryBuilder.Append(")");
                queryBuilder.Append(" values (");
                for (int i = 0; i < arrayParamName.Count; i++)
                {
                    queryBuilder.Append(arrayParamName[i]);
                    if (i < arrayParamName.Count - 1)
                        queryBuilder.Append(",");
                }
                queryBuilder.Append(")");
                //
                int exec = ExecuteNonQuery(queryBuilder.ToString(), arrayParamName, arrayValue);
                return exec;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //2014-10-16 @Vannn
        protected object ExecuteProcedure(string procedureName, string sqlQuery)
        {
            try
            {
                object result = null;
                using (SqlConnection con = new SqlConnection(m_ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(procedureName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@sqlQuery", sqlQuery);
                        cmd.Parameters.Add("@iCount", SqlDbType.Int);
                        cmd.Parameters["@iCount"].Direction = ParameterDirection.Output;
                        con.Open();
                        int x = cmd.ExecuteNonQuery();
                        con.Close();
                        //
                        result = cmd.Parameters["@iCount"].Value;
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("ExecuteProcedure Exception: " + ex.Message);
            }
        }

        protected int ExecuteUpdate(string tableName, string tableFields, List<string> arrayFieldToCompare, List<string> arrayFieldToUpdate, List<string> arrayValue)
        {
            try
            {
                string query = string.Empty;
                int numUpdate = 0;

                // Create temp Table   
                string tmpTable = "#temp" + tableName;
                query = "if db_id('" + tmpTable + "') is not null drop table " + tmpTable + "; select * into " + tmpTable + " from " + tableName + " where 1 = 2;";
                ExecuteNonQuery(query);
                while (arrayValue.Count > 0)
                {
                    string[] arrTmpValues = arrayValue.Take(100).ToArray();
                    arrayValue.RemoveRange(0, arrTmpValues.Length);
                    string values = string.Join(",", arrTmpValues);
                    query = "insert into " + tmpTable + "(" + tableFields + ") values " + values;
                    ExecuteNonQuery(query);
                }

                //Set Fields
                if (arrayFieldToUpdate.Count == 0) arrayFieldToUpdate = tableFields.Split(",".ToCharArray()).ToList();
                string[] updateFields = arrayFieldToUpdate.Select(ite => "t1." + ite + " = t2." + ite).ToArray();
                string setFields = string.Join(",", updateFields);

                //Where Fields
                if (arrayFieldToCompare.Count == 0) arrayFieldToCompare = arrayFieldToUpdate;
                string[] conditionFields = arrayFieldToCompare.Select(ite => "t1." + ite + " = t2." + ite).ToArray();
                string whereFields = string.Join(" and ", conditionFields);

                query = "update t1 set " + setFields + " from " + tableName + " t1 join " + tmpTable + " t2 on " + whereFields;
                query += "; if db_id('" + tmpTable + "') is not null drop table " + tmpTable;

                ExecuteNonQuery(query);

                return numUpdate;

            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //2014-12-27 @Vunggg : @Vannn
        protected List<Hashtable> ExecuteArrayHastable(string tableName, List<string> arrayFieldToGet, List<string> arrayFieldToCompare, List<string> arrayValue)
        {
            try
            {
                if (arrayFieldToGet.Count == 0 || arrayFieldToCompare.Count == 0 || arrayValue.Count == 0)
                    return new List<Hashtable>();

                string selectFields = string.Join(",", arrayFieldToGet.Select(ite => "tbl1." + ite).ToArray());
                string values = string.Join(",", arrayValue.ToArray());
                string whereFields = "join (values " + values + ") as tbl2 (" + string.Join(",", arrayFieldToCompare.ToArray()) + ")";
                whereFields += " on " + string.Join(" and ", arrayFieldToCompare.Select(ite => "tbl1." + ite + "=" + "tbl2." + ite).ToArray());
                string query = "select " + selectFields + " from " + tableName + " tbl1 " + whereFields;
                List<Hashtable> arrHsObj = ExecuteArrayHastable(query);
                return arrHsObj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //2015-01-21 @Vann
        /// <summary>
        /// xoa het toan bo du lieu
        /// </summary>
        /// <returns></returns>
        protected int TruncateTable(string tableName)
        {
            try
            {
                string query = "truncate table " + tableName;
                int executed = ExecuteNonQuery(query);
                return executed;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //2015-03-06 @Vannn: lay ket qua tu danh sach cau query
        protected List<object> GetListObject(List<string> arrayStrSQL)
        {
            if (arrayStrSQL == null || arrayStrSQL.Count == 0)
                return new List<object>();
            //
            SqlConnection sqlConn = null;
            SqlCommand sqlCmd = null;
            SqlDataReader reader = null;
            List<object> arrObject = new List<object>();
            try
            {
                sqlConn = new SqlConnection(m_ConnectionString);
                sqlConn.Open();

                sqlCmd = new SqlCommand();
                sqlCmd.CommandText = GlobalFuncs.ArrayStringToString(arrayStrSQL, ";");

                sqlCmd.Connection = sqlConn;

                if (m_commandTimeout > 0)
                    sqlCmd.CommandTimeout = m_commandTimeout;

                reader = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection);
                object obj;


                do
                {
                    while (reader.Read())
                    {
                        obj = reader.GetValue(0);
                        arrObject.Add(obj);
                    }
                } while (reader.NextResult());

                reader.Close();
                reader = null;

                sqlCmd.Dispose();

                sqlConn.Close();
                sqlConn.Dispose();

                return arrObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader = null;
                }
                if (sqlCmd != null)
                    sqlCmd.Dispose();
                if (sqlConn != null)
                {
                    sqlConn.Close();
                    sqlConn.Dispose();
                }
            }
        }//end

        //2015-09-11 @Vann
        public List<string> GetListDatabaseName()
        {
            try
            {
                string query = "select [name] from master.sys.databases";
                List<object> arrResult = GetListObject(query);
                List<string> arrDbName = arrResult.Select(x => Convert.ToString(x)).ToList();
                return arrDbName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        //2016-06-24 @Vann
        public List<object> GetListObject(SqlConnection externalSqlConnection, string strSQL)
        {
            SqlCommand sqlCmd = null;
            SqlDataReader reader = null;
            List<object> arrObject = new List<object>();
            try
            {
                sqlCmd = new SqlCommand();

                sqlCmd.CommandText = strSQL;
                sqlCmd.Connection = externalSqlConnection;

                if (m_commandTimeout > 0)
                    sqlCmd.CommandTimeout = m_commandTimeout;

                reader = sqlCmd.ExecuteReader();
                object obj;
                while (reader.Read())
                {
                    obj = reader.GetValue(0);
                    arrObject.Add(obj);
                }
                reader.Close();
                reader.Dispose();
                reader = null;

                sqlCmd.Dispose();

                return arrObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader = null;
                }
                if (sqlCmd != null)
                    sqlCmd.Dispose();
            }
        }//end


        public Hashtable ExecuteHashtable(string query)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(m_ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.Text;

                    if (m_commandTimeout > 0)
                        cmd.CommandTimeout = m_commandTimeout;

                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    Hashtable hsObjs = null;
                    if (reader.Read())
                    {

                        hsObjs = new Hashtable();
                        string fieldName;
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            fieldName = reader.GetName(i).ToLower();
                            if (!hsObjs.ContainsKey(fieldName))
                                hsObjs.Add(fieldName, reader[i]);
                        }
                    }

                    if (reader != null)
                        reader.Close();
                    if (cmd != null)
                        cmd.Dispose();
                    return hsObjs;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }

        //2016-06-25 @Vann
        protected Hashtable ExecuteHashtable(SqlConnection sqlConn, string query)
        {
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            try
            {
                cmd = new SqlCommand(query, sqlConn);
                cmd.CommandType = CommandType.Text;

                if (m_commandTimeout > 0)
                    cmd.CommandTimeout = m_commandTimeout;

                reader = cmd.ExecuteReader();
                Hashtable hsObjs = null;
                if (reader.Read())
                {

                    hsObjs = new Hashtable();
                    string fieldName;
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        fieldName = reader.GetName(i).ToLower();
                        if (!hsObjs.ContainsKey(fieldName))
                            hsObjs.Add(fieldName, reader[i]);
                    }
                }
                return hsObjs;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                }
                if (cmd != null)
                    cmd.Dispose();
            }

        }
        //2016-06-25 @Vannn
        protected object ExecuteScalar(SqlConnection sqlConn, string strSQL)
        {
            SqlCommand cmd = null;
            try
            {
                cmd = new SqlCommand(strSQL, sqlConn);
                if (m_commandTimeout > 0)
                    cmd.CommandTimeout = m_commandTimeout;

                object obj = cmd.ExecuteScalar();
                return obj;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
            }
        }

        //2016-06-25 @Vannn
        public List<Hashtable> ExecuteArrayHastable(SqlConnection sqlConn, string query)
        {
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            try
            {
                List<Hashtable> arrHasObj = new List<Hashtable>();
                cmd = new SqlCommand(query, sqlConn);
                cmd.CommandType = CommandType.Text;

                if (m_commandTimeout > 0)
                    cmd.CommandTimeout = m_commandTimeout;

                reader = cmd.ExecuteReader();
                Hashtable hsObjs = null;
                while (reader.Read())
                {
                    hsObjs = new Hashtable();
                    string fieldName;
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        fieldName = reader.GetName(i).ToLower();
                        if (!hsObjs.ContainsKey(fieldName))
                            hsObjs.Add(fieldName, reader[i]);
                    }
                    arrHasObj.Add(hsObjs);
                }
                return arrHasObj;

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader = null;
                }
                if (cmd != null)
                    cmd.Dispose();
            }
        }

        //2016-06-25 @Vannn: them SqlConnection
        public List<object[]> GetListObjects(SqlConnection sqlConn, string strSQL)
        {
            SqlCommand sqlCmd = null;
            SqlDataReader reader = null;
            List<object[]> arrObject = new List<object[]>();
            try
            {
                sqlCmd = new SqlCommand();

                sqlCmd.CommandText = strSQL;
                sqlCmd.Connection = sqlConn;

                if (m_commandTimeout > 0)
                    sqlCmd.CommandTimeout = m_commandTimeout;

                reader = sqlCmd.ExecuteReader();
                object[] objs = null;
                while (reader.Read())
                {
                    objs = new object[reader.FieldCount];
                    reader.GetValues(objs);

                    arrObject.Add(objs);
                }
                return arrObject;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader = null;
                }
                if (sqlCmd != null)
                    sqlCmd.Dispose();
            }
        }//end

        //2016-07-08 @Vann
        protected int InsertIntoTable(SqlConnection sqlConn, string tableName, List<string> arrayFieldName
            , List<string> arrayParamName, List<object> arrayValue)
        {

            try
            {
                StringBuilder queryBuilder = new StringBuilder();
                queryBuilder.Append("insert into " + tableName + " (");
                for (int i = 0; i < arrayFieldName.Count; i++)
                {
                    queryBuilder.Append(arrayFieldName[i]);
                    if (i < arrayFieldName.Count - 1)
                        queryBuilder.Append(",");
                }
                queryBuilder.Append(")");
                queryBuilder.Append(" values (");
                for (int i = 0; i < arrayParamName.Count; i++)
                {
                    queryBuilder.Append(arrayParamName[i]);
                    if (i < arrayParamName.Count - 1)
                        queryBuilder.Append(",");
                }
                queryBuilder.Append(")");
                //
                int exec;
                if (sqlConn == null)
                    exec = ExecuteNonQuery(queryBuilder.ToString(), arrayParamName, arrayValue);
                else
                    exec = ExecuteNonQuery(sqlConn, queryBuilder.ToString(), arrayParamName, arrayValue);
                return exec;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //2016-07-08 @Vann
        protected int ExecuteNonQuery(SqlConnection sqlConn, string strSQL, List<string> arrPramName, List<object> arrValue)
        {
            SqlCommand cmd = null;
            try
            {
                cmd = new SqlCommand(strSQL, sqlConn);

                if (m_commandTimeout > 0)
                    cmd.CommandTimeout = m_commandTimeout;

                cmd.CommandType = CommandType.Text;
                for (int i = 0; i < arrPramName.Count; i++)
                {
                    cmd.Parameters.Add(new SqlParameter(arrPramName[i], arrValue[i]));
                }
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
            }
        }



        protected string GetQueryFromToIndex(string innerIndexQuery, int fromIndex, int toIndex)
        {
            try
            {
                string query = "select res.* from (" + innerIndexQuery + ") res where res.RowIndex >= " + fromIndex + " and res.RowIndex <= " + toIndex;
                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //2016-08-26 @Vunggg : @Vannn
        protected int InsertIntoTableWithParameters(string strSQL, Dictionary<string, object> FieldsValues, SqlConnection externalSqlConnection)
        {
            SqlConnection cn = externalSqlConnection != null ? externalSqlConnection : new SqlConnection(m_ConnectionString);
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(strSQL, cn);
                if (m_commandTimeout > 0) cmd.CommandTimeout = m_commandTimeout;
                cmd.CommandType = CommandType.Text;

                foreach (KeyValuePair<string, object> fv in FieldsValues)
                {
                    cmd.Parameters.Add(new SqlParameter("@" + fv.Key, fv.Value));
                }

                int executed = cmd.ExecuteNonQuery();

                cn.Close();
                cn.Dispose();
                return executed;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cn != null)
                {
                    cn.Close();
                    cn.Dispose();
                }
            }
        }

        //2017-04-20 @Vann
        public int ExecuteNonQuery(SqlConnection sqlConn, string strSQL)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(strSQL, sqlConn);
                if (m_commandTimeout > 0)
                    cmd.CommandTimeout = m_commandTimeout;

                cmd.CommandType = CommandType.Text;
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected SqlDataReader ExecuteReader(string strSQL)
        {
            if (strSQL != "")
            {
                SqlConnection cn = null;
                try
                {
                    SqlDataReader reader = null;
                    cn = new SqlConnection(m_ConnectionString);
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(strSQL, cn);
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = m_commandTimeout;
                    reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    return reader;
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
            return null;
        }

        //2019-05-25 @Van
        protected object[] GetObjects(SqlConnection cn, string strSQL)
        {
            SqlDataReader reader = null;
            try
            {
                //cn.Open(); <@Van CODE_20190905: close code "cn.Open()"
                SqlCommand cmd = new SqlCommand(strSQL, cn);
                cmd.CommandType = CommandType.Text;

                if (m_commandTimeout > 0)
                    cmd.CommandTimeout = m_commandTimeout;

                reader = cmd.ExecuteReader();

                object[] objs = null;

                if (reader.Read())
                {
                    objs = new object[reader.FieldCount];
                    reader.GetValues(objs);
                }
                reader.Close();
                reader = null;
                return objs;
            }
            catch (Exception ex)
            {
                if (reader != null)
                {
                    reader.Close();
                    reader = null;
                }
                throw ex;
            }
        }

        //2022-08-04 @Van
        protected object ExecuteScalar(SqlConnection sqlConn, string strSQL, List<string> arrParam, List<object> arrValue)
        {
            try
            {

                SqlCommand cmd = new SqlCommand(strSQL, sqlConn);
                if (m_commandTimeout > 0)
                    cmd.CommandTimeout = m_commandTimeout;
                for (int i = 0; i < arrParam.Count; i++)
                {
                    cmd.Parameters.Add(new SqlParameter(arrParam[i], arrValue[i]));
                }
                object obj = cmd.ExecuteScalar();
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //2022-08-16 @Van
        public List<object[]> GetListObjects(SqlConnection sqlConn, string strSQL, List<string> arrParam, List<object> arrValue)
        {
            SqlCommand sqlCmd = null;
            SqlDataReader reader = null;
            List<object[]> arrObject = new List<object[]>();
            try
            {
                sqlCmd = new SqlCommand();

                sqlCmd.CommandText = strSQL;
                sqlCmd.Connection = sqlConn;

                for (int i = 0; i < arrParam.Count; i++)
                {
                    sqlCmd.Parameters.Add(new SqlParameter(arrParam[i], arrValue[i]));
                }

                if (m_commandTimeout > 0)
                    sqlCmd.CommandTimeout = m_commandTimeout;

                reader = sqlCmd.ExecuteReader();
                object[] objs = null;
                while (reader.Read())
                {
                    objs = new object[reader.FieldCount];
                    reader.GetValues(objs);

                    arrObject.Add(objs);
                }
                return arrObject;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader = null;
                }
                if (sqlCmd != null)
                    sqlCmd.Dispose();
            }
        }//end

        //<@van CODE_20220822>
        public List<Hashtable> ExecuteArrayHastable(SqlConnection sqlConn, string query, List<SqlParameter> arrparams, CommandType cmdType)
        {
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            try
            {
                List<Hashtable> arrHasObj = new List<Hashtable>();
                cmd = new SqlCommand(query, sqlConn);
                cmd.CommandType = cmdType;

                if (m_commandTimeout > 0)
                    cmd.CommandTimeout = m_commandTimeout;

                for (int i = 0; i < arrparams.Count; i++)
                {
                    cmd.Parameters.Add(arrparams[i]);
                }

                reader = cmd.ExecuteReader();
                Hashtable hsObjs = null;
                while (reader.Read())
                {
                    hsObjs = new Hashtable();
                    string fieldName;
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        fieldName = reader.GetName(i).ToLower();
                        if (!hsObjs.ContainsKey(fieldName))
                            hsObjs.Add(fieldName, reader[i]);
                    }
                    arrHasObj.Add(hsObjs);
                }
                return arrHasObj;

            }
            catch (System.Exception ex)
            {
                throw;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader = null;
                }
                if (cmd != null)
                    cmd.Dispose();
            }
        }
        public List<Hashtable> ExecuteArrayHastable(string query, List<SqlParameter> arrparams, CommandType cmdType)
        {
            try
            {
                List<Hashtable> arrHasObj = new List<Hashtable>();

                using (SqlConnection cn = new SqlConnection(m_ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = cmdType;

                    if (m_commandTimeout > 0)
                        cmd.CommandTimeout = m_commandTimeout;

                    for (int i = 0; i < arrparams.Count; i++)
                    {
                        cmd.Parameters.Add(arrparams[i]);
                    }

                    SqlDataReader reader = cmd.ExecuteReader();
                    Hashtable hsObjs = null;
                    while (reader.Read())
                    {
                        hsObjs = new Hashtable();
                        string fieldName;
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            fieldName = reader.GetName(i).ToLower();
                            if (!hsObjs.ContainsKey(fieldName))
                                hsObjs.Add(fieldName, reader[i]);
                        }
                        arrHasObj.Add(hsObjs);
                    }

                    if (reader != null)
                    {
                        reader.Close();
                        reader = null;
                    }
                    if (cmd != null)
                        cmd.Dispose();
                    return arrHasObj;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public string ConvertSqlCommandToString(List<SqlParameter> parameters, string sqlCommandText, CommandType commandType = CommandType.Text)
        {
            string sqlString = sqlCommandText;
            try
            {

                if (parameters != null && parameters.Count > 0)
                {
                    string Value;
                    if (commandType == CommandType.StoredProcedure)
                    {
                        List<string> list = new List<string>();
                        foreach (SqlParameter p in parameters)
                        {
                            Value = GetParameterValue(p).Replace("\r", "").Replace("\n", "");
                            list.Add(string.Format("{0}={1}", p.ParameterName, Value));
                        }
                        sqlString = "EXEC " + sqlCommandText + " " + string.Join(",", list.ToArray());
                    }
                    else
                    {
                        sqlString = sqlCommandText;
                        foreach (SqlParameter p in parameters)
                        {
                            Value = GetParameterValue(p);
                            sqlCommandText = sqlCommandText.Replace(p.ParameterName, Value);
                        }

                        sqlString = sqlCommandText.Replace("= NULL", "IS NULL").Replace("!= NULL", "IS NOT NULL");
                    }
                }
            }
            catch { }
            return sqlString;
        }

        string GetParameterValue(SqlParameter p)
        {
            string retval = "";

            try
            {
                switch (p.SqlDbType)
                {
                    case SqlDbType.VarChar:
                    case SqlDbType.NVarChar:
                    case SqlDbType.NChar:
                    case SqlDbType.Text:
                    case SqlDbType.Char:
                    case SqlDbType.NText:
                        if (p.Value == DBNull.Value)
                            retval = "''";
                        else
                            retval = "'" + p.Value.ToString().Replace("'", "''") + "'";
                        break;

                    case SqlDbType.Xml:
                    case SqlDbType.UniqueIdentifier:
                        if (p.Value == DBNull.Value)
                            retval = "NULL";
                        else
                            retval = p.Value.ToString();
                        break;

                    case SqlDbType.Date:
                    case SqlDbType.Time:
                    case SqlDbType.DateTime:
                    case SqlDbType.DateTimeOffset:
                    case SqlDbType.SmallDateTime:
                    case SqlDbType.Timestamp:
                    case SqlDbType.DateTime2:
                        if (p.Value == DBNull.Value || string.Compare("null", p.Value.ToString(), true) == 0)
                            retval = "''";
                        else
                            retval = "'" + (p.Value ?? string.Empty) + "'";
                        break;

                    case SqlDbType.Bit:
                        if (p.Value == DBNull.Value)
                            retval = "0";
                        else
                            retval = (bool.Parse(p.Value.ToString() ?? "False")) ? "1" : "0";
                        break;

                    case SqlDbType.Binary:
                    case SqlDbType.VarBinary:
                        if (p.Value == DBNull.Value)
                            retval = "NULL";
                        else
                            retval = System.Text.Encoding.UTF8.GetString((Byte[])p.Value);
                        break;

                    case SqlDbType.Int:
                    case SqlDbType.TinyInt:
                    case SqlDbType.SmallInt:
                    case SqlDbType.BigInt:
                    case SqlDbType.Float:
                    case SqlDbType.Decimal:
                        if (p.Value == DBNull.Value)
                            retval = "0";
                        else
                            retval = p.Value.ToString();
                        break;

                    default:
                        if (p.Value == DBNull.Value)
                            retval = "NULL";
                        else
                            retval = p.Value.ToString();

                        break;
                }
            }
            catch { }
            return retval;
        }

        //<@van CODE_20220823>
        protected object ExecuteScalar(SqlConnection sqlConn, string strSQL, List<SqlParameter> arrayParams, CommandType cmdType)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(strSQL, sqlConn);
                if (m_commandTimeout > 0)
                    cmd.CommandTimeout = m_commandTimeout;
                cmd.CommandType = cmdType;
                for (int i = 0; i < arrayParams.Count; i++)
                {
                    cmd.Parameters.Add(arrayParams[i]);
                }
                object obj = cmd.ExecuteScalar();
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //<@van CODE_20220823>
        protected object ExecuteScalar(string strSQL, List<SqlParameter> arrayParams, CommandType cmdType)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(m_ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(strSQL, cn);
                    if (m_commandTimeout > 0)
                        cmd.CommandTimeout = m_commandTimeout;
                    for (int i = 0; i < arrayParams.Count; i++)
                    {
                        cmd.Parameters.Add(arrayParams[i]);
                    }
                    object obj = cmd.ExecuteScalar();
                    return obj;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //2022-08-24 @Van
        protected DataSet ExecuteDataset(string strSQL, List<SqlParameter> arrayParams, CommandType cmdType)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(m_ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(strSQL, cn);
                    cmd.CommandType = cmdType;

                    if (m_commandTimeout > 0)
                        cmd.CommandTimeout = m_commandTimeout;

                    for (int i = 0; i < arrayParams.Count; i++)
                        cmd.Parameters.Add(arrayParams[i]);
                    //
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    DataSet dst = new DataSet();
                    adap.Fill(dst);
                    cmd.Dispose();
                    return dst;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //2022-08-24 @Van
        protected DataSet ExecuteDataset(SqlConnection sqlConn, string strSQL, List<SqlParameter> arrayParams, CommandType cmdType)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(strSQL, sqlConn);
                cmd.CommandType = cmdType;

                if (m_commandTimeout > 0)
                    cmd.CommandTimeout = m_commandTimeout;

                for (int i = 0; i < arrayParams.Count; i++)
                    cmd.Parameters.Add(arrayParams[i]);
                //
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                DataSet dst = new DataSet();
                adap.Fill(dst);

                cmd.Dispose();

                return dst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
