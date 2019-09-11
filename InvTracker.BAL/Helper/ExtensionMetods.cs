using InvTracker.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvTracker.BAL.Helper
{
    public static class ExtensionMetods
    {
        public static List<DropDownModel> ToDropDownList(this DataTable table, bool IsWithSelect = true)
        {
            List<DropDownModel> list = new List<DropDownModel>();
            if (IsWithSelect)
            {
                list.Add(new DropDownModel() { label = "Select", value = "" });
            }
            foreach (DataRow row in table.Rows)
            {
                list.Add(new DropDownModel()
                {
                    label = row["label"].ToString(),
                    value = row["value"].ToString(),
                });
            }
            return list;
        }
    }
}
