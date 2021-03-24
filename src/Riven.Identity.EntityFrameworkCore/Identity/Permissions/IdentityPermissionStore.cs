using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Riven.Identity.Permissions
{
    public class IdentityPermissionStore<TPermission> : IIdentityPermissionStore<TPermission>
        where TPermission : IdentityPermission
    {
        protected static List<string> _emptyPermissionNames = new List<string>();

        protected bool _disposed;

        protected readonly IServiceProvider _serviceProvider;

        protected readonly IIdentityDbContextAccessor _contextAccessor;

        protected IdentityPermissionStore(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _contextAccessor = _serviceProvider
                .GetService<IIdentityDbContextAccessor>();
        }

        public bool AutoSaveChanges { get; set; } = true;

        public virtual DbContext Context => _contextAccessor.Context;

        public virtual IQueryable<TPermission> Query => Context.Set<TPermission>();

        public virtual async Task CreateAsync([NotNull] TPermission permission)
        {
            Check.NotNull(permission, nameof(permission));

            ThrowIfDisposed();

            await Context.AddAsync(permission);

            await this.SaveChanges(default);
        }

        public virtual async Task CreateAsync([NotNull] IEnumerable<TPermission> permissions)
        {
            Check.NotNull(permissions, nameof(permissions));

            ThrowIfDisposed();

            await Context.AddRangeAsync(permissions);

            await this.SaveChanges(default);
        }

        public virtual async Task Remove([NotNull] string type, [NotNull] string provider, params string[] names)
        {
            Check.NotNull(type, nameof(type));
            Check.NotNull(provider, nameof(provider));

            ThrowIfDisposed();


            var query = default(IQueryable<TPermission>);
            if (names == null || names.Length == 0)
            {
                query = this.Query.Where(o => o.Type == type && o.Provider == provider)
                    .AsNoTracking();
            }
            else
            {
                query = this.Query.Where(o => o.Type == type
                    && o.Provider == provider
                    && names.Contains(o.Name))
                    .AsNoTracking();
            }

            var permissions = query.ToListAsync();

            this.Context.RemoveRange(permissions);

            await this.SaveChanges(default);
        }

        public virtual async Task Remove(params string[] names)
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


        public virtual async Task<IEnumerable<string>> GetAll()
        {
            return await Query.AsNoTracking()
                   .GroupBy(o => o.Name)
                   .Select(o => o.Key)
                   .ToListAsync();
        }


        public virtual async Task<IEnumerable<string>> FindPermissions(string type, string provider)
        {
            if (string.IsNullOrWhiteSpace(type)
                || string.IsNullOrWhiteSpace(provider)
                )
            {
                return _emptyPermissionNames;
            }

            return await Query.AsNoTracking()
                    .Where(o => o.Type == type && o.Provider == provider)
                     .GroupBy(o => o.Name)
                    .Select(o => o.Key)
                    .ToListAsync();
        }



        public virtual async Task<bool> ExistPermission(string name, string type, string provider)
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

        public virtual async Task<IEnumerable<string>> FindPermissions(IEnumerable<string> types, IEnumerable<string> providers)
        {
            if (providers == null
                || providers.Count() == 0
                || types == null 
                || types.Count() == 0)
            {
                return _emptyPermissionNames;
            }

            return await Query.AsNoTracking()
                    .Where(o => types.Contains(o.Type) && providers.Contains(o.Provider))
                    .GroupBy(o => o.Name)
                    .Select(o => o.Key)
                    .ToListAsync();
        }

        public virtual async Task<IEnumerable<string>> FindPermissions(string type, IEnumerable<string> providers)
        {
            if (providers == null || providers.Count() == 0 || string.IsNullOrWhiteSpace(type))
            {
                return _emptyPermissionNames;
            }

            return await Query.AsNoTracking()
                    .Where(o => o.Type == type && providers.Contains(o.Provider))
                    .GroupBy(o => o.Name)
                    .Select(o => o.Key)
                    .ToListAsync();
        }



        protected virtual Task SaveChanges(CancellationToken cancellationToken)
        {
            return AutoSaveChanges ? Context.SaveChangesAsync(cancellationToken) : Task.CompletedTask;
        }

        /// <summary>
        /// Throws if this class has been disposed.
        /// </summary>
        protected virtual void ThrowIfDisposed()
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
