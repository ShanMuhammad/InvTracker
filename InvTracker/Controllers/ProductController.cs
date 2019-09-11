using InvTracker.BAL.Implementations;
using InvTracker.BAL.Interfaces;
using InvTracker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InvTracker.Controllers
{
    public class ProductController : ApiController
    {
        IProductMasterBAL objProductMasterBAL;
        public ProductController()
        {
            objProductMasterBAL = new ProductMasterBAL();
        }
        #region Product Master
        [HttpPost]
        public BaseModel SaveProductMaster(ProductMasterModel model)
        {
            BaseModel Response = new BaseModel();
            objProductMasterBAL.SaveProductMaster(model);
            Response.RequestEntityId = model.RequestEntityId;
            Response.RequestMessage = model.RequestMessage;
            return Response;
        }

        [HttpPost]
        public BaseModel GetProductMasters(ProductMasterModel model)
        {
            BaseModel Response = new BaseModel();
            Response = objProductMasterBAL.GetProductMasters(model);
            return Response;
        }
        #endregion
    }
}