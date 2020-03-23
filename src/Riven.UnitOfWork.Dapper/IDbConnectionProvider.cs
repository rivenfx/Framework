using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Riven
{
    public interface IDbConnectionProvider
    {
        /// <summary>
        /// DbContext 标识名称,默认为空字符串
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 配置函数
        /// </summary>
        Func<DbConnectionConfiguration,IDbConnection> Configuration { get; }
    }

    public class DbConnectionProvider : IDbConnectionProvider
    {
        public virtual string Name { get; protected set; }

        public virtual Func<DbConnectionConfiguration, IDbConnection> Configuration { get; protected set; }


        public DbConnectionProvider(string name, Func<DbConnectionConfiguration, IDbConnection> configuration)
        {
            Name = name;
            Configuration = configuration;
        }
    }
}
