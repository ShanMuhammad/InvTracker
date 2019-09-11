using InvTracker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvTracker.BAL.Interfaces
{
    public interface IProductMasterBAL
    {
        void SaveProductMaster(ProductMasterModel model);
        BaseModel GetProductMasters(ProductMasterModel model);
    }
}
