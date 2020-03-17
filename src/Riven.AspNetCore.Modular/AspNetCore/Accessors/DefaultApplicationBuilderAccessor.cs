using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riven.AspNetCore.Accessors
{
    internal class DefaultApplicationBuilderAccessor : IApplicationBuilderAccessor
    {
        private IApplicationBuilder _applicationBuilder;
        public IApplicationBuilder ApplicationBuilder
        {
            get
            {
                return this._applicationBuilder;
            }
            set
            {
                if (_applicationBuilder == null)
                {
                    this._applicationBuilder = value;
                }
            }
        }
    }
}
