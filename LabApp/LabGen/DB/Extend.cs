using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context
{
  public partial class AnketAttr
  {
    public string TypeName { get { return this.Type.Item; } }
  }
  //---------------------------------------------------------------------------
  public partial class Point
  {
    public string PopulName { get { return this.Popul.Name; } }
    public string ExpedName { get { return this.Exped.Name; } }
    public string EtnoName { get { return this.Popul.Etno.Name; } }
  }
  //---------------------------------------------------------------------------
  public partial class AnketRel
  {
    public string PopulName { get { return this.Popul.Name; } }
    public string RelTypeName { get { return this.RelType.Item; } }
    public string LinguaName { get { return this.Lingua.Item; } }
  }
  //---------------------------------------------------------------------------
  public partial class Store
  {
    public string StoreName { 
      get { 
        return string.Format("{0} {1} {2} {3} {4}",
          !string.IsNullOrWhiteSpace(this.Container) ? "контейнер:" + this.Container + ",": "",
          !string.IsNullOrWhiteSpace(this.Fridge) ? "холодильник:" + this.Fridge + "," : "",
          !string.IsNullOrWhiteSpace(this.FridgeModule) ? "отсек:" + this.FridgeModule + "," : "",
          !string.IsNullOrWhiteSpace(this.FridgeShelf) ? "полка:" + this.FridgeShelf + "," : "",
          this.Lab).Trim(); 
          //this.Lab, 
          //!string.IsNullOrWhiteSpace(this.Fridge) ? " - " + this.Fridge : "",
          //!string.IsNullOrWhiteSpace(this.FridgeModule) ? " [" + this.FridgeModule + "]" : "",
          //!string.IsNullOrWhiteSpace(this.FridgeShelf) ? " [" + this.FridgeShelf + "]" : "",
          //!string.IsNullOrWhiteSpace(this.Container) ? " - " + this.Container : ""); 
      } 
    }
  }
  //---------------------------------------------------------------------------
  public partial class Sample
  {
    public string SampleTypeName { get { return this.SampleType.Item; } }
  }
  //---------------------------------------------------------------------------
  public partial class SampleItem
  {
    public string VialType { get { return this.SampleItemType.Item; } }
    public string SampleCode { get { return this.Sample.SampleCode; } }
    public int BlockId { get { return this.BlockItem.Block.BlockId; } }
    public string BlockCode { get { return this.BlockItem.Block.BlockCode; } }
    public string BlockItemCode { get { return this.BlockItem.BlockItemCode; } }
    public Store Store { get { return this.BlockItem.Block.Store; } }
    public string Fridge { get { return this.Store == null ? "" : this.Store.Fridge; } }
    public string FridgeModule { get { return this.Store == null ? "" : this.Store.FridgeModule; } }
    public string FridgeShelf { get { return this.Store == null ? "" : this.Store.FridgeShelf; } }
    public string Container { get { return this.Store == null ? "" : this.Store.Container; } }
    public string Lab { get { return this.Store == null ? "" : this.Store.Lab; } }
  }
  //---------------------------------------------------------------------------
  public partial class DnkItem
  {
    public string VialType { get { return this.DnkItemType.Item; } }
    public string ExtractMethodName { get { return this.ExtractMethod.Item; } }
    public string SampleCode { get { return this.Sample.SampleCode; } }
    public int BlockId { get { return this.BlockItem.Block.BlockId; } }
    public string BlockCode { get { return this.BlockItem.Block.BlockCode; } }
    public string BlockItemCode { get { return this.BlockItem.BlockItemCode; } }
    public Store Store { get { return this.BlockItem.Block.Store; } }
    public string Fridge { get { return this.Store == null ? "" : this.Store.Fridge; } }
    public string FridgeModule { get { return this.Store == null ? "" : this.Store.FridgeModule; } }
    public string FridgeShelf { get { return this.Store == null ? "" : this.Store.FridgeShelf; } }
    public string Container { get { return this.Store == null ? "" : this.Store.Container; } }
    public string Lab { get { return this.Store == null ? "" : this.Store.Lab; } }
  }
  //---------------------------------------------------------------------------
  public partial class Block
  {
    public bool IsSetItems { get; set; }
  }
  //---------------------------------------------------------------------------
  public partial class BlockItem
  {
    public int SampleId { get; set; }
    public string SampleCode { get; set; }
    public string SampleType { get; set; }
    public string VialType { get; set; }
    public string StoreName { get { return this.Block.Store == null ? "" : this.Block.Store.StoreName; } }
    public string BlockCode { get { return string.Format("штатив: {0}, ячейка: {1}", this.Block.BlockCode, this.BlockItemCode); } }

    partial void OnLoaded()
    {
      SampleItem s = this.SampleItems.FirstOrDefault();
      if (s != null)
      {
        SampleId = s.Sample.SampleId;
        SampleCode = s.Sample.SampleCode;
        SampleType = s.Sample.SampleType.Item;
        VialType = s.SampleItemType.Item;
      }
      else
      {
        DnkItem d = this.DnkItems.FirstOrDefault();
        if (d != null)
        {
          SampleId = d.Sample.SampleId;
          SampleCode = d.Sample.SampleCode;
          SampleType = d.Sample.SampleType.Item;
          VialType = "ДНК " + d.DnkItemType.Item;
        } 
      }
    }
  }
  //---------------------------------------------------------------------------
  public partial class Result
  {
    public string ResultTypeName { get { return this.ResultType == 0 ? "Mt" : this.ResultType == 1 ? "Y" : "Unknown!"; } }
    public string SampleCode { get { return this.Sample.SampleCode; } }
    public string SampleTypeName { get { return this.Sample.SampleType.Item; } }
    public string PopulName { get { return this.Popul.Name; } }
    public string EtnoName { get { return this.Popul.Etno.Name; } }
    public string AnketRUSID { get { return this.Sample.Anket.RUSID; } }
    public string AnketGPID { get { return this.Sample.Anket.GPID; } }
    public string AnketMan { get { return this.Sample.Anket.Man == 1 ? "М" : "Ж"; } }
    public string PointName { get { return this.Sample.Anket.Point.PointName; } }
    //public int PointId { get { return this.Sample.Anket.Point.PointId; } }
  }
}
