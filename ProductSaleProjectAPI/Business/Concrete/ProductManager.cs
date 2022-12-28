using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public IResult Add(Product product)
        {
            if(product.ProductName.Length < 2)
            {
                return new ErrorResult(Messages.ProductNameInvalid);
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }
        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
        }

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult(Messages.ProductDeleted);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int categoryId)
        {
            return new DataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == categoryId),true,Messages.CategoryById);
        }

        public IDataResult<List<Product>> GetAllByProductPrice(decimal min, decimal max)
        {
            return new DataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max),true,Messages.GetAllByProductPrice); 
        }

        public IDataResult<List<Product>> GetAllProducts()
        {
            return new DataResult<List<Product>>(_productDal.GetAll(),true,Messages.ProductAllListed);
        }

        public IDataResult<Product> GetProductById(int productId)
        {
            return new DataResult<Product>(_productDal.Get(p => p.ProductId == productId),true,Messages.ProductByProductId);
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new DataResult<List<ProductDetailDto>>(_productDal.GetProductDetails(), true, Messages.ProductDetails) ;
        }
    }
}
