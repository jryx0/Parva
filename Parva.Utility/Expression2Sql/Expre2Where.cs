using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Parva.Utility.Expression2Sql
{
    public static class Expre2Where
    {
        public static DatabaseType DatabaseType { get; set; }

        public static void Init(DatabaseType dbType)
        {
            DatabaseType = dbType;
        }


        public static Expression2SqlCore<T> Where<T>(Expression<Func<T, bool>> expression)
        {
            return new Expression2SqlCore<T>(DatabaseType).Where(expression);
        }


    }
}
