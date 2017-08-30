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
  public partial class FDnkItemEdit : FormEdit
  {
    public FDnkItemEdit()
    {
      InitializeComponent();
      SetFormEditSize(sbBlockItem);
    }
    //-------------------------------------------------------------------------
    protected override void Bind()
    {
      base.Bind();
      DataSourceUpdateMode m = DataSourceUpdateMode.OnValidation;
      BindingSource bs = bss[EntityName];

      sbDnkItemType.BindParentFromCombo(
        bss["ListDnkItemType"], 
        "Item", 
        bss["DnkItemType"], 
        "ListId", 
        bs, 
        "DnkItemType", 
        "DnkItemTypeId = ListId", 
        dc.GetParentFunc("Main", "DnkItemType"), 
        false);

      sbExtractMethod.BindParentFromCombo(
        bss["ListExtractMethod"],
        "Item",
        bss["ExtractMethod"],
        "ListId",
        bs,
        "ExtractMethod",
        "ExtractMethodId = ListId",
        dc.GetParentFunc("Main", "ExtractMethod"),
        false);
      
      sbBlockItem.BindParentFromForm(
        bss["BlockItem"], 
        "BlockId", 
        bs, 
        "BlockItem", 
        "BlockItemId = BlockItemId;", 
        "FBlocks", 
        dc.GetParentFunc("Main", "BlockItem"), 
        "BlockCode", 
        true);

      nbConcentration.DataBindings.Add("Text", bs, "Concentration", true, m);
      nbQuality.DataBindings.Add("Text", bs, "Quality", true, m);
      nbVolume.DataBindings.Add("Text", bs, "Volume", true, m);
      tStoreName.DataBindings.Add("Text", bss["BlockItem"], "StoreName", true, m);
      tComment.DataBindings.Add("Text", bs, "Comment", true, m);

      object srcCode = CommonLib.GetValueFromObject(bs.DataSource, "SampleCode");
      if (srcCode != null)
        this.Text = this.Text + " (для образца " + srcCode.ToString() + ")";
    }
  }
}
