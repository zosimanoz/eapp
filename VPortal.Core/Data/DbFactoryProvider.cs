using System;
using System.Collections.Generic;
using System.Text;

namespace VPortal.Core.Data
{
    public static class DbFactoryProvider
    {
        private static IDatabaseFactory _currentDbFactory;
        
        public static void SetCurrentDbFactory(IDatabaseFactory dbFactory)
        {
            _currentDbFactory = dbFactory;
        }

        public static IDatabaseFactory GetFactory()
        {
            if (_currentDbFactory == null)
            {
                throw new Exception("Please set first default db factory!");
            }
            return _currentDbFactory;
        }
    }
}
