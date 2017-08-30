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
  public partial class FSamples : FormList
  {
    public FSamples()
    {
      InitializeComponent();
      foreach (string colName in FResults.VerticalCols)
      {
        listResults.Columns[colName].DataPropertyName = listResults.Columns[colName].Name;
        listResults.Columns[colName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        listResults.Columns[colName].Width = 30;
      }
    }
    //-------------------------------------------------------------------------
    protected override void Link()
    {
      listSampleItems.SetMaster(listSamples, "SampleItems");
      listDnkItems.SetMaster(listSamples, "DnkItems");
      listResults.SetMaster(listSamples, "Results");
      base.Link();
    }
    //-------------------------------------------------------------------------
    protected override void BindFilter()
    {
      base.BindFilter();
      sbPoint.BindParentFromForm(bss["FilterPoint"], "PointId", bss["FilterPoint"], null, null, "FPoints", dc.GetParentFunc("Main", "Point"), "PointName", true);
    }
    //-------------------------------------------------------------------------
    public override void SetExternalFilter(object filter)
    {
      bss["FilterPoint"].DataSource = dc.GetParentFunc("Main", "Point")(null);
    }
    //-------------------------------------------------------------------------
    private void FSamples_OnControlChanged(Control c, bool entered)
    {
      if (!entered && c.Name == "sbPoint")
        LoadData(null, null);
    }
    //-------------------------------------------------------------------------
    private object listSamples_OnSetFilter()
    {
      Dictionary<string, object> f = new Dictionary<string, object>();
      f.Add("PointId", CommonLib.GetValueFromObject(bss["FilterPoint"].Current, "PointId"));
      return f;
    }
    //-------------------------------------------------------------------------
    protected override void SetCommands()
    {
      base.SetCommands();
      cmds.Items.Add(new Command("Blocks", "Штатив", null, null, null, new[] { tools, menus }));
      menus.Items[menus.Items.Add(new ToolStripSeparator())].Visible = false;
    }
    //-------------------------------------------------------------------------
    private void listSampleItems_OnSetMenu(object obj)
    {
      if (listSampleItems.CellClicked && listSampleItems.ThisSource.Current != null)
        ((Dictionary<string, Action<string>>)obj).Add("Blocks", listSampleItems.ExecCommand);
    }
    //-------------------------------------------------------------------------
    private void listDnkItems_OnSetMenu(object obj)
    {
      if (listDnkItems.CellClicked && listDnkItems.ThisSource.Current != null)
        ((Dictionary<string, Action<string>>)obj).Add("Blocks", listDnkItems.ExecCommand);
    }
    //-------------------------------------------------------------------------
    private object listSampleItems_OnExecCommand(string cmd, object key, object filter, object data, object[] keys)
    {
      if (cmd == "Blocks")
      {
        if (listSampleItems.CellClicked && listSampleItems.ThisSource.Current != null)
        {
          var k = CommonLib.GetKeyFromObject(listSampleItems.ThisSource.Current, "BlockId");
          FormManager.Io.ExecForm("FBlocks", FormManager.Io.MainForm, FormModes.Default, null, k, null, true);
        }
        return null;
      }
      else if (listSampleItems.DoExecCommand != null)
      {
        return listSampleItems.DoExecCommand(cmd, key, filter, data, keys);
      }
      else
        return null;
    }
    //-------------------------------------------------------------------------
    private object listDnkItems_OnExecCommand(string cmd, object key, object filter, object data, object[] keys)
    {
      if (cmd == "Blocks")
      {
        if (listDnkItems.CellClicked && listDnkItems.ThisSource.Current != null)
        {
          var k = CommonLib.GetKeyFromObject(listDnkItems.ThisSource.Current, "BlockId");
          FormManager.Io.ExecForm("FBlocks", FormManager.Io.MainForm, FormModes.Default, null, k, null, true);
        }
        return null;
      }
      else if (listDnkItems.DoExecCommand != null)
      {
        return listDnkItems.DoExecCommand(cmd, key, filter, data, keys);
      }
      else
        return null;      
    }
    //-------------------------------------------------------------------------
    private void listResults_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
    {
      CommonLib.DoVerticalCols(listResults, e, FResults.VerticalCols, 80);
    }
    //-------------------------------------------------------------------------
    private void listResults_Scroll(object sender, ScrollEventArgs e)
    {
      listResults.Refresh();
    }
  }
}
