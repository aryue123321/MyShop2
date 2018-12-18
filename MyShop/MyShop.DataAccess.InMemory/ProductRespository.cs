using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    class ProductRespository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products = new List<Product>();

        public ProductRespository()
        {
            products = cache["products"] as List<Product>;
            if (products == null)
                products = new List<Product>();

        }

        public void Commit()
        {
            cache["products"] = products;
        }

        public void Insert(Product p)
        {
            products.Add(p);
        }
        public void Update(Product p)
        {
            Product res = products.Find(x => x.Id == p.Id);
            if (res == null)
                throw new Exception("Product not found!");
            res = p;
            
        }
        public Product Find(string Id)
        {
            Product res = products.Find(x => x.Id == Id);
            if (res == null)
                throw new Exception("Product not found!");
            return res;
        }
        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }
        public void Delete(string Id)
        {
            Product res = products.Find(x => x.Id == Id);
            if (res == null)
                throw new Exception("Product not found!");
            products.Remove(res);
        }

    }
}
