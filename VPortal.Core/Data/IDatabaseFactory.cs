using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using VPortal.Core.Data.Crud;

namespace VPortal.Core.Data
{
    public interface IDatabaseFactory : IDisposable
    {
        IDbConnection Db { get; }
        Dialect Dialect { get; }
        QueryBuilder QueryBuilder { get; }
        IDbConnection GetConnection();
    }
}
