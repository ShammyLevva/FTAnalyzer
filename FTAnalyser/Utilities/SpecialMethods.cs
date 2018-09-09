using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FTAnalyzer.Utilities
{
    public static class SpecialMethods
    {
        public static IEnumerable<Control> GetAllControls(Control aControl)
        {
            Stack<Control> stack = new Stack<Control>();

            stack.Push(aControl);

            while (stack.Any())
            {
                var nextControl = stack.Pop();

                foreach (Control childControl in nextControl.Controls)
                {
                    stack.Push(childControl);
                }

                yield return nextControl;
            }
        }

        public static void SetFonts(Form form)
        {
            foreach (Control theControl in GetAllControls(form))
                if (theControl.Font.Name.Equals(Properties.FontSettings.Default.SelectedFont.Name))
                    theControl.Font = Properties.FontSettings.Default.SelectedFont;
        }
    }
}