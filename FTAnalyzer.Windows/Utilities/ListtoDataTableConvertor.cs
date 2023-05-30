using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

namespace FTAnalyzer.Utilities
{
    public class ListtoDataTableConvertor
    {
        public static DataTable ToDataTable<T>(List<T> items, DataGridViewColumnCollection cols = null)
        {
            DataTable dataTable = new(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                if (cols is not null)
                {
                    // Only add a column if in the DataGrid and visible.
                    // Use the datagrid caption, not the property name.
                    var dgvCol = cols[prop.Name];
                    if (dgvCol is not null && dgvCol.Visible)
                        dataTable.Columns.Add(dgvCol.HeaderText);
                }
                else
                {
                    //Setting column names as Property names
                    dataTable.Columns.Add(prop.Name);
                }
            }
            if (items is not null)
            {
                foreach (T item in items)
                {
                    var values = new object[dataTable.Columns.Count];
                    int dex = 0; // which column the value goes in may be disconnected from the property index
                    for (int i = 0; i < Props.Length; i++)
                    {
                        // if the property is not in the datagrid or not visible, don't add its value
                        if (cols is null ||
                            (cols[Props[i].Name] is not null && cols[Props[i].Name].Visible))
                        {
                            //inserting property values to datatable rows
                            values[dex++] = Props[i].GetValue(item, null);
                        }
                    }
                    dataTable.Rows.Add(values);
                }
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        //public DataTable ToDataTable<T>(this IList<T> data)
        //{
        //    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
        //    DataTable table = new DataTable();
        //    foreach (PropertyDescriptor prop in properties)
        //        table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        //    foreach (T item in data)
        //    {
        //        DataRow row = table.NewRow();
        //        foreach (PropertyDescriptor prop in properties)
        //            row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
        //        table.Rows.Add(row);
        //    }
        //    return table;
        //}
    }
}
