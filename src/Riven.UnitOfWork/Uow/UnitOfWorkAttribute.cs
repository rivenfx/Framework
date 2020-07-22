using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

using Riven.Extensions;

namespace Riven.Uow
{
    /// <summary>
    /// This attribute is used to indicate that declaring method is atomic and should be considered as a unit of work.
    /// A method that has this attribute is intercepted, a database connection is opened and a transaction is started before call the method.
    /// At the end of method call, transaction is committed and all changes applied to the database if there is no exception,
    /// otherwise it's rolled back. 
    /// </summary>
    /// <remarks>
    /// This attribute has no effect if there is already a unit of work before calling this method, if so, it uses the same transaction.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
    public class UnitOfWorkAttribute : Attribute
    {
        /// <summary>
        /// Scope option.
        /// </summary>
        public TransactionScopeOption? Scope { get; set; }

        /// <summary>
        /// Is this UOW transactional?
        /// Uses default value if not supplied.
        /// </summary>
        public bool? IsTransactional { get; set; }

        /// <summary>
        /// Timeout of UOW As milliseconds.
        /// Uses default value if not supplied.
        /// </summary>
        public TimeSpan? Timeout { get; set; }

        /// <summary>
        /// If this UOW is transactional, this option indicated the isolation level of the transaction.
        /// Uses default value if not supplied.
        /// </summary>
        public IsolationLevel? IsolationLevel { get; set; }

        /// <summary>
        /// Used to prevent starting a unit of work for the method.
        /// If there is already a started unit of work, this property is ignored.
        /// Default: false.
        /// </summary>
        public bool IsDisabled { get; set; }

        /// <summary>
        /// Creates a new UnitOfWorkAttribute object.
        /// </summary>
        public UnitOfWorkAttribute()
        {

        }

        /// <summary>
        /// Creates a new <see cref="UnitOfWorkAttribute"/> object.
        /// </summary>
        /// <param name="isTransactional">
        /// Is this unit of work will be transactional?
        /// </param>
        public UnitOfWorkAttribute(bool isTransactional)
            : this()
        {
            IsTransactional = isTransactional;
        }

        /// <summary>
        /// Creates a new <see cref="UnitOfWorkAttribute"/> object.
        /// </summary>
        /// <param name="timeout">As milliseconds</param>
        public UnitOfWorkAttribute(int timeout)
            : this()
        {
            Timeout = TimeSpan.FromMilliseconds(timeout);
        }

        /// <summary>
        /// Creates a new <see cref="UnitOfWorkAttribute"/> object.
        /// </summary>
        /// <param name="isTransactional">Is this unit of work will be transactional?</param>
        /// <param name="timeout">As milliseconds</param>
        public UnitOfWorkAttribute(bool isTransactional, int timeout)
            : this()
        {
            IsTransactional = isTransactional;
            Timeout = TimeSpan.FromMilliseconds(timeout);
        }

        /// <summary>
        /// Creates a new <see cref="UnitOfWorkAttribute"/> object.
        /// <see cref="IsTransactional"/> is automatically set to true.
        /// </summary>
        /// <param name="isolationLevel">Transaction isolation level</param>
        public UnitOfWorkAttribute(IsolationLevel isolationLevel)
            : this()
        {
            IsTransactional = true;
            IsolationLevel = isolationLevel;
        }

        /// <summary>
        /// Creates a new <see cref="UnitOfWorkAttribute"/> object.
        /// <see cref="IsTransactional"/> is automatically set to true.
        /// </summary>
        /// <param name="isolationLevel">Transaction isolation level</param>
        /// <param name="timeout">Transaction  timeout as milliseconds</param>
        public UnitOfWorkAttribute(IsolationLevel isolationLevel, int timeout)
            : this()
        {
            IsTransactional = true;
            IsolationLevel = isolationLevel;
            Timeout = TimeSpan.FromMilliseconds(timeout);
        }

        /// <summary>
        /// Creates a new <see cref="UnitOfWorkAttribute"/> object.
        /// <see cref="IsTransactional"/> is automatically set to true.
        /// </summary>
        /// <param name="scope">Transaction scope</param>
        public UnitOfWorkAttribute(TransactionScopeOption scope)
            : this()
        {
            IsTransactional = true;
            Scope = scope;
        }

