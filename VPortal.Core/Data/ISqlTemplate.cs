using System;
using System.Collections.Generic;
using System.Text;

namespace VPortal.Core.Data
{
    public interface ISQLTemplate
    {
        string Select { get; }
        string IdentitySql { get; }
        string PaginatedSql { get; }
        string Encapsulation { get; }
    }
}
