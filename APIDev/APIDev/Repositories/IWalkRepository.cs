using APIDev.Models.Domain;

namespace APIDev.Repositories
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk>> GetAllAsync();
        Task<Walk> GetWalkAsync(Guid id);

        Task<Walk> AddAsync(Walk walk);

        Task<Walk> UpdateAsync(Guid id,Walk walk);

        Task<Walk> DeleteAsyn(Guid id);
    }
}
