using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models;

// http://localhost:5153        -- Base URL

namespace ProductAPI.Controllers;

[Route("api/[controller]")]             // http://localhost:5153/api/Product
[ApiController]                         // Annotation defines this class as a Rest Controller
public class ProductController : ControllerBase
{
    // Find All Products
    [HttpGet]                                 // http://localhost:5153/api/Product
    public List<Product> FindAllProducts()
    {
        return ProductData.Products;        
    }

    // Search Product By Id             // http://localhost:5153/api/Product/1
    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]         // ActionResult : Response + Status Code
    [ProducesResponseType(400)]             
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Product> GetProductById(int id)   
    {
        if ( id <= 0 )
        {
            return BadRequest();
        }
        var product = ProductData.Products.FirstOrDefault(x => x.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }


    // Search Product By Name      // http://localhost:5153/api/Product/byName/Laptop
    [HttpGet("byName/{name:alpha}")]
    [ProducesResponseType(200)]         // ActionResult : Response + Status Code
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Product> GetProductByName(string name)
    {
        if ( name == null)
        {
            return BadRequest();
        }
        var product = ProductData.Products.FirstOrDefault(x => x.Name == name);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    // Create/Insert Product 
    [HttpPost]                              // http://localhost:5153/api/Product
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public ActionResult<Product> CreateProduct( [FromBody] Product product)
    {

        if (product == null)
        {
            return BadRequest();         // 400 Error
        }
        if ( !ModelState.IsValid)       // if product properties according to validation
        {
            return BadRequest();            
        }
        // Checking if Product Name already exist
        // Select Count() From Products Where Name = product.Name

        if (ProductData.Products.Where(p => p.Name == product.Name).Count() > 0)
        {
            ModelState.AddModelError("CustomError", "Name Already Exist");
            return BadRequest(ModelState);
        }

        if (product.Id > 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);// 500 Error
        }

        product.Id = ProductData.Products.Count + 1;
        ProductData.Products.Add(product);
        return Ok(product);                             // 200 Ok

    }


    // Delete Product

    [HttpDelete("{id:int}")]         // http://localhost:5153/api/Product/1
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult DeleteProduct(int id)       
    {
        if (id <= 0)
        {
            return BadRequest();
        }
        var product = ProductData.Products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        ProductData.Products.Remove(product);
        return NoContent();
    }


    // Update Product
    [HttpPut("{id:int}")]           // http://localhost:5153/api/Product/1
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public ActionResult<Product> UpdateProduct(int id, [FromBody] Product NewProduct)
    {
        if (id <= 0 || NewProduct == null)
        {
            return BadRequest();
        }
        var product = ProductData.Products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        product.Name = NewProduct.Name;
        product.Price = NewProduct.Price;   
        return Ok(product);

    }


}

