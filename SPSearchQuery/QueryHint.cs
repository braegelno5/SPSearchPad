using System;

namespace MunirHusseini.SPSearchPad
{
    [Flags]
    public enum QueryHint
    {
        None = 0,

        OptimizeWithFullTextIndex = 1,

        OptimizeWithPropertyStore = 2,

        UseSqlFirstJoinStrategy = 8,

        AvoidSqlOuterJoins = 16,

        [Obsolete("This hint is provided only for binary backwards compatibility and is no longer applicable.")]
        PropertySelectWithInClause = 16777216,

        [Obsolete("This hint is provided only for binary backwards compatibility and is no longer applicable.")]
        PropertySelectWithTempTable = 33554432,
    }
}