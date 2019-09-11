using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvTracker.Model
{
    public class BaseModel
    {
        public int UserId { get; set; }
        public string RequestEntityId { get; set; }
        public string RequestEntityCode { get; set; }
        public bool RequestIsSuccess { get; set; }
        public string RequestErrorCode { get; set; }
        public string RequestMessage { get; set; }
        public object DataResult { get; set; }
        public int TotalRecords { get; set; }
        public int PageNumber { get; set; }
        public int RecordSize { get; set; }


    }
}
