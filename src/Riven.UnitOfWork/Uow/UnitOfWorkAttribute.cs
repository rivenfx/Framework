using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

using Riven.Extensions;

namespace Riven.Uow
{
    /// <summary>
    /// 此属性用于指示方法声明是原子性的，应该将其视为一个工作单元。
    /// 拦截具有此属性的方法，在调用该方法之前打开数据库连接并启动事务。
    /// 在方法调用结束时，提交事务并将所有更改应用到数据库(如果没有异常)，否则将回滚。
    /// </summary>
    /// <remarks>
    /// 如果在调用此方法之前已经有一个工作单元，则此属性无效，它使用相同的事务。
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
    public class UnitOfWorkAttribute : Attribute
    {
        /// <summary>
        /// 工作单元事务范围,默认 <see cref="TransactionScopeOption.Required"/>
        /// </summary>
        public TransactionScopeOption? Scope { get; set; }

        /// <summary>
        /// uow 是否启用事务。
        /// 如果未设置则使用默认值
        /// </summary>
        public bool? IsTransactional { get; set; }

        /// <summary>
        /// uow 超时时间，单位为毫秒。
        /// 如果未设置则使用默认值
        /// </summary>
        public TimeSpan? Timeout { get; set; }

        /// <summary>
        /// 如果此uow启用事务性，则此选项指示事务的隔离级别。如果没有提供，则使用默认值。
        /// </summary>
        public IsolationLevel? IsolationLevel { get; set; }

        /// <summary>
        /// 标识方法不启用工作单元。
        /// 如果已经启动了一个工作单元，此属性将不生效。
        /// 默认值:false。
        /// </summary>
        public bool IsDisabled { get; set; }

        /// <summary>
        /// 创建一个新的<see cref="UnitOfWorkAttribute"/> 对象.
        /// </summary>
        public UnitOfWorkAttribute()
        {

        }

        /// <summary>
        /// 创建一个新的<see cref="UnitOfWorkAttribute"/> 对象.
        /// </summary>
        /// <param name="isTransactional">
        /// 是否启用事务
        /// </param>
        public UnitOfWorkAttribute(bool isTransactional)
            : this()
        {
            IsTransactional = isTransactional;
        }

        /// <summary>
        /// 创建一个新的<see cref="UnitOfWorkAttribute"/> 对象.
        /// </summary>
        /// <param name="timeout">uow 超时时间 (毫秒)</param>
        public UnitOfWorkAttribute(int timeout)
            : this()
        {
            Timeout = TimeSpan.FromMilliseconds(timeout);
        }

        /// <summary>
        /// 创建一个新的<see cref="UnitOfWorkAttribute"/> 对象.
        /// </summary>
        /// <param name="isTransactional">是否启用事务</param>
        /// <param name="timeout">uow 超时时间 (毫秒)</param>
        public UnitOfWorkAttribute(bool isTransactional, int timeout)
            : this()
        {
            IsTransactional = isTransactional;
            Timeout = TimeSpan.FromMilliseconds(timeout);
        }

        /// <summary>
        /// 创建一个新的<see cref="UnitOfWorkAttribute"/> 对象.
        /// <see cref="IsTransactional"/> 自动设置为true.
        /// </summary>
        /// <param name="isolationLevel">事务隔离级别</param>
        public UnitOfWorkAttribute(IsolationLevel isolationLevel)
            : this()
        {
            IsTransactional = true;
            IsolationLevel = isolationLevel;
        }

        /// <summary>
        /// 创建一个新的<see cref="UnitOfWorkAttribute"/> 对象.
        /// <see cref="IsTransactional"/> 自动设置为true.
        /// </summary>
        /// <param name="isolationLevel">事务隔离级别</param>
        /// <param name="timeout">事务超时(以毫秒为单位)</param>
        public UnitOfWorkAttribute(IsolationLevel isolationLevel, int timeout)
            : this()
        {
            IsTransactional = true;
            IsolationLevel = isolationLevel;
            Timeout = TimeSpan.FromMilliseconds(timeout);
        }

        /// <summary>
        /// 创建一个新的<see cref="UnitOfWorkAttribute"/> 对象.
        /// <see cref="IsTransactional"/> 自动设置为true.
        /// </summary>
        /// <param name="scope">事务范围</param>
        public UnitOfWorkAttribute(TransactionScopeOption scope)
            : this()
        {
            IsTransactional = true;
            Scope = scope;
        }

        /// <summary>
        /// 创建一个新的<see cref="UnitOfWorkAttribute"/> 对象.
        /// </summary>
        /// <param name="scope">事务范围</param>
        /// <param name="isTransactional">
        /// 是否启用事务
        /// </param>
        public UnitOfWorkAttribute(TransactionScopeOption scope, bool isTransactional)
            : this()
        {
            Scope = scope;
            IsTransactional = isTransactional;
        }

        /// <summary>
        /// 创建一个新的<see cref="UnitOfWorkAttribute"/> 对象.
        /// <see cref="IsTransactional"/> 自动设置为true.
        /// </summary>
        /// <param name="scope">事务范围</param>
        /// <param name="timeout">事务超时(以毫秒为单位)</param>
        public UnitOfWorkAttribute(TransactionScopeOption scope, int timeout)
            : this()
        {
            IsTransactional = true;
            Scope = scope;
            Timeout = TimeSpan.FromMilliseconds(timeout);
        }

        /// <summary>
        /// 创建一个新的<see cref="UnitOfWorkAttribute"/> 对象.
        /// <see cref="IsTransactional"/> 自动设置为true.
        /// </summary>
        /// <param name="scope">事务范围</param>
        /// <param name="isolationLevel">事务隔离级别</param>
        public UnitOfWorkAttribute(TransactionScopeOption scope, IsolationLevel isolationLevel)
            : this()
        {
            IsTransactional = true;
            Scope = scope;
            IsolationLevel = isolationLevel;
        }

        /// <summary>
        /// 创建一个新的<see cref="UnitOfWorkAttribute"/> 对象.
        /// <see cref="IsTransactional"/> 自动设置为true.
        /// </summary>
        /// <param name="scope">事务范围</param>
        /// <param name="isolationLevel">事务隔离级别</param>
        /// <param name="timeout">事务超时(以毫秒为单位)</param>
        public UnitOfWorkAttribute(TransactionScopeOption scope, IsolationLevel isolationLevel, int timeout)
            : this()
        {
            IsTransactional = true;
            Scope = scope;
            IsolationLevel = isolationLevel;
            Timeout = TimeSpan.FromMilliseconds(timeout);
        }

        /// <summary>
        /// 创建一个新的<see cref="UnitOfWorkAttribute"/> 对象.
        /// </summary>
        /// <param name="scope">事务范围</param>
        /// <param name="isTransactional"/>
        /// <param name="timeout">事务超时(以毫秒为单位)</param>
        public UnitOfWorkAttribute(TransactionScopeOption scope, bool isTransactional, int timeout)
            : this(scope, null, isTransactional, timeout)
        {

        }


        /// <summary>
        /// 创建一个新的<see cref="UnitOfWorkAttribute"/> 对象.
        /// </summary>
        /// <param name="scope">事务范围</param>
        /// <param name="isolationLevel">事务隔离级别</param>
        /// <param name="isTransactional"/>
        /// <param name="timeout">事务超时(以毫秒为单位)</param>
        public UnitOfWorkAttribute(TransactionScopeOption scope, IsolationLevel? isolationLevel, bool isTransactional, int timeout)
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
                ConnectionStringName = connectionStringName
            };
        }
    }
}
