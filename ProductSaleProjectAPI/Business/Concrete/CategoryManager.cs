using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal;
        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }
        public IDataResult<List<Category>> GetAllCategories()
        {
            return new DataResult<List<Category>>(_categoryDal.GetAll(),true, Messages.AllCategoriesTaken) ;
        }

        public IDataResult<Category> GetCategoryById(int categoryId)
        {
            return new DataResult<Category>(_categoryDal.Get(c => c.CategoryId == categoryId),true,Messages.CategoryById);
        }
    }
}
