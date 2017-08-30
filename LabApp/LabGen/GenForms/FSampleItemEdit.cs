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
  public partial class FSampleItemEdit : FormEdit
  {
    public FSampleItemEdit()
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

      sbSampleItemType.BindParentFromCombo(
        bss["ListTypes"], 
        "Item", 
        bss["SampleItemType"], 
        "ListId", 
        bs, 
        "SampleItemType", 
        "SampleItemTypeId = ListId", 
        dc.GetParentFunc("Main", "SampleItemType"), 
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
      
      tStoreName.DataBindings.Add("Text", bss["BlockItem"], "StoreName", true, m);
      tComment.DataBindings.Add("Text", bs, "Comment", true, m);

      object srcCode = CommonLib.GetValueFromObject(bs.DataSource, "SampleCode");
      if (srcCode != null)
        this.Text = this.Text + " (для образца " + srcCode.ToString() + ")";
    }
  }
}
