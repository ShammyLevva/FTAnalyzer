﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public interface IDisplaySource
    {
        string SourceID { get; }
        string Publication { get; }
        string Author { get; }
        string SourceText { get; }
        string SourceTitle { get; }
        string SourceMedium { get; }
    }
}