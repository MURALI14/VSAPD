using APIDev.Data;
using APIDev.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace APIDev.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly APIDbContext aPIDbContext;

        public WalkDifficultyRepository(APIDbContext aPIDbContext)
        {
            this.aPIDbContext = aPIDbContext;
        }

        public async Task<WalkDifficulty> AddAsync(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id = Guid.NewGuid();
            await aPIDbContext.WalkDifficulty.AddAsync(walkDifficulty);
            await aPIDbContext.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<WalkDifficulty> DeleteAsync(Guid id)
        {
            var exiswd = await aPIDbContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);
            if(exiswd != null)
            {
                aPIDbContext.Remove(exiswd);
                await aPIDbContext.SaveChangesAsync();
                return exiswd;
            }
            return null;
        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllAsync()
        {
            return await aPIDbContext.WalkDifficulty.ToListAsync();
        }

        public async Task<WalkDifficulty> GetAsync(Guid id)
        {
            return await aPIDbContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<WalkDifficulty> UpdateAsync(Guid id, WalkDifficulty walkDifficulty)
        {
            var exiswd = await aPIDbContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);    
            if(exiswd != null)
            {
                exiswd.code = walkDifficulty.code;
                await aPIDbContext.SaveChangesAsync();
                return exiswd;
            }
            return null;
        }
    }
}
