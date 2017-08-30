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
  public partial class FAnketEdit : FormEdit
  {
    public FAnketEdit()
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
      BindingSource bs = bss[EntityName];
      tRUSID.DataBindings.Add("Text", bs, "RUSID", true, m);
      tGPID.DataBindings.Add("Text", bs, "GPID", true, m);
      tOrigin.DataBindings.Add("Text", bs, "Origin", true, m);
      tComment.DataBindings.Add("Text", bs, "Comment", true, m);
      tFio.DataBindings.Add("Text", bs, "Fio", true, m);
      tLiveAddress.DataBindings.Add("Text", bs, "LiveAddress", true, m);
      dtBirthDate.DataBindings.Add("Text", bs, "BirthDate", true, m);
      dtInDate.DataBindings.Add("Text", bs, "InDate", true, m);
      
      sbPopul.BindParentFromForm(bss["Popul"], "PopulId", bs, "Popul", "PopulId = PopulId;", "FPopuls", dc.GetParentFunc("Main", "Popul"), "Name", false);
      sbPoint.BindParentFromForm(bss["Point"], "PointId", bs, "Point", "PointId = PointId;", "FPoints", dc.GetParentFunc("Main", "Point"), "PointName", false);
      sbLingua.BindParentFromCombo(bss["ListLingua"], "Item", bss["Lingua"], "ListId", bs, "Lingua", "LinguaId = ListId", dc.GetParentFunc("Main", "Lingua"), false);
      sbBirthPlace.BindTextFromForm(bs, "BirthPlace = Name", "FPlaces", dc.GetParentFunc("Main", "Place"), "BirthPlace", true, false);
      sbLivePlace.BindTextFromForm(bs, "LivePlace = Name", "FPlaces", dc.GetParentFunc("Main", "Place"), "LivePlace", true, false);
      sbInPlace.BindTextFromForm(bs, "InPlace = Name", "FPlaces", dc.GetParentFunc("Main", "Place"), "InPlace", true, false);

      rbM.DataBindings.Add("Checked", bs, "Man", true, m);
      rbW.Checked = !rbM.Checked;

      string anc = (string)CommonLib.GetValueFromObject(bs.Current, "OtherAncestors") ?? "1";
      bool[] ch = new bool[11];
      for (int i = 0; i < 11; i++)
        ch[i] = (anc.Length > i && anc[i] != '0');
      if (ch[0]) rbNo.Checked = true; else if (ch[1]) rbDontKnow.Checked = true; else if (ch[2]) rbYes.Checked = true;
      if (rbYes.Checked)
      {
        FAnketEdit_OnControlChanged(rbYes, false);
        cbMama.Checked  = ch[3];
        cbMamaB.Checked = ch[4];
        cbMamaD.Checked = ch[5];
        cbMamaO.Checked = ch[6];
        cbPapa.Checked  = ch[7];
        cbPapaB.Checked = ch[8];
        cbPapaD.Checked = ch[9];
        cbPapaO.Checked = ch[10];
      }
    }
    //-------------------------------------------------------------------------
    private void FAnketEdit_OnControlChanged(Control c, bool entered)
    {
      if (!entered) 
      { 
        if (new []{rbYes, rbNo, rbDontKnow}.Any(x => x == c))
          CommonLib.ForControls(gbParents, (cb) => { cb.Enabled = c == rbYes; }, typeof(CheckBox));
      }
    }
    //-------------------------------------------------------------------------
    private void FAnketEdit_BeforeSave()
    {
      StringBuilder s = new StringBuilder("00000000000");
      if (rbNo.Checked) s[0] = '1'; else if (rbDontKnow.Checked) s[1] = '1'; else if (rbYes.Checked) s[2] = '1'; else s[0] = '1';
      if (rbYes.Checked)
      {
        if (cbMama.Checked) s[3] = '1';
        if (cbMamaB.Checked) s[4] = '1';
        if (cbMamaD.Checked) s[5] = '1';
        if (cbMamaO.Checked) s[6] = '1';
        if (cbPapa.Checked)  s[7] = '1';
        if (cbPapaB.Checked) s[8] = '1';
        if (cbPapaD.Checked) s[9] = '1';
        if (cbPapaO.Checked) s[10] = '1';
      }
      CommonLib.SetValueToObject(GetEntityBind(), "OtherAncestors", s.ToString());
    }
    //-------------------------------------------------------------------------
    private void FAnketEdit_OnSetMenu(object obj)
    {
      SetMenuThruController(data, obj);
    }
  }
}
