using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPIProduct.BusinessModel;
using WebAPIProduct.DapperORM;
using WebAPIProduct.DataAccess;
using static WebAPIProduct.BusinessModel.CommonResponse;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIProduct.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAttributeLookupController : ControllerBase
    {
        MyActionFilter objAutorized;
        public ProductAttributeLookupController(MyActionFilter obj)
        {
            objAutorized = obj;
        }
        // GET: api/<ProductController>
        [ServiceFilter(typeof(MyActionFilter))]
        [HttpGet]
        public ActionResult Get()
        {
            if (objAutorized.ValidateTokenResult == null)
            {
                return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = "Invalid access." });
            }
            if (objAutorized.ValidateTokenResult.isValid == false)
            {
                return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = objAutorized.ValidateTokenResult.error_message });
            }

            ProductAttributeLookupDBAccess obj = ProductAttributeLookupDBAccess.getInstance;
            List<ProductAttributeLookupTable> objListData = obj.GetALLProductAttributeLookup();
            return Ok(
                    new ProductAttributeLookupResponse.SelectData()
                    {
                        data = objListData,
                        error_code = Convert.ToInt32(ErrorCode.Success),
                        message = "Product attribute data found."
                    }
                    );

        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        [ServiceFilter(typeof(MyActionFilter))]
        public ActionResult Get(int id)
        {
            try
            {
                if (objAutorized.ValidateTokenResult == null)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = "Invalid access." });
                }
                if (objAutorized.ValidateTokenResult.isValid == false)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = objAutorized.ValidateTokenResult.error_message });
                }
                ProductAttributeLookupDBAccess obj = ProductAttributeLookupDBAccess.getInstance;
                List<ProductAttributeLookupTable> objListData = obj.GetProductAttributeLookupById(id);
                if (objListData.Count == 0)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = "Product not found." });
                }
                return Ok(
                    new ProductAttributeLookupResponse.SelectData()
                    {
                        data = objListData,
                        error_code = Convert.ToInt32(ErrorCode.Success),
                        message = "Product attribute data found."
                    }
                    );
            }
            catch (Exception ex)
            {
                return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = ex.Message });
            }
        }

        // POST api/<ProductController>
        [HttpPost]
        [ServiceFilter(typeof(MyActionFilter))]
        public ActionResult Post(ProductAttributeLookupInsertParam objProductInsertParam)
        {
            try
            {
                if (objAutorized.ValidateTokenResult == null)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = "Invalid access." });
                }
                if (objAutorized.ValidateTokenResult.isValid == false)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = objAutorized.ValidateTokenResult.error_message });
                }
                ProductAttributeLookupTable objProduct = new ProductAttributeLookupTable();
                objProduct.ProdCatId = objProductInsertParam.ProdCatId;
                objProduct.AttributeName = objProductInsertParam.AttributeName;
                ValidationContext vc = new ValidationContext(objProduct);
                var validationsResults = new List<ValidationResult>();
                bool correct = Validator.TryValidateObject(objProduct, vc, validationsResults, true);
                if (!correct)
                {
                    string errorMessage = "";
                    foreach (var item in validationsResults.Select(ov => ov.ErrorMessage))
                    {
                        errorMessage = string.Concat(errorMessage, item);
                    }
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = errorMessage });
                }
                ProductAttributeLookupDBAccess obj = ProductAttributeLookupDBAccess.getInstance;
                List<ProductAttributeLookupResponse.InsertData> objListData = obj.InsertProductAttributeLookup(objProduct);
                if (objListData.Count == 0)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = "No data insert." });
                }

                if (objListData[0].RowEffect == 0)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = objListData[0].message });
                }

                return Ok(new ProductAttributeLookupResponse.InsertData()
                {
                    error_code = Convert.ToInt32(ErrorCode.Success),
                    message = objListData[0].message,
                    RowEffect = objListData[0].RowEffect,
                    AttributeId = objListData[0].AttributeId
                });

            }
            catch (Exception ex)
            {
                return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = ex.Message });
            }

        }

        // PUT api/<ProductController>
        [HttpPut]
        [ServiceFilter(typeof(MyActionFilter))]
        public ActionResult Put(ProductAttributeLookupUpdateParam objProductUpdateParam)
        {
            try
            {
                if (objAutorized.ValidateTokenResult == null)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = "Invalid access." });
                }
                if (objAutorized.ValidateTokenResult.isValid == false)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = objAutorized.ValidateTokenResult.error_message });
                }
                ProductAttributeLookupTable objProduct = new ProductAttributeLookupTable();
                objProduct.AttributeId = objProductUpdateParam.AttributeId;
                objProduct.ProdCatId = objProductUpdateParam.ProdCatId;
                objProduct.AttributeName = objProductUpdateParam.AttributeName;
                ValidationContext vc = new ValidationContext(objProduct);
                var validationsResults = new List<ValidationResult>();
                bool correct = Validator.TryValidateObject(objProduct, vc, validationsResults, true);
                if (!correct)
                {
                    string errorMessage = "";
                    foreach (var item in validationsResults.Select(ov => ov.ErrorMessage))
                    {
                        errorMessage = string.Concat(errorMessage, item);
                    }
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = errorMessage });
                }
                ProductAttributeLookupDBAccess obj = ProductAttributeLookupDBAccess.getInstance;
                List<ProductAttributeLookupResponse.UpdateData> objListData = obj.UpdateProductAttributeLookup(objProduct);
                if (objListData.Count == 0)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = "No data update." });
                }

                if (objListData[0].RowEffect == 0)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = objListData[0].message });
                }

                return Ok(new ProductAttributeLookupResponse.UpdateData()
                {
                    error_code = Convert.ToInt32(ErrorCode.Success),
                    message = objListData[0].message,
                    RowEffect = objListData[0].RowEffect
                });

            }
            catch (Exception ex)
            {
                return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = ex.Message });
            }
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{AttributeId}")]
        [ServiceFilter(typeof(MyActionFilter))]
        public ActionResult Delete(int AttributeId)
        {
            try
            {
                if (objAutorized.ValidateTokenResult == null)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = "Invalid access." });
                }
                if (objAutorized.ValidateTokenResult.isValid == false)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = objAutorized.ValidateTokenResult.error_message });
                }
                if (AttributeId < 0)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = "Invalid attribute id." });
                }

                ProductAttributeLookupDBAccess obj = ProductAttributeLookupDBAccess.getInstance;
                List<ProductAttributeLookupResponse.DeleteData> objListData = obj.DeleteProductAttributeLookup(AttributeId);
                if (objListData.Count == 0)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = "No data found." });
                }

                if (objListData[0].RowEffect == 0)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = objListData[0].message });
                }

                return Ok(new ProductAttributeLookupResponse.DeleteData()
                {
                    error_code = Convert.ToInt32(ErrorCode.Success),
                    message = objListData[0].message,
                    RowEffect = objListData[0].RowEffect
                });

            }
            catch (Exception ex)
            {
                return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = ex.Message });
            }
        }
    }
}

