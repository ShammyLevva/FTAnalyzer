using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using System.IO;

namespace FTAnalyzer.Utilities
{
    public class ExportToExcel
    {
        public static void Export(System.Data.DataTable table, string filename)
        {
            string q = "\"";
            StreamWriter output = new StreamWriter(filename);
            //am getting my grid's column headers
            int columnscount = table.Columns.Count;

            for (int j = 0; j < columnscount; j++)
            {   //Get column headers  and make it as bold in excel columns
                output.Write(q + table.Columns[j].ColumnName.ToString() + q);
                if (j < columnscount - 1)
                    output.Write(",");
            }
            output.WriteLine();
            foreach (DataRow row in table.Rows)
            {
                //write in new row
                for (int i = 0; i < columnscount; i++)
                {
                    output.Write(q + row[i].ToString() + q);
                    if (i < columnscount - 1)
                        output.Write(",");
                }
                output.WriteLine();
            }
            output.Close();
        }
    }
}
