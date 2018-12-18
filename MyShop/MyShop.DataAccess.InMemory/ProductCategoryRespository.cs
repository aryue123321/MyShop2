using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRespository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategories = new List<ProductCategory>();

        public ProductCategoryRespository()
        {
            productCategories = cache["productCategories"] as List<ProductCategory>;
            if (productCategories == null)
                productCategories = new List<ProductCategory>();

        }

        public void Commit()
        {
            cache["productCategories"] = productCategories;
        }

        public void Insert(ProductCategory p)
        {
            productCategories.Add(p);
        }
        public void Update(ProductCategory p)
        {
            ProductCategory res = productCategories.Find(x => x.Id == p.Id);
            if (res == null)
                throw new Exception("Product not found!");
            res = p;

        }
        public ProductCategory Find(string Id)
        {
            ProductCategory res = productCategories.Find(x => x.Id == Id);
            if (res == null)
                throw new Exception("Product Category not found!");
            return res;
        }
        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();
        }
        public void Delete(string Id)
        {
            ProductCategory res = productCategories.Find(x => x.Id == Id);
            if (res == null)
                throw new Exception("Product Category not found!");
            productCategories.Remove(res);
        }

    }
}

