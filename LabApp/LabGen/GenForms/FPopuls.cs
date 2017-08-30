using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Forms;

namespace GenForms
{
  public partial class FPopuls : FormList
  {
    public FPopuls()
    {
      InitializeComponent();
    }
    //-------------------------------------------------------------------------
    protected override void SetCommands()
    {
      base.SetCommands();
      cmds.Items.Add(new Command("Ankets", "Анкеты", null, null, null, new[] { tools, menus }));
      menus.Items[menus.Items.Add(new ToolStripSeparator())].Visible = false;
    }
    //-------------------------------------------------------------------------
    private void dataList1_OnSetMenu(object obj)
    {
      if (dataList1.CellClicked)
        ((Dictionary<string, Action<string>>)obj).Add("Ankets", dataList1.ExecCommand);
    }
    //-------------------------------------------------------------------------
    private object dataList1_OnExecCommand(string cmd, object key, object filter, object data, object[] keys)
    {
      if (cmd == "Ankets")
      {
        if (dataList1.CellClicked)
          FormManager.Io.ExecForm("FAnkets", FormManager.Io.MainForm, FormModes.Default, null, null, key, true);
        return null;
      }
      else if (dataList1.DoExecCommand != null)
      {
        return dataList1.DoExecCommand(cmd, key, filter, data, keys);
      }
      else
        return null;
    }
  }
}
