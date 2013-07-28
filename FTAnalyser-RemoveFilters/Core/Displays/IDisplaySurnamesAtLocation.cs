using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public interface IDisplaySurnamesAtLocation : IDisplayLocation
    {
        List<string> Surnames { get; }
    }
}
