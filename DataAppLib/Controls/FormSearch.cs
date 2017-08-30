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
  /// <summary>Форма поиска
  /// </summary>
  public partial class FormSearch : Form
  {
    Func<Search, SearchMode, bool> callback;
    //-------------------------------------------------------------------------
    /// <summary>Создание формы поиска
    /// </summary>
    /// <param name="func">функция запуска поиска</param>
    public FormSearch(Func<Search, SearchMode, bool> func)
    {
      InitializeComponent();
      this.callback = func;
      EventHandler eh = (s, e) => { tbStr.BackColor = SystemColors.Window; };
      chEq.CheckedChanged += eh;
      chCs.CheckedChanged += eh;
      tbStr.TextChanged += eh;
      bExec.MouseLeave += (s, e) => { bExec.Image = Properties.Resources.play; };
      bExec.MouseMove += (s, e) => { bExec.Image = Properties.Resources.play_down; };
      ttip.SetToolTip(bExec, "Поиск вперед (F3), назад (Shift+F3), вниз (Ctrl+F3), вверх (Ctrl+Shift+F3)");
    }
    //-------------------------------------------------------------------------
    private void Go(SearchMode mode, bool close)
    { 
      Search search = new Search(tbStr.Text, chCs.Checked, chEq.Checked);
      bool res = callback(search, mode);
      if (close)
        Close();
      else
        tbStr.BackColor = res ? SystemColors.Window : SystemColors.Info;
    }
    //-------------------------------------------------------------------------
    private void bExec_Click(object sender, EventArgs e)
    {
      Go(SearchMode.Left, true);
    }
    //-------------------------------------------------------------------------
    private void FormSearch_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Escape)
        Close();
      if (e.KeyCode == Keys.Enter)
        Go(SearchMode.Left, true);
      else if (e.KeyCode == Keys.F3 && ModifierKeys == Keys.None)
        Go(SearchMode.Left, false);
      else if (e.KeyCode == Keys.F3 && ModifierKeys == Keys.Shift)
        Go(SearchMode.Right, false);
      else if (e.KeyCode == Keys.F3 && ModifierKeys == Keys.Control)
        Go(SearchMode.Down, false);
      else if (e.KeyCode == Keys.F3 && ModifierKeys == (Keys.Control | Keys.Shift))
        Go(SearchMode.Up, false);
    }
  }
}
