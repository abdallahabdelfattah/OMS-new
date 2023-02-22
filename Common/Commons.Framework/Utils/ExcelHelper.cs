using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons.Framework.Utils
{
    /// <summary>
    /// this helper helps you in converting any Excel file into Data Table .
    /// Note: Excel file must be Xlsx extension.
    /// </summary>
    public static class ExcelHelper
    {
        public static List<DataTable> ImportExcel(string filePath, Dictionary<int, int> sheetsIndexNumbersAndStartFrom)
        {
            var result = new List<DataTable>();
            using (SpreadsheetDocument doc = SpreadsheetDocument.Open(filePath, false, new OpenSettings { }))
            {
                foreach (var sheetInfo in sheetsIndexNumbersAndStartFrom)
                {
                    //Read the first Sheet from Excel file.
                    Sheet sheet = (Sheet)doc.WorkbookPart.Workbook.Sheets.ChildElements[sheetInfo.Key];
                    //Get the Worksheet instance.
                    Worksheet worksheet = (doc.WorkbookPart.GetPartById(sheet.Id.Value) as WorksheetPart).Worksheet;
                    //Fetch all the rows present in the Worksheet.
                    IEnumerable<Row> rows = worksheet.GetFirstChild<SheetData>().Descendants<Row>();
                    DataTable dt = new DataTable();
                    //Loop through the Worksheet rows.


                    var index = 0;
                    foreach (Row row in rows)
                    {
                        if (sheetInfo.Value > index)
                        {
                            index++;
                            continue;
                        }
                        //Use the first row to add columns to DataTable.
                        if (row.RowIndex.Value == sheetInfo.Value + 1)
                        {
                            foreach (Cell cell in row.Descendants<Cell>())
                            {
                                dt.Columns.Add(GetValue(doc, cell));
                            }
                            break;
                        }
                    }


                    var rowsWithoutHeader = rows.Where(x => x.RowIndex > sheetInfo.Value + 1).ToList();
                    foreach (Row row in rowsWithoutHeader)
                    {
                        dt.Rows.Add();
                        var currentIndex = dt.Rows.Count - 1;
                        var cells = row.Descendants<Cell>().ToList();
                        int r = 0;
                        foreach (Cell cell in cells)
                        {
                            dt.Rows[currentIndex][r] = GetValue(doc, cell);
                            r++;
                        }
                    }

                    result.Add(dt);
                }
            }

            return result;
        }

        private static string GetValue(SpreadsheetDocument doc, Cell cell)
        {
            string value = string.Empty;
            if (cell.CellValue != null)
            {
                value = cell.CellValue.InnerText;
                if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
                {
                    return doc.WorkbookPart.SharedStringTablePart.SharedStringTable.ChildElements.GetItem(int.Parse(value)).InnerText.Trim();
                }
            }
            return value.Trim();
        }
    }
}
