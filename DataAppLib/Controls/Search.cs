using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Ctrls
{
  /// <summary>Направление поиска: 
  /// Left/Right - по строкам, с переходом на следующую/предыдущую, Up/Down - вверх/вниз в пределах столбца
  /// </summary>
  public enum SearchMode { Left, Right, Up, Down }

  //===========================================================================
  /// <summary>Поиск DataGridView 
  /// </summary>
  public class Search
  {
    string str { get; set; }
    bool cs { get; set; }
    bool eq { get; set; }
    //-------------------------------------------------------------------------
    /// <summary>Создать объект поиска
    /// </summary>
    /// <param name="str">строка поиска</param>
    /// <param name="cs">чувствителен к регистру</param>
    /// <param name="eq">точное соответствие</param>
    public Search(string str, bool cs, bool eq)
    {
      this.str = (str ?? "").Trim();
      this.cs = cs;
      this.eq = eq;
    }
    //-------------------------------------------------------------------------
    /// <summary>Проверка значения по условиям поиска
    /// </summary>
    /// <param name="val">значение</param>
    /// <returns>true если значение удовлетворяет условиям</returns>
    public bool Check(object val)
    {
      string valStr = (val ?? "").ToString().Trim();
      bool res = str == valStr;
      if (res) return res;

      if (!eq)
      {
        if (val is DateTime)
        {
          DateTime valDT = (DateTime)val;
          valStr = valDT.ToString(valDT.Date == valDT ? "dd.MM.yyyy" : "dd.MM.yyyy HH:mm:ss");
        }
        res = valStr.IndexOf(str, cs ? StringComparison.InvariantCulture : StringComparison.InvariantCultureIgnoreCase) >= 0;
      }
      else if (val != null)
      { 
        Type t = val.GetType();
        if (t == typeof(DateTime))
        {
          DateTime dt;
          if (DateTime.TryParseExact(str, new[]{"dd.MM.yyyy", "dd.MM.yyyy hh:mm:ss"}, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out dt))
            res = dt.CompareTo((DateTime)val) == 0;
        }
        else if (t == typeof(sbyte) || t == typeof(short) || t == typeof(int) || t == typeof(long) || t == typeof(byte) || t == typeof(ushort) || t == typeof(uint) || t == typeof(ulong) || t == typeof(float) || t == typeof(double) || t == typeof(decimal))
        { 
          decimal dec, valDec;
          if (decimal.TryParse(str, NumberStyles.Number, CultureInfo.InvariantCulture, out dec) && decimal.TryParse(valStr, NumberStyles.Number, CultureInfo.InvariantCulture, out valDec))
            res = dec.CompareTo(valDec) == 0;
        }
        else
          res = String.Compare(valStr, str, !cs, CultureInfo.InvariantCulture) == 0;
      }

      return res;
    }
  }
}
