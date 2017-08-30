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
  public partial class FormSelectCols : Form
  {
    class Col 
    {
      public string caption, name;
      public override string ToString()
      {
        return caption;
      }
    }
    Action<List<string>> callback;
    //-------------------------------------------------------------------------
    public FormSelectCols(Action<List<string>> action, DataGridViewColumnCollection columns) : base()
    {
      InitializeComponent();
      callback = action;
      foreach (DataGridViewColumn col in columns.OfType<DataGridViewColumn>().OrderBy(x => x.DisplayIndex))
	    {
        chlbCols.Items.Add(new Col { caption = col.HeaderText, name = col.Name }, col.Visible);
	    }
    }
    //-------------------------------------------------------------------------
    private void bOk_Click(object sender, EventArgs e)
    {
      if (callback != null)
        callback(chlbCols.CheckedItems.OfType<Col>().Select(x => x.name).ToList());
      Close();
    }
    //-------------------------------------------------------------------------
    private void chlbCols_ItemCheck(object sender, ItemCheckEventArgs e)
    {
      if (e.NewValue == CheckState.Unchecked && chlbCols.CheckedItems.Count <= 1)
        e.NewValue = CheckState.Checked;
    }

    private void FormSelectCols_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Escape)
        Close();
      if (e.KeyCode == Keys.Enter)
        bOk.PerformClick();
    }
  }
}
