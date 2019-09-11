
using Microsoft.ApplicationBlocks.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;


namespace InvTracker.WebUpload.Common
{
    public class ADOContext
    {
        public string _ConnectionString;
        public ADOContext(string ConnectionString)
        {
            _ConnectionString = ConnectionString;
        }

        public List<DbParameter> OutParameters { get; private set; }
        public string readConfig()
        {
            string _connString = "";
            try
            {
                _connString = _ConnectionString;

            }
            catch (Exception ex) { throw ex; }
            return _connString;
        }
        protected bool OpenConnection(SqlConnection con)
        {
            try
            {
                // SqlConnection con = new SqlConnection(readConfig());
                if (con == null)
                    return false;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    return true;
                }
            }
            catch (Exception ex) { throw ex; }
            return false;
        }
        protected bool CloseConnection(SqlConnection con)
        {
            try
            {
                //SqlConnection con = new SqlConnection(readConfig());
                if (con == null)
                    return false;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    return true;
                }
            }
            catch (Exception ex) { throw ex; }
            return false;
        }
        public DataTable ExecSQL(string sqlStatement)
        {
            SqlConnection con = new SqlConnection(readConfig());
            DataTable dt = new DataTable();
            if (con == null)
                return null;
            try
            {
                if (con.State == ConnectionState.Closed)
                {

                    con.Open();
                    DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlStatement);
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }

                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
            return dt;
        }
        public DataTable ExecSQLProc(string spName, SqlParameter[] param)
        {
            SqlConnection connection = null;
            DataTable dt = new DataTable();
            try
            {
                connection = new SqlConnection(readConfig());
                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = spName,
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddRange(param.HandelNullParam());
                command.CommandTimeout = 0;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
                return dt;
            }
            catch (SqlException ex)
            {
                throw ex;
                //if (ex.Message.Split('\n').Length > 1)
                //    dt.Columns.Add(ex.Message.Split('\n')[1], this.GetType());
                //else
                //    dt.Columns.Add(ex.Message, this.GetType());
            }
            catch (Exception ex)
            {
                throw ex;
                // throw;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }

        }
        public DataTable ExecSQLProc(string spName, Object obj)
        {
            SqlConnection connection = null;
            DataTable dt = new DataTable();
            try
            {
                connection = new SqlConnection(readConfig());
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = spName;
                command.CommandType = CommandType.StoredProcedure;
                Type classType = obj.GetType();
                List<PropertyInfo> propertyList = classType.GetProperties().ToList();
                if (propertyList.Count < 1)
                {
                    return new DataTable();
                }
                foreach (PropertyInfo property in propertyList)
                {
                    string paramname = "@" + property.Name;
                    object paramvalue = null;
                    paramvalue = property.GetValue(obj);
                    if (paramvalue != null)
                    {
                        command.Parameters.AddWithValue(paramname, paramvalue);
                    }
                }
                command.CommandTimeout = 0;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }

        }
        public DataTable ExecSQLProc(string spName)
        {
            SqlConnection connection = null;
            DataTable dt = new DataTable();
            try
            {
                connection = new SqlConnection(readConfig());
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = spName;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 0;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }

        }
        public DataTable ExecSQLPro_withCommandTimeout(string spName, SqlParameter[] param, int CommandTimeout)
        {
            SqlConnection connection = null;
            DataTable dt = new DataTable();
            try
            {
                connection = new SqlConnection(readConfig());
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = spName;
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(param);
                command.CommandTimeout = CommandTimeout;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
                return dt;
            }
            catch (SqlException ex)
            {
                throw ex;
                //if (ex.Message.Split('\n').Length > 1)
                //    dt.Columns.Add(ex.Message.Split('\n')[1], this.GetType());
                //else
                //    dt.Columns.Add(ex.Message, this.GetType());
            }
            catch (Exception ex)
            {
                throw ex;
                // throw;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }

        }
        public DataTable GetDataByCmdText(string commandText)
        {
            SqlConnection connection = null;
            DataTable dt = new DataTable();
            try
            {
                connection = new SqlConnection(readConfig());
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = commandText;
                command.CommandType = CommandType.Text;
                command.CommandTimeout = 0;

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }

        }
        public DataTable GetDataByCmdText(string commandText, SqlParameter[] param)
        {
            SqlConnection connection = null;
            DataTable dt = new DataTable();
            try
            {
                connection = new SqlConnection(readConfig());
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = commandText;
                command.CommandType = CommandType.Text;
                command.CommandTimeout = 0;
                command.Parameters.AddRange(param);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }

        }
        public DataSet ExecDsSQLProc(string spName, SqlParameter[] param)
        {
            SqlConnection connection = null;
            DataSet ds = new DataSet();
            try
            {
                connection = new SqlConnection(readConfig());
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = spName;
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(param);
                command.CommandTimeout = 0;
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(ds);
                return ds;
            }
            catch (SqlException ex)
            {
                throw ex;
                //if (ex.Message.Split('\n').Length > 1)
                //    dt.Tables[0].Columns.Add(ex.Message.Split('\n')[1], this.GetType());
                //else
                //    dt.Tables[0].Columns.Add(ex.Message, this.GetType());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }


        }
        public DataTable GetData(SqlCommand cmd)
        {
            SqlConnection con = null;
            DataTable dt = new DataTable();
            try
            {
                con = new SqlConnection(readConfig());
                SqlDataAdapter sda = new SqlDataAdapter();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
            }

            catch (Exception ex) { throw ex; }
            finally { con.Close(); }
            return dt;
        }
        public string ExecuteNonQuery(string spName, SqlParameter[] param)
        {
            string retValue = "";
            SqlConnection connection = new SqlConnection(readConfig());
            try
            {

                OpenConnection(connection);
                SqlCommand cmdGetGlobalSiteType = new SqlCommand(spName, connection);
                cmdGetGlobalSiteType.CommandType = CommandType.StoredProcedure;
                cmdGetGlobalSiteType.Parameters.AddRange(param);

                SqlParameter prmSiteTypeName = new SqlParameter("@ReturnValue", SqlDbType.VarChar);
                prmSiteTypeName.Direction = ParameterDirection.ReturnValue;
                cmdGetGlobalSiteType.Parameters.Add(prmSiteTypeName);

                cmdGetGlobalSiteType.ExecuteNonQuery();
                if (Convert.ToString(prmSiteTypeName.Value) != "")
                    retValue = Convert.ToString(prmSiteTypeName.Value);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { CloseConnection(connection); }
            return retValue;


        }
        public int ExecuteNonQuery(string spName, SqlParameter[] param, int option)
        {
            int retvalue = 0;
            SqlConnection connection = new SqlConnection(readConfig());
            try
            {

                OpenConnection(connection);
                SqlCommand cmdGetGlobalSiteType = new SqlCommand(spName, connection);
                cmdGetGlobalSiteType.CommandType = CommandType.StoredProcedure;
                cmdGetGlobalSiteType.Parameters.AddRange(param);

                SqlParameter prmSiteTypeName = new SqlParameter("@ReturnValue", SqlDbType.VarChar);
                prmSiteTypeName.Direction = ParameterDirection.ReturnValue;
                cmdGetGlobalSiteType.Parameters.Add(prmSiteTypeName);

                retvalue = cmdGetGlobalSiteType.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { CloseConnection(connection); }
            return retvalue;


        }



    }


}


public class DbParameter
{
    public string Name { get; set; }
    public ParameterDirection Direction { get; set; }
    public object Value { get; set; }

    public DbParameter(string paramName, ParameterDirection paramDirection, object paramValue)
    {
        Name = paramName;
        Direction = paramDirection;
        Value = paramValue;
    }
}
public enum ExecuteType
{
    ExecuteReader,
    ExecuteNonQuery,
    ExecuteScalar
};

public static class ClassExtensions
{
    public static SqlParameter[] HandelNullParam(this SqlParameter[] param)
    {
        foreach (var item in param)
        {
            if (item.Value == null)
            {
                item.Value = DBNull.Value;
            }
        }
        return param;
    }

    public static SelectList ToSelectList(this DataTable table, string selectedvalue = "", bool IsWithSelect = true)
    {
        string valueField = "Value";
        string textField = "Text";
        List<SelectListItem> list = new List<SelectListItem>();
        if (IsWithSelect)
        {
            list.Add(new SelectListItem() { Text = "  Select  ", Value = "" });
        }
        foreach (DataRow row in table.Rows)
        {
            list.Add(new SelectListItem()
            {
                Text = row[textField].ToString(),
                Value = row[valueField].ToString(),
                Selected = (row[valueField].ToString() == selectedvalue)
            });
        }

        return new SelectList(list, "Value", "Text", selectedvalue);
    }
}