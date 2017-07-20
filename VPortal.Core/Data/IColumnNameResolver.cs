using System.Reflection;

namespace VPortal.Core.Data
{
    public interface IColumnNameResolver
    {
        string ResolveColumnName(PropertyInfo propertyInfo);
    }
}