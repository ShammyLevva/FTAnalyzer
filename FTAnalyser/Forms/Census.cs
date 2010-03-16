using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FTAnalyzer.Forms
{
    public partial class Census : Form
    {
        private Dictionary<int, DataGridViewCellStyle> rowStyles;
        private int numFamilies;

        public Census()
        {
            InitializeComponent();
        }

        public void setupCensus(RegistrationsProcessor rp, FactDate date, 
            bool censusDone, bool includeResidence, int maxAge)
        {
            FamilyTree ft = FamilyTree.Instance;
            List<Registration> regs = ft.getAllCensusRegistrations(date, censusDone, includeResidence);
            List<Registration> census = rp.processRegistrations(regs);
            List<IDisplayCensus> ds = new List<IDisplayCensus>();
            rowStyles = new Dictionary<int, DataGridViewCellStyle>();
            foreach (CensusRegistration r in census)
                foreach (Individual i in r.Members)
                {
                    if (i.getAge(date).MinAge <= maxAge) 
                        ds.Add(new DisplayCensus(r.FamilyGed, r.RegistrationLocation, r.registrationDate, i));
                }
            // ds.sort(new IndividualNameComparator());
            dgCensus.DataSource = ds;
            ResizeColumns();
            StyleRows();
            tsRecords.Text = ds.Count + " Records / " + numFamilies + " Families.";
        }

        private void ResizeColumns()
        {
            foreach (DataGridViewColumn c in dgCensus.Columns)
                c.Width = c.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true);
        }

        private void StyleRows()
        {
            string currentFamilyGed = "";
            bool highlighted = true;
            numFamilies = 0;
            Font boldFont = new Font(dgCensus.DefaultCellStyle.Font, FontStyle.Bold);
            Font regularFont = new Font(dgCensus.DefaultCellStyle.Font, FontStyle.Regular);
            foreach (DataGridViewRow r in dgCensus.Rows)
            {
                DisplayCensus cr = (DisplayCensus)r.DataBoundItem;
                if (cr.FamilyGed != currentFamilyGed)
                {
                    currentFamilyGed = cr.FamilyGed;
                    highlighted = !highlighted;
                    numFamilies++;
                }
                DataGridViewCellStyle style = new DataGridViewCellStyle();
                style.BackColor = highlighted ? Color.LightGray : Color.White;
                style.ForeColor = cr.RelationType == Individual.DIRECT ? Color.Red : Color.Black;
                style.Font = cr.isAlive ? boldFont : regularFont;
                rowStyles.Add(r.Index, style);
            }
        }

        private string GetTooltipText(DataGridViewCellStyle style)
        {
            if (style.Font.Bold && style.ForeColor == Color.Red)
                return "This direct ancestor is known to be alive on this census.";
            else if (style.Font.Bold)
                return "This individual is known to be alive on this census.";
            else if (style.ForeColor == Color.Red)
                return "This is a direct ancestor that may be alive on this census.";
            else
                return string.Empty;
        }

        private void dgCensus_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewCellStyle style = dgCensus.DefaultCellStyle;
            DataGridViewCell cell = dgCensus.Rows[e.RowIndex].Cells[e.ColumnIndex];
            rowStyles.TryGetValue(e.RowIndex, out style);
            if (style != null)
            {
                e.CellStyle.BackColor = style.BackColor;
                e.CellStyle.ForeColor = style.ForeColor;
                e.CellStyle.Font = style.Font;
                cell.ToolTipText = GetTooltipText(style);
            }
        }
    }
}
