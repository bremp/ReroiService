using Reroi.Data.Abstract;
using Reroi.Model.Entities;

namespace Reroi.Data.Repositories
{
    public class PropertyRepository : EntityBaseRepository<Property>, IPropertyRepository
    {
        public PropertyRepository(ReroiContext context)
            : base(context)
        { }
    }
}
