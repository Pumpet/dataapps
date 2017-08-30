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
  public partial class FExpedEdit : FormEdit
  {
    public FExpedEdit()
    {
      InitializeComponent();
      SetFormEditSize(tInfo);
    }
    //-------------------------------------------------------------------------
    protected override void Bind()
    {
      base.Bind();
      DataSourceUpdateMode m = DataSourceUpdateMode.OnValidation;
      //----------------
      tName.DataBindings.Add("Text", bss[EntityName], "Name", true, m);
      tRegion.DataBindings.Add("Text", bss[EntityName], "Region", true, m);
      tHead.DataBindings.Add("Text", bss[EntityName], "Head", true, m);
      tInfo.DataBindings.Add("Text", bss[EntityName], "Info", true, m);
      dtDateStart.DataBindings.Add("Text", bss[EntityName], "DateStart", true, m);
      dtDateEnd.DataBindings.Add("Text", bss[EntityName], "DateEnd", true, m);
    }
  }
}
