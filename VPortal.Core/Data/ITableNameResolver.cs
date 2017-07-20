using System;

namespace VPortal.Core.Data
{
    public interface ITableNameResolver
    {
        string ResolveTableName(Type type);
    }
}