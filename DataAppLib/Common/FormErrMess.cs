using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Common
{
  public partial class FormErrMess : Form
  {
    string shortMess;
    string fullMess;
    public string ShortMess { get { return shortMess; } }
    public string FullMess { get { return string.Format("{0}{1}{2}", shortMess, Environment.NewLine, fullMess); } }

    bool expanded;
    ToolTip tt = new ToolTip();

    public FormErrMess(string mess, string details)
    {
      InitializeComponent();
      shortMess = string.IsNullOrWhiteSpace(mess) ? "Неизвестная ошибка" : mess;
      fullMess = details;
    }

    private void FormErrMess_Load(object sender, EventArgs e)
    {
      tbErr.Text = ShortMess;
      tbErrFull.Text = FullMess;
      tt.SetToolTip(bExec, "Подробности (F2)\nКопировать в буфер по Ctrl+C");
      Expand(false);
    }

    private void bOK_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void FormErrMess_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control)
        Clipboard.SetDataObject(FullMess);
      if (e.KeyCode == Keys.F2)
        Expand(!expanded); 
      if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Enter)
        Close();
    }

    private void bExec_Click(object sender, EventArgs e)
    {
      Expand(!expanded);
    }

    private void Expand(bool exp)
    {
      int h = 0;
      for (int i = 0; i <= panel.RowCount - (exp ? 1 : 2); i++)
        h += (int)panel.RowStyles[i].Height;
      Height = h + this.Height - panel.Height;
      expanded = exp;
      bExec.Image = exp ? Properties.Resources.up : Properties.Resources.down;
      tbErrFull.TabStop = exp;
      bOK.Focus();
    }
  }
}
