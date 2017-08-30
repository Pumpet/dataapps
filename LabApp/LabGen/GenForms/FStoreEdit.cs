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
  public partial class FStoreEdit : FormEdit
  {
    public FStoreEdit()
    {
      InitializeComponent();
      SetFormEditSize(sbContainer);
    }
    //-------------------------------------------------------------------------
    protected override void Bind()
    {
      base.Bind();
      //----------------
      BindingSource bs = bss[EntityName];
      sbLab.BindTextFromCombo(bss["Labs"], null, bs, "Lab", true, false);
      sbFridge.BindTextFromCombo(bss["Fridges"], null, bs, "Fridge", true, true);
      sbFridgeModule.BindTextFromCombo(bss["FridgeModules"], null, bs, "FridgeModule", true, true);
      sbFridgeShelf.BindTextFromCombo(bss["FridgeShelfs"], null, bs, "FridgeShelf", true, true);
      sbContainer.BindTextFromCombo(bss["Containers"], null, bs, "Container", true, true);
    }
  }
}
