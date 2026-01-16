using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// Optional: set assembly metadata if you want explicit values
// [assembly: AssemblyTitle("FTAnalyzer Unit Tests")]
// [assembly: AssemblyDescription("Unit tests for FTAnalyzer")]
// [assembly: AssemblyCompany("Your Company")]
// [assembly: AssemblyProduct("FTAnalyzer")]

// MSTEST001 recommendation: configure test parallelization
[assembly: Parallelize(Workers = 0, Scope = ExecutionScope.MethodLevel)]