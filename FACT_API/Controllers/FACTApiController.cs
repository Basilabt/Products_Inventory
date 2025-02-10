using FACT_API.DataAccess;
using FACT_API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics.CodeAnalysis;




namespace FACT_API.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/FACT")]
    [ApiController]
    public class FACTApiController : ControllerBase
    {


        [HttpGet("UserInventory/{user_id}",Name = "GetUserInventory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<clsUserInventory>> GetUserInventory(int user_id)
        {

            if(user_id < 1)
            {
                return BadRequest("Invalid User ID");
            }


            List<clsUserInventory> list = clsInventoryDataAccess.getUserInventoriesByUserID(user_id);

            if(list.Count == 0)
            {
                return NotFound("Not Found");
            }

            return Ok(list); 
        }

        [HttpGet("Products" , Name = "GetAvailableProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<clsProduct>> GetAvailableProducts()
        {
            List<clsProduct> productsList = clsProductDataAccess.getProducts();

            if(productsList.Count == 0)
            {
                return NotFound("No Products Available");
            }

            return Ok(productsList);
        }
       
        



    }
}
