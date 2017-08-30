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
  class DataSampleItems : DataObject
  { 
    public DB Db { get { return (DB)db; } }

    public DataSampleItems(DB context, string name = "Main", DataObject p = null) : base(context, name, p) 
    {
      OnCloneEntity = null;
    }
    //-------------------------------------------------------------------------
    public override object GetEntity(object key)
    {
      if (key == null || !KeyExists<int>(key, "SampleItemId")) return typeof(SampleItem);
      return GetObjectFresh(key);
    }
    //-------------------------------------------------------------------------
    public override object GetObject(object key)
    {
      CheckKey<int>(key, "SampleItemId");
      return Db.SampleItems.Where(x => x.SampleItemId == GetKey<int>(key, "SampleItemId")).SingleOrDefault();
    }
    //-------------------------------------------------------------------------
    public override void GetDataBinds(object key, object filter)
    {
      DataBinds.Add("Sample", null);
      GetParentFuncs.Add("Sample", (new DataSamples(Db)).GetEntity);

      DataBinds.Add("ListTypes", Db.Lists.Where(w => w.ListType.Code == "SAMPLEITEMTYPE").Select(x => x));
      DataBinds.Add("SampleItemType", null);
      GetParentFuncs.Add("SampleItemType", (new DataLists(Db)).GetEntity);

      DataBinds.Add("BlockItem", null);
      GetParentFuncs.Add("BlockItem", (new DataBlockItems(Db)).GetEntity);
    }
    //-------------------------------------------------------------------------
    public override object GetList(object key, object filter)
    {
      ChangeContext(new DB(conn));
      return Db.SampleItems.Select(x => x);
    }
    //-------------------------------------------------------------------------
    public override void GetEditData(object key, bool add, object addKey)
    {
      var obj = GetEntityEditData<SampleItem>(key, add, addKey);
      DataBinds["Sample"] = obj.Sample ?? (object)typeof(Sample);
      DataBinds["SampleItemType"] = obj.SampleItemType ?? (object)typeof(List);
      DataBinds["BlockItem"] = obj.BlockItem ?? (object)typeof(BlockItem);
    }
    //-------------------------------------------------------------------------
    public override void SetDefaults(object data, object addKey)
    {
      SampleItem obj = (SampleItem)data;
      
      if (KeyExists<int>(addKey, "SampleId"))
      {
        obj.Sample = (Sample)(new DataSamples(Db)).GetObject(addKey);
        if (obj.Sample != null) obj.SampleId = KeyValue<int>(addKey, "SampleId");
      }
    }
    //-------------------------------------------------------------------------
    public override void Delete(object[] keys)
    {
      DeleteEntities<SampleItem>(keys, Db.SampleItems, null);
    }
    //-------------------------------------------------------------------------
    public override bool Save(object data, bool add) 
    {
      return SaveEntity<SampleItem>(Db.SampleItems, data, add);
    }
    //-------------------------------------------------------------------------
    public override void CloneEntity(object src, object dst)
    {
      SampleItem obj = (SampleItem)src, res = (SampleItem)dst;
    }
    //-------------------------------------------------------------------------
    protected override void CheckEntity(object data, Dictionary<string, string> errs)
    {
      SampleItem obj = (SampleItem)data;
      
      if (obj.SampleId == 0 || obj.Sample == null)
        errs.Add("SampleId", msgNoValue);
      
      if (obj.SampleItemTypeId == 0 || obj.SampleItemType == null)
        errs.Add("sbSampleItemType", msgNoValue);
      
      if (obj.BlockItemId == 0 || obj.BlockItem == null)
        errs.Add("sbBlockItem", msgNoValue);
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
