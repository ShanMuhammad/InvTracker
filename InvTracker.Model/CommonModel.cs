using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvTracker.Model
{
    public class DropDownModel
    {
        public DropDownModel()
        {
            listDropdown = new List<DropDownModel>();
        }
        public string label { get; set; }
        public string value { get; set; }
        public string Flag { get; set; }
        public string Param1 { get; set; }
        public string Param2 { get; set; }
        public string Param3 { get; set; }
        public List<DropDownModel> listDropdown { get; set; }
    }

     
}
