using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Collections;
using System.Data.Common;
using System.Reflection;
using System.Data;
using BMS.Utils;
using BMS.Exceptions;
using System.Windows.Forms;
using Dapper;

namespace BMS
{
    public class SQLHelper<T> where T : class, new()
    {
        static string ConnectionString = Global.ConnectionString;
        public static int Timeout = 2000;
        static DateTime _minDate = new DateTime(1900, 01, 01);

        //public static T ProcedureToModel(string procedureName, string[] paramName, object[] paramValue)
        //{
        //    T model = new T();
        //    SqlConnection mySqlConnection = new SqlConnection(connectionString);
        //    SqlParameter sqlParam;
        //    mySqlConnection.Open();

        //    try
        //    {
        //        SqlCommand mySqlCommand = new SqlCommand(procedureName, mySqlConnection);
        //        mySqlCommand.CommandType = CommandType.StoredProcedure;
        //        if (paramName != null)
        //        {
        //            for (int i = 0; i < paramName.Length; i++)
        //            {
        //                sqlParam = new SqlParameter(paramName[i], paramValue[i]);
        //                mySqlCommand.Parameters.Add(sqlParam);
        //            }
        //        }
        //        SqlDataReader reader = mySqlCommand.ExecuteReader();
        //        model = reader.MapToSingle<T>();
        //    }
        //    catch (SqlException e)
        //    {
        //        throw new Exception(e.ToString());
        //    }
        //    finally
        //    {
        //        mySqlConnection.Close();
        //    }

        //    return model;
        //}
        //public static List<T> ProcedureToList( string procedureName, string[] paramName, object[] paramValue)
        //{
        //    List<T> lst = new List<T>();
        //    SqlConnection mySqlConnection = new SqlConnection(connectionString);
        //    SqlParameter sqlParam;
        //    mySqlConnection.Open();

        //    try
        //    {
        //        SqlCommand mySqlCommand = new SqlCommand(procedureName, mySqlConnection);
        //        mySqlCommand.CommandType = CommandType.StoredProcedure;
        //        if (paramName != null)
        //        {
        //            for (int i = 0; i < paramName.Length; i++)
        //            {
        //                sqlParam = new SqlParameter(paramName[i], paramValue[i]);
        //                mySqlCommand.Parameters.Add(sqlParam);
        //            }
        //        }
        //        SqlDataReader reader = mySqlCommand.ExecuteReader();
        //        lst = reader.MapToList<T>();
        //    }
        //    catch (SqlException e)
        //    {
        //        throw new Exception(e.ToString());
        //    }
        //    finally
        //    {
        //        mySqlConnection.Close();
        //    }

        //    return lst;
        //}
        //public static T SqlToModel(string sql)
        //{
        //    T model = new T();
        //    SqlConnection mySqlConnection = new SqlConnection(connectionString);
        //    mySqlConnection.Open();
        //    try
        //    {
        //        SqlCommand mySqlCommand = new SqlCommand(sql, mySqlConnection);
        //        mySqlCommand.CommandType = CommandType.Text;
        //        SqlDataReader reader = mySqlCommand.ExecuteReader();
        //        model = reader.MapToSingle<T>();
        //    }
        //    catch (SqlException e)
        //    {
        //        throw new Exception(e.ToString());
        //    }
        //    finally
        //    {
        //        mySqlConnection.Close();
        //    }

        //    return model;
        //}
        //public static List<T> SqlToList(string sql)
        //{
        //    List<T> lst = new List<T>();
        //    SqlConnection mySqlConnection = new SqlConnection(connectionString);
        //    mySqlConnection.Open();
        //    try
        //    {
        //        SqlCommand mySqlCommand = new SqlCommand(sql, mySqlConnection);
        //        mySqlCommand.CommandType = CommandType.Text;
        //        SqlDataReader reader = mySqlCommand.ExecuteReader();
        //        lst = reader.MapToList<T>();
        //    }
        //    catch (SqlException e)
        //    {
        //        throw new Exception(e.ToString());
        //    }
        //    finally
        //    {
        //        mySqlConnection.Close();
        //    }

        //    return lst;
        //}
        //public static List<T> FindByExpression(Expression exp)
        //{
        //    SqlConnection conn = new SqlConnection(connectionString);
        //    List<T> lst = new List<T>();
        //    T model = new T();
        //    Type type = model.GetType();
        //    string tableName = type.Name.StartsWith("Model") ? type.Name : type.Name.Replace("Model", "");
        //    try
        //    {
        //        string sql = DBUtils.SQLSelect(tableName, exp);
        //        SqlCommand cmd = new SqlCommand(sql, conn);
        //        ////cmd.CommandTimeout = Timeout;
        //        cmd.CommandType = CommandType.Text;
        //        conn.Open();
        //        SqlDataReader reader = cmd.ExecuteReader();
        //        lst = reader.MapToList<T>();
        //        reader.Close();
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }

        //    return lst;
        //}

