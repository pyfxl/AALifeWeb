using AALife.Core.Domain.Configuration;

namespace AALife.Core.Repositorys.Configuration
{
    public partial class ParameterRepository : EfRepository<Parameter, int>, IParameterRepository
    {
        public ParameterRepository(IDbContext context) : base(context)
        {
        }
    }
}
