using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore.Metadata;

using Riven.Storage;

using System;
using System.Collections.Generic;
using System.Text;

namespace Riven.Uow
{
    /// <summary>
    /// DbContext Model Storage
    /// </summary>
    public interface IEFCoreDbContextModelStorage : IAnyStorage<IModel>
    {

    }

    public class DefaultEFCoreDbContextModelStorage : AnyStorageBase<IModel>, IEFCoreDbContextModelStorage
    {
        public override void AddOrUpdate([NotNull] string key, [NotNull] IModel val)
        {
            Check.NotNull(key, nameof(key));
            Check.NotNull(val, nameof(val));

            _dataMap.TryAdd(key, val);
        }
    }
}
