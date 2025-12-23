using FTAnalyzer.Filters;
using FTAnalyzer.Forms;
using FTAnalyzer.Mapping;
using FTAnalyzer.Properties;
using FTAnalyzer.Utilities;
using System.ComponentModel;

namespace FTAnalyzer
{
    public partial class MapIndividuals : Form
    {
        readonly FamilyTree ft = FamilyTree.Instance;
        readonly Font italicFont;
        readonly ReportFormHelper reportFormHelper;
        readonly List<MapLocation> locations;
        readonly Form mapForm;

        public MapIndividuals(List<MapLocation> locations, string year, Form mapForm)
        {
            try
            {
                InitializeComponent();
                Top += NativeMethods.TopTaskbarOffset;
                this.mapForm = mapForm;
                this.locations = locations;
                dgIndividuals.AutoGenerateColumns = false;
                dgIndividuals.DataSource = new SortableBindingList<MapLocation>(this.locations);
                reportFormHelper = new ReportFormHelper(this, Text, dgIndividuals, ResetTable, "Map Individuals");
                italicFont = new(dgIndividuals.DefaultCellStyle.Font.FontFamily, FontSettings.Default.FontSize, FontStyle.Italic);
                reportFormHelper.LoadColumnLayout("MapIndividualColumns.xml");
                tsRecords.Text = this.locations.Count + " Records. " + Messages.Hints_Individual;
                MapLocation mostCommon = this.locations.MostCommon();
                string titleText = mostCommon.Location.ToString();
                if (mapForm is TimeLine)
                    titleText += " in " + year;
                Text = this.locations.Count < 2 ? titleText : $"Centred near {titleText}";
                DatabaseHelper.GeoLocationUpdated += new EventHandler(DatabaseHelper_GeoLocationUpdated);
            }
            catch (Exception) { }
        }

        void ResetTable()
        {
            dgIndividuals.Sort(dgIndividuals.Columns["IndividualID"], ListSortDirection.Ascending);
            dgIndividuals.AutoResizeColumns();
        }

        void PrintToolStripButton_Click(object sender, EventArgs e) => reportFormHelper.PrintReport("Map Individuals");

        void PrintPreviewToolStripButton_Click(object sender, EventArgs e) => reportFormHelper.PrintPreviewReport();

        void Facts_TextChanged(object sender, EventArgs e) => reportFormHelper.PrintTitle = Text;

        void MnuExportToExcel_Click(object sender, EventArgs e) => reportFormHelper.DoExportToExcel<MapLocation>();

        void MnuResetColumns_Click(object sender, EventArgs e) => reportFormHelper.ResetColumnLayout("MapIndividualColumns.xml");

        void MnuSaveColumnLayout_Click(object sender, EventArgs e)
        {
            reportFormHelper.SaveColumnLayout("MapIndividualColumns.xml");
            UIHelpers.ShowMessage("Form Settings Saved", "Map Individuals");
        }

        void DgIndividuals_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                MapLocation? loc = (MapLocation?)dgIndividuals.Rows[e.RowIndex].DataBoundItem;
                if(loc is not null)
                    e.ToolTipText = "Geocoding status: " + loc.Location.Geocoded;
            }
        }

        void DgIndividuals_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string? indID = (string?)dgIndividuals.CurrentRow.Cells["IndividualID"].Value;
                if(indID is not null)
                    MainForm.ShowIndividualsFacts(indID);
            }
        }

        void MnuEditLocation_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                MapLocation? loc = (MapLocation?)dgIndividuals.SelectedRows[0].DataBoundItem;
                if (loc is null) return;
                EditLocation editform = new(loc.Location);
                Cursor = Cursors.Default;
                DialogResult result = editform.ShowDialog(this);
                editform.Dispose(); // needs disposed as it is only hidden because it is a modal dialog
                if (mapForm is not null && mapForm.Visible)
                {
                    if (mapForm is TimeLine line)
                        line.RefreshClusters();
                    else if (mapForm is Places places)
                        places.RefreshClusters();
                }
                UpdateIcons(loc.Location);
            }
            catch (Exception) { }
        }

        void UpdateIcons(FactLocation changed)
        {
            foreach (MapLocation loc in locations)
            {
                if (loc.Location.Equals(changed))
                    loc.UpdateIcon();
            }
            dgIndividuals.Refresh();
        }

        void EditLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ft.Geocoding)
            {
                Cursor = Cursors.WaitCursor;
                MapLocation? loc = (MapLocation?)dgIndividuals.CurrentRow.DataBoundItem;
                if(loc is not null)
                    EditLocation(loc.Location);
            }
        }

        void EditLocation(FactLocation loc)
        {
            try
            {
                EditLocation editform = new(loc);
                Cursor = Cursors.Default;
                DialogResult result = editform.ShowDialog(this);
                editform.Dispose(); // needs disposed as it is only hidden because it is a modal dialog
                                    // force refresh of locations from new edited data
                dgIndividuals.Refresh();
            }
            catch (Exception) { }
        }

        void DgIndividuals_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                dgIndividuals.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
        }

        void DatabaseHelper_GeoLocationUpdated(object? location, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => DatabaseHelper_GeoLocationUpdated(location, e)));
                return;
            }
            if (location is FactLocation loc)
                UpdateIcons(loc);
        }

        void MapIndividuals_FormClosed(object sender, FormClosedEventArgs e)
        {
            DatabaseHelper.GeoLocationUpdated -= DatabaseHelper_GeoLocationUpdated;
            Dispose();
        }

        void MapIndividuals_Load(object sender, EventArgs e) => SpecialMethods.SetFonts(this);
    }
}
