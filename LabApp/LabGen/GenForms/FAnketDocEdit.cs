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
using System.IO;
using Common;

namespace GenForms
{
  public partial class FAnketDocEdit : FormEdit
  {
    public FAnketDocEdit()
    {
      InitializeComponent();
      SetFormEditSize(tInfo);
    }
    //-------------------------------------------------------------------------
    protected override void SetCommands()
    {
      base.SetCommands();
      cmds.Items.Add(new Command("OpenFile", "Открыть файл", null, null, null, new[] { tools }, onExec: ExecFile));
    }
    //-------------------------------------------------------------------------
    protected override void Bind()
    {
      base.Bind();
      DataSourceUpdateMode m = DataSourceUpdateMode.OnValidation;
      //----------------
      BindingSource bs = bss[EntityName];
      tInfo.DataBindings.Add("Text", bs, "Info", true, m);
      tLink.DataBindings.Add("Text", bs, "Link", true, m);
      sbDocType.BindTextFromCombo(bss["DocTypes"], null, bs, "DocType", true, false);
    }
    //-------------------------------------------------------------------------
    private void bLink_Click(object sender, EventArgs e)
    {
      if (File.Exists(tLink.Text))
        fileDialog.InitialDirectory = Path.GetDirectoryName(tLink.Text);
      else
        fileDialog.InitialDirectory = AppConfig.Prop("LinkFilesPath");

      if (fileDialog.ShowDialog(this) == DialogResult.OK)
        tLink.Text = fileDialog.FileName;
    }
    //-------------------------------------------------------------------------
    private void ExecFile(string cmd)
    {
      SetControlsData();
      ExecCommand(cmd);
    }
  }
}
