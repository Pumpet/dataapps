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
  public partial class FRepSamples : FormEdit
  {
    public FRepSamples()
    {
      InitializeComponent();
      SetFormEditSize(sbExped);
    }
    //-------------------------------------------------------------------------
    protected override void SetCommands()
    {
      base.SetCommands();
      cmds["Save"].Ctrls[0].ToolTipText = "Сформировать отчет (Ctrl+Enter)";
      cmds["Save"].Ctrls[0].Image = Properties.Resources.excel;
      cmds["Cancel"].Visible = false;
    }
    //-------------------------------------------------------------------------
    protected override void InitData()
    {
      base.InitData();
      this.Text = "Отчет по образцам";
    }
    //-------------------------------------------------------------------------
    private void FRepSamples_BeforeSave()
    {
      //lbInfo.Text = "Отчет формируется...";
      this.Refresh();
    }
    //-------------------------------------------------------------------------
    protected override void Bind()
    {
      base.Bind();
      DataSourceUpdateMode m = DataSourceUpdateMode.OnValidation;
      //----------------
      sbExped.BindParentFromForm(bss["Exped"], "ExpedId", bss[EntityName], "Exped", "ExpedId = ExpedId;",
        "FExpeds", dc.GetParentFunc("Main", "Exped"), "Name", true);
      sbPopul.BindParentFromForm(bss["Popul"], "PopulId", bss[EntityName], "Popul", "PopulId = PopulId;",
        "FPopuls", dc.GetParentFunc("Main", "Popul"), "Name", true);
      tSampleCode.DataBindings.Add("Text", bss[EntityName], "SampleCode", true, m);
      tRUSID.DataBindings.Add("Text", bss[EntityName], "RUSID", true, m);
      tGPID.DataBindings.Add("Text", bss[EntityName], "GPID", true, m);
    }

  }
}
