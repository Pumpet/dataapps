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
  public partial class FPlaces : FormList
  {
    public FPlaces()
    {
      InitializeComponent();
    }
    //-------------------------------------------------------------------------
    protected override void SetCommands()
    {
      base.SetCommands();
      cmds.Items.Add(new Command("GetKladr", "Загрузить...", null, null, null, new[] { tools }, onExec: dataList1.ExecCommand, activeOnDefault: true));
    }
  }
}
