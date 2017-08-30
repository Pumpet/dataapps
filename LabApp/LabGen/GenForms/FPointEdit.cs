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
  public partial class FPointEdit : FormEdit
  {
    public FPointEdit()
    {
      InitializeComponent();
      SetFormEditSize(tComment);
    }
    //-------------------------------------------------------------------------
    protected override void Bind()
    {
      base.Bind();
      DataSourceUpdateMode m = DataSourceUpdateMode.OnValidation;
      //----------------
      tName.DataBindings.Add("Text", bss[EntityName], "PointName", true, m);
      sbExped.BindParentFromForm(bss["Exped"], "ExpedId", bss[EntityName], "Exped", "ExpedId = ExpedId;",
        "FExpeds", dc.GetParentFunc("Main", "Exped"), "Name", false);
      sbPopul.BindParentFromForm(bss["Popul"], "PopulId", bss[EntityName], "Popul", "PopulId = PopulId;",
        "FPopuls", dc.GetParentFunc("Main", "Popul"), "Name", false);
      tRegion.DataBindings.Add("Text", bss[EntityName], "Region", true, m);
      tRegionEn.DataBindings.Add("Text", bss[EntityName], "RegionEn", true, m);
      tPeriod.DataBindings.Add("Text", bss[EntityName], "Period", true, m);
      tLocName.DataBindings.Add("Text", bss[EntityName], "LocName", true, m);
      nLocX.DataBindings.Add("Text", bss[EntityName], "LocX", true, m);
      nLocY.DataBindings.Add("Text", bss[EntityName], "LocY", true, m);
      tHead.DataBindings.Add("Text", bss[EntityName], "Head", true, m);
      tComment.DataBindings.Add("Text", bss[EntityName], "Comment", true, m);
    }
    //-------------------------------------------------------------------------
    protected override void InitControls()
    {
      base.InitControls();
      sbExped.Locked = (inFilter is Dictionary<string, object> && ((Dictionary<string, object>)inFilter).ContainsKey("ExpedId"));
    }
  }
}
