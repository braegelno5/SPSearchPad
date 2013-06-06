using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Search.Query;

namespace MunirHusseini.SPSearchPad
{
    public class QuerySession : Notifiable
    {
        public SearchQuery Query { get; private set; }

        private QueryResult _results;

        public QueryResult Result
        {
            get
            {
                return _results;
            }
            set
            {
                if (value == _results)
                {
                    return;
                }

                _results = value;
                OnNotifyPropertyChanged("Result");
            }
        }

        private string _url;

        public string Url
        {
            get
            {
                return _url;
            }
            set
            {
                if (value == _url) return;

                _url = value;
                OnNotifyPropertyChanged("Url");
            }
        }

        public Command ExecuteCommand { get; private set; }

        public Command NextCommand { get; private set; }

        public Command PrevCommand { get; private set; }

        private Visibility _statusBarVisibility;
        public Visibility StatusBarVisibility
        {
            get { return _statusBarVisibility; }
            private set
            {
                if (value == _statusBarVisibility) return;

                _statusBarVisibility = value;
                OnNotifyPropertyChanged("StatusBarVisibility");
            }
        }

        private Visibility _waitSpinnerVisibility;
        public Visibility WaitSpinnerVisibility
        {
            get { return _waitSpinnerVisibility; }
            private set
            {
                if (value == _waitSpinnerVisibility) return;

                _waitSpinnerVisibility = value;
                OnNotifyPropertyChanged("WaitSpinnerVisibility");
            }
        }


        public QuerySession()
        {
            WaitSpinnerVisibility = Visibility.Hidden;
            StatusBarVisibility = Visibility.Collapsed;

            ExecuteCommand = new Command(Execute);
            NextCommand = new Command(delegate
                {
                    Query.StartRow += Query.RowLimit;
                    Execute();
                });
            PrevCommand = new Command(delegate
                {
                    Query.StartRow -= Query.RowLimit;
                    Execute();
                },
                () => Query.StartRow >= Query.RowLimit);
            PrevCommand = new Command(Execute);

            Url = "http://" + Environment.MachineName;
            Query = new SearchQuery
                             {
                                 EnableStemming = true,
                                 HighlightedSentenceCount = 3,
                                 IgnoreAllNoiseQuery = false,
                                 KeywordInclusion = KeywordInclusion.AnyKeyword,
                                 Hint = QueryHint.OptimizeWithPropertyStore,
                                 ResultTypes = ResultType.RelevantResults,
                                 RowLimit = 10,
                                 StartRow = 0,
                                 Timeout = 10000,
                                 TrimDuplicates = true,
                                 QueryText = @"SELECT WorkId,Path,Title,Write,Author,HitHighlightedSummary,
       HitHighlightedProperties,CollapsingStatus
FROM Scope()
WHERE FREETEXT(defaultproperties, 'SharePoint') 
ORDER BY Rank Desc"
                             };
        }

        private void Execute()
        {
            StatusBarVisibility = Visibility.Collapsed;
            WaitSpinnerVisibility = Visibility.Visible;

            try
            {
                ThreadPool.QueueUserWorkItem(ExecuteCore);
            }
            catch
            {
                StatusBarVisibility = Visibility.Visible;
                WaitSpinnerVisibility = Visibility.Hidden;
            }
        }

        private void ExecuteCore(object dummy)
        {
            IEnumerable<ResultTable> tables = null;
            string errors = null;
            var elapsedTime = 0;

            try
            {
                using (var site = new SPSite(Url))
                using (var query = new FullTextSqlQuery(site)
                                       {
                                           QueryText = Query.QueryText,
                                           EnableStemming = Query.EnableStemming,
                                           HighlightedSentenceCount = Query.HighlightedSentenceCount,
                                           IgnoreAllNoiseQuery = Query.IgnoreAllNoiseQuery,
                                           KeywordInclusion = (Microsoft.SharePoint.Search.Query.KeywordInclusion)(int)Query.KeywordInclusion,
                                           Hint = (Microsoft.SharePoint.Search.Query.QueryHint)(int)Query.Hint,
                                           ResultTypes = (Microsoft.SharePoint.Search.Query.ResultType)(int)Query.ResultTypes,
                                           RowLimit = Query.RowLimit,
                                           StartRow = Query.StartRow,
                                           Timeout = Query.Timeout,
                                           TrimDuplicates = Query.TrimDuplicates,
                                           EnableNicknames = Query.EnableNicknames,
                                           EnablePhonetic = Query.EnablePhonetic
                                       })
                {
                    var r = query.Execute();
                    var rt = r.Cast<Microsoft.SharePoint.Search.Query.ResultTable>();
                    elapsedTime = r.ElapsedTime;
                    tables = from t in rt
                             select new ResultTable
                                        {
                                            Table = t.Table,
                                            TotalRows = t.TotalRows,
                                            TotalPages = (int)Math.Ceiling((double)t.TotalRows / Query.RowLimit),
                                            Page = t.TotalRows == 0 ? 0 : (Query.StartRow / Query.RowLimit) + 1
                                        };
                }
            }
            catch (Exception ex)
            {
                errors = ex.Message;
            }

            Result = new QueryResult(tables, errors)
                              {
                                  ElapsedTime = elapsedTime
                              };

            StatusBarVisibility = Visibility.Visible;
            WaitSpinnerVisibility = Visibility.Hidden;
        }
    }
}