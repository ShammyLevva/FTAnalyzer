using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyzer
{
    public class ParentalGroup {

        public Individual Individual { get; private set; }
        public Individual Father { get; private set; }
        public Individual Mother { get; private set; }
        public Fact ParentsMarriage { get; private set; }
        
        public ParentalGroup (Individual i, Individual f, Individual m, Fact marriage) {
            this.Individual = i;
            this.Father = f;
            this.Mother = m;
            this.ParentsMarriage = marriage;
        }

        #region Properties 

        public string Residence {
            get
            {
                Fact residence = Individual.GetPreferredFact(Fact.RESIDENCE);
                return (residence == null) ? "" : residence.Place;
            }
        }
        
        public string FathersName {
            get { return (Father == null) ? ""  : Father.Name; }
        }

        public string MothersName {
            get { return (Mother == null) ? ""  : Mother.Name; }
        }

        public string FathersOccupation {
            get { return (Father == null) ? ""  : Father.Occupation; }
        }

        public string MothersOccupation {
            get { return (Mother == null) ? ""  : Mother.Occupation; }
        }
        
        public string ParentsMarriageDate {
            get { return (ParentsMarriage == null) ? "" : ParentsMarriage.FactDate.DateString; }
        }
        
        public string ParentsMarriageLocation {
            get { return (ParentsMarriage == null) ? "" : ParentsMarriage.Place; }
        }

        #endregion

        public FactLocation BestLocation(FactDate when)
        {
            FactLocation i = Individual.BestLocation(when);
            if (ParentsMarriage == null)
                return i;
            FactLocation f = FactLocation.GetLocation(ParentsMarriage.Place);
            if (f.Level > i.Level)
                return f;
            else
                return i;
        }

        public string IsFatherDeceased(FactDate when)
        {
            return (Father == null || !Father.IsDeceased(when)) ? "" : "(Deceased)";
        }

        public string IsMotherDeceased(FactDate when)
        {
            return (Mother == null || !Mother.IsDeceased(when)) ? "" : "(Deceased)";
        }

        public Fact GetPreferredFact(string factType)
        {
            return Individual.GetPreferredFact(factType);
        }

        public FactDate GetPreferredFactDate(string factType)
        {
            return Individual.GetPreferredFactDate(factType);
        }

    }
}