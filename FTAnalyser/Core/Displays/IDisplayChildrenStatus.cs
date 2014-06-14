using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public interface IDisplayChildrenStatus : IDisplayFamily
    {
        int ChildrenTotal { get; }
        int ChildrenAlive { get; }
        int ChildrenDead { get; }
        int ExpectedTotal { get; }
        int ExpectedAlive { get; }
        int ExpectedDead { get; }
    }
}
