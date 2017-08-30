using System;
using System.Collections.Generic;
using System.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace Common
{
  public static class ExcelLib
  {
    //-------------------------------------------------------------------------
    /// <summary>Выгрузка коллекции объектов в Excel.
    /// </summary>
    /// <param name="objects">коллекция объектов</param>
    /// <param name="showHeader">формировать заголовок из имен полей</param>
    /// <param name="patternFile">путь к файлу-шаблону - из него берем UsedRange, после которой будут вставлены данные</param>
    public static void ObjectsToExcel(IEnumerable<object> objects, bool showHeader = true, string patternFile = null)
    {
      List<object> list = objects.Where(x => x != null).ToList();
      if (list.Count == 0) return;

      var type = list.FirstOrDefault().GetType();
      list = list.Where(x => x.GetType() == type).ToList();
      if (list.Count == 0) return;

      List<PropertyInfo> props = type.GetProperties().ToList();

      int colCnt = props.Count;
      if (colCnt == 0) return;

      object[,] data = new object[list.Count, colCnt];

      Excel.Application xlsApp = null;
      Excel.Workbook xlsWb = null, wbPatt = null;
      Excel.Worksheet ws = null, wsPatt = null;
      Excel.Range rg = null, rgPatt = null;
      try
      {
        xlsApp = new Excel.Application();
        xlsApp.Visible = false;
        xlsApp.ScreenUpdating = false;
        xlsWb = xlsApp.Workbooks.Add();
        xlsApp.Calculation = Excel.XlCalculation.xlCalculationManual;
        ws = (Excel.Worksheet)xlsWb.Worksheets.Add();
        Excel.Range cell = ws.get_Range("A1");

        if (File.Exists(patternFile))
        {
          wbPatt = xlsApp.Workbooks.Open(patternFile);
          if (wbPatt == null || wbPatt.Worksheets.Count == 0)
            throw new Exception("Excel workbook \"" + patternFile + "\" not found!");
          wsPatt = wbPatt.Worksheets[1];
          rgPatt = wsPatt.UsedRange;
          rg = ws.get_Range(cell, cell.get_Offset(rgPatt.Rows.Count - 1, rgPatt.Columns.Count - 1));
          rgPatt.Copy(rg);
          cell = cell.get_Offset(rgPatt.Rows.Count, 0);
          wbPatt.Close(false);
        }

        //----- caps
        if (showHeader)
        {
          string[] caps = new string[colCnt];
          for (int c = 0; c < colCnt; c++)
            caps[c] = props[c].Name;
          rg = ws.get_Range(cell, cell.get_Offset(0, colCnt - 1));
          rg.set_Value(Excel.XlRangeValueDataType.xlRangeValueDefault, caps);
          rg.Font.Bold = true;
          rg.HorizontalAlignment = Excel.Constants.xlCenter;
          cell = cell.get_Offset(1, 0);
        }

        //----- data
        for (int r = 0; r < data.GetLength(0); r++)
          for (int c = 0; c < colCnt; c++)
          {
            object value = props[c].GetValue(list[r]);
            if (value is Guid)
              data[r, c] = value.ToString();
            else
              data[r, c] = value;
          }

        rg = ws.get_Range(cell, cell.get_Offset(data.GetLength(0) - 1, colCnt - 1));
        rg.set_Value(Excel.XlRangeValueDataType.xlRangeValueDefault, data);

        ws.UsedRange.Columns.AutoFit();
        xlsApp.ScreenUpdating = true;
        xlsApp.Visible = true;
      }
      catch (Exception ex)
      {
        if (xlsWb != null && xlsApp.Workbooks.Count > 0)
          xlsWb.Close(false);
        if (xlsApp != null) xlsApp.Quit();
        throw ex;
      }
      finally
      {
        ReleaseCom(ref rg);
        ReleaseCom(ref rgPatt);
        ReleaseCom(ref ws);
        ReleaseCom(ref wsPatt);
        ReleaseCom(ref xlsWb);
        ReleaseCom(ref wbPatt);
        ReleaseCom(ref xlsApp);
      }
    }
    //-------------------------------------------------------------------------
    /// <summary>Выгрузка грида в Excel.
    /// </summary>
    /// <param name="dg">грид</param>
    /// <param name="maxRows">максимально возможное кол-во строк для выдачи</param>
    public static void GridToExcel(DataGridView dg, int maxRows = 0)
    {
      List<DataGridViewColumn> cols = dg.Columns.OfType<DataGridViewColumn>().Where(x => x.Visible).OrderBy(o => o.DisplayIndex).ToList();
      int colCnt = cols.Count;

      if (colCnt == 0) return;

      if (maxRows > 0 && dg.RowCount > maxRows) // ограничение, чтобы не подвешивать надолго
        Loger.SendMess(string.Format("Максимальное количество строк для выдачи в Excel = {0}", maxRows));
      else
        maxRows = dg.RowCount;

      object[,] data = new object[maxRows, colCnt];

      Form f = dg.FindForm();
      f.Cursor = Cursors.WaitCursor;
      Excel.Application xlsApp = null;
      Excel.Workbook xlsWb = null;
      Excel.Worksheet ws = null;
      Excel.Range rg = null;
      try
      {
        xlsApp = new Excel.Application();
        xlsApp.Visible = false;
        xlsApp.ScreenUpdating = false;
        xlsWb = xlsApp.Workbooks.Add();
        xlsApp.Calculation = Excel.XlCalculation.xlCalculationManual;
        ws = (Excel.Worksheet)xlsWb.Worksheets.Add();
        Excel.Range cell = ws.get_Range("A1");

        //----- caps
        string[] caps = new string[colCnt];
        for (int c = 0; c < colCnt; c++)
          caps[c] = cols[c].HeaderText;
        rg = ws.get_Range(cell, cell.get_Offset(0, colCnt - 1));
        rg.set_Value(Excel.XlRangeValueDataType.xlRangeValueDefault, caps);
        rg.Font.Bold = true;
        rg.HorizontalAlignment = Excel.Constants.xlCenter;
        cell = cell.get_Offset(1, 0);

        //----- data
        for (int r = 0; r < data.GetLength(0); r++)
          for (int c = 0; c < colCnt; c++)
          {
            if (dg.Rows[r].Cells[cols[c].Index].Value is Guid)
              data[r, c] = dg.Rows[r].Cells[cols[c].Index].Value.ToString();
            else
              data[r, c] = dg.Rows[r].Cells[cols[c].Index].Value;
          }

        rg = ws.get_Range(cell, cell.get_Offset(maxRows - 1, colCnt - 1));
        rg.set_Value(Excel.XlRangeValueDataType.xlRangeValueDefault, data);

        ws.UsedRange.Columns.AutoFit();
        xlsApp.ScreenUpdating = true;
        xlsApp.Visible = true;
      }
      catch (Exception)
      {
        if (xlsWb != null && xlsApp.Workbooks.Count > 0)
          xlsWb.Close(false);
        if (xlsApp != null) xlsApp.Quit();
        throw;
      }
      finally
      {
        ReleaseCom(ref rg);
        ReleaseCom(ref ws);
        ReleaseCom(ref xlsWb);
        ReleaseCom(ref xlsApp);
        f.Cursor = Cursors.Default;
      }
    }
    //-------------------------------------------------------------------------
    /* уборка ком, чтобы эксели не болтались в памяти */
    public static void ReleaseCom<T>(T o) where T : class
    {
      ReleaseCom(ref o);
    }
    public static void ReleaseCom<T>(ref T o) where T : class
    {
      if (o != null)
      {
        Marshal.FinalReleaseComObject(o);
        o = null;
        GC.Collect();
      }
    }
  }
}
