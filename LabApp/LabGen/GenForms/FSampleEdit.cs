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
  public partial class FSampleEdit : FormEdit
  {
    public FSampleEdit()
    {
      InitializeComponent();
      SetFormEditSize(tAnketGPID);
    }
    //-------------------------------------------------------------------------
    protected override void Bind()
    {
      base.Bind();
      DataSourceUpdateMode m = DataSourceUpdateMode.OnValidation;
      BindingSource bs = bss[EntityName];
      sbSampleType.BindParentFromCombo(bss["ListTypes"], "Item", bss["SampleType"], "ListId", bs, "SampleType", "SampleTypeId = ListId", dc.GetParentFunc("Main", "Type"), false);
      sbAnket.BindParentFromForm(bss["Anket"], "AnketId", bs, "Anket", "AnketId = AnketId; SampleCode = RUSID", "FAnkets", dc.GetParentFunc("Main", "Anket"), "RUSID", false);
      tAnketGPID.DataBindings.Add("Text", bss["Anket"], "GPID", true, m);
      tSampleCode.DataBindings.Add("Text", bs, "SampleCode", true, m);
    }
    //-------------------------------------------------------------------------
    protected override void InitControls()
    {
      base.InitControls();
      sbAnket.Locked = (inFilter is Dictionary<string, object> && ((Dictionary<string, object>)inFilter).ContainsKey("AnketId"));
    }
  }
}
