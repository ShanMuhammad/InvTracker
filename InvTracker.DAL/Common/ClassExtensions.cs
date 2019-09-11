using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InvTracker.DAL.Common
{
    public static class ClassExtensions
    {

        /// <summary>
        /// Converts datatable to list<T> dynamically
        /// </summary>
        /// <typeparam name="T">Class name</typeparam>
        /// <param name="dataTable">data table to convert</param>
        /// <returns>List<T></returns>
        public static List<T> ToList<T>(this DataTable dataTable) where T : new()
        {
            var dataList = new List<T>();

            //Define what attributes to be read from the class
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

            //Read Attribute Names and Types
            var objFieldNames = typeof(T).GetProperties(flags).Cast<PropertyInfo>().
                Select(item => new
                {
                    Name = item.Name,
                    Type = Nullable.GetUnderlyingType(item.PropertyType) ?? item.PropertyType
                }).ToList();

            //Read Datatable column names and types
            var dtlFieldNames = dataTable.Columns.Cast<DataColumn>().
                Select(item => new
                {
                    Name = item.ColumnName,
                    Type = item.DataType
                }).ToList();

            foreach (DataRow dataRow in dataTable.AsEnumerable().ToList())
            {
                var classObj = new T();

                foreach (var dtField in dtlFieldNames)
                {
                    PropertyInfo propertyInfos = classObj.GetType().GetProperty(dtField.Name);

                    var field = objFieldNames.Find(x => x.Name == dtField.Name);

                    if (field != null)
                    {

                        if (propertyInfos.PropertyType == typeof(DateTime))
                        {
                            propertyInfos.SetValue
                            (classObj, convertToDateTime(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(DateTime?))
                        {
                            propertyInfos.SetValue(classObj, GetNullDateTimeIfDateISBlank(dataRow[dtField.Name]), null);
                            //(classObj, convertToDateTime(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(int))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToInt(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(int?))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToNullableInt(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(short))
                        {
                            propertyInfos.SetValue(classObj, ConvertToInt16(dataRow[dtField.Name]), null);
                            //(classObj, (short)(dataRow[dtField.Name]), null);

                        }
                        else if (propertyInfos.PropertyType == typeof(long))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToLong(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(decimal))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToDecimal(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType.Equals(typeof(Boolean)))
                        {
                            propertyInfos.SetValue(classObj, ConvertToBoolean(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType.Equals(typeof(byte[])))
                        {
                            //if((int)dataRow[dtField.Name].ReturnZeroIfNull()>0)
                            {
                                propertyInfos.SetValue(classObj, dataRow[dtField.Name].ReturnZeroIfNull(), null);
                            }

                            // propertyInfos.SetValue(classObj, dataRow[dtField.Name].ReturnZeroIfNull(), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(String))
                        {
                            if (dataRow[dtField.Name].GetType() == typeof(DateTime))
                            {
                                propertyInfos.SetValue
                                (classObj, ConvertToDateString(dataRow[dtField.Name]), null);
                            }
                            else
                            {
                                propertyInfos.SetValue
                                (classObj, ConvertToString(dataRow[dtField.Name]), null);
                            }
                        }
                    }
                }
                dataList.Add(classObj);
            }
            return dataList;
        }

        private static string ConvertToDateString(object date)
        {
            if (date == null)
                return string.Empty;

            return ConvertDate(Convert.ToDateTime(date));
        }
        private static string ConvertDate(this DateTime datetTime, bool excludeHoursAndMinutes = false)
        {
            if (datetTime != DateTime.MinValue)
            {
                if (excludeHoursAndMinutes)
                    return datetTime.ToString("yyyy-MM-dd");
                return datetTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            }
            return null;
        }
        private static string ConvertToString(object value)
        {
            return Convert.ToString(ReturnEmptyIfNull(value));
        }
        private static object ReturnEmptyIfNull(this object value)
        {
            if (value == DBNull.Value)
                return string.Empty;
            if (value == null)
                return string.Empty;
            return value;
        }

        private static int ConvertToInt(object value)
        {
            return Convert.ToInt32(ReturnZeroIfNull(value));
        }
        private static int? ConvertToNullableInt(object value)
        {
            int? result = null;
            if (value == DBNull.Value)
                result = (int?)null;
            else if (value == null)
                result = (int?)null;
            else
                result = Convert.ToInt32(value);
            return result;
        }
        private static object ReturnZeroIfNull(this object value)
        {
            if (value == DBNull.Value)
                return 0;
            if (value == null)
                return 0;
            return value;
        }
        private static long ConvertToLong(object value)
        {
            return Convert.ToInt64(ReturnZeroIfNull(value));
        }

        private static decimal ConvertToDecimal(object value)
        {
            return Convert.ToDecimal(ReturnZeroIfNull(value));
        }

        private static bool ConvertToBoolean(object value)
        {
            bool result = false;
            Boolean.TryParse(value.ToString(), out result);
            return result;
        }

        private static DateTime convertToDateTime(object date)
        {
            return Convert.ToDateTime(ReturnDateTimeMinIfNull(date));
        }
        public static object ReturnDateTimeMinIfNull(this object value)
        {
            if (value == DBNull.Value)
                return DateTime.MinValue;
            if (value == null)
                return DateTime.MinValue;
            return value;
        }
        public static Int16 ConvertToInt16(object value)
        {
            return Convert.ToInt16(ReturnZeroIfNull(value));
        }

        public static DateTime? GetNullDateTimeIfDateISBlank(object value)
        {
            DateTime? dt = null;
            if (value == DBNull.Value || value == null)
            {
                dt = null;
            }
            else
            {
                dt = (DateTime?)value;
            }
            return dt;
        }

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
    }
    public class CommonCls
    {
        public static DataTable ClassToDataTable<T>() where T : class
        {
            Type classType = typeof(T);

            List<PropertyInfo> propertyList = classType.GetProperties().ToList();
            if (propertyList.Count < 1)
            {
                return new DataTable();
            }

            string className = classType.UnderlyingSystemType.Name;
            DataTable result = new DataTable(className);

            foreach (PropertyInfo property in propertyList)
            {
                DataColumn col = new DataColumn();
                col.ColumnName = property.Name;

                Type dataType = property.PropertyType;

                if (IsNullable(dataType))
                {
                    if (dataType.IsGenericType)
                    {
                        dataType = dataType.GenericTypeArguments.FirstOrDefault();
                    }
                }
                else
                {   // True by default
                    col.AllowDBNull = false;
                }

                col.DataType = dataType;

                result.Columns.Add(col);
            }

            return result;
        }
        public static DataTable ClassListToDataTable<T>(List<T> ClassList) where T : class
        {
            DataTable result = ClassToDataTable<T>();

            if (result.Columns.Count < 1)
            {
                return new DataTable();
            }
            if (ClassList.Count < 1)
            {
                return result;
            }

            foreach (T item in ClassList)
            {
                ClassToDataRow(ref result, item);
            }

            return result;
        }

        public static void ClassToDataRow<T>(ref DataTable Table, T Data) where T : class
        {
            Type classType = typeof(T);
            string className = classType.UnderlyingSystemType.Name;

            // Checks that the table name matches the name of the class. 
            // There is not required, and it may be desirable to disable this check.
            // Comment this out or add a boolean to the parameters to disable this check.
            if (!Table.TableName.Equals(className))
            {
                return;
            }

            DataRow row = Table.NewRow();
            List<PropertyInfo> propertyList = classType.GetProperties().ToList();

            foreach (PropertyInfo prop in propertyList)
            {
                if (Table.Columns.Contains(prop.Name))
                {
                    if (Table.Columns[prop.Name] != null)
                    {
                        row[prop.Name] = prop.GetValue(Data, null);
                    }
                }
            }
            Table.Rows.Add(row);
        }

        public static bool IsNullable(Type Input)
        {
            if (!Input.IsValueType) return true; // Is a ref-type, such as a class
            if (Nullable.GetUnderlyingType(Input) != null) return true; // Nullable
            return false; // Must be a value-type
        }
        public static DataTable ClassToList<T>() where T : class
        {
            Type classType = typeof(T);

            List<PropertyInfo> propertyList = classType.GetProperties().ToList();
            if (propertyList.Count < 1)
            {
                return new DataTable();
            }

            string className = classType.UnderlyingSystemType.Name;
            DataTable result = new DataTable(className);

            foreach (PropertyInfo property in propertyList)
            {
                DataColumn col = new DataColumn();
                col.ColumnName = property.Name;

                Type dataType = property.PropertyType;

                if (IsNullable(dataType))
                {
                    if (dataType.IsGenericType)
                    {
                        dataType = dataType.GenericTypeArguments.FirstOrDefault();
                    }
                }
                else
                {   // True by default
                    col.AllowDBNull = false;
                }

                col.DataType = dataType;

                result.Columns.Add(col);
            }

            return result;
        }
    }
}

