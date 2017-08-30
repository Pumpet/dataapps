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
using Common;

namespace GenForms
{
  public partial class FBlocks : FormList
  {
    public FBlocks()
    {
      InitializeComponent();
    }
    //-------------------------------------------------------------------------
    protected override void Link()
    {
      listBlockItems.SetMaster(listBlocks, "BlockItems");
      base.Link();
    }
    //-------------------------------------------------------------------------
    protected override void BindFilter()
    {
      base.BindFilter();
      sbFridge.BindTextFromCombo(bss["Fridges"], null, null, null, true, true);
      sbFridge.Text = "";
    }
    //-------------------------------------------------------------------------
    public override void SetExternalFilter(object filter)
    {
      sbFridge.Text = "";
    }
    //-------------------------------------------------------------------------
    private void FBlocks_OnControlChanged(Control c, bool entered)
    {
      if (!entered && c.Name == "sbFridge")
        LoadData(null, null);
    }
    //-------------------------------------------------------------------------
    private object listBlocks_OnSetFilter()
    {
      Dictionary<string, object> f = new Dictionary<string, object>();
      f.Add("Fridge", sbFridge.Text);
      return f;
    }
    //-------------------------------------------------------------------------
    protected override void SetCommands()
    {
      base.SetCommands();
      cmds.Items.Add(new Command("Samples", "Образцы", null, null, null, new[] { tools, menus }));
      menus.Items[menus.Items.Add(new ToolStripSeparator())].Visible = false;

      if (getResult)
      {
        cmds["Select"].ActiveOnDefault = false;
        cmds["Select"].AddControl(menus);
      }
     }
    //-------------------------------------------------------------------------
    private void listBlockItems_OnSetMenu(object obj)
    {
      if (!getResult && listBlockItems.CellClicked && listBlockItems.ThisSource.Current != null
          && (int)(CommonLib.GetValueFromObject(listBlockItems.ThisSource.Current, "SampleId") ?? 0) != 0)
        ((Dictionary<string, Action<string>>)obj).Add("Samples", listBlockItems.ExecCommand);

      if (getResult && listBlockItems.CellClicked && listBlockItems.ThisSource.Current != null
          && (int)(CommonLib.GetValueFromObject(listBlockItems.ThisSource.Current, "SampleId") ?? 0) == 0)
        ((Dictionary<string, Action<string>>)obj).Add("Select", listBlockItems.ExecCommand);

    }
    //-------------------------------------------------------------------------
    private object listBlockItems_OnExecCommand(string cmd, object key, object filter, object data, object[] keys)
    {
      if (cmd == "Samples")
      {
        if (listBlockItems.CellClicked && listBlockItems.ThisSource.Current != null)
        {
          var k = CommonLib.GetKeyFromObject(listBlockItems.ThisSource.Current, "SampleId");
          FormManager.Io.ExecForm("FSamples", FormManager.Io.MainForm, FormModes.Default, null, k, null, true);
        }
        return null;
      }
      else if (cmd == "Select")
      {
        listBlockItems.EndEdit();
        listBlockItems.ThisSource.EndEdit();
        if (listBlockItems.Save() && CallBack != null)
        {
          CallBack(listBlockItems.GetKey());
          Close();
        }
        return null;
      }
      else if (listBlockItems.DoExecCommand != null)
      {
        return listBlockItems.DoExecCommand(cmd, key, filter, data, keys);
      }
      else
        return null;
    }
  }
}
