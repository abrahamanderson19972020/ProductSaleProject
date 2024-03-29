﻿using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.BusinessRules;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        //Validation Codes: is related to data structure like the length of product name or minimum price.
        //FluentValidation is a .NET library for building strongly-typed validation rules. It Uses a fluent interface and lambda expressions for building validation rules. 
        
        IProductDal _productDal;
        ICategoryDal _categoryDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        //Claim
        [SecuredOperation("prodcut.add,admin")]
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        {
            //ValidationTool.Validate(new ProductValidator(), product); //instead of this we use AOP

            //Business Code
            IResult result = BusinessRules.Run(CheckIfProductWithSameName(product), CheckNumberOfProductByCategory(product));
            if(result !=null)
            {
                return result;
            }
            
                _productDal.Add(product);

            return new SuccessResult(Messages.ProductAdded);
        }

        [CacheRemoveAspect("IProductService.Get")]
        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
        }

        [CacheRemoveAspect("IProductService.Get")]
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

        [CacheAspect]
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

        //Business Rules
        private IResult CheckNumberOfProductByCategory(Product product)
        {
            if (_productDal.GetAll(p => p.CategoryId == product.CategoryId).Count > 10)
            {
                return new ErrorResult(Messages.ProductNumberByCategoryExceeded);
            }
            return new SuccessResult();
        }
        private IResult CheckIfProductWithSameName(Product product)
        {
            if (_productDal.GetAll(p => p.ProductName == product.ProductName).Any())
            {
                return new ErrorResult(Messages.ProductAlreadyExists);
            }
            return new SuccessResult();
        }
    }
}
