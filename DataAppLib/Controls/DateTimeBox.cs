using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Drawing;

namespace Ctrls
{
  /// <summary>Варианты отображения даты-времени</summary>
  public enum DateTimeStyle { Date, Time, DateTime };
  /// <summary>Стандартные диапазоны дат для Sql</summary>
  public enum DateTimeSqlType { None, Smalldatetime, Datetime }
  
  //===========================================================================
  public partial class DateTimeBox : TextBox, ISupportInitialize
  {
    bool docheck;
    string old;
    readonly string[] formats = new[] { "dd.MM.yyyy", "dd.MM.yyyy HH:mm:ss", "dd.MM.yyyy HH:mm", "HH:mm:ss", "HH:mm" };

    /// <summary>Строка формата в зависимости от варианта отображения</summary>
    [Browsable(false)]
    string DateFormat
    {
      get
      {
        switch (Style)
        {
          case DateTimeStyle.Date:
            return "dd.MM.yyyy";
          case DateTimeStyle.Time:
            return "HH:mm:ss";
          case DateTimeStyle.DateTime:
            return "dd.MM.yyyy HH:mm:ss";
          default:
            return "";
        };
      }
    }

    /// <summary>Дата по умолчанию в текстовом представлении</summary>
    [Browsable(false)]
    string DefaultText
    {
      get
      {
        if (NowOnDefault)
          DefaultDate = DateTime.Now;
        else if (DefaultDate == DateTime.MinValue && !Nullable)
          DefaultDate = MinDate;

        if (DefaultDate == DateTime.MinValue && Nullable)
          return "";
        else
          return DefaultDate.ToString(DateFormat);
      }
    }

    /// <summary>Дата по умолчанию</summary>
    [Category("Mask options"), DefaultValue(typeof(DateTime), ""), Description("Дата по умолчанию, установится при очистке, если не Nullable")]
    public DateTime DefaultDate { get; set; }

    /// <summary>Минимально допустимая дата. 
    /// Определяет DefaultDate, если она не задана и не Nullable.
    /// Если задан SqlType - определяется в соответствии с ним.
    /// </summary>
    [Category("Mask options"), DefaultValue(typeof(DateTime), ""), Description("Минимально допустимая дата. Определяет DefaultDate, если она не задана и не Nullable. Если задан SqlType - определяется в соответствии с ним.")]
    public DateTime MinDate { get; set; }

    /// <summary>Максимально допустимая дата. 
    /// Если задан SqlType - определяется в соответствии с ним.
    /// </summary>
    [Category("Mask options"), DefaultValue(typeof(DateTime), ""), Description("Минимально допустимая дата. Если задан SqlType - определяется в соответствии с ним.")]
    public DateTime MaxDate { get; set; }

    /// <summary>Определяет что DefaultDate всегда будет текущей</summary>
    [Category("Mask options"), DefaultValue(false), Description("Определяет что DefaultDate всегда будет текущей")]
    public bool NowOnDefault { get; set; }

    /// <summary>Допускает пустое значение при редактировании</summary>
    [Category("Mask options"), DefaultValue(false), Description("Допускает пустое значение при редактировании")]
    public bool Nullable { get; set; }

    /// <summary>Проверять в событиях Leave и Validating</summary>
    [Category("Mask options"), DefaultValue(true), Description("Проверять в событиях Leave и Validating")]
    public bool ValidateOnLeave { get; set; }

    /// <summary>Вариант отображения</summary>
    [Category("Mask options"), DefaultValue(DateTimeStyle.Date), Description("Вариант отображения")]
    public DateTimeStyle Style { get; set; }

    /// <summary>Стандартный диапазон дат для Sql, если None - используется MinDate и MaxDate</summary>
    [Category("Mask options"), DefaultValue(DateTimeSqlType.Smalldatetime), Description("Стандартный диапазон дат для Sql. Если установлен - определяет Min и MaxDate")]
    public DateTimeSqlType SqlType { get; set; }

