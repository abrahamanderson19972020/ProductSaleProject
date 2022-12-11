

using Business.Concrete;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;

ProductManager productManager = new ProductManager( new InMemoryProductDal());
foreach(Product product in productManager.GetAllProducts())
{
    Console.WriteLine(product.ProductName);
}

