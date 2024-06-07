using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoAPI01.Data;
using DemoAPI01.Models.Domains;
using DemoAPI01.Models.DTOs;
using DemoAPI01.Repositories.Abstracts;

namespace DemoAPI01.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        private readonly IFileService _service;
        private readonly IProductRepository _repository;

        public ProductController(ApplicationDbContext context,
            Microsoft.AspNetCore.Hosting.IHostingEnvironment environment, IFileService service,
            IProductRepository repository)
        {
            _context = context;
            _environment = environment;
            _service = service;
            _repository = repository;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            if (_context.Products == null)
            {
                return NotFound();
            }

            return await _context.Products.ToListAsync();
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(Guid id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Product/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(Guid id, [FromForm] Product model)
        {
            if (id != model.ProductId)
            {
                return BadRequest();
            }

            var status = new Status();

            if (!ModelState.IsValid)
            {
                status.StatusCode = HttpStatusCode.BadRequest;
                status.Message = "Please pass the valid data";
                return Ok(status);
            }

            var productFound = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == id);

            if (model.ImageFile != null)
            {
                var fileResult = _service.SaveImage(model.ImageFile);

                if (fileResult.Item1 == 1)
                {
                    model.ProductImage = fileResult.Item2;
                }
            }
            else
            {
                model.ProductImage = productFound?.ProductImage;
            }

            var productResult = _repository.Update(model);

            if (productResult)
            {
                status.StatusCode = HttpStatusCode.NoContent;
                status.Message = "Updated Success";
            }
            else
            {
                status.StatusCode = HttpStatusCode.BadRequest;
                status.Message = "Updated Failed";
            }

            return Ok(status);
        }

        // POST: api/Product
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult PostProduct([FromForm] Product model)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Products'  is null.");
            }

            var status = new Status();

            if (!ModelState.IsValid)
            {
                status.StatusCode = HttpStatusCode.BadRequest;
                status.Message = "Please pass the valid data";
                return Ok(status);
            }

            if (model.ImageFile != null)
            {
                var fileResult = _service.SaveImage(model.ImageFile);

                if (fileResult.Item1 == 1)
                {
                    model.ProductImage = fileResult.Item2;
                }
            }


            var productResult = _repository.Add(model);
            if (productResult)
            {
                status.StatusCode = HttpStatusCode.Created;
                status.Message = "Added Success";
            }
            else
            {
                status.StatusCode = HttpStatusCode.BadRequest;
                status.Message = "Add Failed";
            }

            return Ok(status);
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FirstOrDefaultAsync(x=> x.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            _service.DeleteImage(product?.ProductImage);

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool ProductExists(Guid id)
        {
            return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}