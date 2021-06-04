using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Riven.Uow
{
    public class NullUnitOfWork : UnitOfWorkBase
    {
        public override void SaveChanges()
        {

        }

        public override Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        protected override void CompleteUow()
        {

        }

        protected override Task CompleteUowAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        protected override void DisposeUow()
        {

        }
    }
}
