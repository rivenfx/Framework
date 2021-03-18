using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Riven.Identity.Permissions
{
    public class IdentityPermissionStore<TPermission> : IIdentityPermissionStore<TPermission>
        where TPermission : IdentityPermission
    {
        protected static List<string> _emptyPermissionNames = new List<string>();

        protected bool _disposed;

        public bool AutoSaveChanges { get; set; } = true;

        public virtual DbContext Context => throw new NotImplementedException(nameof(Context));

        public virtual IQueryable<TPermission> Query => Context.Set<TPermission>();



        public async Task CreateAsync([NotNull] TPermission permission)
        {
            Check.NotNull(permission, nameof(permission));

            ThrowIfDisposed();

            await Context.AddAsync(permission);

            await this.SaveChanges(default);
        }

        public async Task Remove(params string[] names)
        {
            if (names == null || names.Length == 0)
            {
                return;
            }

            ThrowIfDisposed();

            await Task.Yield();


            var permissions = await Query
                .Where(o => names.Contains(o.Name))
                .ToListAsync();

            this.Context.RemoveRange(permissions);

            await this.SaveChanges(default);
        }


        public async Task<IEnumerable<string>> GetAll()
        {
            return await Query.AsNoTracking()
                   .GroupBy(o => o.Name)
                   .Select(o => o.Key)
                   .ToListAsync();
        }


        public async Task<IEnumerable<string>> FindPermissions(string type, string provider)
        {
            if (string.IsNullOrWhiteSpace(type)
                || string.IsNullOrWhiteSpace(provider)
                )
            {
                return _emptyPermissionNames;
            }

            return await Query.AsNoTracking()
                    .Where(o => o.Type == type && o.Provider == provider)
                    .Select(o => o.Name)
                    .ToListAsync();
        }



        public async Task<bool> ExistPermission(string name, string type, string provider)
        {
            if (string.IsNullOrWhiteSpace(name)
                || string.IsNullOrWhiteSpace(type)
                || string.IsNullOrWhiteSpace(provider)
                )
            {
                return false;
            }

            ThrowIfDisposed();

            return await Query.AsNoTracking()
                   .Where(o => o.Name == name && o.Type == type && o.Provider == provider)
                   .AnyAsync();
        }

        public async Task<IEnumerable<string>> FindPermissions(string type, string[] providers)
        {
            if (providers == null || providers.Length == 0 || string.IsNullOrWhiteSpace(type))
            {
                return _emptyPermissionNames;
            }

            return await Query.AsNoTracking()
                    .Where(o => o.Type == type && providers.Contains(o.Provider))
                    .Select(o => o.Name)
                    .ToListAsync();
        }



        protected virtual Task SaveChanges(CancellationToken cancellationToken)
        {
            return AutoSaveChanges ? Context.SaveChangesAsync(cancellationToken) : Task.CompletedTask;
        }

        /// <summary>
        /// Throws if this class has been disposed.
        /// </summary>
        protected void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        /// <summary>
        /// Dispose the stores
        /// </summary>
        public void Dispose()
        {
            _disposed = true;
        }
    }
}
