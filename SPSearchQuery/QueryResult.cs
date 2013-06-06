using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace MunirHusseini.SPSearchPad
{
    public class QueryResult : Notifiable
    {
        public ResultTable[] ResultTables { get; private set; }

        public ResultTable FirstResultTable { get; private set; }

        public Visibility TablesVisibility { get; private set; }

        public Visibility ErrorsVisibility { get; private set; }

        public int ElapsedTime { get; internal set; }

        public string Errors { get; private set; }

        public QueryResult(IEnumerable<ResultTable> resultTables, string errors)
        {
            if (!string.IsNullOrEmpty(errors))
            {
                TablesVisibility = Visibility.Collapsed;
                ErrorsVisibility = Visibility.Visible;
                Errors = errors;
            }
            else
            {
                TablesVisibility = Visibility.Visible;
                ErrorsVisibility = Visibility.Collapsed;
                ResultTables = resultTables.ToArray();
                FirstResultTable = ResultTables.FirstOrDefault();
            }
        }
    }
}