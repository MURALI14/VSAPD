using APIDev.Data;
using APIDev.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace APIDev.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly APIDbContext aPIDbContext;
        public RegionRepository(APIDbContext aPIDbContext)
        {
            this.aPIDbContext = aPIDbContext;
        }
        async Task<IEnumerable<Region>> IRegionRepository.GetAllAsync()
        {
            return await aPIDbContext.Regions.ToListAsync();
        }
    }
}
