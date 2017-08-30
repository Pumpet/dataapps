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
  public partial class FAnkets : FormList
  {
    public FAnkets()
    {
      InitializeComponent();
    }
    //-------------------------------------------------------------------------
    protected override void SetCommands()
    {
      base.SetCommands();
      cmds.Items.Add(new Command("AnketProcess", "Обработать анкету", null, null, null, new[] { tools, menus }));
      cmds.Items.Add(new Command("OpenFile", "Открыть файл", null, null, null, new[] { tools, menus }));
      cmds.Items.Add(new Command("Samples", "Образцы", null, null, null, new[] { tools, menus }));
      menus.Items[menus.Items.Add(new ToolStripSeparator())].Visible = false;
    }
    //-------------------------------------------------------------------------
    protected override void Link()
    {
      listDocs.SetMaster(listAnkets, "AnketDocs");
      listAttrs.SetMaster(listAnkets, "AnketAttrs");
      listRels.SetMaster(listAnkets, "AnketRels");
      listSamples.SetMaster(listAnkets, "Samples");
      base.Link();
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
    private void FAnkets_OnControlChanged(Control c, bool entered)
    {
      if (!entered && (c.Name == "sbPopul" || c.Name == "sbPoint"))
        LoadData(null, null);
    }
    //-------------------------------------------------------------------------
    private object listAnkets_OnSetFilter()
    {
      Dictionary<string, object> f = new Dictionary<string, object>();
      f.Add("PointId", CommonLib.GetValueFromObject(bss["FilterPoint"].Current, "PointId"));
      f.Add("PopulId", CommonLib.GetValueFromObject(bss["FilterPopul"].Current, "PopulId"));
      return f;
    }
    //-------------------------------------------------------------------------
    private void listDocs_OnSetMenu(object obj)
    {
      if (listDocs.CellClicked)
        ((Dictionary<string, Action<string>>)obj).Add("OpenFile", listDocs.ExecCommand);
      listDocs.SetMenuThruController(data, obj);
    }
    //-------------------------------------------------------------------------
    private void listAnkets_OnSetMenu(object obj)
    {
      if (listAnkets.CellClicked)
        ((Dictionary<string, Action<string>>)obj).Add("AnketProcess", listAnkets.ExecCommand);
    }
    //-------------------------------------------------------------------------
    private void listSamples_OnSetMenu(object obj)
    {
      if (listSamples.CellClicked)
        ((Dictionary<string, Action<string>>)obj).Add("Samples", listSamples.ExecCommand);
    }
    //-------------------------------------------------------------------------
    private object listSamples_OnExecCommand(string cmd, object key, object filter, object data, object[] keys)
    {
      if (cmd == "Samples")
      {
        if (listSamples.CellClicked)
          FormManager.Io.ExecForm("FSamples", FormManager.Io.MainForm, FormModes.Default, null, key, null, true);
        return null;
      }
      else if (listSamples.DoExecCommand != null)
      {
        return listSamples.DoExecCommand(cmd, key, filter, data, keys);
      }
      else
        return null;
    }
  }
}
