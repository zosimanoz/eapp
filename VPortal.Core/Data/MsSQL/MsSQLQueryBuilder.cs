using System;
using System.Collections.Generic;
using System.Text;

namespace VPortal.Core.Data.MsSQL
{
    public sealed class MsSQLQueryBuilder : QueryBuilder
    {
        public MsSQLQueryBuilder(ISQLTemplate template, ITableNameResolver tblresolver, IColumnNameResolver colresolver) 
          : base(template, tblresolver, colresolver)
        {
        }

        public MsSQLQueryBuilder(ISQLTemplate template)
         : base(template)
        {
        }
    }
}