    //-------------------------------------------------------------------------
    public DateTimeBox()
    {
      InitializeComponent();
      StartValues();
    }
    public DateTimeBox(IContainer container)
    {
      container.Add(this);
      InitializeComponent();
      StartValues();
    }
    protected void StartValues()
    {
      TextAlign = HorizontalAlignment.Right;
      ValidateOnLeave = true;
      Nullable = false;
      NowOnDefault = false;
      Style = DateTimeStyle.Date;
      SqlType = DateTimeSqlType.Smalldatetime;
    }
    //=========================================================================
    // ISupportInitialize
    public void BeginInit() { }
    public void EndInit()
    {
      if (!DesignMode)
        Init();
    }
    //-------------------------------------------------------------------------
    protected void Init()
    {
      if (SqlType == DateTimeSqlType.Smalldatetime)
      {
        MinDate = new DateTime(1900, 1, 1);
        MaxDate = new DateTime(2079, 6, 6);
      }
      else if (SqlType == DateTimeSqlType.Datetime)
      {
        MinDate = new DateTime(1753, 1, 1);
        MaxDate = new DateTime(9999, 12, 31);
      }
      else if (MaxDate == DateTime.MinValue)
        MaxDate = DateTime.MaxValue;

      if (MaxDate < MinDate)
        MaxDate = MinDate;

      Text = DefaultText;
      old = Text;
      docheck = true;
      if (DataBindings != null)
        DataBindings.CollectionChanged += DataBindings_CollectionChanged;
    }
    //-------------------------------------------------------------------------
    // формат биндинга устанавливаем принудительно в соответствии в зависимости от формата отображения
    void DataBindings_CollectionChanged(object sender, CollectionChangeEventArgs e)
    {
      if (e.Action == CollectionChangeAction.Add && e.Element is Binding)
        ((Binding)e.Element).FormatString = DateFormat;
    }
    //=========================================================================
    protected override void OnKeyDown(KeyEventArgs e)
    {
      int pos = SelectionStart;

      docheck = true;

      // разрешаем этот ввод (стрелки, буфер, стирание)
      if ((e.KeyValue >= 33 && e.KeyValue <= 40)
        || e.KeyCode == Keys.Shift || e.KeyCode == Keys.Control || e.KeyCode == Keys.Escape
        || (e.Modifiers == Keys.Control && e.KeyCode == Keys.C)
        || (e.Modifiers == Keys.Control && e.KeyCode == Keys.X)
        || (e.Modifiers == Keys.Control && e.KeyCode == Keys.V)
        || (e.KeyCode == Keys.Delete && SelectionLength > 0)
        || (e.KeyCode == Keys.Back && SelectionLength > 0)
        )
      {
        e.SuppressKeyPress = false;
        return; 
      }
      
      // стоим на разделителе
      bool isSep = (pos < Text.Length && (Text[pos] == '.' || Text[pos] == ':' || Text[pos] == ' '));
      // нажата цифра
      bool isNum = ((e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9) || (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9));
      // что нажато - в строку
      string numCh = (isNum ? e.KeyCode.ToString().Last().ToString() : "0");
      
      // было пусто - ставим маску (если цифра) или дефолт (если ins)
      if (string.IsNullOrEmpty(Text) && Nullable)
      {
        docheck = false;
        if (isNum)
          Text = Style == DateTimeStyle.Date ? "00.00.0000" : (Style == DateTimeStyle.DateTime ? "00.00.0000 00:00:00" : "00:00:00");
        else if (e.KeyCode == Keys.Insert)
          Text = DefaultText;
      }

      // можно цифры, backspace или delete - изменим содержание даты, передвинем курсор и проверим
      if (isNum || e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
      {
        docheck = false;
        e.SuppressKeyPress = true;
        if (e.KeyCode == Keys.Back && pos > 0)
          pos--;
        if (isSep && isNum)
          pos++;
        string s = Text;
        if (pos < s.Length && Char.IsNumber(s[pos]))
          s = s.Remove(pos, 1).Insert(pos, numCh);
        // новый текст допустим (проверка не строгая - разрешаем любые даты)
        if (CheckText(s, Mark, false, out s))
        {
          Text = s;
          if (isNum)
            pos++;
          Select(pos, 0);
        }
      }
      else if (isSep) // перешагнем разделитель
      {
        if (!isNum) Select(pos + 1, 0);
        e.SuppressKeyPress = true;
      }
      else
        e.SuppressKeyPress = true;

      base.OnKeyDown(e);
    }
    //-------------------------------------------------------------------------
    protected override void OnTextChanged(EventArgs e)
    {
      int pos = SelectionStart;
      string newtext;

      // новый текст допустим (проверка не строгая - разрешаем любые даты)
      if (CheckText(Text, Mark, false, out newtext))
      {
        old = Text;
        Text = newtext;
      }
      else
      {
        Text = old;
        pos = pos - 1;
      }
      Select(pos >= 0 ? pos : 0, 0);

      base.OnTextChanged(e);
    }
    //-------------------------------------------------------------------------
    protected override void OnLeave(EventArgs e)
    {
      if (ValidateOnLeave && !CheckText())
        Focus();
      else
        base.OnLeave(e);
    }
    //-------------------------------------------------------------------------
    protected override void OnValidating(CancelEventArgs e)
    {
      if (ValidateOnLeave && !CheckText())
        e.Cancel = true;
      base.OnValidating(e);
    }
    //=========================================================================
    private void Mark(bool mark)
    {
      ForeColor = mark ? Color.Red : SystemColors.WindowText;
    }
    //-------------------------------------------------------------------------
    /// <summary>Проверка значения
    /// </summary>
    /// <returns>true - значение допустимо</returns>
    public bool CheckText()
    {
      docheck = true;
      string s = Text;
      // строгая проверка - только правильные даты
      return CheckText(s, Mark, true, out s);
    }
    //-------------------------------------------------------------------------
    /* допустимость текста как даты */
    private bool CheckText(string text, Action<bool> markFunc, bool strong, out string outtext)
    {
      bool check = true, badDate = false;

      string newText = "";
      if (!string.IsNullOrEmpty(text))
      {
        DateTime d;
        check = DateTime.TryParseExact(text, formats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out d);
        badDate = !check;
        if (check)
        {
          newText = d.ToString(DateFormat, DateTimeFormatInfo.InvariantInfo);
          badDate = (d < MinDate || d > MaxDate);
          if (badDate && d == DateTime.MinValue) // чтобы заменить 01.01.0001 на дефолт, если диапазон не начинался с нее
          {
            newText = ""; text = "";
            check = Nullable;
            badDate = false;
          }
        }
      }
      else
        check = Nullable;

      if (check)
        text = newText;
      else if (String.IsNullOrEmpty(text)) // установка дефолта при сбросе даты (если не допустимо пустое значение)
      {
        text = DefaultText;
        check = true;
        badDate = false;
      }

      if (markFunc != null)
        markFunc(badDate);

      outtext = text;
      if (badDate && strong) check = false; // строгая проверка рассматривает неправильную дату
      return check || !docheck;
    }
    //-------------------------------------------------------------------------
    /// <summary>Значение в формате даты-времени. Обертка над DateTime.TryParseExact
    /// </summary>
    /// <returns>Если значение - не дата-время, вернет MinValue</returns>
    public DateTime GetDateTime()
    {
      DateTime d;
      DateTime.TryParseExact(Text, formats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out d);
      return d;
    }
  }
}
