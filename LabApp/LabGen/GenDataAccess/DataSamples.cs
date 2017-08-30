using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using Manager;
using Context;
using Common;

namespace GenDataAccess
{
  class DataSamples : DataObject
  {
    public DB Db { get { return (DB)db; } }

    public DataSamples(DB context, string name = "Main", DataObject p = null) : base(context, name, p) { }
    //-------------------------------------------------------------------------
    public override object GetEntity(object key)
    {
      if (key == null || !KeyExists<int>(key, "SampleId")) return typeof(Sample);
      return GetObjectFresh(key);
    }
    //-------------------------------------------------------------------------
    public override object GetObject(object key)
    {
      CheckKey<int>(key, "SampleId");
      return Db.Samples.Where(x => x.SampleId == GetKey<int>(key, "SampleId")).SingleOrDefault();
    }
    //-------------------------------------------------------------------------
    public override void GetDataBinds(object key, object filter)
    {
      DataBinds.Add("ListTypes", Db.Lists.Where(w => w.ListType.Code == "SAMPLETYPE").Select(x => x));

      DataBinds.Add("FilterPoint", (new DataPoints(Db)).GetEntity(filter));
      GetParentFuncs.Add("Point", (new DataPoints(Db)).GetEntity);
      
      DataBinds.Add("Anket", null);
      GetParentFuncs.Add("Anket", (new DataAnkets(Db)).GetEntity);
      DataBinds.Add("SampleType", null);
      GetParentFuncs.Add("SampleType", (new DataLists(Db)).GetEntity);
    }
    //-------------------------------------------------------------------------
    public override object GetList(object key, object filter)
    {
      ChangeContext(new DB(conn));
      var set = Db.Samples.Select(x => new
        {
          x.SampleId,
          x.AnketId,
          AnketRUSID = x.Anket.RUSID,
          AnketGPID = x.Anket.GPID,
          AnketPointId = x.Anket.Point.PointId,
          AnketPointName = x.Anket.Point.PointName,
          AnketPopulName = x.Anket.Popul.Name,
          Man = x.Anket.Man == 1 ? "M" : "Ж",
          x.SampleCode,
          x.SampleTypeId,
          x.SampleTypeName,
          x.SampleItems,
          x.DnkItems,
          x.Results
        });

      int PointId = KeyValue<int>(filter, "PointId");
      if (PointId > 0)
        set = set.Where(x => x.AnketPointId == PointId);

      return set;
    }
    //-------------------------------------------------------------------------
    public override void GetEditData(object key, bool add, object addKey)
    {
      var obj = GetEntityEditData<Sample>(key, add, addKey);
      DataBinds["Anket"] = obj.Anket ?? (object)typeof(Anket);
      DataBinds["SampleType"] = obj.SampleType ?? (object)typeof(List);
    }
    //-------------------------------------------------------------------------
    public override void SetDefaults(object data, object addKey)
    {
      Sample obj = (Sample)data;

      if (KeyExists<int>(addKey, "AnketId"))
      {
        obj.Anket = (Anket)(new DataAnkets(Db)).GetObject(addKey);
        if (obj.Anket != null)
        {
          obj.AnketId = KeyValue<int>(addKey, "AnketId");
          obj.SampleCode = obj.Anket.RUSID;
        }
      }
    }
    //-------------------------------------------------------------------------
    public override void Delete(object[] keys)
    {
      DeleteEntities<Sample>(keys, Db.Samples, null);
    }
    //-------------------------------------------------------------------------
    public override bool Save(object data, bool add) 
    {
      return SaveEntity<Sample>(Db.Samples, data, add);
    }
    //-------------------------------------------------------------------------
    public override void CloneEntity(object src, object dst)
    {
      Sample obj = (Sample)src, res = (Sample)dst;

      res.SampleType = obj.SampleType;
      res.SampleTypeId = obj.SampleTypeId;
      res.Anket = obj.Anket;
      res.AnketId = obj.AnketId;
    }
    //-------------------------------------------------------------------------
    protected override void CheckEntity(object data, Dictionary<string, string> errs)
    {
      Sample obj = (Sample)data;
      
      if (obj.AnketId == 0 || obj.Anket == null)
        errs.Add("sbAnket", msgNoValue);
      if (obj.SampleTypeId == 0 || obj.SampleType == null)
        errs.Add("sbSampleType", msgNoValue);
      
      if (string.IsNullOrWhiteSpace(obj.SampleCode))
        errs.Add("SampleCode", msgNoValue);
      else if (Db.Samples.Any(x => x.SampleCode == obj.SampleCode.Trim() && x.SampleId != obj.SampleId))
        errs.Add("SampleCode", msgNoUnique);
    }
    //-------------------------------------------------------------------------
    public override object ExecCommand(string command, object key, object filter, object data, object[] keys)
    {
      return null;
    }
    //-------------------------------------------------------------------------
    public override void SetCommands(object cmds, object key, object data, object[] keys, string code)
    {
    }
  }
}
