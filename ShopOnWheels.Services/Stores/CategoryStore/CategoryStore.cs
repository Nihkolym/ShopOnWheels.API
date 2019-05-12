using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopOnWheels.Domain;
using ShopOnWheels.Domain.Models.Category;
using ShopOnWheels.Entities.Models.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnWheels.Services.Stores.CategoryStore
{
    public class CategoryStore : ICategoryStore
    {
        private readonly IMapper _mapper;
        private readonly ShopOnWheelsDbContext _context;

        public CategoryStore(IMapper mapper, ShopOnWheelsDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CategoryDTO> AddCategory(CategoryDTO category)
        {
            var model = _mapper.Map<Category>(category);

            await _context.AddAsync(model);
            await _context.SaveChangesAsync();

            return _mapper.Map<CategoryDTO>(model);
        }

        public async Task<IEnumerable<CategoryDTO>> AllCategories()
        {
            return _mapper.Map<List<CategoryDTO>>(_context.Categories
                .Include(o => o.Products));

        }

        public async Task<CategoryDTO> UpdateCategory(Guid id, CategoryDTO modelDto)
        {
            var model = _mapper.Map<Category>(modelDto);
            model.Id = id;

            _context.Categories.Update(model);

            await _context.SaveChangesAsync();

            return _mapper.Map<CategoryDTO>(model);
        }

        public async Task<CategoryDTO> DeleteCategory(Guid Id)
        {
            var model = _context.Categories.FirstOrDefault(p => p.Id == Id);
            _context.Categories.Remove(model);

            await _context.SaveChangesAsync();
            return _mapper.Map<CategoryDTO>(model);
        }

        public async Task<CategoryDTO> GetCategory(Guid Id)
        {
            return _mapper.Map<CategoryDTO>(_context.Categories.FirstOrDefault(p => p.Id == Id));
        }
    }
}
