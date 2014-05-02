using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace FTAnalyzer
{
    public class DisplayFact : IDisplayFact, IComparable
    {
        public Image Icon { get; private set; }
        public string Surname { get; private set; }
        public string Forenames { get; private set; }
        public Individual Ind { get; private set; }
        public Fact Fact { get; set; }
        public Color BackColour { get; set; }

        public DisplayFact(Individual ind, Fact fact) : this(ind, ind.Surname, ind.Forenames, fact) { }
        public DisplayFact(Individual ind, string surname, string forenames, Fact fact)
        {
            this.Ind = ind;
            this.Surname = surname;
            this.Forenames = forenames;
            this.Fact = fact;
            this.Icon = FactImage.ErrorIcon(fact.FactErrorLevel).Icon;
        }

        public string TypeOfFact { get { return Fact.FactTypeDescription; } }
        public FactDate FactDate { get { return Fact.FactDate; } }
        public FactLocation Location { get { return Fact.Location; } }
        public IList<FactSource> Sources { get { return Fact.Sources; } }
        public double Latitude { get { return Fact.Location.Latitude; } }
        public double Longitude { get { return Fact.Location.Longitude; } }
        public string Comment { get { return Fact.Comment; } }
        public string IndividualID { get { return Ind == null ? string.Empty : Ind.IndividualID; } }
        public Age AgeAtFact { get { return Ind == null ? null : Ind.GetAge(Fact.FactDate, Fact.FactType); } }
        public string SourceList { get { return Fact.SourceList;  } }
        public string CensusReference { get { return Fact.CensusReference.Reference; } }
        public string GoogleLocation { get { return Fact.Location.GoogleLocation; } }
        public string GoogleResultType { get { return Fact.Location.GoogleResultType; } }
        public string GeocodeStatus { get { return Fact.Location.Geocoded; } }
        public Image LocationIcon { get { return FactLocationImage.ErrorIcon(Fact.Location.GeocodeStatus).Icon; } }
        public string Relation { get { return Ind == null ? string.Empty : Ind.Relation; } }
        public string RelationToRoot { get { return Ind == null ? string.Empty : Ind.RelationToRoot; } }
        public string SurnameAtDate { get { return Ind == null ? string.Empty : Ind.SurnameAtDate(FactDate); } }
        public bool Preferred { get { return Fact.Preferred; } }
        
        public int CompareTo(object obj)
        {
            DisplayFact that = (DisplayFact)obj;
            if (this.FactDate == that.FactDate)
                return this.Ind.CompareTo(that.Ind);
            return this.FactDate.CompareTo(that.FactDate);
        }

        public override bool Equals(object obj)
        {
            DisplayFact that = (DisplayFact)obj;
            return this.Ind.Equals(that.Ind) && this.Fact.Equals(that.Fact);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return IndividualID + ": " + Forenames + " " + Surname + ", " + Fact.ToString();
        }
    }
}
