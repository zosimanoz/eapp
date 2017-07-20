using System;
using System.Collections.Generic;
using System.Text;

namespace VPortal.Core.Data.MsSQL
{
    public class MsSQLTemplate : ISQLTemplate
    {
        public string Select => $"Select {0} from {1}";

        public string IdentitySql => string.Format("SELECT CAST(SCOPE_IDENTITY()  AS BIGINT) AS [id]");

        //public string PaginatedSql => "SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY {OrderBy}) AS PagedNumber, {SelectColumns} FROM {TableName} {WhereClause}) AS u WHERE PagedNUMBER BETWEEN (({PageNumber}-1) * {RowsPerPage} + 1) AND ({PageNumber} * {RowsPerPage})";
        // for join : SELECT COUNT(1) OVER() AS RowTotal, {SelectColumns} FROM {TableName} {join} {WhereClause} Order By {OrderBy} OFFSET {PageNumber}-1 ROWS FETCH NEXT {RowsPerPage} ROWS ONLY
        public string PaginatedSql => "SELECT COUNT(1) OVER() AS RowTotal, {SelectColumns} FROM {TableName} {WhereClause} Order By {OrderBy} OFFSET {PageNumber}-1 ROWS FETCH NEXT {RowsPerPage} ROWS ONLY";
        public string Encapsulation => "[{0}]";
    }
}
