using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ctrls
{
  /// <summary>Настройки фильтра (флаги)
  /// 1:точное совпадение (равно) 2:учет регистра 4:больше 8:меньше 16:не равно 32:фильтр по пустому значению 64:за период
  /// </summary>
  [Flags]
  public enum FilterMode { None = 0, Eq = 1, Cs = 2, Piu = 4, Meno = 8, NotEq = 16, Empty = 32, Period = 64, 
    RangeMeno = 128, RangeMenoEq = 256, InList = 512 }

  /// <summary>Тип данных фильтра: строка, дата, число 
  /// </summary>
  public enum FilterType { Str, Date, Num }
  
  //===========================================================================
  /// <summary>Фильтр для столбца DataGridView 
  /// </summary>
  public class Filter
  {
    string[] sList; 
    DateTime date1;
    DateTime date2;
    decimal num;
    decimal num2;

    /// <summary>имя столбца</summary>
    public string ColName { get; set; }
    /// <summary>настройки фильтра (флаги)</summary>
    public FilterMode Mode { get; set; }
    /// <summary>тип данных фильтра</summary>
    public FilterType DataType { get; set; }
    /// <summary>описание фильтра</summary>
    public string FilterName { get; set; }
    /// <summary>внешние (родительские) ключи</summary>
    public object MasterKey { get; set; } 
    //-------------------------------------------------------------------------
    private Filter(string colName, FilterType ft, object value, FilterMode mode) // для вызова из Create
    {
      ColName = colName;
      DataType = ft;
      Mode = mode;
      MasterKey = null;

      SetValue(value);
      SetName();
    }
    //-------------------------------------------------------------------------
    private void SetValue(object value) // приведение условий фильтра в зависимости от типа данных и настроек
    {
      if (Mode.HasFlag(FilterMode.Empty))
        return;
      if (DataType == FilterType.Date && value is Tuple<DateTime, DateTime>)
      {
        date1 = ((Tuple<DateTime, DateTime>)value).Item1;
        date2 = ((Tuple<DateTime, DateTime>)value).Item2;
      }
      if (DataType == FilterType.Num && value is Tuple<string, string>)
      {
        decimal.TryParse(((Tuple<string, string>)value).Item1, out num);
        decimal.TryParse(((Tuple<string, string>)value).Item2, out num2);
      }
      if (DataType == FilterType.Str && value is string[])
      {
        sList = ((string[])value).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
      }
    }
    //-------------------------------------------------------------------------
    private void SetName() // описание фильтра в зависимости от типа данных и настроек
    {
      string fName = "";
      if (Mode.HasFlag(FilterMode.Empty))
        fName = "= пусто";
      else if (DataType == FilterType.Date)
      {
        if (Mode.HasFlag(FilterMode.Period))
          fName = string.Format("с {0} по {1}", date1.ToString("dd.MM.yyyy"), date2.ToString("dd.MM.yyyy"));
        else
          fName = "= " + date1.ToString("dd.MM.yyyy");
      }
      else if (DataType == FilterType.Num)
      {
        string sign = "";
        fName = num.ToString();
        switch (Mode)
        {
          case FilterMode.Piu | FilterMode.Eq:
          case FilterMode.Piu | FilterMode.Eq | FilterMode.RangeMeno:
          case FilterMode.Piu | FilterMode.Eq | FilterMode.RangeMenoEq:
            sign = ">=";
            break;
          case FilterMode.Meno | FilterMode.Eq:
            sign = "<=";
            break;
          case FilterMode.Piu:
          case FilterMode.Piu | FilterMode.RangeMeno:
          case FilterMode.Piu | FilterMode.RangeMenoEq:
            sign = ">";
            break;
          case FilterMode.Meno:
            sign = "<";
            break;
          case FilterMode.NotEq:
            sign = "<>";
            break;
          case FilterMode.Eq:
            sign = "=";
            break;
          default:
            break;
        }
        string sign2 = Mode.HasFlag(FilterMode.RangeMeno) ? "<" : Mode.HasFlag(FilterMode.RangeMenoEq) ? "<=" : "";
        if (sign2 != "")
          fName = string.Format("{0}{1} и {2}{3}", sign, fName, sign2, num2.ToString());
        else
          fName = string.Format("{0}{1}", sign, fName);
      }
      else if (DataType == FilterType.Str)
      {
        if (sList.Length == 1)
          fName = (Mode.HasFlag(FilterMode.Eq) ? "=" : "~") + sList[0];
        else
          fName = "по списку";
      }
      FilterName = fName;
    }
    //-------------------------------------------------------------------------
    /// <summary>Создание фильтра для столбца грида DataGridView с указанным условием и настройками.
    /// Определение типа данных, проверка на допустимость условий, приведение условий, формирование описания.
    /// </summary>
    /// <param name="list">грид</param>
    /// <param name="colName">имя столбца</param>
    /// <param name="value">условие</param>
    /// <param name="mode">настройки фильтра (флаги)</param>
    /// <returns>объект фильтра или null если создать не удалось</returns>
    public static Filter Create(DataGridView list, string colName, object value, FilterMode mode)
    {
      if (list == null || !list.Columns.Contains(colName))
        return null;

      DataGridViewColumn col = list.Columns[colName];
      FilterType ft = Filter.GetFilterType(col.ValueType);

      decimal dValue;
      if (!mode.HasFlag(FilterMode.Empty)
        && ((ft == FilterType.Str && !(value is string[] && ((string[])value).Any(x => !string.IsNullOrWhiteSpace(x)))) 
          || (ft == FilterType.Date && !(value is Tuple<DateTime, DateTime>))
          || (ft == FilterType.Num 
              && !(value is Tuple<string, string> 
                  && decimal.TryParse(((Tuple<string, string>)value).Item1, out dValue)
                  && decimal.TryParse(((Tuple<string, string>)value).Item2, out dValue)))))
        return null;

      return new Filter(colName, ft, value, mode);
    }
    //-------------------------------------------------------------------------
    /// <summary>Тип данных фильтра исходя из типа dotnet
    /// </summary>
    /// <param name="t">тип</param>
    /// <returns>тип данных фильтра</returns>
    public static FilterType GetFilterType(Type t)
    {
      FilterType ft = FilterType.Str;
      if (t == typeof(DateTime))
        ft = FilterType.Date;
      if (t == typeof(sbyte) || t == typeof(sbyte?)
        || t == typeof(short) || t == typeof(short?) 
        || t == typeof(int) || t == typeof(int?) 
        || t == typeof(long) || t == typeof(long?) 
        || t == typeof(byte) || t == typeof(byte?) 
        || t == typeof(ushort) || t == typeof(ushort?) 
        || t == typeof(uint) || t == typeof(uint?) 
        || t == typeof(ulong) || t == typeof(ulong?) 
        || t == typeof(float) || t == typeof(float?) 
        || t == typeof(double) || t == typeof(double?) 
        || t == typeof(decimal) || t == typeof(decimal?))
        ft = FilterType.Num;
      return ft;
    }
    //-------------------------------------------------------------------------
    /// <summary>Проверка значения по условиям фильтра
    /// </summary>
    /// <param name="value">значение</param>
    /// <returns>true если проходит через фильтр</returns>
    public bool Check(object value)
    {
      if (Mode.HasFlag(FilterMode.Empty))
      {
        return value == null || string.IsNullOrWhiteSpace(value.ToString());
      }

      bool res = true;
      decimal dValue;
      if (DataType == FilterType.Str)
      {
        StringComparison cs = Mode.HasFlag(FilterMode.Cs) ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
        value = value ?? "";
        for (int i = 0; i < sList.Length; i++)
        {
          if (Mode.HasFlag(FilterMode.Eq))
            res = String.Compare(value.ToString().Trim(), sList[i], cs) == 0;
          else
            res = value.ToString().IndexOf(sList[i], cs) >= 0;
          if (res) break;
        }
      }
      //----
      else if (DataType == FilterType.Date && value is DateTime)
      {
        if (Mode.HasFlag(FilterMode.Period))
          res = (DateTime)value >= date1 && (DateTime)value <= date2;
        else
          res = (DateTime)value == date1;
      }
      //----
      else if (DataType == FilterType.Num)
      {
        if (value == null)
          res = false;
        else if (!decimal.TryParse(value.ToString(), out dValue))
          res = false;
        else
        {
          switch (Mode)
          {
            case FilterMode.Piu | FilterMode.Eq:
            case FilterMode.Piu | FilterMode.Eq | FilterMode.RangeMeno:
            case FilterMode.Piu | FilterMode.Eq | FilterMode.RangeMenoEq:
              res = dValue >= num;
              break;
            case FilterMode.Meno | FilterMode.Eq:
              res = dValue <= num;
              break;
            case FilterMode.Piu:
            case FilterMode.Piu | FilterMode.RangeMeno:
            case FilterMode.Piu | FilterMode.RangeMenoEq:
              res = dValue > num;
              break;
            case FilterMode.Meno:
              res = dValue < num;
              break;
            case FilterMode.NotEq:
              res = dValue != num;
              break;
            default:
              res = dValue == num;
              break;
          }
          if (res && Mode.HasFlag(FilterMode.RangeMeno))
            res = dValue < num2;
          if (res && Mode.HasFlag(FilterMode.RangeMenoEq))
            res = dValue <= num2;
        }
      }
      return res;
    }
  }
}
