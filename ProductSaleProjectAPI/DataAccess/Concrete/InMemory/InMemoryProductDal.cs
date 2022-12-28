using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products;
        public InMemoryProductDal()
        {
            _products = new List<Product>{
             new Product { ProductId=1,ProductName= "Computer",CategoryId=1, UnitPrice=12311,UnitsInStock=12},
             new Product { ProductId = 2, ProductName = "Screen", CategoryId = 1, UnitPrice = 54211, UnitsInStock = 9 },
             new Product { ProductId = 3, ProductName = "Mouse", CategoryId = 2, UnitPrice = 43, UnitsInStock = 1200 },
             new Product { ProductId = 4, ProductName = "Keyboard", CategoryId = 2, UnitPrice = 78, UnitsInStock = 431 },
             new Product { ProductId = 5, ProductName = "Gaming Chair", CategoryId = 3, UnitPrice = 2300, UnitsInStock = 32 }
            };
        }
        public void Add(Product product)
        {
            _products.Add(product); 
        }

        public void Delete(Product product)
        {
            Product productToDelete = _products.FirstOrDefault(p => p.ProductId == product.ProductId);  
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllByCategoryId(int categoryId)
        {
            return _products.Where(p=> p.CategoryId == categoryId).ToList();    
        }

        public List<ProductDetailDto> GetProductDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Product product)
        {
            Product productToUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
            productToUpdate.UnitPrice = product.UnitPrice;  
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.UnitsInStock = product.UnitsInStock;
        }
    }
}
