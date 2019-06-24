using ShopOnWheels.Domain.Models.Box;
using ShopOnWheels.Entities.Models.Box;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnWheels.Services.Stores.BoxStore
{
    public interface IBoxStore
    {
        BoxDTO GetBox(Guid Id);
        Box GetBoxByProductListId(Guid Id);
        List<BoxDTO> GetBoxes();
        Task<BoxDTO> UpdateBox(Guid id, BoxDTO modelDto);
        IEnumerable<BoxDTO> GetBoxesByOrder(Guid orderId);
    }
}
