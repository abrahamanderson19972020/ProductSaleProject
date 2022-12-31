using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator:AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.ProductName).MinimumLength(2);
            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p=>p.UnitPrice).NotEmpty();
            RuleFor(p=>p.UnitPrice).GreaterThan(0);
            RuleFor(p => p.UnitPrice).GreaterThan(10).When(p=>p.CategoryId==1);
            //We can write our won validation methods as follows
            //RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("Product Name should start with A");// we can add our specific message WithMessage
        }
        //This is out method for our specific error
        private bool StartWithA(string arg)
        {
            return arg.StartsWith("A");
        }
    }
}
