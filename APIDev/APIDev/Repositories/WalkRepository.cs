using APIDev.Data;
using APIDev.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace APIDev.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly APIDbContext aPIDbContext;

        public WalkRepository(APIDbContext aPIDbContext)
        {
            this.aPIDbContext = aPIDbContext;
        }

        public async Task<Walk> AddAsync(Walk walk)
        {
            walk.Id = Guid.NewGuid();
            await aPIDbContext.Walk.AddAsync(walk);
            await aPIDbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> DeleteAsyn(Guid id)
        {
            var exwalk = await aPIDbContext.Walk.FindAsync(id);
            if(exwalk != null)
            {
                aPIDbContext.Walk.Remove(exwalk);
                await aPIDbContext.SaveChangesAsync();
                return exwalk;
            }
            return null;
        }

        public async Task<IEnumerable<Walk>> GetAllAsync()
        {
            return await 
                aPIDbContext.Walk
                .Include(x=> x.Region)
                .Include(x=> x.WalkDifficulty)
                .ToListAsync();
        }

        public async Task<Walk> GetWalkAsync(Guid id)
        {
            return await aPIDbContext.Walk
                        .Include(x => x.Region)
                        .Include(x => x.WalkDifficulty)
                        .FirstOrDefaultAsync(x => x.Id == id);

            

        }

        public async Task<Walk> UpdateAsync(Guid id, Walk walk)
        {
          var exwalk=  await aPIDbContext.Walk.FindAsync(id);
          if(exwalk!=null)
          {
                exwalk.WalkDifficultyId = walk.WalkDifficultyId;
                exwalk.Name = walk.Name;
                exwalk.Length = walk.Length;
                exwalk.RegionId = walk.RegionId;
                await aPIDbContext.SaveChangesAsync();
                return exwalk;
           }
            return null;
        }
    }
}
