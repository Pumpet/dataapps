using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using Manager;
using Context;
using Common;
using System.IO;

namespace GenDataAccess
{
  class DataAnkets : DataObject
  {
    public DB Db { get { return (DB)db; } }

    public DataAnkets(DB context, string name = "Main", DataObject p = null) : base(context, name, p) { }
    //-------------------------------------------------------------------------
    public override object GetEntity(object key)
    {
      if (key == null || !KeyExists<int>(key, "AnketId")) return typeof(Anket);
      return GetObjectFresh(key);
    }
    //-------------------------------------------------------------------------
    public override object GetObject(object key)
    {
      CheckKey<int>(key, "AnketId");
      return Db.Ankets.Where(x => x.AnketId == GetKey<int>(key, "AnketId")).SingleOrDefault();
    }
    //-------------------------------------------------------------------------
    public override void GetDataBinds(object key, object filter)
    {
      DataBinds.Add("ListLingua", Db.Lists.Where(w => w.ListType.Code == "LANG").Select(x => x));

      DataBinds.Add("FilterPoint", (new DataPoints(Db)).GetEntity(filter));
      DataBinds.Add("FilterPopul", (new DataPopuls(Db)).GetEntity(filter));

      DataBinds.Add("Point", null);
      GetParentFuncs.Add("Point", (new DataPoints(Db)).GetEntity);

      DataBinds.Add("Popul", null);
      GetParentFuncs.Add("Popul", (new DataPopuls(Db)).GetEntity);

      DataBinds.Add("Place", null);
      GetParentFuncs.Add("Place", (new DataPlaces(Db)).GetEntity);

      DataBinds.Add("Lingua", null);
      GetParentFuncs.Add("Lingua", (new DataLists(Db)).GetEntity);
    }

    //-------------------------------------------------------------------------
    public override object GetList(object key, object filter)
    {
      ChangeContext(new DB(conn));
      var set = Db.Ankets.Select(x => new { 
        x.AnketId,
        x.RUSID,
        x.GPID,
        x.PointId,
        PointName = x.Point.PointName,
        x.PopulId,
        AnketPopulName = x.Popul.Name,
        x.Origin,
        Man = x.Man == 1 ? "M" : "Ж",
        x.InDate,
        x.InPlace,
        x.Fio,
        x.Comment,
        x.AnketDocs,
        x.AnketAttrs,
        x.AnketRels,
        x.Samples
      });

      int PointId = KeyValue<int>(filter, "PointId");
      if (PointId > 0)
        set = set.Where(x => x.PointId == PointId);

      int PopulId = KeyValue<int>(filter, "PopulId");
      if (PopulId > 0)
        set = set.Where(x => x.PopulId == PopulId);

      return set;
    }
    //-------------------------------------------------------------------------
    public override void GetEditData(object key, bool add, object addKey)
    {
      var obj = GetEntityEditData<Anket>(key, add, addKey);
      DataBinds["Point"] = obj.Point ?? (object)typeof(Point);
      DataBinds["Popul"] = obj.Popul ?? (object)typeof(Popul);
      DataBinds["Lingua"] = obj.Lingua ?? (object)typeof(List);
    }
    //-------------------------------------------------------------------------
    public override void SetDefaults(object data, object addKey)
    {
      Anket obj = (Anket)data;
      
      if (KeyExists<int>(addKey, "PointId"))
      {
        obj.Point = (Point)(new DataPoints(Db)).GetObject(addKey);
        if (obj.Point != null) obj.PointId = KeyValue<int>(addKey, "PointId");
      }

      if (KeyExists<int>(addKey, "PopulId"))
      {
        obj.Popul = (Popul)(new DataPopuls(Db)).GetObject(addKey);
        if (obj.Popul != null) obj.PopulId = KeyValue<int>(addKey, "PopulId");
      }
    }
    //-------------------------------------------------------------------------
    public override void Delete(object[] keys)
    {
      DeleteEntities<Anket>(keys, Db.Ankets, null);
    }
    //-------------------------------------------------------------------------
    public override bool Save(object data, bool add) 
    {
      return SaveEntity<Anket>(Db.Ankets, data, add);
    }
    //-------------------------------------------------------------------------
    public override void CloneEntity(object src, object dst)
    {
      Anket obj = (Anket)src, res = (Anket)dst;

      res.Popul = obj.Popul;
      res.PopulId = obj.PopulId;
      res.Point = obj.Point;
      res.PointId = obj.PointId;
      res.Origin = obj.Origin;
      res.Lingua = obj.Lingua;
      res.LinguaId = obj.LinguaId;
      res.LivePlace = obj.LivePlace;
      res.InPlace = obj.InPlace;
      res.InDate = obj.InDate;
    }
    //-------------------------------------------------------------------------
    protected override void CheckEntity(object data, Dictionary<string, string> errs)
    {
      Anket obj = (Anket)data;
     
      if (obj.PointId == 0 || obj.Point == null)
        errs.Add("sbPoint", msgNoValue);
      
      if (obj.PopulId == 0 || obj.Popul == null)
        errs.Add("sbPopul", msgNoValue);
      
      if (obj.LinguaId == 0 || obj.Lingua == null)
        errs.Add("sbLingua", msgNoValue);

      if (string.IsNullOrWhiteSpace(obj.RUSID))
        errs.Add("RUSID", msgNoValue);
      else if (Db.Ankets.Any(x => x.RUSID == obj.RUSID.Trim() && x.AnketId != obj.AnketId))
        errs.Add("RUSID", msgNoUnique);

      if (string.IsNullOrWhiteSpace(obj.GPID))
        errs.Add("GPID", msgNoValue);
      else if (Db.Ankets.Any(x => x.GPID == obj.GPID.Trim() && x.AnketId != obj.AnketId))
        errs.Add("GPID", msgNoUnique);

      if (string.IsNullOrWhiteSpace(obj.Fio)) errs.Add("Fio", msgNoValue);
      if (DateTime.Compare(obj.BirthDate, minSqlSmallDate) <= 0) errs.Add("BirthDate", msgNoValue);
      if (DateTime.Compare(obj.InDate, minSqlSmallDate) <= 0) errs.Add("InDate", msgNoValue);

    }
    //-------------------------------------------------------------------------
    public override object ExecCommand(string command, object key, object filter, object data, object[] keys)
    {
      if (command == "AnketProcess")
      {
        foreach (var item in keys)
	      {
          try
          {
            string msg = null;
            CheckKey<int>(item, "AnketId");
            int ret = Db.AnketProcess(GetKey<int>(item, "AnketId"), ref msg);
            if (ret > 0)
              throw new Exception(string.IsNullOrWhiteSpace(msg) ? "Код ошибки = " + ret.ToString() : msg);
          }
          catch (Exception e)
          {
            Loger.SendMess(e, "Ошибка обработки анкеты!");
          }
	      }
      }
      return null;
    }
    //-------------------------------------------------------------------------
    public override void SetCommands(object cmds, object key, object data, object[] keys, string code)
    {
      if (!(cmds is Dictionary<string, Action<string>>))
        return;
      var cs = (Dictionary<string, Action<string>>)cmds;
      if (data is AnketDoc)
      {
        var obj = (AnketDoc)data;
        if (!File.Exists(obj.Link))
          cs.Remove("OpenFile");
      }
    }
  }
}
