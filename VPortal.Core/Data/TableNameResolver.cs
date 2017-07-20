﻿using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Linq;
using System.Reflection;
using VPortal.Core.Data.Crud.Attributes;

namespace VPortal.Core.Data
{
    internal class TableNameResolver : ITableNameResolver
    {
        private ISQLTemplate SqlTemplate { get; set; }
        private QueryBuilder Builder { get; }

        public TableNameResolver(ISQLTemplate template, QueryBuilder queryBuilder)
        {
            SqlTemplate = template;
            Builder = queryBuilder;
        }

        public string ResolveTableName(Type type)
        {
            var tableName = Builder.Encapsulate(type.Name);

            var tableattr = type.GetTypeInfo().GetCustomAttributes(true).SingleOrDefault(attr => attr.GetType().Name == typeof(TableAttribute).Name) as dynamic;
            if (tableattr != null)
            {
                tableName = Builder.Encapsulate(tableattr.Name);
                try
                {
                    if (!String.IsNullOrEmpty(tableattr.Schema))
                    {
                        string schemaName = Builder.Encapsulate(tableattr.Schema);
                        tableName = String.Format("{0}.{1}", schemaName, tableName);
                    }
                }
                catch (RuntimeBinderException)
                {
                    //Schema doesn't exist on this attribute.
                }
            }

            return tableName;
        }
    }
}