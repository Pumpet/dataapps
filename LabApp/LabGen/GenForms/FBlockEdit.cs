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
  public partial class FBlockEdit : FormEdit
  {
    public FBlockEdit()
    {
      InitializeComponent();
      SetFormEditSize(sbStore);
    }
    //-------------------------------------------------------------------------
    protected override void Bind()
    {
      base.Bind();
      DataSourceUpdateMode m = DataSourceUpdateMode.OnValidation;
      BindingSource bs = bss[EntityName];
      tBlockCode.DataBindings.Add("Text", bs, "BlockCode", true, m);
      sbStore.BindParentFromForm(bss["Store"], "StoreId", bs, "Store", "StoreId = StoreId;", "FStores", dc.GetParentFunc("Main", "Store"), "StoreName", true);
      cbIsSetItems.DataBindings.Add("Checked", bs, "IsSetItems", true, m);
      tX.DataBindings.Add("Text", bs, "DimX", true, m);
      tY.DataBindings.Add("Text", bs, "DimY", true, m);
    }
    //-------------------------------------------------------------------------
    private void FBlockEdit_OnControlChanged(Control c, bool enter)
    {
      tX.ReadOnly = !cbIsSetItems.Checked;
      tY.ReadOnly = !cbIsSetItems.Checked;
    }
  }
}
