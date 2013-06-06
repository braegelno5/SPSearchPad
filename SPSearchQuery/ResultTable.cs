using System.Data;

namespace MunirHusseini.SPSearchPad
{
    public class ResultTable
    {
        public DataTable Table { get; internal set; }

        public int TotalRows { get; internal set; }

        public int TotalPages { get; internal set; }

        public int Page { get; internal set; }
    }
}