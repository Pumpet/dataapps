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
  public partial class FPopulEdit : FormEdit
  {
    public FPopulEdit()
    {
      InitializeComponent();
    }
    protected override void InitControls()
    {
      base.InitControls();
      SetFormEditSize(tComment);
    }

    //-------------------------------------------------------------------------
    protected override void Bind()
    {
      base.Bind();
      DataSourceUpdateMode m = DataSourceUpdateMode.OnValidation;
      //----------------
      sbEtno.BindParentFromForm(bss["Etno"], "EtnoId", bss[EntityName], "Etno", "EtnoId = EtnoId;",
        "FEtnos", dc.GetParentFunc("Main", "Etno"), "Name", true);
      tEtnoNameEn.DataBindings.Add("Text", bss["Etno"], "NameEn", true, m);
      tName.DataBindings.Add("Text", bss[EntityName], "Name", true, m);
      tNameEn.DataBindings.Add("Text", bss[EntityName], "NameEn", true, m);
      tCode.DataBindings.Add("Text", bss[EntityName], "Codes", true, m);
      tComment.DataBindings.Add("Text", bss[EntityName], "Comment", true, m);
    }
  }
}
