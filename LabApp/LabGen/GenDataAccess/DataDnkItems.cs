using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manager;
using Context;
using Common;

namespace GenDataAccess
{
  class DataDnkItems : DataObject
  {
    public DB Db { get { return (DB)db; } }

    public DataDnkItems(DB context, string name = "Main", DataObject p = null) : base(context, name, p) 
    {
      OnCloneEntity = null;
    }
    //-------------------------------------------------------------------------
    public override object GetEntity(object key)
    {
      if (key == null || !KeyExists<int>(key, "DnkItemId")) return typeof(DnkItem);
      return GetObjectFresh(key);
    }
    //-------------------------------------------------------------------------
    public override object GetObject(object key)
    {
      CheckKey<int>(key, "DnkItemId");
      return Db.DnkItems.Where(x => x.DnkItemId == GetKey<int>(key, "DnkItemId")).SingleOrDefault();
    }
    //-------------------------------------------------------------------------
    public override void GetDataBinds(object key, object filter)
    {
      DataBinds.Add("Sample", null);
      GetParentFuncs.Add("Sample", (new DataSamples(Db)).GetEntity);

      DataBinds.Add("ListDnkItemType", Db.Lists.Where(w => w.ListType.Code == "DNKITEMTYPE").Select(x => x));
      DataBinds.Add("DnkItemType", null);
      GetParentFuncs.Add("DnkItemType", (new DataLists(Db)).GetEntity);

      DataBinds.Add("ListExtractMethod", Db.Lists.Where(w => w.ListType.Code == "DNKEXTRACTMETHOD").Select(x => x));
      DataBinds.Add("ExtractMethod", null);
      GetParentFuncs.Add("ExtractMethod", (new DataLists(Db)).GetEntity);

      DataBinds.Add("BlockItem", null);
      GetParentFuncs.Add("BlockItem", (new DataBlockItems(Db)).GetEntity);

    }
    //-------------------------------------------------------------------------
    public override object GetList(object key, object filter)
    {
      ChangeContext(new DB(conn));
      return Db.DnkItems.Select(x => x);
    }
    //-------------------------------------------------------------------------
    public override void GetEditData(object key, bool add, object addKey)
    {
      var obj = GetEntityEditData<DnkItem>(key, add, addKey);
      DataBinds["Sample"] = obj.Sample ?? (object)typeof(Sample);
      DataBinds["DnkItemType"] = obj.DnkItemType ?? (object)typeof(List);
      DataBinds["ExtractMethod"] = obj.ExtractMethod ?? (object)typeof(List);
      DataBinds["BlockItem"] = obj.BlockItem ?? (object)typeof(BlockItem);
    }
    //-------------------------------------------------------------------------
    public override void SetDefaults(object data, object addKey)
    {
      DnkItem obj = (DnkItem)data;
      
      if (KeyExists<int>(addKey, "SampleId"))
      {
        obj.Sample = (Sample)(new DataSamples(Db)).GetObject(addKey);
        if (obj.Sample != null) obj.SampleId = KeyValue<int>(addKey, "SampleId");
      }
    }
    //-------------------------------------------------------------------------
    public override void Delete(object[] keys)
    {
      DeleteEntities<DnkItem>(keys, Db.DnkItems, null);
    }
    //-------------------------------------------------------------------------
    public override bool Save(object data, bool add) 
    {
      return SaveEntity<DnkItem>(Db.DnkItems, data, add);
    }
    //-------------------------------------------------------------------------
    public override void CloneEntity(object src, object dst)
    {
      DnkItem obj = (DnkItem)src, res = (DnkItem)dst;
    }
    //-------------------------------------------------------------------------
    protected override void CheckEntity(object data, Dictionary<string, string> errs)
    {
      DnkItem obj = (DnkItem)data;
      
      if (obj.SampleId == 0 || obj.Sample == null)
        errs.Add("SampleId", msgNoValue);

      if (obj.DnkItemTypeId == 0 || obj.DnkItemType == null)
        errs.Add("sbDnkItemType", msgNoValue);
      else if (Db.DnkItems.Any(x => x.DnkItemTypeId == obj.DnkItemTypeId && x.SampleId == obj.SampleId && x.DnkItemId != obj.DnkItemId))
        errs.Add("sbDnkItemType", msgNoUnique);

      if (obj.BlockItemId == 0 || obj.BlockItem == null)
        errs.Add("sbBlockItem", msgNoValue);

      if (obj.Concentration <= 0)
        errs.Add("Concentration", msgIncorrect);
      if (obj.Quality <= 0)
        errs.Add("Quality", msgIncorrect);
      if (obj.Volume <= 0)
        errs.Add("Volume", msgIncorrect);
      
      if (obj.ExtractMethodId == 0 || obj.ExtractMethod == null)
        errs.Add("sbExtractMethod", msgNoValue);
    }
    //-------------------------------------------------------------------------
    public override object ExecCommand(string command, object key, object filter, object data, object[] keys)
    {
      return null;
    }
    //-------------------------------------------------------------------------
    public override void SetCommands(object cmds, object key, object data, object[] keys, string code)
    {
      if (!(cmds is Dictionary<string, Action<string>>))
        return;
      var cs = (Dictionary<string, Action<string>>)cmds;
    }

  }
}
