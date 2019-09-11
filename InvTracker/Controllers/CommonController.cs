using InvTracker.BAL.Implementations;
using InvTracker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InvTracker.Controllers
{
    public class CommonController : ApiController
    {
        

        BindDropDownBAL _dropDown;
        public CommonController()
        {
            _dropDown = new BindDropDownBAL();
        }
        [HttpPost]
        public List<DropDownModel> BindDropDown(DropDownModel model)
        {
            List<DropDownModel> lstResult = new List<DropDownModel>();
            _dropDown.BindDropDown(model);
            return model.listDropdown;
        }


    }
}
