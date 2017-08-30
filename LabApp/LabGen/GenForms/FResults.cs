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
  public partial class FResults : FormList
  {
    public static string[] VerticalCols = "GWS1,GWS2,B_DYS389I,B_DYS389II,B_DYS390,B_DYS456,G_DYS19,G_DYS385,G_DYS385_2,G_DYS458,R_DYS437,R_DYS438,R_DYS448,R_Y_GATA_H4,Y_DYS391,Y_DYS392,Y_DYS393,Y_DYS439,Y_DYS635,DYS449,DYS460,DYS481,DYS518,DYS533,DYS570,DYS576,DYS627,DYF387S1,DYS447".Split(',');
    //-------------------------------------------------------------------------
    public FResults()
    {
      InitializeComponent();
      foreach (string colName in VerticalCols)
      {
        listResults.Columns[colName].DataPropertyName = listResults.Columns[colName].Name;
        listResults.Columns[colName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        listResults.Columns[colName].Width = 30;
      }
    }
    //-------------------------------------------------------------------------
    protected override void SetCommands()
    {
      base.SetCommands();
      cmds.Items.Add(new Command("Samples", "Образцы", null, null, null, new[] { tools, menus }));
      menus.Items[menus.Items.Add(new ToolStripSeparator())].Visible = false;
    }
    //-------------------------------------------------------------------------
    protected override void BindFilter()
    {
      base.BindFilter();
      sbPoint.BindParentFromForm(bss["FilterPoint"], "PointId", bss["FilterPoint"], null, null, "FPoints", dc.GetParentFunc("Main", "Point"), "PointName", true);
      sbPopul.BindParentFromForm(bss["FilterPopul"], "PopulId", bss["FilterPopul"], null, null, "FPopuls", dc.GetParentFunc("Main", "Popul"), "Name", true);
    }
    //-------------------------------------------------------------------------
    public override void SetExternalFilter(object filter)
    {
      bss["FilterPoint"].DataSource = dc.GetParentFunc("Main", "Point")(filter);
      bss["FilterPopul"].DataSource = dc.GetParentFunc("Main", "Popul")(filter);
    }
    //-------------------------------------------------------------------------
    private void FResults_OnControlChanged(Control c, bool entered)
    {
      if (!entered && (c == chMt || c == chY))
      {
        if (c == chMt && !chY.Checked)
          chMt.Checked = true;
        else if (c == chY && !chMt.Checked)
          chY.Checked = true;
        else
          LoadData(null, null);
      }
      if (!entered && (c == sbPopul || c == sbPoint))
        LoadData(null, null);
    }
    //-------------------------------------------------------------------------
    private object listResults_OnSetFilter()
    {
      Dictionary<string, object> f = new Dictionary<string, object>();
      f.Add("PointId", CommonLib.GetValueFromObject(bss["FilterPoint"].Current, "PointId"));
      f.Add("PopulId", CommonLib.GetValueFromObject(bss["FilterPopul"].Current, "PopulId"));
      f.Add("Mt", chMt.Checked);
      f.Add("Y", chY.Checked);
      return f;
    }
    //-------------------------------------------------------------------------
    private void listResults_OnSetMenu(object obj)
    {
      if (listResults.CellClicked)
        ((Dictionary<string, Action<string>>)obj).Add("Samples", listResults.ExecCommand);
    }
    //-------------------------------------------------------------------------
    private object listSamples_OnExecCommand(string cmd, object key, object filter, object data, object[] keys)
    {
      if (cmd == "Samples")
      {
        if (listResults.CellClicked && listResults.ThisSource.Current != null)
        {
          var k = CommonLib.GetKeyFromObject(listResults.ThisSource.Current, "SampleId");
          FormManager.Io.ExecForm("FSamples", FormManager.Io.MainForm, FormModes.Default, null, k, null, true);
        }
        return null;
      }
      else if (listResults.DoExecCommand != null)
      {
        return listResults.DoExecCommand(cmd, key, filter, data, keys);
      }
      else
        return null;
    }
    //-------------------------------------------------------------------------
    private void listResults_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
    {
      CommonLib.DoVerticalCols(listResults, e, VerticalCols, 80);
    }
    //-------------------------------------------------------------------------
    private void listResults_Scroll(object sender, ScrollEventArgs e)
    {
      listResults.Refresh();
    }
  }
}