        /// <summary>
        /// Creates a new <see cref="UnitOfWorkAttribute"/> object.
        /// </summary>
        /// <param name="scope">Transaction scope</param>
        /// <param name="isTransactional">
        /// Is this unit of work will be transactional?
        /// </param>
        public UnitOfWorkAttribute(TransactionScopeOption scope, bool isTransactional)
            : this()
        {
            Scope = scope;
            IsTransactional = isTransactional;
        }

        /// <summary>
        /// Creates a new <see cref="UnitOfWorkAttribute"/> object.
        /// <see cref="IsTransactional"/> is automatically set to true.
        /// </summary>
        /// <param name="scope">Transaction scope</param>
        /// <param name="timeout">Transaction  timeout as milliseconds</param>
        public UnitOfWorkAttribute(TransactionScopeOption scope, int timeout)
            : this()
        {
            IsTransactional = true;
            Scope = scope;
            Timeout = TimeSpan.FromMilliseconds(timeout);
        }

        /// <summary>
        /// Creates a new <see cref="UnitOfWorkAttribute"/> object.
        /// <see cref="IsTransactional"/> is automatically set to true.
        /// </summary>
        /// <param name="scope">Transaction scope</param>
        /// <param name="isolationLevel">Transaction isolation level</param>
        public UnitOfWorkAttribute(TransactionScopeOption scope, IsolationLevel isolationLevel)
            : this()
        {
            IsTransactional = true;
            Scope = scope;
            IsolationLevel = isolationLevel;
        }

        /// <summary>
        /// Creates a new <see cref="UnitOfWorkAttribute"/> object.
        /// <see cref="IsTransactional"/> is automatically set to true.
        /// </summary>
        /// <param name="scope">Transaction scope</param>
        /// <param name="isolationLevel">Transaction isolation level</param>
        /// <param name="timeout">Transaction  timeout as milliseconds</param>
        public UnitOfWorkAttribute(TransactionScopeOption scope, IsolationLevel isolationLevel, int timeout)
            : this()
        {
            IsTransactional = true;
            Scope = scope;
            IsolationLevel = isolationLevel;
            Timeout = TimeSpan.FromMilliseconds(timeout);
        }

        /// <summary>
        /// Creates a new <see cref="UnitOfWorkAttribute"/> object.
        /// </summary>
        /// <param name="scope">Transaction scope</param>
        /// <param name="isTransactional"/>
        /// <param name="timeout">Transaction  timeout as milliseconds</param>
        public UnitOfWorkAttribute(TransactionScopeOption scope, bool isTransactional, int timeout)
            : this(scope, null, isTransactional, timeout, RivenUnitOfWorkConsts.DefaultConnectionStringName)
        {

        }

        /// <summary>
        /// Creates a new <see cref="UnitOfWorkAttribute"/> object.
        /// </summary>
        /// <param name="connectionStringName">数据库连接字符串名称</param>
        public UnitOfWorkAttribute(string connectionStringName)
        {

        }

        /// <summary>
        /// Creates a new <see cref="UnitOfWorkAttribute"/> object.
        /// </summary>
        /// <param name="scope">Transaction scope</param>
        /// <param name="isolationLevel">Transaction isolation level</param>
        /// <param name="isTransactional"/>
        /// <param name="timeout">Transaction  timeout as milliseconds</param>
        /// <param name="connectionStringName">数据库连接字符串名称</param>
        public UnitOfWorkAttribute(TransactionScopeOption scope, IsolationLevel? isolationLevel, bool isTransactional, int timeout, string connectionStringName)
        {
            Scope = scope;
            if (isolationLevel.HasValue)
            {
                this.IsolationLevel = isolationLevel;
            }

            IsTransactional = isTransactional;
            Timeout = TimeSpan.FromMilliseconds(timeout);
        }

        public virtual UnitOfWorkOptions CreateOptions(string connectionStringName = null)
        {
            return new UnitOfWorkOptions
            {
                IsTransactional = IsTransactional,
                IsolationLevel = IsolationLevel,
                Timeout = Timeout,
                Scope = Scope ?? TransactionScopeOption.Required,
                ConnectionStringName = connectionStringName.IsNullOrWhiteSpace() ? RivenUnitOfWorkConsts.DefaultConnectionStringName : connectionStringName
            };
        }
    }
}
