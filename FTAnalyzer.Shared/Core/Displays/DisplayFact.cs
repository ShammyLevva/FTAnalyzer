using System;
using System.Collections.Generic;
#if !__MAC__
using System.Drawing;
#endif

namespace FTAnalyzer
{
    public class DisplayFact : IDisplayFact, IComparable
    {
#if __MAC__
        public Image Icon { get; private set; }
#else
        public Image Icon { get; private set; }
#endif
        public string Surname { get; private set; }
        public string Forenames { get; private set; }
        public Individual Ind { get; private set; }
        public Fact Fact { get; set; }
#if __MAC__
        public Color BackColour { get; set; }
#else
        public Color BackColour { get; set; }
#endif
        public DisplayFact(Individual ind, Fact fact) : this(ind, ind.Surname, ind.Forenames, fact) { }
        public DisplayFact(Individual ind, string surname, string forenames, Fact fact)
        {
            this.Ind = ind;
            this.Surname = surname;
            this.Forenames = forenames;
            this.Fact = fact;
            this.IgnoreFact = false;
            this.Icon = FactImage.ErrorIcon(fact.FactErrorLevel).Icon;
        }
        public FactDate DateofBirth { get { return Ind == null ? FactDate.UNKNOWN_DATE : Ind.BirthDate; } }
        public string TypeOfFact { get { return Fact.FactTypeDescription; } }
        public FactDate FactDate { get { return Fact.FactDate; } }
        public FactLocation Location { get { return Fact.Location; } }
        public IList<FactSource> Sources { get { return Fact.Sources; } }
        public double Latitude { get { return Fact.Location.Latitude; } }
        public double Longitude { get { return Fact.Location.Longitude; } }
        public string Comment { get { return Fact.Comment; } }
        public string IndividualID { get { return Ind == null ? string.Empty : Ind.IndividualID; } }
        public Age AgeAtFact { get { return Ind?.GetAge(Fact.FactDate, Fact.FactType); } }
        public string SourceList { get { return Fact.SourceList;  } }
        public CensusReference CensusReference { get { return Fact.CensusReference; } }
        public string CensusRefYear { get { return Fact.CensusReference.CensusYear.IsKnown ? Fact.CensusReference.CensusYear.ToString() : string.Empty; } }
        public string FoundLocation { get { return Fact.Location.FoundLocation; } }
        public string FoundResultType { get { return Fact.Location.FoundResultType; } }
        public string GeocodeStatus { get { return Fact.Location.Geocoded; } }
        public Image LocationIcon { get { return FactLocationImage.ErrorIcon(Fact.Location.GeocodeStatus).Icon; } }
        public string Relation { get { return Ind == null ? string.Empty : Ind.Relation; } }
        public string RelationToRoot { get { return Ind == null ? string.Empty : Ind.RelationToRoot; } }
        public string SurnameAtDate { get { return Ind == null ? string.Empty : Ind.SurnameAtDate(FactDate); } }
        public bool Preferred { get { return Fact.Preferred; } }
        public bool IgnoreFact { get; set; }
        public string FactHash { get { return Ind == null ? Fact.Preferred + Fact.FactTypeDescription + Fact.DateString + Fact.Location.GEDCOMLocation :
                                                            Ind.IndividualID + Fact.Preferred + Fact.FactTypeDescription + Fact.DateString + Fact.Location.GEDCOMLocation; } }
        
        public int CompareTo(object obj)
        {
            DisplayFact that = (DisplayFact)obj;
            if (this.FactDate == that.FactDate && Ind != null)
                return this.Ind.CompareTo(that.Ind);
            return this.FactDate.CompareTo(that.FactDate);
        }

        public override bool Equals(object obj)
        {
            DisplayFact that = (DisplayFact)obj;
            return this.FactHash.Equals(that.FactHash); //this.Ind.Equals(that.Ind) && this.Fact.Equals(that.Fact);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return Ind == null ? Fact.FactTypeDescription + ": " + Fact.FactDate + " " + Fact.Comment 
                               : IndividualID + ": " + Forenames + " " + Surname + ", " + Fact.ToString();
        }
    }
}
