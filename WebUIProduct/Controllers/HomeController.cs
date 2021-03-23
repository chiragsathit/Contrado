using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using WebUIProduct.Models;

namespace WebUIProduct.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            APICallProduct obj = new APICallProduct();
            APICallProduct.ProductData objData = obj.GetProductList();
            if (objData.error_code == 0)
            {
                ViewBag.MainData = objData.data;
            }
            if (objData.error_code == 1)
            {
                ViewBag.Message = objData.message;
            }
            return View();
        }

        public IActionResult DeleteCategory()
        {
            int category_id = 0;
            if (Request.Form.ContainsKey("hidCategoryId"))
            {
                category_id = Convert.ToInt32(Request.Form["hidCategoryId"].ToString());
            }
            APICallProductCategory obj = new APICallProductCategory();
            string message = string.Empty;
            if (category_id < 0)
            {
                message = "Invalid Category ID";
            }
            if (category_id > 0)
            {
                APICallProductCategory.ProductCategoryDeleteData objDeleteData = obj.GetProductCategoryDelete(category_id);
                message = objDeleteData.message;
            }
            TempData.Add("Message", message);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Delete()
        {
            int product_id = 0;
            if (Request.Form.ContainsKey("hidProductid"))
            {
                product_id = Convert.ToInt32(Request.Form["hidProductid"].ToString());
            }
            APICallProduct obj = new APICallProduct();
            string message = string.Empty;
            if (product_id < 0)
            {
                message = "Invalid Product ID";
            }
            if (product_id > 0)
            {
                APICallProduct.ProductDeleteData objDeleteData = obj.GetProductDelete(product_id);
                message = objDeleteData.message;
            }
            //APICallProduct.ProductData objData = obj.GetProductList();
            //if (objData.error_code == 0)
            //{
            //    ViewBag.MainData = objData.data;
            //}
            //if (objData.error_code == 1)
            //{
            //    ViewBag.Message = objData.message;
            //}
            TempData.Add("Message", message);
            return RedirectToAction("Index", "Home");
            //return View("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public string getProductCategory()
        {
            APICall.APIData objAPIResult = APICall.CallGETAPI(CommonConfig.GetAPIEndPoint + "ProductCategory");
            return objAPIResult.apiResponseString;
        }

        public ActionResult AddCategory()
        {
            string Action = string.Empty;
            if (Request.Form.ContainsKey("hidActionCategorySaveUpdate"))
            {
                Action = Request.Form["hidActionCategorySaveUpdate"].ToString();
            }
            string CategoryName = string.Empty;
            if (Request.Form.ContainsKey("txtCategoryName"))
            {
                CategoryName = Request.Form["txtCategoryName"].ToString();
            }
            APICall.CommomResponse objData = new APICall.CommomResponse();
            objData.message = "NO ACTION";
            if (Action.Equals("Save"))
            {
                string send_data = "{\n  \"CategoryName\": \"" + CategoryName + "\"\n}";

                APICall.APIData objAPIResult = APICall.CallPOST_PUTAPI(CommonConfig.GetAPIEndPoint + "ProductCategory", send_data, HttpMethod.Post);
                objData = JsonConvert.DeserializeObject<APICall.CommomResponse>(objAPIResult.apiResponseString);

            }
            if (Action.Equals("Update"))
            {
                int ProdCatId = 0;
                if (Request.Form.ContainsKey("hidCid"))
                {
                    ProdCatId = Convert.ToInt32(Request.Form["hidCid"].ToString());
                }
                string send_data = "{\n \"ProdCatId\": " + ProdCatId + ",\n \"CategoryName\": \"" + CategoryName + "\"\n}";

                APICall.APIData objAPIResult = APICall.CallPOST_PUTAPI(CommonConfig.GetAPIEndPoint + "ProductCategory", send_data, HttpMethod.Put);
                objData = JsonConvert.DeserializeObject<APICall.CommomResponse>(objAPIResult.apiResponseString);

            }
            TempData.Add("Message", objData.message);
            return RedirectToAction("Index", "Home");

        }
        public ActionResult AddProduct()
        {
            string Action = string.Empty;
                 if (Request.Form.ContainsKey("hidActionSaveUpdate"))
            {
                Action = Request.Form["hidActionSaveUpdate"].ToString();
            }

            int ProdCatId = 0;
            if (Request.Form.ContainsKey("ddlCategory"))
            {
                ProdCatId = Convert.ToInt32(Request.Form["ddlCategory"].ToString());
            }
            string ProdName = string.Empty;
            if (Request.Form.ContainsKey("txtProductName"))
            {
                ProdName = Request.Form["txtProductName"].ToString();
            }
            string ProdDescription = string.Empty;
            if (Request.Form.ContainsKey("txtProductDesc"))
            {
                ProdDescription = Request.Form["txtProductDesc"].ToString();
            }

            APICall.CommomResponse objData = new APICall.CommomResponse();
            objData.message = "NO ACTION";
            if (Action.Equals("Save"))
            {
                string send_data = "{\n  \"ProdCatId\": " + ProdCatId + ",\n  \"ProdName\": \"" + ProdName + "\",\n  \"ProdName\": \"" + ProdName + "\",\n  \"ProdDescription\": \"" + ProdDescription + "\"\n}";

                APICall.APIData objAPIResult = APICall.CallPOST_PUTAPI(CommonConfig.GetAPIEndPoint + "Product", send_data, HttpMethod.Post);
                objData = JsonConvert.DeserializeObject<APICall.CommomResponse>(objAPIResult.apiResponseString);
                
            }
            if (Action.Equals("Update"))
            {
                int ProductId = 0;
                if (Request.Form.ContainsKey("hidPid"))
                {
                    ProductId = Convert.ToInt32(Request.Form["hidPid"].ToString());
                }
                string send_data = "{\n  \"ProductId\": " + ProductId + ",\n\"ProdCatId\": " + ProdCatId + ",\n  \"ProdName\": \"" + ProdName + "\",\n  \"ProdName\": \"" + ProdName + "\",\n  \"ProdDescription\": \"" + ProdDescription + "\"\n}";

                APICall.APIData objAPIResult = APICall.CallPOST_PUTAPI(CommonConfig.GetAPIEndPoint + "Product", send_data,HttpMethod.Put);
                objData = JsonConvert.DeserializeObject<APICall.CommomResponse>(objAPIResult.apiResponseString);
                
            }
            TempData.Add("Message", objData.message);
            return RedirectToAction("Index", "Home");

        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
