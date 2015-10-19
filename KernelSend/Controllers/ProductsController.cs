using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using KernelSend.Models;

namespace KernelSend.Controllers
{
    public class ProductsController : ODataController
    {

        AdventureWorks2008R2Entities db = new AdventureWorks2008R2Entities();

        //test to see if any product exists
        private bool ProductExists(int key)
        {
            return db.Products.Any(p => p.ProductID == key);
        }

        /// <summary>
        /// Dispose the dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        //Get All Products
        [EnableQuery]
        public IQueryable<Product> Get()
        {
            return db.Products;
        }

        //SingleResult of Product based on int parameter
        [EnableQuery]
        public SingleResult<Product> Get([FromODataUri] int key)
        {
            IQueryable<Product> result = db.Products.Where(p => p.ProductID == key);
            return SingleResult.Create(result);
        }

        /// <summary>
        /// Async Task action to create a new Product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>

        public async Task<IHttpActionResult> Post(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Products.Add(product);
            await db.SaveChangesAsync();
            return Created(product);
        }
    }
}