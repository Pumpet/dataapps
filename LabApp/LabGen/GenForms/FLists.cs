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
  public partial class FLists : FormList
  {
    public FLists()
    {
      InitializeComponent();
    }
    //-------------------------------------------------------------------------
    protected override void BindFilter()
    {
      base.BindFilter();

      sbTypes.BindTextFromCombo(bss["ListTypes"], "Name", null, null, false, false);
      sbTypes.SelectedIndex = 0;
    }
    //-------------------------------------------------------------------------
    private void FLists_OnControlChanged(Control c, bool entered)
    {
      if (!entered && c.Name == "sbTypes")
      {
        LoadData(null, null);
      }
    }
    //-------------------------------------------------------------------------
    private object dataList1_OnSetFilter()
    {
      Dictionary<string, object> f = new Dictionary<string, object>();
      f.Add("TypeCode", CommonLib.GetValueFromObject(sbTypes.SelectedItem, "Code"));
      return f;
    }
  }
}
