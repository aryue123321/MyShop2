using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class InMemoryRepo<T> where T : BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;
        List<T> items;
        string className;

        public InMemoryRepo()
        {
            className = typeof(T).Name;
            items = cache[className] as List<T>;
            if (items == null)
                items = new List<T>();
        }

        public void Commit()
        {
            cache[className] = items;
        }

        public void Insert(T t)
        {
            items.Add(t);
        }
        public void update(T t)
        {
            T toBeUpdate = items.Find(x => x.Id == t.Id);
            if (toBeUpdate == null)
                throw new Exception(className + "Not Found");
            toBeUpdate = t;
        }
        public T Find(String Id)
        {
            T res = items.Find(x => x.Id == Id);
            if (res == null)
                throw new Exception(className + "Not Found");
            return res;
        }
        public void Delete(string Id)
        {
            T toBeDelete = items.Find(x => x.Id == Id);
            if (toBeDelete == null)
                throw new Exception(className + "Not Found");
            items.Remove(toBeDelete);
        }
        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
        }
    }
}
