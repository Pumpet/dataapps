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
  public partial class FMain : Form
  {
    public FMain()
    {
      InitializeComponent();
    }
    private void FMain_Shown(object sender, EventArgs e)
    {
      Refresh();
      FormManager.Io.CheckConnection(this);
    }
    private void FMain_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.F9)
        tools.Items[0].Select();
    }
    //-------------------------------------------------------------------------
    private void bPopuls_Click(object sender, EventArgs e)
    {
      FormManager.Io.ExecForm(FormManager.Io.GetForm("FPopuls"), this);
    }

    private void bExpeds_Click(object sender, EventArgs e)
    {
      FormManager.Io.ExecForm(FormManager.Io.GetForm("FExpeds"), this);
    }

    private void toolStripButton1_Click(object sender, EventArgs e)
    {
      FormManager.Io.ExecForm(FormManager.Io.GetForm("FAnkets"), this);
    }

    private void toolStripButton2_Click(object sender, EventArgs e)
    {
      FormManager.Io.ExecForm(FormManager.Io.GetForm("FSamples"), this);
    }

    private void bMtDNK_Click(object sender, EventArgs e)
    {
      // FormManager.Io.ExecForm(FormManager.Io.GetForm(""), this);
    }

    private void bYChrom_Click(object sender, EventArgs e)
    {
      // FormManager.Io.ExecForm(FormManager.Io.GetForm(""), this);
    }

    private void bStores_Click(object sender, EventArgs e)
    {
      FormManager.Io.ExecForm(FormManager.Io.GetForm("FStores"), this);
    }

    private void bBlocks_Click(object sender, EventArgs e)
    {
      FormManager.Io.ExecForm(FormManager.Io.GetForm("FBlocks"), this);
    }

    private void bLists_Click(object sender, EventArgs e)
    {
      FormManager.Io.ExecForm(FormManager.Io.GetForm("FLists"), this);
    }

    private void bPlaces_Click(object sender, EventArgs e)
    {
      FormManager.Io.ExecForm(FormManager.Io.GetForm("FPlaces"), this);
    }

    private void toolStripButton4_Click(object sender, EventArgs e)
    {
      FormManager.Io.ExecForm(FormManager.Io.GetForm("FResults"), this);
    }

    private void bRepSamples_Click(object sender, EventArgs e)
    {
      FormManager.Io.ExecForm(FormManager.Io.GetForm("FRepSamples"), this, FormModes.NewEntity | FormModes.Modal | FormModes.GetResult); //FormModes.NewEntity | FormModes.Modal
    }
  }
}
