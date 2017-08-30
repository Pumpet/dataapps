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
  public partial class FResultEdit : FormEdit
  {
    public FResultEdit()
    {
      InitializeComponent();
      SetFormEditSize(tComment);
    }
    //-------------------------------------------------------------------------
    protected override void Bind()
    {
      base.Bind();
      DataSourceUpdateMode m = DataSourceUpdateMode.OnValidation;
      DataSourceUpdateMode n = DataSourceUpdateMode.Never;
      //----------------
      BindingSource bs = bss[EntityName];

      rbY.DataBindings.Add("Checked", bs, "ResultType", true, m);
      rbMt.Checked = !rbY.Checked;
      FResultEdit_OnControlChanged(rbY, false);

      sbSample.BindParentFromForm(bss["Sample"], "SampleId", bs, "Sample", "SampleId = SampleId;", "FSamples", dc.GetParentFunc("Main", "Sample"), "SampleCode", false);
      sbPopul.BindParentFromForm(bss["Popul"], "PopulId", bs, "Popul", "PopulId = PopulId;", "FPopuls", dc.GetParentFunc("Main", "Popul"), "Name", false);
      tPrediction.DataBindings.Add("Text", bs, "Prediction", true, m);
      nbProbability.DataBindings.Add("Text", bs, "Probability", true, m);
      tHaplogroup.DataBindings.Add("Text", bs, "Haplogroup", true, m);
      tMarker.DataBindings.Add("Text", bs, "Marker", true, m);
      tComment.DataBindings.Add("Text", bs, "Comment", true, m);

      nbGWS1.DataBindings.Add("Text", bs, "GWS1", true, n, "");
      nbGWS2.DataBindings.Add("Text", bs, "GWS2", true, n, "");

      B_DYS389I.DataBindings.Add("Text", bs, "B_DYS389I", true, n, "");
      B_DYS389II.DataBindings.Add("Text", bs, "B_DYS389II", true, n, "");
      B_DYS390.DataBindings.Add("Text", bs, "B_DYS390", true, n, "");
      B_DYS456.DataBindings.Add("Text", bs, "B_DYS456", true, n, "");
      G_DYS19.DataBindings.Add("Text", bs, "G_DYS19", true, n, "");
      G_DYS385.DataBindings.Add("Text", bs, "G_DYS385", true, n, "");
      G_DYS385_2.DataBindings.Add("Text", bs, "G_DYS385_2", true, n, "");
      G_DYS458.DataBindings.Add("Text", bs, "G_DYS458", true, n, "");
      R_DYS437.DataBindings.Add("Text", bs, "R_DYS437", true, n, "");
      R_DYS438.DataBindings.Add("Text", bs, "R_DYS438", true, n, "");
      R_DYS448.DataBindings.Add("Text", bs, "R_DYS448", true, n, "");
      R_Y_GATA_H4.DataBindings.Add("Text", bs, "R_Y_GATA_H4", true, n, "");
      Y_DYS391.DataBindings.Add("Text", bs, "Y_DYS391", true, n, "");
      Y_DYS392.DataBindings.Add("Text", bs, "Y_DYS392", true, n, "");
      Y_DYS393.DataBindings.Add("Text", bs, "Y_DYS393", true, n, "");
      Y_DYS439.DataBindings.Add("Text", bs, "Y_DYS439", true, n, "");
      Y_DYS635.DataBindings.Add("Text", bs, "Y_DYS635", true, n, "");
      DYS449.DataBindings.Add("Text", bs, "DYS449", true, n, "");
      DYS460.DataBindings.Add("Text", bs, "DYS460", true, n, "");
      DYS481.DataBindings.Add("Text", bs, "DYS481", true, n, "");
      DYS518.DataBindings.Add("Text", bs, "DYS518", true, n, "");
      DYS533.DataBindings.Add("Text", bs, "DYS533", true, n, "");
      DYS570.DataBindings.Add("Text", bs, "DYS570", true, n, "");
      DYS576.DataBindings.Add("Text", bs, "DYS576", true, n, "");
      DYS627.DataBindings.Add("Text", bs, "DYS627", true, n, "");
      DYF387S1.DataBindings.Add("Text", bs, "DYF387S1", true, n, "");
      DYS447.DataBindings.Add("Text", bs, "DYS447", true, n, "");
    }
    //-------------------------------------------------------------------------
    private void FResultEdit_OnControlChanged(Control c, bool entered)
    {
      if (!entered && (c == rbMt || c == rbY))
      { 
        nbGWS1.Enabled = rbMt.Checked;
        nbGWS2.Enabled = rbMt.Checked;
        panelY.Enabled = rbY.Checked;
      }
    }
    //-------------------------------------------------------------------------
    protected override void InitControls()
    {
      base.InitControls();
      sbSample.Locked = (inFilter is Dictionary<string, object> && ((Dictionary<string, object>)inFilter).ContainsKey("SampleId"));
    }
  }
}
