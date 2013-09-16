using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace FTAnalyzer
{
    public class DataErrorGroup
    {
        private static string[] DATAERROR = new string[] 
                    { "Birth after death/burial",
                      "Birth after father aged 90+", 
                      "Birth after mother aged 60+", 
                      "Birth after mothers death", 
                      "Birth more than 9m after fathers death",
                      "Birth before father aged 13", 
                      "Birth before mother aged 13", 
                      "Burial before death", 
                      "Aged more than 110 at death", 
                      "Facts dated before birth", 
                      "Facts dated after death", 
                      "Marriage after death", 
                      "Marriage after spouse's death", 
                      "Marriage before aged 13", 
                      "Marriage before spouse aged 13", 
                      "Lost Cousins tag in non Census Year", 
                      "Lost Cousins tag in non supported Year",
                      "Census date range too wide/Unknown",
                      "Fact error detected"
                    };

        //"Later marriage before previous spouse died"

        public IList<DataError> Errors { get; private set; }
        private int errorNumber;

        public static string ErrorDescription(int errorNumber)
        {
            return DATAERROR[errorNumber];
        }

        public static MessageBoxIcon ErrorIcon(Fact.FactError errorLevel)
        {
            switch (errorLevel)
            {
                case Fact.FactError.GOOD:
                    return MessageBoxIcon.None;
                case Fact.FactError.WARNINGALLOW:
                    return MessageBoxIcon.Warning;
                case Fact.FactError.ERROR:
                    return MessageBoxIcon.Error;
            }
            return MessageBoxIcon.None;
        }

        public DataErrorGroup(int errorNumber, IList<DataError> errors)
        {
            this.errorNumber = errorNumber;
            this.Errors = errors;
        }

        public override string ToString()
        {
            return ErrorDescription(errorNumber);
        }
    }
}
