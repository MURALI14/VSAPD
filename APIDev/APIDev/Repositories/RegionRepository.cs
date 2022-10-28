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

        public async Task<Region> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await aPIDbContext.AddAsync(region);
            await aPIDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var region = await aPIDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            
            if(region == null)
            {
                return null; 
            }
            
            aPIDbContext.Regions.Remove(region);
            await aPIDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> GetAsync(Guid id)
        {
            return await aPIDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var curregion =  await aPIDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if(curregion == null)
            {
                return null;
            }
            curregion.Code = region.Code;
            curregion.Name = region.Name;
            curregion.Area = region.Area;
            curregion.Lat = region.Lat;
            curregion.Long = region.Long;
            curregion.Population = region.Population;

            await aPIDbContext.SaveChangesAsync();

            return curregion;
        }

        async Task<IEnumerable<Region>> IRegionRepository.GetAllAsync()
        {
            return await aPIDbContext.Regions.ToListAsync();
        }
    }
}
