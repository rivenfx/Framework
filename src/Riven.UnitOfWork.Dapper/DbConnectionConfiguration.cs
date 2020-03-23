using Riven.Uow;
using System;
using System.Collections.Generic;
using System.Text;

namespace Riven
{
    public class DbConnectionConfiguration
    {


        /// <summary>
        /// 连接字符串
        /// </summary>
        public virtual string ConnectionString { get; protected set; }

        /// <summary>
        /// 工作单元选项
        /// </summary>
        public virtual UnitOfWorkOptions UnitOfWorkOptions { get; protected set; }


        public DbConnectionConfiguration(string connectionString, UnitOfWorkOptions unitOfWorkOptions)
        {
            ConnectionString = connectionString;
            UnitOfWorkOptions = unitOfWorkOptions;
        }
    }
}
