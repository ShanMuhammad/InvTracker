using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Reflection;
using Microsoft.ApplicationBlocks.Data;
using System.Data;

namespace InvTracker.Repository.Common
{
   public class ADOContext
    {

        public List<DbParameter> OutParameters { get; private set; }
        public string readConfig()
        {
            string _connString = "";
            try
            {
                _connString = ConfigurationManager.ConnectionStrings["FortisDbContext"].ToString();

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
                command.Parameters.AddRange(param);
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
            return dt;
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
            return dt;
        }
        //======Create By Deepak For Return dataset start Here 23-11-2017=====
        public DataSet ExecDsSQLProc(string spName, SqlParameter[] param)
        {
            SqlConnection connection = null;
            DataSet dt = new DataSet();
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

                adapter.Fill(dt);
                return dt;
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
            return dt;
        }
        //===Create By Deepak For Return dataset End Here 23-11-2017
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
            return dt;
        }
        //public List<string> ExecSQLProcList(string spName,SqlParameter[] param)
        //{
        //    List<string> lstDDl = new List<string>();
        //    SqlConnection connection = null;
        //    try
        //    {
        //        connection = new SqlConnection(readConfig());
        //        SqlCommand command = new SqlCommand();
        //        command.Connection = connection;
        //        command.CommandText = spName;
        //        command.CommandType = CommandType.StoredProcedure;
        //        command.Parameters.AddRange(param);
        //        command.CommandTimeout = 0;
        //        using (SqlDataReader objReader = command.ExecuteReader())
        //        {
        //            if (objReader.HasRows)
        //            {
        //                while (objReader.Read())
        //                {
        //                    //I would also check for DB.Null here before reading the value.
        //                    string item = objReader.GetString(objReader.GetOrdinal("Column1"));
        //                    lstDDl.Add(item);
        //                }
        //            }
        //        }

        //        return lstDDl;
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {
        //        if (connection != null && connection.State == ConnectionState.Open)
        //            connection.Close();
        //    }
        //    return lstDDl;
        //}
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
            return dt;
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
            return dt;
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
            return dt;
        }

        public DataSet ExecSQLProcReturnDS(string spName, SqlParameter[] param)
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
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return ds;
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

public class DbManager
{
    private static string ConnectionString
    {
        get
        {
            string _connString = "";
            try
            {
                _connString = ConfigurationManager.AppSettings["FortisDbContext"].ToString();
            }
            catch (Exception ex) { throw ex; }
            return _connString;
        }
    }

    private SqlConnection Connection { get; set; }

    private SqlCommand Command { get; set; }

    public List<DbParameter> OutParameters { get; private set; }

    private void Open()
    {
        try
        {
            Connection = new SqlConnection(ConnectionString);

            Connection.Open();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void Close()
    {
        if (Connection != null)
        {
            Connection.Close();
        }
    }

    // executes stored procedure with DB parameteres if they are passed
    private object ExecuteProcedure(string procedureName, ExecuteType executeType, List<DbParameter> parameters)
    {
        object returnObject = null;

        if (Connection != null)
        {
            if (Connection.State == ConnectionState.Open)
            {
                Command = new SqlCommand(procedureName, Connection);
                Command.CommandType = CommandType.StoredProcedure;

                // pass stored procedure parameters to command
                if (parameters != null)
                {
                    Command.Parameters.Clear();

                    foreach (DbParameter dbParameter in parameters)
                    {
                        SqlParameter parameter = new SqlParameter();
                        parameter.ParameterName = "@" + dbParameter.Name;
                        parameter.Direction = dbParameter.Direction;
                        parameter.Value = dbParameter.Value;
                        Command.Parameters.Add(parameter);
                    }
                }

                switch (executeType)
                {
                    case ExecuteType.ExecuteReader:
                        returnObject = Command.ExecuteReader();
                        break;
                    case ExecuteType.ExecuteNonQuery:
                        returnObject = Command.ExecuteNonQuery();
                        break;
                    case ExecuteType.ExecuteScalar:
                        returnObject = Command.ExecuteScalar();
                        break;
                    default:
                        break;
                }
            }
        }

        return returnObject;
    }

    // updates output parameters from stored procedure
    private void UpdateOutParameters()
    {
        if (Command.Parameters.Count > 0)
        {
            OutParameters = new List<DbParameter>();
            OutParameters.Clear();

            for (int i = 0; i < Command.Parameters.Count; i++)
            {
                if (Command.Parameters[i].Direction == ParameterDirection.Output)
                {
                    OutParameters.Add(new DbParameter(Command.Parameters[i].ParameterName,
                                                      ParameterDirection.Output,
                                                      Command.Parameters[i].Value));
                }
            }
        }
    }

    // executes scalar query stored procedure without parameters
    public T ExecuteSingle<T>(string procedureName) where T : new()
    {
        return ExecuteSingle<T>(procedureName, null);
    }

    // executes scalar query stored procedure and maps result to single object
    public T ExecuteSingle<T>(string procedureName, List<DbParameter> parameters) where T : new()
    {
        Open();
        IDataReader reader = (IDataReader)ExecuteProcedure(procedureName, ExecuteType.ExecuteReader, parameters);
        T tempObject = new T();

        if (reader.Read())
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                PropertyInfo propertyInfo = typeof(T).GetProperty(reader.GetName(i));
                propertyInfo.SetValue(tempObject, reader.GetValue(i), null);
            }
        }

        reader.Close();

        UpdateOutParameters();

        Close();

        return tempObject;
    }

    // executes list query stored procedure without parameters
    public List<T> ExecuteList<T>(string procedureName) where T : new()
    {
        return ExecuteList<T>(procedureName, null);
    }

    // executes list query stored procedure and maps result generic list of objects
    public List<T> ExecuteList<T>(string procedureName, List<DbParameter> parameters) where T : new()
    {
        List<T> objects = new List<T>();

        Open();
        IDataReader reader = (IDataReader)ExecuteProcedure(procedureName, ExecuteType.ExecuteReader, parameters);

        while (reader.Read())
        {
            T tempObject = new T();

            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetValue(i) != DBNull.Value)
                {
                    PropertyInfo propertyInfo = typeof(T).GetProperty(reader.GetName(i));
                    propertyInfo.SetValue(tempObject, reader.GetValue(i), null);
                }
            }

            objects.Add(tempObject);
        }

        reader.Close();

        UpdateOutParameters();

        Close();

        return objects;
    }

    // executes non query stored procedure with parameters
    public int ExecuteNonQuery(string procedureName, List<DbParameter> parameters)
    {
        int returnValue;

        Open();

        returnValue = (int)ExecuteProcedure(procedureName, ExecuteType.ExecuteNonQuery, parameters);

        UpdateOutParameters();

        Close();

        return returnValue;
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