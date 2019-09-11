using InvTracker.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvTracker.DAL.Common
{

    public class BindDropDownDAL
    {
        ADOContext _dbContext = new ADOContext();

        public static DataTable BindDropdown(DropDownModel model)
        {
            DataTable dt = new DataTable();

            SqlParameter[] arrproc;
            string spName = "BindDropDown";
            arrproc = new SqlParameter[3];
            arrproc[0] = new SqlParameter("@Flag", model.Flag);
            arrproc[1] = new SqlParameter("@Param1", model.Param1);
            arrproc[2] = new SqlParameter("@Param2", model.Param2);

            ADOContext _dbContext = new ADOContext();
            dt = _dbContext.ExecSQLProc(spName, arrproc);
            return dt;
        }
    }
}
