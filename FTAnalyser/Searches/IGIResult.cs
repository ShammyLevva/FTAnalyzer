using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace FTAnalyzer
{
    public class IGIResult {
        
	    int searchType;
	    Parish parish;
	    String batch;
        String person;
        String gender;
        String spouse;
        String father;
        String mother;
        String birth;
        String christening;
        String death;
        String burial;
        String marriage;
        String link;
        
        public IGIResult(int searchType, ParishBatch pb, string link) {
            this.searchType = searchType;
            this.parish = pb.Parish;
            this.batch = pb.Batch;
    	    this.link = link;
            this.person = link;
            this.gender = "";
            this.spouse = "";
            this.mother = "";
            this.father = "";
            this.birth = "";
            this.christening = "";
            this.death = "";
            this.burial = "";   
            this.marriage = "";
        }
        
        public void updateValue(String field, String value) {
            if(field.IndexOf("Birth") != -1)
                this.birth = value;
            else if(field.IndexOf("Christening") != -1)
                this.christening = value;
            else if(field.IndexOf("Death") != -1)
                this.death = value;
            else if(field.IndexOf("Burial") != -1)
                this.burial = value;
            else if(field.IndexOf("Father") != -1)
                this.father = value;
            else if(field.IndexOf("Mother") != -1)
                this.mother = value;
            else if(field.IndexOf("Spouse") != -1)
                this.spouse = value;
            else if(field.IndexOf("Marriage") != -1)
                this.marriage = value;
        }
        
        public HttpWebRequest URL {
            get { return (HttpWebRequest)WebRequest.Create(link); }
        }
        
        public String Mother {
            // return only the XXXX bits between ">XXXX</a>
            get { return noLink(mother); }
        }

        public String Person {
            get { return person; }
        }

        public String Spouse {
            // return only the XXXX bits between ">XXXX</a>
            get { return noLink(spouse); }
        }

        public String Birth {
            get { return noNBSP(birth); }
        }

        public String Father {
            // return only the XXXX bits between ">XXXX</a>
            get { return noLink(father); }
        }

        public String Burial {
            get { return noNBSP(burial); }
        }

        public String Christening {
            get { return noNBSP(christening); }
        }

        public String Death {
            get { return noNBSP(death); }
        }

        public String Gender {
            get { return gender; }
            set { this.gender = value; }
        }
     
        public String Marriage {
            get { return noNBSP(marriage); }
        }

        public int SearchType
        {
            get { return searchType; }
        }

        public String Batch
        {
            get { return batch; }
            set { this.batch = value; }
        }

        public Parish Parish
        {
            get { return parish; }
            set { this.parish = value; }
        }

        private String noNBSP(String input)
        {
            String temp = input;
            String output = "";
            while (temp.IndexOf("&nbsp;") !=-1) {
                int pos = temp.IndexOf("&nbsp;");
                if(pos > 0)
                    output = output + temp.Substring(0,pos);
                temp = temp.Substring(pos+6);
            }
            if(temp.Length > 0)
                output = output + temp;
            return output;
        }

        private String noLink(String input) {
            String output = input;
            if (input.IndexOf(">") !=-1) {
                int start = input.IndexOf(">") + 1;
                int end = input.IndexOf("</a>") == -1 ? input.Length : input.IndexOf("</a>");
                output = input.Substring(start,end-start);
            }
            return output;
        }
    }
}