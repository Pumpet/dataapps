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
  public partial class FAnketRelEdit : FormEdit
  {
    public FAnketRelEdit()
    {
      InitializeComponent();
      SetFormEditSize(tOrigin);
    }
    //-------------------------------------------------------------------------
    protected override void Bind()
    {
      base.Bind();
      DataSourceUpdateMode m = DataSourceUpdateMode.OnValidation;
      BindingSource bs = bss[EntityName];
      tOrigin.DataBindings.Add("Text", bs, "Origin", true, m);

      sbPopul.BindParentFromForm(bss["Popul"], "PopulId", bs, "Popul", "PopulId = PopulId;", "FPopuls", dc.GetParentFunc("Main", "Popul"), "Name", false);
      sbRelType.BindParentFromCombo(bss["ListRelTypes"], "Item", bss["RelType"], "ListId", bs, "RelType", "RelTypeId = ListId", dc.GetParentFunc("Main", "RelType"), false);
      sbLingua.BindParentFromCombo(bss["ListLingua"], "Item", bss["Lingua"], "ListId", bs, "Lingua", "LinguaId = ListId", dc.GetParentFunc("Main", "Lingua"), false);
      sbBirthPlace.BindTextFromForm(bs, "BirthPlace = Name", "FPlaces", dc.GetParentFunc("Main", "Place"), "BirthPlace", true, false);
    }
  }
}
