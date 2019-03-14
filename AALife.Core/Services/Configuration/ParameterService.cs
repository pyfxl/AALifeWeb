using System;
using System.Collections.Generic;
using System.Linq;
using AALife.Core.Caching;
using AALife.Core.Domain.Configuration;
using AALife.Core.Repositorys.Configuration;

namespace AALife.Core.Services.Configuration
{
    /// <summary>
    /// Setting manager
    /// </summary>
    public partial class ParameterService : BaseService<Parameter>, IParameterService
    {
        #region Constants

        /// <summary>
        /// Key for caching
        /// </summary>
        private const string PARAMETERS_ALL_KEY = "aalife.parameter.all";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string PARAMETERS_PATTERN_KEY = "aalife.parameter.{0}";

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="eventPublisher">Event publisher</param>
        /// <param name="settingRepository">Setting repository</param>
        public ParameterService(IParameterRepository parameterRepository,
            ICacheManager cacheManager,
            IDbContext dbContext)
            : base(parameterRepository, cacheManager, dbContext)
        {
        }

        public IEnumerable<Parameter> GetParamsByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name empty");

            //cache
            string key = string.Format(PARAMETERS_PATTERN_KEY, name);
            return _cacheManager.Get(key, () =>
            {
                var param = from s in _repository.TableNoTracking
                            join c in _repository.TableNoTracking on s.ParentId equals c.Id
                            where c.SystemName == name
                            select s;

                return param.ToList();
            });
        }

        #endregion

    }

    public static class ParameterExtensions
    {
        public static dynamic ToValue(this IEnumerable<Parameter> parameter)
        {
            return parameter.Select(x =>
            new {
                text = x.Name,
                value = x.Value
            });
        }
    }
}