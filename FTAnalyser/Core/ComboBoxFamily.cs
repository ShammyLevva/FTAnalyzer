using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class ComboBoxFamily : Family
    {
        public ComboBoxFamily(Family family)
            : base(family)
        { }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            if (Husband != null)
                result.Append(Husband.Name);
            if(Wife != null)
                result.Append(Husband == null ? Wife.Name : " and " + Wife.Name);
            return (result.ToString() + " " + base.ToString()).Trim();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(ComboBoxFamily))
                return false;
            Family that = obj as Family;
            return this.FamilyID.Equals(that.FamilyID);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
