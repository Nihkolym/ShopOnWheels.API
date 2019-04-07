using System;
using System.Collections.Generic;
using System.Text;

namespace ShopOnWheels.Entities.Models.Base
{
    [Serializable]
    public class BaseGuidDTOEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime ModifiedOn { get; private set; }
        public Boolean IsDeleted { get; set; }
    }
}
