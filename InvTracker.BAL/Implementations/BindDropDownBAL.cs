using InvTracker.BAL.Helper;
using InvTracker.DAL.Common;
using InvTracker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvTracker.BAL.Implementations
{
    
    public class BindDropDownBAL
    {
        
        public void BindDropDown(DropDownModel model)
        {
            model.listDropdown = BindDropDownDAL.BindDropdown(model).ToDropDownList();
        }
    }
}
