using InvTracker.WebUpload.Common;
using InvTracker.WebUpload.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace InvTracker.WebUpload.Repository
{
    public class CommonRepository
    {
        static ADOContext _dbContext;
        string ConnectionString;
        public IConfiguration Configuration { get; }
        public CommonRepository(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionString = Configuration.GetConnectionString("InvTrackerContext");
            _dbContext = new ADOContext(ConnectionString);

        }
        public DataTable BindDropdown(string Flag, string Param1 = null, string Param2 = null)
        {
            DataTable dt = new DataTable();

            SqlParameter[] arrproc;
            string spName = "BindDropDown";
            arrproc = new SqlParameter[3];
            arrproc[0] = new SqlParameter("@Flag", Flag);
            arrproc[1] = new SqlParameter("@Param1", Param1);
            arrproc[2] = new SqlParameter("@Param2", Param2);

            dt = _dbContext.ExecSQLProc(spName, arrproc);
            return dt;
        }
    }
}
