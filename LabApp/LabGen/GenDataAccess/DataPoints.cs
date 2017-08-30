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
  class DataPoints : DataObject
  {
    public DB Db { get { return (DB)db; } }

    public DataPoints(DB context, string name = "Main", DataObject p = null) : base(context, name, p)
    {
      if (name == "Main") // ввод через грид, дочерний к экспедициям
      {
        OnDelete = null;
      }
    }
    //-------------------------------------------------------------------------
    public override object GetEntity(object key)
    {
      if (key == null || !KeyExists<int>(key,"PointId")) return typeof(Point);
      return GetObjectFresh(key);
    }
    //-------------------------------------------------------------------------
    public override object GetObject(object key)
    {
      CheckKey<int>(key, "PointId");
      return Db.Points.Where(x => x.PointId == GetKey<int>(key, "PointId")).SingleOrDefault();
    }
    //-------------------------------------------------------------------------
    public override void GetDataBinds(object key, object filter)
    {
      DataBinds.Add("Popul", null);
      GetParentFuncs.Add("Popul", (new DataPopuls(Db)).GetEntity);
      
      DataBinds.Add("Exped", null);
      GetParentFuncs.Add("Exped", (new DataExpeds(Db)).GetEntity);
    }
    //-------------------------------------------------------------------------
    public override object GetList(object key, object filter)
    {
      ChangeContext(new DB(conn));
      return Db.Points.Select(x => x);
    }
    //-------------------------------------------------------------------------
    public override void GetEditData(object key, bool add, object addKey)
    {
      var obj = GetEntityEditData<Point>(key, add, addKey);      
      DataBinds["Popul"] = obj.Popul ?? (object)typeof(Popul);
      DataBinds["Exped"] = obj.Exped ?? (object)typeof(Exped);
    }
    //-------------------------------------------------------------------------
    public override void SetDefaults(object data, object addKey)
    {
      Point obj = (Point)data;
      
      if (KeyExists<int>(addKey, "ExpedId"))
      {
        obj.Exped = (Exped)(new DataExpeds(Db)).GetObject(addKey);
        if (obj.Exped != null) obj.ExpedId = KeyValue<int>(addKey, "ExpedId");
      }

      if (string.IsNullOrWhiteSpace(obj.Head) && obj.Exped != null)
        obj.Head = obj.Exped.Head;
      if (string.IsNullOrWhiteSpace(obj.Region) && obj.Exped != null)
        obj.Region = obj.Exped.Region;
      if (string.IsNullOrWhiteSpace(obj.RegionEn) && !string.IsNullOrWhiteSpace(obj.Region))
        obj.RegionEn = Translit.GetTranslit(obj.Region);
    }
    //-------------------------------------------------------------------------
    public override void Delete(object[] keys)
    {
      DeleteEntities<Point>(keys, Db.Points, null);
    }
    //-------------------------------------------------------------------------
    public override bool Save(object data, bool add)
    {
      return SaveEntity<Point>(Db.Points, data, add);
    }
    //-------------------------------------------------------------------------
    public override void CloneEntity(object src, object dst) 
    {
      Point entity = (Point)src, res = (Point)dst;
      res.Exped = entity.Exped;
      res.ExpedId = entity.ExpedId;
      res.Popul = entity.Popul;
      res.PopulId = entity.PopulId;
      res.Head = entity.Head;
      res.Region = entity.Region;
      res.RegionEn = entity.RegionEn;
    }
    //-------------------------------------------------------------------------
    protected override void CheckEntity(object data, Dictionary<string, string> errs)
    {
      
      Point entity = (Point)data;

      if (string.IsNullOrWhiteSpace(entity.PointName))
        errs.Add("PointName", msgNoValue);
      else if (Db.Points.Any(x => x.PointName == entity.PointName.Trim() && x.PointId != entity.PointId))
        errs.Add("PointName", msgNoUnique);

      if (string.IsNullOrWhiteSpace(entity.Period))
        errs.Add("Period", msgNoValue);
      if (string.IsNullOrWhiteSpace(entity.Region))
        errs.Add("Region", msgNoValue);
      if (string.IsNullOrWhiteSpace(entity.Head))
        errs.Add("Head", msgNoValue);

      if (entity.PopulId == 0 || entity.Popul == null)
        errs.Add("sbPopul", msgNoValue);
      if (entity.ExpedId == 0 || entity.Exped == null)
        errs.Add("sbExped", msgNoValue);
    }
    //-------------------------------------------------------------------------
    public override object ExecCommand(string command, object key, object filter, object data, object[] keys)
    {
      return null;
    }
    //-------------------------------------------------------------------------
    public override void SetCommands(object cmds, object key, object data, object[] keys, string code) { }
  }
}
