using System;
using System.Collections.Generic;
using System.Text;

namespace Riven.Repositories
{
    public interface ITableNameAccessor
    {
        string GetTableName<T>();

        string GetTableName(Type type);
    }
}
