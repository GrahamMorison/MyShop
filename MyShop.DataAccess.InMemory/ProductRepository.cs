using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products;

        public ProductRepository() 
        {
            products = cache["products"] as List<Product>; //upon launch check the cache for any products
            if (products == null) //create a new list if no cache exists
            {
                products = new List<Product>();
            }
        }

        public void Commit()
        {
            cache["products"] = products;
        }

        public void Insert(Product p)
        {
            products.Add(p);
        }

        public void Update(Product product)
        {
            Product productsToUpdate = products.Find(p => p.Id == product.Id);
            if(productsToUpdate != null)
            {
                productsToUpdate = product;
            }
            else
            {
                throw new Exception("Product Not Found");
            }
        }

        public Product Find(string Id)
        {
            Product product = products.Find(p => p.Id == Id);

            if(product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product Not Found");
            }
        }
        public IQueryable<Product> Collection() 
        {
            return products.AsQueryable();
        }

        public void Delete(string id)
        {
            Product productToDelete = products.Find(p => p.Id == id);

            if (productToDelete != null) 
            {
                products.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product Not Found");
            }
        }
    }
}
