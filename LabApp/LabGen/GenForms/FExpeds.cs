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
  public partial class FExpeds : FormList
  {
    public FExpeds()
    {
      InitializeComponent();
    }
    //-------------------------------------------------------------------------
    protected override void Link()
    {
      dataList2.SetMaster(dataList1, "Points");
      base.Link();
    }
    //-------------------------------------------------------------------------
    protected override void SetCommands()
    {
      base.SetCommands();
      cmds.Items.Add(new Command("Ankets", "Анкеты", null, null, null, new[] { tools, menus }));
      menus.Items[menus.Items.Add(new ToolStripSeparator())].Visible = false;
    }
    //-------------------------------------------------------------------------
    private void dataList2_OnSetMenu(object obj)
    {
      if (dataList2.CellClicked)
        ((Dictionary<string, Action<string>>)obj).Add("Ankets", dataList2.ExecCommand);
    }
    //-------------------------------------------------------------------------
    private object dataList2_OnExecCommand(string cmd, object key, object filter, object data, object[] keys)
    {
      if (cmd == "Ankets")
      {
        FormManager.Io.ExecForm("FAnkets", FormManager.Io.MainForm, FormModes.Default, null, null, key, true);
        return null;
      }
      else if (dataList2.DoExecCommand != null)
      {
        return dataList2.DoExecCommand(cmd, key, filter, data, keys);
      }
      else
        return null;
    }
  }
}
