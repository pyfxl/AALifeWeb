using AALife.Core.Domain.Configuration;

namespace AALife.Core.Repositorys.Configuration
{
    public partial class SettingRepository : EfRepository<Setting>, ISettingRepository
    {
        public SettingRepository(IDbContext context) : base(context)
        {
        }
    }
}
