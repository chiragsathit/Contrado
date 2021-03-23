using Newtonsoft.Json;
using System;

namespace WebUIProduct.Models
{
    public class APICallProduct
    {
        public ProductData GetProductList()
        {
            ProductData objProductData = new ProductData();
            try
            {
                APICall.APIData objAPIResult = APICall.CallGETAPI(CommonConfig.GetAPIEndPoint + "Product");
                if (objAPIResult.isAPIResponse)
                {
                    objProductData = JsonConvert.DeserializeObject<ProductData>(objAPIResult.apiResponseString);
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

        public ProductDeleteData GetProductDelete(int product_id)
        {
            ProductDeleteData objProductData = new ProductDeleteData();
            try
            {
                APICall.APIData objAPIResult = APICall.CallDeleteAPI(CommonConfig.GetAPIEndPoint + "Product/"+ product_id);
                if (objAPIResult.isAPIResponse)
                {
                    objProductData = JsonConvert.DeserializeObject<ProductDeleteData>(objAPIResult.apiResponseString);
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

        public class ProductData
        {
            public int error_code { get; set; }
            public string message { get; set; }
            public Datum[] data { get; set; }
        }
        public class ProductDeleteData
        {
            public int error_code { get; set; }
            public string message { get; set; }
            public int RowEffect { get; set; }
        }

        public class Datum
        {
            public int productId { get; set; }
            public int prodCatId { get; set; }
            public string prodName { get; set; }
            public string prodDescription { get; set; }
            public string categoryName { get; set; }
        }

    }
}
