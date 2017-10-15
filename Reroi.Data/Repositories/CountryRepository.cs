using Reroi.Data.Abstract;
using Reroi.Model.Entities;

namespace Reroi.Data.Repositories
{
    public class CountryRepository : EntityBaseRepository<Country>, ICountryRepository
    {
        public CountryRepository(ReroiContext context)
            : base(context)
        { }
    }
}
