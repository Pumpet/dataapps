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
  class DataResults : DataObject
  {
    public DB Db { get { return (DB)db; } }

    public DataResults(DB context, string name = "Main", DataObject p = null) : base(context, name, p) { }
    //-------------------------------------------------------------------------
    public override object GetEntity(object key)
    {
      if (key == null || !KeyExists<int>(key, "ResultId")) return typeof(Result);
      return GetObject(key);
    }
    //-------------------------------------------------------------------------
    public override object GetObject(object key)
    {
      CheckKey<int>(key, "ResultId");
      return Db.Results.Where(x => x.ResultId == GetKey<int>(key, "ResultId")).SingleOrDefault();
    }
    //-------------------------------------------------------------------------
    public override void GetDataBinds(object key, object filter)
    {
      DataBinds.Add("Sample", null);
      GetParentFuncs.Add("Sample", (new DataSamples(Db)).GetEntity);

      DataBinds.Add("FilterPopul", (new DataPopuls(Db)).GetEntity(filter));
      DataBinds.Add("Popul", null);
      GetParentFuncs.Add("Popul", (new DataPopuls(Db)).GetEntity);

      DataBinds.Add("FilterPoint", (new DataPoints(Db)).GetEntity(filter));
      GetParentFuncs.Add("Point", (new DataPoints(Db)).GetEntity);
    }
    //-------------------------------------------------------------------------
    public override object GetList(object key, object filter)
    {
      ChangeContext(new DB(conn));
      var set = Db.Results.Select(x => x);

      int PointId = KeyValue<int>(filter, "PointId");
      if (PointId > 0)
        set = set.Where(x => x.Sample.Anket.PointId == PointId);

      int PopulId = KeyValue<int>(filter, "PopulId");
      if (PopulId > 0)
        set = set.Where(x => x.PopulId == PopulId);

      if (!KeyValue<bool>(filter, "Mt"))
        set = set.Where(x => x.ResultType == 1);
      if (!KeyValue<bool>(filter, "Y"))
        set = set.Where(x => x.ResultType == 0);

      return set;
    }
    //-------------------------------------------------------------------------
    public override void GetEditData(object key, bool add, object addKey)
    {
      var obj = GetEntityEditData<Result>(key, add, addKey);
      DataBinds["Sample"] = obj.Sample ?? (object)typeof(Sample);
      DataBinds["Popul"] = obj.Popul ?? (object)typeof(Popul);
    }
    //-------------------------------------------------------------------------
    public override void SetDefaults(object data, object addKey)
    {
      Result obj = (Result)data;
      if (KeyExists<int>(addKey, "SampleId"))
      {
        obj.Sample = (Sample)(new DataSamples(Db)).GetObject(addKey);
        if (obj.Sample != null)
        {
          obj.SampleId = KeyValue<int>(addKey, "SampleId");
          obj.Popul = obj.Sample.Anket.Popul;
          if (obj.Popul != null)
            obj.PopulId = obj.Popul.PopulId;
        }
      }
    }
    //-------------------------------------------------------------------------
    public override void Delete(object[] keys)
    {
      DeleteEntities<Result>(keys, Db.Results, null);
    }
    //-------------------------------------------------------------------------
    public override bool Save(object data, bool add) 
    {
      Result res = (Result)data;
      if (res.ResultType == 1)
      {
        res.GWS1 = null;
        res.GWS2 = null;
      }
      if (res.ResultType == 0)
      {
        res.B_DYS389I = null;
        res.B_DYS389II = null;
        res.B_DYS390 = null;
        res.B_DYS456 = null;
        res.G_DYS19 = null;
        res.G_DYS385 = null;
        res.G_DYS385_2 = null;
        res.G_DYS458 = null;
        res.R_DYS437 = null;
        res.R_DYS438 = null;
        res.R_DYS448 = null;
        res.R_Y_GATA_H4 = null;
        res.Y_DYS391 = null;
        res.Y_DYS392 = null;
        res.Y_DYS393 = null;
        res.Y_DYS439 = null;
        res.Y_DYS635 = null;
        res.DYS449 = null;
        res.DYS460 = null;
        res.DYS481 = null;
        res.DYS518 = null;
        res.DYS533 = null;
        res.DYS570 = null;
        res.DYS576 = null;
        res.DYS627 = null;
        res.DYF387S1 = null;
        res.DYS447 = null; 
      }
      return SaveEntity<Result>(Db.Results, data, add);
    }
    //-------------------------------------------------------------------------
    public override void CloneEntity(object src, object dst)
    {
      Result obj = (Result)src, res = (Result)dst;
      res.Sample = obj.Sample;
      res.SampleId = obj.SampleId;
      res.ResultType = obj.ResultType;
      res.Popul = obj.Popul;
      res.PopulId = obj.PopulId;
      res.Prediction = obj.Prediction;
      res.Probability = obj.Probability;
      res.Haplogroup = obj.Haplogroup;
      res.Marker = obj.Marker;
      res.Comment = obj.Comment;
      if (obj.ResultType == 0)
      {
        res.GWS1 = obj.GWS1;
        res.GWS2 = obj.GWS2;
      }
      if (obj.ResultType == 1)
      {
        res.B_DYS389I = obj.B_DYS389I;
        res.B_DYS389II = obj.B_DYS389II;
        res.B_DYS390 = obj.B_DYS390;
        res.B_DYS456 = obj.B_DYS456;
        res.G_DYS19 = obj.G_DYS19;
        res.G_DYS385 = obj.G_DYS385;
        res.G_DYS385_2 = obj.G_DYS385_2;
        res.G_DYS458 = obj.G_DYS458;
        res.R_DYS437 = obj.R_DYS437;
        res.R_DYS438 = obj.R_DYS438;
        res.R_DYS448 = obj.R_DYS448;
        res.R_Y_GATA_H4 = obj.R_Y_GATA_H4;
        res.Y_DYS391 = obj.Y_DYS391;
        res.Y_DYS392 = obj.Y_DYS392;
        res.Y_DYS393 = obj.Y_DYS393;
        res.Y_DYS439 = obj.Y_DYS439;
        res.Y_DYS635 = obj.Y_DYS635;
        res.DYS449 = obj.DYS449;
        res.DYS460 = obj.DYS460;
        res.DYS481 = obj.DYS481;
        res.DYS518 = obj.DYS518;
        res.DYS533 = obj.DYS533;
        res.DYS570 = obj.DYS570;
        res.DYS576 = obj.DYS576;
        res.DYS627 = obj.DYS627;
        res.DYF387S1 = obj.DYF387S1;
        res.DYS447 = obj.DYS447;
      }
    }
    //-------------------------------------------------------------------------
    protected override void CheckEntity(object data, Dictionary<string, string> errs)
    {
      Result obj = (Result)data;
      if (obj.SampleId == 0 || obj.Sample == null)
        errs.Add("SampleId", msgNoValue);

      if (obj.ResultType != 0 && obj.ResultType != 1)
        errs.Add("ResultType", msgNoValue);
      else if (Db.Results.Any(x => x.SampleId == obj.SampleId && x.ResultType == obj.ResultType && x.ResultId != obj.ResultId))
        errs.Add("ResultType", msgNoUnique);

      if (obj.PopulId == 0 || obj.Popul == null)
        errs.Add("sbPopul", msgNoValue);

      if (string.IsNullOrWhiteSpace(obj.Prediction))
        errs.Add("Prediction", msgNoValue);

      if (obj.Probability <= 0 || obj.Probability > 100)
        errs.Add("Probability", msgIncorrect);      
      
      if (string.IsNullOrWhiteSpace(obj.Haplogroup))
        errs.Add("Haplogroup", msgNoValue);

      if (string.IsNullOrWhiteSpace(obj.Marker))
        errs.Add("Marker", msgNoValue);
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
