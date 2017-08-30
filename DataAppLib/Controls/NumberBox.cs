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
  public partial class NumberBox : TextBox, ISupportInitialize
  {
    bool docheck;
    string old;
    string sep = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;


    /// <summary>Текст по умолчанию</summary>
    [Browsable(false)]
    string DefaultText
    {
      get
      {
        if (Nullable)
          return "";
        else
          return "0";
      }
    }
    
    /// <summary>Допускает пустое значение при редактировании</summary>
    [Category("Mask options"), DefaultValue(false), Description("Допускает пустое значение при редактировании")]
    public bool Nullable { get; set; }

    //-------------------------------------------------------------------------
    public NumberBox()
    {
      InitializeComponent();
      StartValues();
    }
    public NumberBox(IContainer container)
    {
      container.Add(this);
      InitializeComponent();
      StartValues();
    }
    protected void StartValues()
    {
      TextAlign = HorizontalAlignment.Right;
      Nullable = false;
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
      Text = DefaultText;
      old = Text;
      docheck = true;
    }
    //=========================================================================
    protected override void OnKeyDown(KeyEventArgs e)
    {
      int pos = SelectionStart;
      docheck = true;

      bool numKey = (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9) || (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9);

      // двигаемся через точку
      if ((e.KeyCode == Keys.Oemcomma || e.KeyCode == Keys.OemPeriod || e.KeyCode == (Keys)110) && pos < Text.Length && Text[pos] == '.')
      {
        Select(++pos, 0);
        e.SuppressKeyPress = true;
      }

      // отрезать ноль если вводим после точки
      if (numKey && Text.Length > 1 && Text.Substring(Text.Length - 2, 2) == sep + "0" && pos == Text.Length - 1)
      {
        docheck = false;
        Text = Text.Substring(0, Text.Length - 1);
        Select(pos, 0);
      }

      // минус - вперед
      if (e.KeyCode == Keys.OemMinus || e.KeyCode == (Keys)109)
      {
        Text = "-" + Text;
        Select(1, 0);
      }

      e.SuppressKeyPress = (e.SuppressKeyPress
        || e.KeyCode == Keys.Oemplus
        || e.KeyCode == Keys.OemMinus
        || e.KeyCode == Keys.Space);

      base.OnKeyDown(e);
    }
    //-------------------------------------------------------------------------
    protected override void OnTextChanged(EventArgs e)
    {
      int pos = SelectionStart;
      string newtext;

      if (CheckText(Text, out newtext)) // допустимость текста как числа
      {
        old = Text;
        Text = newtext;
      }
      else
      {
        Text = old;
        pos = pos - 1;
      }

      // встать после первого нуля
      if (Text.Length > 0 && Text.Substring(0, 1) == "0" && pos == 0)
        pos = 1;

      Select(pos >= 0 ? pos : 0, 0);

      base.OnTextChanged(e);
    }
    //=========================================================================
    /* допустимость текста как числа */
    private bool CheckText(string text, out string outtext)
    {
      bool check = true;

      if (String.IsNullOrEmpty(text) && !Nullable)
        text = DefaultText;

      string tmp = "";

      // убираем минус из отрицательного
      if (text.Length > 0 && text.Substring(0, 1) == "-")
      {
        tmp = "-";
        text = text.Substring(1);
      }

      // добавим 0 перед первым разделителем
      if (text.Length > 0 && text.Substring(0, 1) == sep)
        text = "0" + text;

      // добавим 0 после последнего разделителя
      if (text.Length > 0 && text.Substring(text.Length - 1, 1) == sep && docheck)
        text = text + "0";

      // убираем первый ноль перед целой частью
      if (text.Length > 1 && text.Substring(0, 1) == "0"
        && text.Substring(0, 2) != "0" + sep)
        text = text.Substring(1);

      text = tmp + text;

      Decimal v;
      check = Decimal.TryParse(text, out v);

      outtext = text;
      return check || text == DefaultText || !docheck;
    }
  }
}
