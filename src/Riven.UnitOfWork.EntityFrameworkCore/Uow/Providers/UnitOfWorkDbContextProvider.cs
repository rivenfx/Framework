using Microsoft.EntityFrameworkCore;
using Riven.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Riven.Uow.Providers
{
    public class UnitOfWorkDbContextProvider : IUnitOfWorkDbContextProvider
    {
        protected readonly ICurrentUnitOfWorkProvider _currentUnitOfWorkProvider;

        public UnitOfWorkDbContextProvider(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider)
        {
            _currentUnitOfWorkProvider = currentUnitOfWorkProvider;
        }

        public virtual DbContext GetDbContext()
        {
            return _currentUnitOfWorkProvider.Current.GetDbContext();
        }
    }
}
