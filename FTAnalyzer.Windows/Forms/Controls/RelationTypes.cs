using FTAnalyzer.Filters;
using FTAnalyzer.Graphics;
using FTAnalyzer.Utilities;
using System.ComponentModel;

namespace FTAnalyzer.Forms.Controls
{
    public partial class RelationTypes : UserControl
    {
        public RelationTypes() => InitializeComponent();

        public bool Directs => ckbDirects.Checked;
        public bool Blood => ckbBlood.Checked;
        public bool Marriage => ckbMarriage.Checked;
        [DefaultValue(true)]
        public bool MarriedToDB { get => ckbMarriageDB.Checked; set => ckbMarriageDB.Checked = value; }
        public bool Unknown => ckbUnknown.Checked;
        public bool Descendant => ckbDescendants.Checked;
        public bool Linked => ckbLinked.Checked;

        public int Status
        {
            get
            {
                int result = 0;
                if (ckbUnknown.Checked)
                    result += (int)RelationshipType.UNKNOWN;
                if (ckbDirects.Checked)
                    result += (int)RelationshipType.DIRECT;
                if (ckbBlood.Checked)
                    result += (int)RelationshipType.BLOOD;
                if (ckbMarriage.Checked)
                    result += (int)RelationshipType.MARRIAGE;
                if (ckbMarriageDB.Checked)
                    result += (int)RelationshipType.MARRIEDTODB;
                if (ckbDescendants.Checked)
                    result += (int)RelationshipType.DESCENDANT;
                if (ckbLinked.Checked)
                    result += (int)RelationshipType.LINKED;
                return result;
            }
        }

        public Predicate<T> BuildFilter<T>(Func<T, int> relationType, bool excludeUnknown = false)
        {
            List<Predicate<T>> relationFilters = [];
            if (Blood)
                relationFilters.Add(FilterUtils.IntFilter(relationType, (int)RelationshipType.BLOOD));
            if (Directs)
                relationFilters.Add(FilterUtils.IntFilter(relationType, (int)RelationshipType.DIRECT));
            if (Marriage)
                relationFilters.Add(FilterUtils.IntFilter(relationType, (int)RelationshipType.MARRIAGE));
            if (MarriedToDB)
                relationFilters.Add(FilterUtils.IntFilter(relationType, (int)RelationshipType.MARRIEDTODB));
            if (Descendant)
                relationFilters.Add(FilterUtils.IntFilter(relationType, (int)RelationshipType.DESCENDANT));
            if (Linked)
                relationFilters.Add(FilterUtils.IntFilter(relationType, (int)RelationshipType.LINKED));
            if (Unknown && !excludeUnknown)
                relationFilters.Add(FilterUtils.IntFilter(relationType, (int)RelationshipType.UNKNOWN));
            return FilterUtils.OrFilter(relationFilters);
        }

        public Predicate<Family> BuildFamilyFilter<Family>(Func<Family, IEnumerable<int>> relationTypes)
        {
            List<Predicate<Family>> relationFilters = [];
            if (Blood)
                relationFilters.Add(FilterUtils.FamilyRelationFilter(relationTypes, (int)RelationshipType.BLOOD));
            if (Directs)
                relationFilters.Add(FilterUtils.FamilyRelationFilter(relationTypes, (int)RelationshipType.DIRECT));
            if (Marriage)
                relationFilters.Add(FilterUtils.FamilyRelationFilter(relationTypes, (int)RelationshipType.MARRIAGE));
            if (MarriedToDB)
                relationFilters.Add(FilterUtils.FamilyRelationFilter(relationTypes, (int)RelationshipType.MARRIEDTODB));
            if (Descendant)
                relationFilters.Add(FilterUtils.FamilyRelationFilter(relationTypes, (int)RelationshipType.DESCENDANT));
            if (Linked)
                relationFilters.Add(FilterUtils.FamilyRelationFilter(relationTypes, (int)RelationshipType.LINKED));
            if (Unknown)
                relationFilters.Add(FilterUtils.FamilyRelationFilter(relationTypes, (int)RelationshipType.UNKNOWN));
            return FilterUtils.OrFilter(relationFilters);
        }

        public event EventHandler? RelationTypesChanged;
        protected void OnRelationTypesChanged() => RelationTypesChanged?.Invoke(this, EventArgs.Empty);

        void Tickbox_CheckedChanged(object sender, EventArgs e) => OnRelationTypesChanged();

        void RelationTypes_Layout(object sender, LayoutEventArgs e) { }

        void GroupBox2_Paint(object sender, PaintEventArgs e)
        {
            GroupBox? box = sender as GroupBox;
            if (box is not null)
                GraphicsUtilities.DrawGroupBox(box, e.Graphics, Color.Black, 2);
        }
    }
}
