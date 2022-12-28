

using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;

ProductTest();
Console.WriteLine("------------------category Dal ile---------------------------");

CategoryTest();

static void ProductTest()
{
    ProductManager productManager = new ProductManager(new EfProductDal());
    foreach (Product product in productManager.GetAllByCategoryId(2).Data)
    {
        Console.WriteLine(product.ProductName);
    }
    Console.WriteLine("****************************************");
    foreach (Product product in productManager.GetAllByProductPrice(10, 15).Data)
    {
        Console.WriteLine(product.ProductName);
    }
    Console.WriteLine("--------------With Joined Data between Categories and Products Tables-----------");
    foreach (ProductDetailDto product in productManager.GetProductDetails().Data)
    {
        Console.WriteLine(product.ProductName + " / in Category Name " + product.CategoryName);
    }
}

static void CategoryTest()
{
    CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
    foreach (Category category in categoryManager.GetAllCategories())
    {
        Console.WriteLine(category.CategoryName);
    }
    Console.WriteLine("-------single category --------");
    Console.WriteLine(categoryManager.GetCategoryById(1).CategoryName);
}