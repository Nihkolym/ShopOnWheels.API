using ShopOnWheels.Entities.Models.Category;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnWheels.Services.Stores.CategoryStore
{
    public interface ICategoryStore
    {
        Task<CategoryDTO> AddCategory(CategoryDTO modelDto);
        Task<CategoryDTO> UpdateCategory(Guid id, CategoryDTO modelDto);
        Task<CategoryDTO> GetCategory(Guid Id);
        Task<IEnumerable<CategoryDTO>> AllCategories();
        Task<CategoryDTO> DeleteCategory(Guid Id);
    }
}
