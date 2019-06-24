using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ShopOnWheels.Domain;
using ShopOnWheels.Entities.Models.Box;
using Microsoft.EntityFrameworkCore;
using ShopOnWheels.Domain.Models.Box;

namespace ShopOnWheels.Services.Stores.BoxStore
{
    public class BoxStore : IBoxStore
    {
        private readonly IMapper _mapper;
        private readonly ShopOnWheelsDbContext _context;

        public BoxStore(IMapper mapper, ShopOnWheelsDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }
        public BoxDTO GetBox(Guid Id)
        {
            return _mapper.Map<BoxDTO>(_context.Boxes.Include(b => b.ProductList).ThenInclude(pl => pl.Product).Include(b => b.ProductList.Order).FirstOrDefault(b => b.Id == Id));
        }

        public Box GetBoxByProductListId(Guid Id)
        {
            return _context.Boxes.Include(b => b.ProductList).ThenInclude(pl => pl.Product).Include(b => b.ProductList.Order).Where(b => b.ProductList.Id == Id).FirstOrDefault();
        }

        public List<BoxDTO> GetBoxes()
        {
            return _mapper.Map<List<BoxDTO>>(_context.Boxes.Include(b => b.ProductList).ThenInclude(pl => pl.Product).Include(b => b.ProductList.Order));
        }

        public IEnumerable<BoxDTO> GetBoxesByOrder(Guid orderId)
        {
            var boxes = _mapper.Map<List<BoxDTO>>(_context.Boxes.Include(b => b.ProductList).ThenInclude(pl => pl.Product).Include(b => b.ProductList.Order));
            var res = boxes.FindAll(box => box.Order.Id == orderId);
            return res;
        }

        public async Task<BoxDTO> UpdateBox(Guid id, BoxDTO modelDto)
        {
            var model = _mapper.Map<Box>(modelDto);
            model.Id = id;

            _context.Boxes.Update(model);

            await _context.SaveChangesAsync();

            return _mapper.Map<BoxDTO>(model);
        }
    }
}
