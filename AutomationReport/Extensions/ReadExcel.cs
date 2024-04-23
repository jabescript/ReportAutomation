using OfficeOpenXml;
using System.Data;
using System.Reflection;

namespace AutomationReport.Extensions;
public static class ReadExcel
{
    public static List<T> ReadExcelToList<T>(this ExcelWorksheet worksheet) where T : new()
    {
        List<T> collection = new();
        try
        {
            DataTable dt = new();
            foreach (var firstRowCell in new T().GetType().GetProperties().ToList())
            {
                //Add table colums with properties of T
                dt.Columns.Add(firstRowCell.Name);
            }
            for (int rowNum = 2; rowNum <= worksheet.Dimension.End.Row; rowNum++)
            {
                var wsRow = worksheet.Cells[rowNum, 1, rowNum, worksheet.Dimension.End.Column];
                DataRow row = dt.Rows.Add();
                foreach (var cell in wsRow)
                {
                    row[cell.Start.Column - 1] = cell.Text;
                }
            }

            //Get the colums of table
            var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToList();

            //Get the properties of T
            List<PropertyInfo> properties = new T().GetType().GetProperties().ToList();

            collection = dt.AsEnumerable().Select(row =>
            {
                T item = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name) || columnNames.Contains(pro.Name.ToUpper()))
                    {
                        PropertyInfo pI = item.GetType().GetProperty(pro.Name);
                        pro.SetValue(item, (row[pro.Name] == DBNull.Value) ? null : Convert.ChangeType(row[pro.Name], (Nullable.GetUnderlyingType(pI.PropertyType) == null) ? pI.PropertyType : Type.GetType(pI.PropertyType.GenericTypeArguments[0].FullName)));
                    }
                }
                return item;
            }).ToList();

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        return collection;
    }
}
