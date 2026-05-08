using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace App.Application.DesignPatterns.CommandPattern
{
    public class ExcelFile<T>
    {// Receiver
        public readonly List<T> _list;

        public string FileName => $"{typeof(T).Name}.xlsx";

        public string FileType => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        public ExcelFile(List<T> list)
        {
            _list = list;
        }
        // we capsulate this create in invoker
        public byte[] Create()
        {
            var wb = new XLWorkbook();

            var ds = new DataSet();

            ds.Tables.Add(GetTable());

            wb.Worksheets.Add(ds);

            var excelMemory = new MemoryStream();

            wb.SaveAs(excelMemory);

            excelMemory.Position = 0; // ← bunu ekleyin

            return excelMemory.ToArray(); // using içinde ToArray() güvenli
        }

        private DataTable GetTable()
        {
            var table = new DataTable();
            var type = typeof(T);

            // Sadece primitive/value type ve string kolonları ekle
            var properties = type.GetProperties()
                .Where(p =>
                {
                    var t = Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType;
                    return t.IsPrimitive || t == typeof(string) || t == typeof(decimal) || t == typeof(DateTime) || t == typeof(Guid);
                })
                .ToList();

            properties.ForEach(p =>
            {
                var colType = Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType;
                table.Columns.Add(p.Name, colType);
            });

            _list.ForEach(x =>
            {
                try
                {
                    var row = table.NewRow();
                    properties.ForEach(p =>
                    {
                        try
                        {
                            row[p.Name] = p.GetValue(x) ?? DBNull.Value;
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"PROP ERROR - {p.Name}: {ex.Message}");
                        }
                    });
                    table.Rows.Add(row);
                    Debug.WriteLine($"ROW ADDED, row count: {table.Rows.Count}");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"ROW ERROR: {ex.Message}");
                }
            });

            Debug.WriteLine($"FINAL table.Rows.Count: {table.Rows.Count}");

            return table;
        }
    }
}
