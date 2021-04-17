using System.ComponentModel;

namespace FTAnalyzer.Forms.Controls
{
    public partial class DateSliders
    {
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class ScoreValues
        {
            public DateSliders Slider { get; private set; }

            public int UnknownDate { get { return Slider.msUnknown.Value; } set { Slider.msUnknown.Value = value; } }
            public int VeryWideDate { get { return Slider.msVeryWide.Value; } set { Slider.msVeryWide.Value = value; } }
            public int WideDate { get { return Slider.msWide.Value; } set { Slider.msWide.Value = value; } }
            public int NarrowDate { get { return Slider.msNarrow.Value; } set { Slider.msNarrow.Value = value; } }
            public int JustYearDate { get { return Slider.msJustYear.Value; } set { Slider.msJustYear.Value = value; } }
            public int ApproxDate { get { return Slider.msApprox.Value; } set { Slider.msApprox.Value = value; } }
            public int ExactDate { get { return Slider.msExact.Value; } set { Slider.msExact.Value = value; } }

            public ScoreValues(DateSliders parent) => Slider = parent;
        }
    }
}
