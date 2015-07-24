using System.Collections.Generic;
using System.Drawing;

namespace Printing.DataGridViewPrint
{
    /// <author>Blaise Braye</author>
    /// <summary>
    /// GridDrawer is able to draw object inherited from this class,
    /// a common use case for GridDraw is to call GetSize method first,
    /// then setting a Rectangle in which the Draw method will be allowed to print.
    /// It's usefull for everyone because it allows to defines some blocks to be printed
    /// without modifying library core
    /// </summary>
    public abstract class PrintBlock
    {
        public virtual RectangleF Rectangle { get; set; }

        public abstract SizeF GetSize(Graphics g, DocumentMetrics metrics);

        public abstract void Draw(Graphics g, Dictionary<CodeEnum, string> codes);

    }
}
