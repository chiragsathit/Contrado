using System;
using Newtonsoft.Json;

namespace WebUIProduct.Models
{
    public class APICallProductCategory
    {
        public ProductCategoryData GetProductCategoryList()
        {
            ProductCategoryData objProductData = new ProductCategoryData();
            try
            {
                APICall.APIData objAPIResult = APICall.CallGETAPI(CommonConfig.GetAPIEndPoint + "ProductCategory");
                if (objAPIResult.isAPIResponse)
                {
                    objProductData = JsonConvert.DeserializeObject<ProductCategoryData>(objAPIResult.apiResponseString);
                    return objProductData;
                }
            }
            catch (Exception ex)
            {
                objProductData.error_code = 1;
                objProductData.message = ex.Message;
            }
            return objProductData;

        }

        public ProductCategoryDeleteData GetProductCategoryDelete(int category_id)
        {
            ProductCategoryDeleteData objProductData = new ProductCategoryDeleteData();
            try
            {
                APICall.APIData objAPIResult = APICall.CallDeleteAPI(CommonConfig.GetAPIEndPoint + "ProductCategory/" + category_id);
                if (objAPIResult.isAPIResponse)
                {
                    objProductData = JsonConvert.DeserializeObject<ProductCategoryDeleteData>(objAPIResult.apiResponseString);
                    return objProductData;
                }
            }
            catch (Exception ex)
            {
                objProductData.error_code = 1;
                objProductData.message = ex.Message;
            }
            return objProductData;
        }

        public class ProductCategoryData
        {
            public int error_code { get; set; }
            public string message { get; set; }
            public Datum[] data { get; set; }
        }
        public class ProductCategoryDeleteData
        {
            public int error_code { get; set; }
            public string message { get; set; }
            public int RowEffect { get; set; }
        }

        public class Datum
        {
            public int categoryId { get; set; }
            public string categoryName { get; set; }
        }

    }
}
