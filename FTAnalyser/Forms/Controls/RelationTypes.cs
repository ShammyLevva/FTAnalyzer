using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FTAnalyzer.Filters;

namespace FTAnalyzer.Forms.Controls
{
    public partial class RelationTypes : UserControl
    {
        public RelationTypes() => InitializeComponent();

        public bool Directs => ckbDirects.Checked;
        public bool Blood => ckbBlood.Checked;
        public bool Marriage => ckbMarriage.Checked;
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
                    result += Individual.UNKNOWN;
                if (ckbDirects.Checked)
                    result += Individual.DIRECT;
                if (ckbBlood.Checked)
                    result += Individual.BLOOD;
                if (ckbMarriage.Checked)
                    result += Individual.MARRIAGE;
                if (ckbMarriageDB.Checked)
                    result += Individual.MARRIEDTODB;
                if (ckbDescendants.Checked)
                    result += Individual.DESCENDANT;
                if (ckbLinked.Checked)
                    result += Individual.LINKED;
                return result;
            }
        }

        public Predicate<T> BuildFilter<T>(Func<T, int> relationType, bool excludeUnknown = false)
        {
            List<Predicate<T>> relationFilters = new List<Predicate<T>>();
            if (Blood)
                relationFilters.Add(FilterUtils.IntFilter(relationType, Individual.BLOOD));
            if (Directs)
                relationFilters.Add(FilterUtils.IntFilter(relationType, Individual.DIRECT));
            if (Marriage)
                relationFilters.Add(FilterUtils.IntFilter(relationType, Individual.MARRIAGE));
            if (MarriedToDB)
                relationFilters.Add(FilterUtils.IntFilter(relationType, Individual.MARRIEDTODB));
            if (Descendant)
                relationFilters.Add(FilterUtils.IntFilter(relationType, Individual.DESCENDANT));
            if (Linked)
                relationFilters.Add(FilterUtils.IntFilter(relationType, Individual.LINKED));
            if (Unknown && !excludeUnknown)
                relationFilters.Add(FilterUtils.IntFilter(relationType, Individual.UNKNOWN));
            return FilterUtils.OrFilter(relationFilters);
        }

        public Predicate<Family> BuildFamilyFilter<Family>(Func<Family, IEnumerable<int>> relationTypes)
        {
            List<Predicate<Family>> relationFilters = new List<Predicate<Family>>();
            if (Blood)
                relationFilters.Add(FilterUtils.FamilyRelationFilter(relationTypes, Individual.BLOOD));
            if (Directs)
                relationFilters.Add(FilterUtils.FamilyRelationFilter(relationTypes, Individual.DIRECT));
            if (Marriage)
                relationFilters.Add(FilterUtils.FamilyRelationFilter(relationTypes, Individual.MARRIAGE));
            if (MarriedToDB)
                relationFilters.Add(FilterUtils.FamilyRelationFilter(relationTypes, Individual.MARRIEDTODB));
            if (Descendant)
                relationFilters.Add(FilterUtils.FamilyRelationFilter(relationTypes, Individual.DESCENDANT));
            if (Linked)
                relationFilters.Add(FilterUtils.FamilyRelationFilter(relationTypes, Individual.LINKED));
            if (Unknown)
                relationFilters.Add(FilterUtils.FamilyRelationFilter(relationTypes, Individual.UNKNOWN));
            return FilterUtils.OrFilter(relationFilters);
        }

        public event EventHandler RelationTypesChanged;
        protected void OnRelationTypesChanged() => RelationTypesChanged?.Invoke(this, EventArgs.Empty);

        void Tickbox_CheckedChanged(object sender, EventArgs e) => OnRelationTypesChanged();
    }
}