        public static bool CheckExist(string field, Int64 value)
        {
            SqlConnection conn = new SqlConnection(Global.ConnectionString);
            T model = new T();
            Type type = model.GetType();
            string tableName = type.Name.StartsWith("Model") ? type.Name : type.Name.Replace("Model", "");
            string sql = string.Format("SELECT TOP 1 {0} FROM {1} WITH (NOLOCK) WHERE {0} = {2}", field, tableName, value);
            //log.Debug(sql);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 6000;
            SqlDataReader reader = null;
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return reader.HasRows;
            }
            catch (Exception ex)
            {
                throw new FacadeException(ex);
            }
            finally
            {
                conn.Close();
            }
        }
        public T ProcedureToModel(string procedureName, string[] paramName, object[] paramValue)
        {
            T model = new T();
            SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand(procedureName, conn);
                cmd.CommandTimeout = Timeout;
                cmd.CommandType = CommandType.StoredProcedure;
                if (paramName != null)
                {
                    for (int i = 0; i < paramName.Length; i++)
                    {
                        SqlParameter sqlParam = new SqlParameter(paramName[i], paramValue[i]);
                        cmd.Parameters.Add(sqlParam);
                    }
                }

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                model = reader.MapToSingle<T>();
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
            finally
            {
                conn.Close();
            }

            return model;
        }
        public static List<T> ProcedureToList(string procedureName, string[] paramName, object[] paramValue)
        {
            List<T> lst = new List<T>();
            SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand(procedureName, conn);
                cmd.CommandTimeout = Timeout;
                cmd.CommandType = CommandType.StoredProcedure;
                if (paramName != null)
                {
                    for (int i = 0; i < paramName.Length; i++)
                    {
                        SqlParameter sqlParam = new SqlParameter(paramName[i], paramValue[i]);
                        cmd.Parameters.Add(sqlParam);
                    }
                }

                conn.Open();


                SqlDataReader reader = cmd.ExecuteReader();

                lst = reader.MapToList<T>();
            }
            catch (Exception e)
            {
                throw new Exception($"{e.Message}\r\n{procedureName}\r\n{string.Join(",", paramName)}\r\n{string.Join(",", paramValue)}");
            }
            finally
            {
                conn.Close();
            }

            return lst;
        }
        public static DataTable GetDataTableFromSP(string procedureName, SqlParameter mySqlParameter, string nameSetToTable)
        {
            DataTable table = new DataTable();
            SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
                conn.Open();

                SqlCommand mySqlCommand = new SqlCommand(procedureName, conn);
                mySqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter mySqlDataAdapter = new SqlDataAdapter(mySqlCommand);

                DataSet myDataSet = new DataSet();
                if (mySqlParameter != null)
                    mySqlCommand.Parameters.Add(mySqlParameter);

                mySqlDataAdapter.Fill(myDataSet, nameSetToTable);
                table = myDataSet.Tables[nameSetToTable];
            }
            catch (SqlException e)
            {
                throw new Exception(e.ToString());
            }
            finally
            {
                conn.Close();
            }
            return table;
        }
        public static DataTable LoadDataFromSP(string procedureName, string[] paramName, object[] paramValue)
        {
            DataTable table = new DataTable();
            SqlConnection mySqlConnection = new SqlConnection(ConnectionString);
            SqlParameter sqlParam;
            mySqlConnection.Open();

            try
            {
                SqlCommand mySqlCommand = new SqlCommand(procedureName, mySqlConnection);
                mySqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter mySqlDataAdapter = new SqlDataAdapter(mySqlCommand);

                DataSet myDataSet = new DataSet();
                if (paramName != null)
                {
                    for (int i = 0; i < paramName.Length; i++)
                    {
                        sqlParam = new SqlParameter(paramName[i], paramValue[i]);
                        mySqlCommand.Parameters.Add(sqlParam);
                    }
                }

                mySqlDataAdapter.Fill(myDataSet);

                table = myDataSet.Tables[0];
            }
            catch (SqlException e)
            {
                throw new Exception(e.ToString());
            }
            finally
            {
                mySqlConnection.Close();
            }

            return table;
        }
        public static DataTable GetTableData(string sqlQuery)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                SqlConnection con = new SqlConnection(ConnectionString);
                SqlDataAdapter da = new SqlDataAdapter(sqlQuery, con);

                con.Open();
                da.Fill(ds);
                dt = ds.Tables[0];
                con.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return dt;
        }
        public static DataSet GetDataSetFromSP(string procedureName, string[] paramName, object[] paramValue)
        {
            DataSet myDataSet = new DataSet();
            SqlConnection mySqlConnection = new SqlConnection(ConnectionString);
            SqlParameter sqlParam;
            try
            {
                mySqlConnection.Open();

                SqlCommand mySqlCommand = new SqlCommand(procedureName, mySqlConnection);
                mySqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter mySqlDataAdapter = new SqlDataAdapter(mySqlCommand);

                if (paramName != null)
                {
                    for (int i = 0; i < paramName.Length; i++)
                    {
                        sqlParam = new SqlParameter(paramName[i], paramValue[i]);
                        mySqlCommand.Parameters.Add(sqlParam);
                    }
                }
                mySqlDataAdapter.Fill(myDataSet);
            }
            catch (SqlException e)
            {
                throw new Exception(e.ToString());
            }
            finally
            {
                mySqlConnection.Close();
            }
            return myDataSet;
        }
        public static void ExcuteProcedure(string storeProcedureName, string[] paramName, object[] paramValue)
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand(storeProcedureName, cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                SqlParameter sqlParam;
                cn.Open();
                if (paramName != null)
                {
                    for (int i = 0; i < paramName.Length; i++)
                    {
                        sqlParam = new SqlParameter(paramName[i], paramValue[i]);
                        cmd.Parameters.Add(sqlParam);
                    }
                }
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                cn.Close();
            }

        }
        public static T SqlToModel(string sql)
        {
            T model = new T();
            SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = Timeout;
                cmd.CommandType = CommandType.Text;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                model = reader.MapToSingle<T>();
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
            finally
            {
                conn.Close();
            }

            return model;
        }
        public static T FindByID(Int64 id)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            T model = new T();
            Type type = model.GetType();
            string tableName = type.Name.StartsWith("Model") ? type.Name : type.Name.Replace("Model", "");
            try
            {
                string sql = string.Format("SELECT top 1 * FROM [{0}] with (nolock) WHERE ID = {1}", tableName, id);
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = Timeout;
                cmd.CommandType = CommandType.Text;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                model = reader.MapToSingle<T>();
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
            finally
            {
                conn.Close();
            }
            return model;
        }
        public static List<T> FindAll()
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            List<T> lst = new List<T>();
            T model = new T();
            Type type = model.GetType();
            string tableName = type.Name.StartsWith("Model") ? type.Name : type.Name.Replace("Model", "");
            try
            {
                string sql = string.Format("SELECT * FROM [{0}] with (nolock)", tableName);
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = Timeout;
                cmd.CommandType = CommandType.Text;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                lst = reader.MapToList<T>();
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
            finally
            {
                conn.Close();
            }

            return lst;
        }
        public static List<T> FindByAttribute(string fieldName, object fieldValue)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            List<T> lst = new List<T>();
            T model = new T();
            Type type = model.GetType();
            string tableName = type.Name.StartsWith("Model") ? type.Name : type.Name.Replace("Model", "");
            try
            {
                string sql = DBUtils.SQLSelect(tableName, fieldName, fieldValue);
                //string sql = $"SELECT * FROM {tableName} with (nolock)  WHERE {fieldName} = {fieldValue}";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = Timeout;
                cmd.CommandType = CommandType.Text;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                lst = reader.MapToList<T>();
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
            finally
            {
                conn.Close();
            }

            return lst;
        }
        public static List<T> SqlToList(string sql)
        {
            List<T> lst = new List<T>();
            SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = Timeout;
                cmd.CommandType = CommandType.Text;

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                lst = reader.MapToList<T>();
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
            finally
            {
                conn.Close();
            }

            return lst;
        }
        public static List<T> FindByExpression(Expression exp)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            List<T> lst = new List<T>();
            T model = new T();
            Type type = model.GetType();
            string tableName = type.Name.StartsWith("Model") ? type.Name : type.Name.Replace("Model", "");
            try
            {
                string sql = DBUtils.SQLSelect(tableName, exp);
                SqlCommand cmd = new SqlCommand(sql, conn);
                ////cmd.CommandTimeout = Timeout;
                cmd.CommandType = CommandType.Text;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                lst = reader.MapToList<T>();
                reader.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conn.Close();
            }

            return lst;
        }
        public static string SQLInsert(T model)
        {
            Type type = model.GetType();
            string tableName = type.Name.StartsWith("Model") ? type.Name : type.Name.Replace("Model", "");

            string Insert = "insert into " + tableName + " (";
            PropertyInfo[] pis = type.GetProperties();

            for (int i = 0; i < pis.Length; i++)
            {
                if (!pis[i].Name.Equals("ID"))
                {
                    Insert = Insert + pis[i].Name;
                    Insert = Insert + ",";
                }
            }
            Insert = Insert.Substring(0, Insert.Length - 1);
            Insert = Insert + ") values (";
            for (int i = 0; i < pis.Length; i++)
            {
                if (!pis[i].Name.Equals("ID"))
                {
                    Insert = Insert + "@";
                    Insert = Insert + pis[i].Name;
                    Insert = Insert + ",";
                }
            }
            Insert = Insert.Substring(0, Insert.Length - 1);
            Insert = Insert + ") Select Scope_Identity()";
            return Insert;
        }
        public static string SQLUpdate(T model)
        {
            Type type = model.GetType();
            string tableName = type.Name.StartsWith("Model") ? type.Name : type.Name.Replace("Model", "");
            string Update = "UPDATE " + tableName + " SET ";
            PropertyInfo[] pis = type.GetProperties();

            for (int i = 0; i < pis.Length; i++)
            {
                if (!pis[i].Name.Equals("ID"))
                {
                    Update = Update + pis[i].Name;
                    Update = Update + "=@" + pis[i].Name;
                    Update = Update + ",";
                }
            }
            Update = Update.Substring(0, Update.Length - 1);
            Update = Update + " WHERE ID=" + type.GetProperty("ID").GetValue(model, null).ToString();

            return Update;
        }
        public static string SQLUpdate(T model, string field)
        {
            Type type = model.GetType();
            string tableName = type.Name.StartsWith("Model") ? type.Name : type.Name.Replace("Model", "");
            //tableName = tableName.Substring(0, tableName.Length - 5);

            string Update = "UPDATE " + tableName + " SET ";
            PropertyInfo[] pis = type.GetProperties();
            for (int i = 0; i < pis.Length; i++)
            {
                if (pis[i].Name.Equals(field))
                {
                    Update = Update + pis[i].Name;
                    Update = Update + "=@" + pis[i].Name;
                    Update = Update + ",";
                    break;
                }
            }
            Update = Update.Substring(0, Update.Length - 1);
            Update = Update + " WHERE ID=" + type.GetProperty("ID").GetValue(model, null).ToString();
            return Update;
        }
        public static string SQLDelete(T model)
        {
            Type type = model.GetType();
            string tableName = type.Name.StartsWith("Model") ? type.Name : type.Name.Replace("Model", "");
            PropertyInfo pi = type.GetProperty("ID");
            string Delete = "DELETE FROM " + tableName + " WHERE ID = " + pi.GetValue(model);
            return Delete;
        }

        public static ResultQuery Insert(T model)
        {
            ResultQuery r = new ResultQuery();
            r.TotalRow = 1;
            Type type = model.GetType();
            SqlConnection conn = new SqlConnection(ConnectionString);

            string fiedName = "";
            try
            {
                string sql = SQLInsert(model);
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandTimeout = Timeout;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;

                PropertyInfo[] propertiesName = type.GetProperties();
                for (int i = 0; i < propertiesName.Length; i++)
                {
                    fiedName = propertiesName[i].Name;
                    object value = propertiesName[i].GetValue(model, null);

                    if (!propertiesName[i].Name.Equals("ID") && !propertiesName[i].Name.Equals("iD"))
                    {
                        if (propertiesName[i].Name.ToLower().Equals("createdby") || propertiesName[i].Name.ToLower().Equals("updatedby"))
                        {
                            cmd.Parameters.Add("@" + propertiesName[i].Name, SqlDbType.NVarChar).Value = !String.IsNullOrEmpty(Global.AppUserName) ? Global.AppUserName : (value ?? "");
                        }
                        else if (propertiesName[i].Name.ToLower().Equals("createddate") || propertiesName[i].Name.ToLower().Equals("updateddate"))
                        {
                            cmd.Parameters.Add("@" + propertiesName[i].Name, SqlDbType.DateTime).Value = DateTime.Now;
                        }
                        else if (propertiesName[i].Name.ToLower().Equals("userinsertid") || propertiesName[i].Name.ToLower().Equals("userupdateid"))
                        {
                            cmd.Parameters.Add("@" + propertiesName[i].Name, SqlDbType.Int).Value = Global.UserID != 0 ? Global.UserID : (value ?? 0);
                        }
                        else if (value != null)
                        {
                            if (propertiesName[i].PropertyType.Equals(typeof(DateTime)))
                            {
                                if ((DateTime)value == DateTime.MinValue)
                                    value = DefValues.Sql_MinDate;
                            }
                            if (propertiesName[i].PropertyType.Name.Equals("Byte[]"))
                            {
                                cmd.Parameters.Add("@" + propertiesName[i].Name, SqlDbType.Image).Value = value;
                            }
                            else
                            {
                                cmd.Parameters.Add("@" + propertiesName[i].Name, DBUtils.ConvertToSQLType(propertiesName[i].PropertyType)).Value = value;
                            }
                        }
                        else
                        {
                            if (propertiesName[i].PropertyType.Equals(typeof(DateTime?)))
                            {
                                cmd.Parameters.Add("@" + propertiesName[i].Name, DBUtils.ConvertToSQLType(propertiesName[i].PropertyType)).Value = DBNull.Value;
                            }
                            else
                            {
                                //cmd.Parameters.Add("@" + propertiesName[i].Name, DBUtils.ConvertToSQLType(propertiesName[i].PropertyType)).Value = "";
                                cmd.Parameters.Add("@" + propertiesName[i].Name, DBUtils.ConvertToSQLType(propertiesName[i].PropertyType)).Value = DBUtils.ReturnValue(propertiesName[i].PropertyType);
                            }

                        }

                        //if (value != null)
                        //{
                        //    if (pis[i].PropertyType.Equals(typeof(DateTime)))
                        //    {
                        //        if ((DateTime)value == DateTime.MinValue)
                        //            value = _minDate;
                        //    }
                        //    else
                        //    {
                        //        cmd.Parameters.Add("@" + pis[i].Name, DBUtils.ConvertToSQLType(pis[i].PropertyType)).Value = value;
                        //    }
                        //}
                        //else
                        //{
                        //    if (pis[i].PropertyType.Equals(typeof(DateTime?)))
                        //    {
                        //        cmd.Parameters.Add("@" + pis[i].Name, DBUtils.ConvertToSQLType(pis[i].PropertyType)).Value = DBNull.Value;
                        //    }
                        //    else
                        //    {
                        //        cmd.Parameters.Add("@" + pis[i].Name, DBUtils.ConvertToSQLType(pis[i].PropertyType)).Value = "";
                        //    }

                        //}
                    }
                }

                conn.Open();

                r.ID = (int)(decimal)cmd.ExecuteScalar();
                r.IsSuccess = true;
            }
            catch (Exception ex)
            {
                r.IsSuccess = false;
                r.ErrorText = ex.ToString();
                throw new Exception(ex.Message + $"\nFied Name: {fiedName}");
            }
            finally
            {
                if (conn.State != ConnectionState.Closed) conn.Close();
                conn.Dispose();
            }
            return r;
        }
        public static ResultQuery Update(T model)
        {
            ResultQuery r = new ResultQuery();
            r.TotalRow = 0;
            Type type = model.GetType();
            SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
                string sql = SQLUpdate(model);
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandTimeout = Timeout;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;

                PropertyInfo[] propertiesName = type.GetProperties();
                for (int i = 0; i < propertiesName.Length; i++)
                {
                    SqlDbType dbType = DBUtils.ConvertToSQLType(propertiesName[i].PropertyType);
                    object value = propertiesName[i].GetValue(model, null);

                    if (propertiesName[i].Name.ToLower().Equals("updatedby"))
                    {
                        cmd.Parameters.Add("@" + propertiesName[i].Name, SqlDbType.NVarChar).Value = !String.IsNullOrEmpty(Global.AppUserName) ? Global.AppUserName : (value ?? "");
                    }
                    else if (propertiesName[i].Name.ToLower().Equals("updateddate"))
                    {
                        cmd.Parameters.Add("@" + propertiesName[i].Name, SqlDbType.DateTime).Value = DateTime.Now;
                    }
                    else if (propertiesName[i].Name.ToLower().Equals("userupdateid"))
                    {
                        cmd.Parameters.Add("@" + propertiesName[i].Name, SqlDbType.Int).Value = Global.UserID != 0 ? Global.UserID : (value ?? 0);
                    }
                    else if (value != null)
                    {
                        if (propertiesName[i].PropertyType.Equals(typeof(DateTime)))
                        {
                            if ((DateTime)value == DateTime.MinValue)
                                value = DefValues.Sql_MinDate;
                        }
                        if (propertiesName[i].PropertyType.Name.Equals("Byte[]"))
                        {
                            cmd.Parameters.Add("@" + propertiesName[i].Name, SqlDbType.Image).Value = value;
                        }
                        else
                        {
                            cmd.Parameters.Add("@" + propertiesName[i].Name, dbType).Value = value;
                        }
                    }
                    else
                    {
                        if (propertiesName[i].PropertyType.Equals(typeof(DateTime?)))
                        {
                            cmd.Parameters.Add("@" + propertiesName[i].Name, dbType).Value = DBNull.Value;
                        }
                        else cmd.Parameters.Add("@" + propertiesName[i].Name, dbType).Value = DBUtils.ReturnValue(propertiesName[i].PropertyType);
                    }

                    //if (value != null)
                    //{
                    //    if (pis[i].PropertyType.Equals(typeof(DateTime)))
                    //    {
                    //        if ((DateTime)value == DateTime.MinValue)
                    //            value = _minDate;
                    //    }
                    //    else
                    //    {
                    //        cmd.Parameters.Add("@" + pis[i].Name, dbType).Value = value;
                    //    }
                    //}
                    //else
                    //{
                    //    if (pis[i].PropertyType.Equals(typeof(DateTime?)))
                    //    {
                    //        cmd.Parameters.Add("@" + pis[i].Name, dbType).Value = DBNull.Value;
                    //    }
                    //    else
                    //        cmd.Parameters.Add("@" + pis[i].Name, dbType).Value = "";
                    //}
                }
                conn.Open();
                r.TotalRow = cmd.ExecuteNonQuery();
                r.IsSuccess = true;
            }
            catch (Exception ex)
            {
                r.IsSuccess = false;
                r.ErrorText = ex.ToString();
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn.State != ConnectionState.Closed) conn.Close();
                conn.Dispose();
            }
            return r;
        }
        public static ResultQuery Update(T model, string field)
        {
            ResultQuery r = new ResultQuery();
            r.TotalRow = 0;
            Type type = model.GetType();
            SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
                string sql = SQLUpdate(model, field);
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandTimeout = Timeout;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;

                PropertyInfo[] pis = type.GetProperties();
                for (int i = 0; i < pis.Length; i++)
                {
                    SqlDbType dbType = DBUtils.ConvertToSQLType(pis[i].PropertyType);
                    object value = pis[i].GetValue(model, null);
                    if (pis[i].Name.Equals(field))
                    {
                        if (value != null)
                        {
                            if (pis[i].PropertyType.Equals(typeof(DateTime)))
                            {
                                if ((DateTime)value == DateTime.MinValue)
                                    value = _minDate;
                            }
                            else
                            {
                                cmd.Parameters.Add("@" + pis[i].Name, dbType).Value = value;
                            }
                        }
                        else
                            cmd.Parameters.Add("@" + pis[i].Name, dbType).Value = "";
                        break;
                    }
                }
                conn.Open();
                r.TotalRow = cmd.ExecuteNonQuery();
                r.IsSuccess = true;
            }
            catch (Exception ex)
            {
                r.IsSuccess = false;
                r.ErrorText = ex.ToString();
            }
            finally
            {
                if (conn.State != ConnectionState.Closed) conn.Close();
                conn.Dispose();
            }
            return r;
        }
        public static ResultQuery Delete(T model)
        {
            ResultQuery r = new ResultQuery();
            r.TotalRow = 0;
            SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
                string sql = SQLDelete(model);
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = Timeout;
                cmd.Connection.Open();
                r.TotalRow = cmd.ExecuteNonQuery();
                r.IsSuccess = true;
            }
            catch (Exception ex)
            {
                r.IsSuccess = false;
                r.ErrorText = ex.ToString();
            }
            finally
            {
                if (conn.State != ConnectionState.Closed) conn.Close();
                conn.Dispose();
            }
            return r;
        }
        public static void DeleteModelByID(Int64 id)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            T model = new T();
            Type type = model.GetType();
            string tableName = type.Name.StartsWith("Model") ? type.Name : type.Name.Replace("Model", "");
            try
            {
                string sql = string.Format("DELETE FROM [{0}] WHERE ID = {1}", tableName, id);
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = Timeout;
                cmd.CommandType = CommandType.Text;
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
            finally
            {
                conn.Close();
            }
        }
        public static void DeleteByAttribute(string name, string value)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            T model = new T();
            Type type = model.GetType();
            string tableName = type.Name.StartsWith("Model") ? type.Name : type.Name.Replace("Model", "");
            string sql = DBUtils.SQLDelete(tableName, name, value);
            ExcuteNonQuerySQL(sql);
        }
        public static void DeleteByAttribute(string name, Int64 value)
        {
            T model = new T();
            Type type = model.GetType();
            string tableName = type.Name.StartsWith("Model") ? type.Name : type.Name.Replace("Model", "");
            string sql = DBUtils.SQLDelete(tableName, name, value);
            ExcuteNonQuerySQL(sql);
        }

        public static void DeleteListModel(List<T> models)
        {
            if (models == null || !models.Any())
            {
                throw new ArgumentException("The collection of models is null or empty.");
            }

            Type type = typeof(T);
            string tableName = type.Name.StartsWith("Model") ? type.Name : type.Name.Replace("Model", "");
            var ids = models.Select(model => type.GetProperty("ID").GetValue(model)).ToList();
            string sql = $"DELETE FROM {tableName} WHERE ID IN ({string.Join(", ", ids)})";
            ExcuteNonQuerySQL(sql);
        }
        public static int ExcuteNonQuerySQL(string sql)
        {
            SqlConnection conn = new SqlConnection(Global.ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 6000;
            try
            {
                cmd.Connection.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (SqlException se)
            {
                throw new FacadeException(se);
            }
            finally
            {
                conn.Close();
            }
        }


        public static ResultQuery UpdateFields(Dictionary<string, object> fieldValues, Expression exp)
        {
            ResultQuery r = new ResultQuery();
            r.TotalRow = 1;
            Type type = typeof(T);
            //string tableName = type.Name;
            string tableName = type.Name.StartsWith("Model") ? type.Name : type.Name.Replace("Model", "");

            string setClause = string.Join(", ", fieldValues.Keys.Select(key => $"{key} = @{key}"));

            string query = $"UPDATE {tableName} SET {setClause} WHERE {exp.ToString()}";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                foreach (var field in fieldValues)
                {

                    command.Parameters.AddWithValue($"@{field.Key}", field.Value == null ? DBNull.Value : field.Value);
                }

                try
                {
                    connection.Open();
                    r.TotalRow = command.ExecuteNonQuery();
                    r.IsSuccess = true;
                }
                catch (Exception ex)
                {
                    r.IsSuccess = false;
                    r.ErrorText = ex.ToString();
                }
            }
            return r;
        }

        public static ResultQuery UpdateFieldsByID(Dictionary<string, object> fieldValues, long ID)
        {
            ResultQuery r = new ResultQuery();
            r.TotalRow = 1;
            Type type = typeof(T);
            //string tableName = type.Name;
            string tableName = type.Name.StartsWith("Model") ? type.Name : type.Name.Replace("Model", "");

            string setClause = string.Join(", ", fieldValues.Keys.Select(key => $"{key} = @{key}"));

            string query = $"UPDATE {tableName} SET {setClause} WHERE ID = {ID}";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                foreach (var field in fieldValues)
                {
                    command.Parameters.AddWithValue($"@{field.Key}", field.Value == null ? DBNull.Value : field.Value);
                }

                try
                {
                    connection.Open();
                    r.TotalRow = command.ExecuteNonQuery();
                    r.IsSuccess = true;
                }
                catch (Exception ex)
                {
                    r.IsSuccess = false;
                    r.ErrorText = ex.ToString();
                    throw new Exception(ex.ToString());
                }
            }
            return r;
        }

        public static void AddNewDataQR(string fieldName, string value, string procedure, int wareHouseId, DataTable dt, DevExpress.XtraGrid.GridControl grd)
        {
            T model = new T();
            Type type = model.GetType();
            string tableName = !type.Name.EndsWith("Model") ? type.Name : type.Name.Substring(0, type.Name.Length - "Model".Length);

            var bill = FindByAttribute($"{fieldName}", value).FirstOrDefault();
            if (bill == null)
            {
                MessageBox.Show($"Không tồn tại phiếu có mã {value}", "Thông báo", MessageBoxButtons.OK);
                return;
            }

            int billTypeNew = tableName == "BillImport" ? 4 : (tableName == "BillImportTechnical" ? 5 : 0);

            if (billTypeNew != 0)
            {
                var ex = new Expression("BillTypeNew", billTypeNew);
                var ex2 = new Expression($"{fieldName}", value);
                var check = FindByExpression(ex2.And(ex)).FirstOrDefault();
                if (check != null)
                {
                    MessageBox.Show($"Bạn không thể thêm phiếu Yêu cầu nhập kho!", "Thông báo");
                    return;
                }
            }


            if (dt.Rows.Count > 0)
            {
                if (dt.Columns.Contains(fieldName))
                {
                    var dtContains = dt.Select($"{fieldName} = '{value}'");
                    if (dtContains.Length > 0)
                    {
                        MessageBox.Show($"Mã {value} đã tồn tại!", "Thông báo", MessageBoxButtons.OK);
                        return;
                    }
                }
            }

            DataTable dtGet = TextUtils.LoadDataFromSP($"{procedure}", "A", new string[] { "@FilterText", "@WareHouseId" }, new object[] { value, wareHouseId });
            if (dtGet.Rows.Count == 0)
            {
                MessageBox.Show($"Không có phiếu mã {value} trong kho", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            dt.Merge(dtGet);
            grd.DataSource = null;
            if (grd.InvokeRequired) grd.Invoke(new Action(() => { grd.DataSource = dt; }));
            else grd.DataSource = dt;
        }

        public static ResultQuery InsertRange(List<T> models, string schema = null)
        {
            ResultQuery r = new ResultQuery();
            r.TotalRow = 0;
            r.IsSuccess = true;
            SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
                conn.Open();
                foreach (var model in models)
                {
                    Type type = model.GetType();
                    string sql = SQLInsert(model);
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandTimeout = Timeout;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = sql;

                        PropertyInfo[] propertiesName = type.GetProperties();
                        foreach (var pi in propertiesName)
                        {
                            if (pi.Name.Equals("ID", StringComparison.OrdinalIgnoreCase))
                                continue;

                            SqlDbType dbType = DBUtils.ConvertToSQLType(pi.PropertyType);
                            object value = pi.GetValue(model, null);

                            if (pi.Name.ToLower().Equals("createdby"))
                            {
                                cmd.Parameters.Add("@" + pi.Name, SqlDbType.NVarChar).Value =
                                    !String.IsNullOrEmpty(Global.AppUserName) ? Global.AppUserName : (value ?? "");
                            }
                            else if (pi.Name.ToLower().Equals("createddate"))
                            {
                                cmd.Parameters.Add("@" + pi.Name, SqlDbType.DateTime).Value = DateTime.Now;
                            }
                            else if (pi.Name.ToLower().Equals("userinsertid"))
                            {
                                cmd.Parameters.Add("@" + pi.Name, SqlDbType.Int).Value =
                                    Global.UserID != 0 ? Global.UserID : (value ?? 0);
                            }
                            else if (value != null)
                            {
                                if (pi.PropertyType.Equals(typeof(DateTime)))
                                {
                                    if ((DateTime)value == DateTime.MinValue)
                                        value = DefValues.Sql_MinDate;
                                }
                                if (pi.PropertyType.Name.Equals("Byte[]"))
                                {
                                    cmd.Parameters.Add("@" + pi.Name, SqlDbType.Image).Value = value;
                                }
                                else
                                {
                                    cmd.Parameters.Add("@" + pi.Name, dbType).Value = value;
                                }
                            }
                            else
                            {
                                if (pi.PropertyType.Equals(typeof(DateTime?)))
                                {
                                    cmd.Parameters.Add("@" + pi.Name, dbType).Value = DBNull.Value;
                                }
                                else
                                {
                                    cmd.Parameters.Add("@" + pi.Name, dbType).Value = DBUtils.ReturnValue(pi.PropertyType);
                                }
                            }
                        }
                        r.ID = (int)(decimal)cmd.ExecuteScalar();
                        r.TotalRow += 1;
                    }
                }
            }
            catch (Exception ex)
            {
                r.IsSuccess = false;
                r.ErrorText = ex.ToString();
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn.State != ConnectionState.Closed) conn.Close();
                conn.Dispose();
            }
            return r;
        }


        public static ResultQuery UpdateRange(List<T> models, string schema = null)
        {
            ResultQuery r = new ResultQuery();
            r.TotalRow = 0;
            r.IsSuccess = true;
            SqlConnection conn = new SqlConnection(ConnectionString);

            try
            {
                conn.Open();
                foreach (var model in models)
                {
                    Type type = model.GetType();
                    string sql = SQLUpdate(model);
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandTimeout = Timeout;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = sql;

                        PropertyInfo[] propertiesName = type.GetProperties();
                        foreach (var pi in propertiesName)
                        {
                            if (pi.Name.Equals("ID", StringComparison.OrdinalIgnoreCase))
                                continue;

                            SqlDbType dbType = DBUtils.ConvertToSQLType(pi.PropertyType);
                            object value = pi.GetValue(model, null);

                            if (pi.Name.ToLower().Equals("updatedby"))
                            {
                                cmd.Parameters.Add("@" + pi.Name, SqlDbType.NVarChar).Value =
                                    !String.IsNullOrEmpty(Global.AppUserName) ? Global.AppUserName : (value ?? "");
                            }
                            else if (pi.Name.ToLower().Equals("updateddate"))
                            {
                                cmd.Parameters.Add("@" + pi.Name, SqlDbType.DateTime).Value = DateTime.Now;
                            }
                            else if (pi.Name.ToLower().Equals("userupdateid"))
                            {
                                cmd.Parameters.Add("@" + pi.Name, SqlDbType.Int).Value =
                                    Global.UserID != 0 ? Global.UserID : (value ?? 0);
                            }
                            else if (value != null)
                            {
                                if (pi.PropertyType.Equals(typeof(DateTime)))
                                {
                                    if ((DateTime)value == DateTime.MinValue)
                                        value = DefValues.Sql_MinDate;
                                }

                                if (pi.PropertyType.Name.Equals("Byte[]"))
                                {
                                    cmd.Parameters.Add("@" + pi.Name, SqlDbType.Image).Value = value;
                                }
                                else
                                {
                                    cmd.Parameters.Add("@" + pi.Name, dbType).Value = value;
                                }
                            }
                            else
                            {
                                if (pi.PropertyType.Equals(typeof(DateTime?)))
                                {
                                    cmd.Parameters.Add("@" + pi.Name, dbType).Value = DBNull.Value;
                                }
                                else
                                {
                                    cmd.Parameters.Add("@" + pi.Name, dbType).Value = DBUtils.ReturnValue(pi.PropertyType);
                                }
                            }
                        }

                        r.TotalRow += cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                r.IsSuccess = false;
                r.ErrorText = ex.ToString();
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn.State != ConnectionState.Closed) conn.Close();
                conn.Dispose();
            }

            return r;
        }

        public static DataTable GetProjectPartlistPurchaseRequest(
     //string connectionString,
     DateTime dateStart,
     DateTime dateEnd,
     int statusRequest,
     int projectId,
     string keyword,
     int supplierSaleId,
     int isApprovedTBP,
     int isApprovedBGD,
     int isCommercialProduct,
     int pokhId,
     int productRtcId,
     int isDeleted,
     int isTechBought,
     int isJobRequirement,
     int isRequestApproved,
     int jobRequirementId,
     int employeeId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                // Mở kết nối
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                // Tạo parameter object cho Dapper
                var parameters = new DynamicParameters();
                parameters.Add("@DateStart", dateStart);
                parameters.Add("@DateEnd", dateEnd);
                parameters.Add("@StatusRequest", statusRequest);
                parameters.Add("@ProjectID", projectId);
                parameters.Add("@Keyword", keyword ?? string.Empty);
                parameters.Add("@SupplierSaleID", supplierSaleId);
                parameters.Add("@IsApprovedTBP", isApprovedTBP);
                parameters.Add("@IsApprovedBGD", isApprovedBGD);
                parameters.Add("@IsCommercialProduct", isCommercialProduct);
                parameters.Add("@POKHID", pokhId);
                parameters.Add("@ProductRTCID", productRtcId);
                parameters.Add("@IsDeleted", isDeleted);
                parameters.Add("@IsTechBought", isTechBought);
                parameters.Add("@IsJobRequirement", isJobRequirement);
                parameters.Add("@IsRequestApproved", isRequestApproved);
                parameters.Add("@JobRequirementID", jobRequirementId);
                parameters.Add("@EmployeeID", employeeId);

                // Thực thi procedure bằng Dapper và trả kết quả về DataTable
                using (var reader = connection.ExecuteReader(
                    "dbo.spGetProjectPartlistPurchaseRequest_New_Khanh",
                    parameters,
                    commandType: CommandType.StoredProcedure))
                {
                    var dt = new DataTable();
                    dt.Load(reader);
                    return dt;
                }
            }
        }
    }

    public class ResultQuery
    {
        public int ID { get; set; }
        public int TotalRow { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorText { get; set; }
    }
}
