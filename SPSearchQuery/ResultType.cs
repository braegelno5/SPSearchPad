using System;

namespace MunirHusseini.SPSearchPad
{
    [Flags]
    public enum ResultType
    {
        DefinitionResults = -2147483648,
        None = 0,
        RelevantResults = 1,
        HighConfidenceResults = 4,
        SpecialTermResults = 8,
        VisualBestBetsResults = 16,
        RefinementResults = 32,
    }
}