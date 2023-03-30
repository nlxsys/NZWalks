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

        public async Task<Region> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await dbCnx.AddAsync(region);
            await dbCnx.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var region = await dbCnx.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (region == null)
            {
                return null;
            }

            dbCnx.Regions.Remove(region);

            await dbCnx.SaveChangesAsync();

            return region;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await dbCnx.Regions.ToListAsync();
        }

        public async Task<Region> GetAsync(Guid id)
        {
            return await dbCnx.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await dbCnx.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (existingRegion == null)
            {
                return null;
            }
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.Area = region.Area;
            existingRegion.Lat = region.Lat;
            existingRegion.Longt = region.Longt;
            existingRegion.Population = region.Population;

            await dbCnx.SaveChangesAsync();

            return existingRegion;
        }
    }
}
