using System;
using System.Collections.Generic;
using AutoMapper;

namespace AALife.WebMvc.Mapper
{
    /// <summary>
    /// AutoMapper configuration
    /// </summary>
    public static class AutoMapperConfiguration
    {
        private static AutoMapper.MapperConfiguration _mapperConfiguration;
        private static IMapper _mapper;
        
        /// <summary>
        /// Initialize mapper
        /// </summary>
        /// <param name="configurationActions">Configuration actions</param>
        public static void Init(List<Action<IMapperConfigurationExpression>> configurationActions)
        {
            if (configurationActions == null)
                throw new ArgumentNullException("configurationActions");

            _mapperConfiguration = new AutoMapper.MapperConfiguration(cfg =>
            {
                foreach (var ca in configurationActions)
                    ca(cfg);
            });

            _mapper = _mapperConfiguration.CreateMapper();
        }

        /// <summary>
        /// Mapper
        /// </summary>
        public static IMapper Mapper
        {
            get
            {
                return _mapper;
            }
        }
        /// <summary>
        /// Mapper configuration
        /// </summary>
        public static AutoMapper.MapperConfiguration MapperConfiguration
        {
            get
            {
                return _mapperConfiguration;
            }
        }
    }
}