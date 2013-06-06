using System.ComponentModel;

namespace MunirHusseini.SPSearchPad
{
    public class SearchQuery : Notifiable
    {
        private string _queryText;

        [Description("Gets or sets the text for the search query.")]
        [Browsable(false)]
        public string QueryText
        {
            get
            {
                return _queryText;
            }
            set
            {
                if (value == _queryText)
                {
                    return;
                }
                _queryText = value;
                OnNotifyPropertyChanged("QueryText");
            }
        }

        private bool _enablePhonetic;
        [Description("Specifies whether the phonetic forms of the query terms are used to find matches.")]
        public bool EnablePhonetic
        {
            get { return _enablePhonetic; }
            set
            {
                if (value == _enablePhonetic) return;

                _enablePhonetic = value;
                OnNotifyPropertyChanged("EnablePhonetic");
            }
        }

        private bool _enableNicknames;
        [Description("Specifies whether the exact terms in the search query are used to find matches, or if nicknames are used as well.")]
        public bool EnableNicknames
        {
            get { return _enableNicknames; }
            set
            {
                if (value == _enableNicknames) return;

                _enableNicknames = value;
                OnNotifyPropertyChanged("EnableNicknames");
            }
        }


        private bool _enableStemming;

        [Description("Gets or sets a Boolean value that specifies whether stemming is enabled.")]
        public bool EnableStemming
        {
            get
            {
                return _enableStemming;
            }
            set
            {
                if (value == _enableStemming)
                {
                    return;
                }
                _enableStemming = value;
                OnNotifyPropertyChanged("EnableStemming");
            }
        }

        private int _highlightedSentenceCount;

        [Description("Gets or sets the number of sentences to include in the hit-highlighted summary.")]
        public int HighlightedSentenceCount
        {
            get
            {
                return _highlightedSentenceCount;
            }
            set
            {
                if (value == _highlightedSentenceCount)
                {
                    return;
                }
                _highlightedSentenceCount = value;
                OnNotifyPropertyChanged("HighlightedSentenceCount");
            }
        }

        private bool _ignoreAllNoiseQuery;

        [Description("Specifies whether the search query should execute if the query text contains only noise words.")]
        public bool IgnoreAllNoiseQuery
        {
            get
            {
                return _ignoreAllNoiseQuery;
            }
            set
            {
                if (value == _ignoreAllNoiseQuery)
                {
                    return;
                }
                _ignoreAllNoiseQuery = value;
                OnNotifyPropertyChanged("IgnoreAllNoiseQuery");
            }
        }

        private KeywordInclusion _keywordInclusion;

        [Description("Specifies whether the query returns results that contain all or any of the specified search terms.")]
        public KeywordInclusion KeywordInclusion
        {
            get
            {
                return _keywordInclusion;
            }
            set
            {
                if (value != _keywordInclusion)
                {
                    _keywordInclusion = value;
                    OnNotifyPropertyChanged("KeywordInclusion");
                }
            }
        }

        private QueryHint _hint;

        [Description("Suggested query processor behavior for the query.")]
        public QueryHint Hint
        {
            get
            {
                return _hint;
            }
            set
            {
                if (value != _hint)
                {
                    _hint = value;
                    OnNotifyPropertyChanged("Hint");
                }
            }
        }

        private ResultType _resultTypes;

        [Description("Specifies the search result type.")]
        public ResultType ResultTypes
        {
            get
            {
                return _resultTypes;
            }
            set
            {
                if (value == _resultTypes)
                {
                    return;
                }
                _resultTypes = value;
                OnNotifyPropertyChanged("ResultTypes");
            }
        }

        private int _rowLimit;

        [Description("The maximum number of rows returned in the search results.")]
        public int RowLimit
        {
            get
            {
                return _rowLimit;
            }
            set
            {
                if (value == _rowLimit)
                {
                    return;
                }
                _rowLimit = value;
                OnNotifyPropertyChanged("RowLimit");
            }
        }

        private int _startRow;

        [Browsable(false)]
        public int StartRow
        {
            get
            {
                return _startRow;
            }
            set
            {
                if (value == _startRow)
                {
                    return;
                }
                _startRow = value;
                OnNotifyPropertyChanged("StartRow");
            }
        }

        private int _timeout;

        [Description("The amount of time, in milliseconds, before the query request times out.")]
        public int Timeout
        {
            get
            {
                return _timeout;
            }
            set
            {
                if (value == _timeout)
                {
                    return;
                }
                _timeout = value;
                OnNotifyPropertyChanged("Timeout");
            }
        }

        private bool _trimDuplicates;

        [Description("Specifies whether duplicate items should be removed from the search results. For FAST Search Server 2010 for SharePoint, this property can also be used to collapse hits in the result set.")]
        public bool TrimDuplicates
        {
            get
            {
                return _trimDuplicates;
            }
            set
            {
                if (value == _trimDuplicates)
                {
                    return;
                }
                _trimDuplicates = value;
                OnNotifyPropertyChanged("TrimDuplicates");
            }
        }
    }
}