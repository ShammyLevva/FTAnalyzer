using System.Text.RegularExpressions;

namespace FTAnalyzer.Utilities
{
    internal static partial class RegexPatterns
    {
        const string CHILDREN_STATUS_PATTERN1 = @"(\d{1,2}) Total ?,? ?(\d{1,2}) (Alive|Living) ?,? ?(\d{1,2}) Dead";
        const string CHILDREN_STATUS_PATTERN2 = @"Total:? (\d{1,2}) ?,? ?(Alive|Living):? (\d{1,2}) ?,? ?Dead:? (\d{1,2})";

        [GeneratedRegex(CHILDREN_STATUS_PATTERN1, RegexOptions.Compiled)]
        public static partial Regex ChildrenStatusPattern1();
        [GeneratedRegex(CHILDREN_STATUS_PATTERN2, RegexOptions.Compiled)]
        public static partial Regex ChildrenStatusPattern2();
    }
}
