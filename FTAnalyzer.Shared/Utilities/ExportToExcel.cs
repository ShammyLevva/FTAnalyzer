using System;
using System.Text;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace FTAnalyzer.Utilities
{
    public class ExportToExcel
    {
        public static void Export(DataTable dt)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                string initialDir = (string)Application.UserAppDataRegistry.GetValue("Excel Export Individual Path");
                saveFileDialog.InitialDirectory = initialDir ?? Environment.SpecialFolder.MyDocuments.ToString();
                saveFileDialog.Filter = "Comma Separated Value (*.csv)|*.csv";
                saveFileDialog.FilterIndex = 1;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string path = Path.GetDirectoryName(saveFileDialog.FileName);
                    Application.UserAppDataRegistry.SetValue("Excel Export Individual Path", path);
                    WriteFile(dt, saveFileDialog.FileName);
                    MessageBox.Show("File written to " + saveFileDialog.FileName, "FTAnalyzer");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FTAnalyzer");
            }
        }

        private static void WriteFile(DataTable table, string filename)
        {
            string q = "\"";
            Encoding isoWesternEuropean = Encoding.GetEncoding(28591);
            StreamWriter output = new StreamWriter(new FileStream(filename, FileMode.Create, FileAccess.Write), isoWesternEuropean);
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
                    output.Write(q + row[i].ToString().Replace("\"", "")  + q);
                    if (i < columnscount - 1)
                        output.Write(",");
                }
                output.WriteLine();
            }
            output.Close();
        }
    }
}
