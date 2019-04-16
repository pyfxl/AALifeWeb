using AALife.Core.Domain.Media;

namespace AALife.Core.Repositorys.Configuration
{
    public partial class PictureRepository : EfRepository<Picture, int>, IPictureRepository
    {
        public PictureRepository(IDbContext context) : base(context)
        {
        }
    }
}
