using AALife.Core.Configuration;
using AALife.Core.Domain.Configuration;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AALife.Core.Services.Configuration
{
    /// <summary>
    /// Setting service interface
    /// </summary>
    public partial interface IParameterService : IBaseService<Parameter, int>
    {
        IEnumerable<Parameter> GetParamsByName(string name);
    }
}
