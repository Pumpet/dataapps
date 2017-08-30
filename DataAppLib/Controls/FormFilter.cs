using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ctrls
{
  /// <summary>Форма фильтра
  /// </summary>
  public partial class FormFilter : Form
  {
    int tbStrMinH;
    int addHeight = 200;
    DataGridViewColumn col;
    FilterType ft;
    Panel panel;
    object initValue;
    Func<Filter, int> callback;
    //-------------------------------------------------------------------------
    /// <summary>Создать форму фильтра
    /// </summary>
    /// <param name="column">столбец грида</param>
    /// <param name="cellValue">значение текущей ячейки</param>
    /// <param name="func">функция установки фильтра</param>
    public FormFilter(DataGridViewColumn column, object cellValue, Func<Filter, int> func)
      : base()
    {
      InitializeComponent();
      col = column;
      initValue = cellValue;
      callback = func;
      Init();
      ttip.SetToolTip(bExec, "Установить фильтр (Enter)");
    }
    //-------------------------------------------------------------------------
    private void Init()
    {
      tbStrMinH = tbStr.Height;
      Text = Text + " " + col.HeaderText;
      ft = Filter.GetFilterType(col.ValueType);
      pStr.Visible = ft == FilterType.Str;
      pDate.Visible = ft == FilterType.Date;
      pNum.Visible = ft == FilterType.Num;
      panel = ft == FilterType.Str ? pStr : (ft == FilterType.Date ? pDate : pNum);
      panel.Top = pCommon.Top + pCommon.Height;
      panel.Left = 0;

      int h = pCommon.Height + panel.Height + Size.Height - ClientRectangle.Height
        , w = panel.Width + Size.Width - ClientRectangle.Width;

      Height = h;
      Width = w;
      MinimumSize = new Size(w, h);
      MaximumSize = new Size(ft == FilterType.Str ? 800 : w, h);

      if (ft == FilterType.Str)
      {
        panel.Anchor |= AnchorStyles.Right;
        tbStr.Anchor |= AnchorStyles.Right;
        if (initValue != null)
          tbStr.Text = initValue.ToString();
      }
      pCommon.Width = panel.Width;
      pCommon.Left = panel.Left;
      pCommon.Anchor |= AnchorStyles.Right;
      cbSign.SelectedItem = "=";
      cbSignTill.SelectedItem = "<=";
      chTillNum.Visible = false;
      cbSignTill.Visible = false;
      tbNumTill.Visible = false;
      chTillNum.Checked = false;
      cbSignTill.Enabled = false;
      tbNumTill.Enabled = false;

      EventHandler eh = (s, e) => { if (!chInList.Checked) mess.Text = ""; };
      chEmpty.CheckedChanged += eh;
      chTill.CheckedChanged += eh;
      chEq.CheckedChanged += eh;
      chCs.CheckedChanged += eh;
      chInList.CheckedChanged += eh;
      tbStr.TextChanged += eh;
      tbNumber.TextChanged += eh;
      tbNumTill.TextChanged += eh;
      tbDateS.TextChanged += eh;
      tbDateToS.TextChanged += eh;

      cbSign.SelectedValueChanged += eh;
      bExec.MouseLeave += (s, e) => { bExec.Image = Properties.Resources.play; };
      bExec.MouseMove += (s, e) => { bExec.Image = Properties.Resources.play_down; };
    }
    //-------------------------------------------------------------------------
    private void FormFilter_Shown(object sender, EventArgs e)
    {
      panel.Focus();
      if (ft == FilterType.Str)
      {
        tbStr.Select(0, 0);
        tbStr.Focus();
      }
      if (ft == FilterType.Date)
      {
        tbDateS.Select(0, 0);
        tbDateS.Focus();
      }
      if (ft == FilterType.Num)
      {
        cbSign.Select(0, 0);
        tbNumber.Focus();
      }
    }
    //-------------------------------------------------------------------------
    private void FormFilter_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Escape)
        Close();
      if ((e.KeyCode == Keys.Enter && !chInList.Checked) || (e.KeyCode == Keys.Enter && e.Modifiers == Keys.Control))
        SetFilter();
    }
    //-------------------------------------------------------------------------
    private void cbSign_SelectionChangeCommitted(object sender, EventArgs e)
    {
      if (cbSign.SelectedItem.ToString() == ">=" || cbSign.SelectedItem.ToString() == ">")
      {
        chTillNum.Visible = true;
        cbSignTill.Visible = true;
        tbNumTill.Visible = true;
      }
      else
      {
        chTillNum.Visible = false;
        cbSignTill.Visible = false;
        tbNumTill.Visible = false;
      }
    }
    //-------------------------------------------------------------------------
    private void cbSign_KeyDown(object sender, KeyEventArgs e)
    {
      e.SuppressKeyPress = !((e.KeyValue >= 33 && e.KeyValue <= 40) || e.KeyCode == Keys.Control || e.KeyCode == Keys.Escape || e.KeyCode == Keys.Enter);
    }
    //-------------------------------------------------------------------------
    private void cbSignTill_KeyDown(object sender, KeyEventArgs e)
    {
      e.SuppressKeyPress = !((e.KeyValue >= 33 && e.KeyValue <= 40) || e.KeyCode == Keys.Control || e.KeyCode == Keys.Escape || e.KeyCode == Keys.Enter);
    }
    //-------------------------------------------------------------------------
    private void chEmpty_CheckedChanged(object sender, EventArgs e)
    {
      panel.Enabled = !chEmpty.Checked;
    }
    //-------------------------------------------------------------------------
    private void chInList_CheckedChanged(object sender, EventArgs e)
    {
      tbStr.Text = "";
      tbStr.Multiline = chInList.Checked;
      tbStr.ScrollBars = chInList.Checked ? ScrollBars.Vertical : ScrollBars.None;
      tbStr.Height = chInList.Checked ? tbStrMinH + addHeight : tbStrMinH; 
      pStr.Height = pStr.Height + (chInList.Checked ? addHeight : -addHeight);
      MaximumSize = new Size(MaximumSize.Width, MaximumSize.Height + (chInList.Checked ? addHeight : -addHeight));
      MinimumSize = new Size(MinimumSize.Width, MaximumSize.Height);
      Height = Height + (chInList.Checked ? addHeight : -addHeight);
      mess.Text = "";
      tbStr.Focus();
    }
    //-------------------------------------------------------------------------
    private void chTill_CheckedChanged(object sender, EventArgs e)
    {
      tbDateToS.Enabled = chTill.Checked;
    }
    //-------------------------------------------------------------------------
    private void chTillNum_CheckedChanged(object sender, EventArgs e)
    {
      cbSignTill.Enabled = chTillNum.Checked;
      tbNumTill.Enabled = chTillNum.Checked;
    }
    //-------------------------------------------------------------------------
    private void b_Click(object sender, EventArgs e)
    {
      SetFilter();
    }
    //-------------------------------------------------------------------------
    private void SetFilter()
    {
      object value = null;
      FilterMode mode = FilterMode.None;
      if (chEmpty.Checked)
        mode = FilterMode.Empty;
      else
        switch (ft)
	      {
		      case FilterType.Str:
            if (chEq.Checked) mode |= FilterMode.Eq;
            if (chCs.Checked) mode |= FilterMode.Cs;
            if (chInList.Checked)
            {
              mode |= FilterMode.InList;
              value = tbStr.Lines;
            }
            else
              value = new string[] {tbStr.Text};
            break;
          case FilterType.Date:
            if (chTill.Checked) mode = FilterMode.Period;
            if (!tbDateS.CheckText() || (chTill.Checked && !tbDateToS.CheckText()))
              return;
            value = new Tuple<DateTime, DateTime>(tbDateS.GetDateTime(), tbDateToS.GetDateTime());
            break;
          case FilterType.Num:
            {
              switch (cbSign.SelectedItem.ToString())
              {
                case ">":
                  mode = FilterMode.Piu; break;
                case "<":
                  mode = FilterMode.Meno; break;
                case ">=":
                  mode = FilterMode.Piu | FilterMode.Eq; break;
                case "<=":
                  mode = FilterMode.Meno | FilterMode.Eq; break;
                case "<>":
                  mode = FilterMode.NotEq; break;
                default:
                  mode = FilterMode.Eq; break;
              }
              if (chTillNum.Checked && cbSignTill.SelectedItem.ToString() == "<") 
                mode = mode | FilterMode.RangeMeno;
              if (chTillNum.Checked && cbSignTill.SelectedItem.ToString() == "<=")
                mode = mode | FilterMode.RangeMenoEq;
              //value = tbNumber.Text;
              value = new Tuple<string, string>(tbNumber.Text, tbNumTill.Text);
              break;
            }
        }

      Filter f = Filter.Create(col.DataGridView, col.Name, value, mode);
      if (f == null)
        mess.Text = "Фильтр не задан! ";
      else
      {
        int res = callback(f);
        if (res > 0)
          Close();
        else if (res == 0)
          mess.Text = "Нет записей по условию! ";
        else
          mess.Text = "Фильтр не установлен! ";
      }
    }
  }
}
