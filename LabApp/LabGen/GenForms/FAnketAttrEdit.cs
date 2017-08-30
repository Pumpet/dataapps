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
  public partial class FAnketAttrEdit : FormEdit
  {
    public FAnketAttrEdit()
    {
      InitializeComponent();
      SetFormEditSize(tValue);
    }
    //-------------------------------------------------------------------------
    protected override void Bind()
    {
      base.Bind();
      DataSourceUpdateMode m = DataSourceUpdateMode.OnValidation;
      BindingSource bs = bss[EntityName];
      tValue.DataBindings.Add("Text", bs, "Value", true, m);
      sbType.BindParentFromCombo(bss["ListTypes"], "Item", bss["Type"], "ListId", bs, "Type", "TypeId = ListId", dc.GetParentFunc("Main", "Type"), false);
    }
  }
}
