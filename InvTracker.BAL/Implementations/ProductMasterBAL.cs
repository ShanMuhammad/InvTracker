using InvTracker.BAL.Interfaces;
using InvTracker.DAL.Masters;
using InvTracker.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvTracker.Infrastructure;
using InvTracker.DAL.Common;

namespace InvTracker.BAL.Implementations
{
    public class ProductMasterBAL : IProductMasterBAL
    {
        ProductMasterDAL objProductMasterDAL = new ProductMasterDAL();

        public BaseModel GetProductMasters(ProductMasterModel model)
        {
            BaseModel objBaseModel = new BaseModel();
            objBaseModel.DataResult = objProductMasterDAL.GetProductMaster(model).ToList<ProductMasterModel>();
            return objBaseModel;
        }

        public void SaveProductMaster(ProductMasterModel model)
        {
            DataTable dt = objProductMasterDAL.SaveProductMaster(model);
            if (dt != null)
            {
                model.RequestEntityId = EncodeDecodeQueryString.EncodeQueryString(dt.Rows[0]["ProductMasterId"].ToString());
                model.RequestMessage = dt.Rows[0]["Message"].ToString();
            }
        }
    }
}
