using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repository
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbCnx;
        public RegionRepository(NZWalksDbContext dbCnx)
        {
            this.dbCnx = dbCnx;
        }
        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await dbCnx.Regions.ToListAsync();
        }
    }
}
