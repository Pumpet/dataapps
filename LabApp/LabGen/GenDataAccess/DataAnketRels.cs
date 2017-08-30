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
  class DataAnketRels : DataObject
  {
    public DB Db { get { return (DB)db; } }

    public DataAnketRels(DB context, string name = "Main", DataObject p = null) : base(context, name, p) { }
    //-------------------------------------------------------------------------
    public override object GetEntity(object key)
    {
      if (key == null || !KeyExists<int>(key, "AnketRelId")) return typeof(AnketRel);
      return GetObjectFresh(key);
    }
    //-------------------------------------------------------------------------
    public override object GetObject(object key)
    {
      CheckKey<int>(key, "AnketRelId");
      return Db.AnketRels.Where(x => x.AnketRelId == GetKey<int>(key, "AnketRelId")).SingleOrDefault();
    }
    //-------------------------------------------------------------------------
    public override void GetDataBinds(object key, object filter)
    {
      DataBinds.Add("ListLingua", Db.Lists.Where(w => w.ListType.Code == "LANG").Select(x => x));
      DataBinds.Add("ListRelTypes", Db.Lists.Where(w => w.ListType.Code == "RELTYPE").Select(x => x));

      DataBinds.Add("Popul", null);
      GetParentFuncs.Add("Popul", (new DataPopuls(Db)).GetEntity);
      DataBinds.Add("RelType", null);
      GetParentFuncs.Add("RelType", (new DataLists(Db)).GetEntity);
      DataBinds.Add("Lingua", null);
      GetParentFuncs.Add("Lingua", (new DataLists(Db)).GetEntity);
      DataBinds.Add("Place", null);
      GetParentFuncs.Add("Place", (new DataPlaces(Db)).GetEntity);
    }
    //-------------------------------------------------------------------------
    public override object GetList(object key, object filter)
    {
      ChangeContext(new DB(conn));
      return Db.AnketRels.Select(x => x);
    }
    //-------------------------------------------------------------------------
    public override void GetEditData(object key, bool add, object addKey)
    {
      var obj = GetEntityEditData<AnketRel>(key, add, addKey);
      DataBinds["Popul"] = obj.Popul ?? (object)typeof(Popul);
      DataBinds["RelType"] = obj.RelType ?? (object)typeof(List);
      DataBinds["Lingua"] = obj.Lingua ?? (object)typeof(List);
    }
    //-------------------------------------------------------------------------
    public override void SetDefaults(object data, object addKey)
    {
      AnketRel obj = (AnketRel)data;
      
      if (KeyExists<int>(addKey, "AnketId"))
      {
        obj.Anket = (Anket)(new DataAnkets(Db)).GetObject(addKey);
        if (obj.Anket != null)
        {
          obj.AnketId = KeyValue<int>(addKey, "AnketId");
          
          ((Dictionary<string,object>)addKey)["PopulId"] = obj.Anket.PopulId;
          obj.Popul = (Popul)(new DataPopuls(Db)).GetObject(addKey);
          if (obj.Popul != null)
            obj.PopulId = KeyValue<int>(addKey, "PopulId");

          ((Dictionary<string, object>)addKey)["ListId"] = obj.Anket.LinguaId;
          obj.Lingua = (List)(new DataLists(Db)).GetObject(addKey);
          if (obj.Lingua != null)
            obj.LinguaId = KeyValue<int>(addKey, "ListId");
        }
      }
    }
    //-------------------------------------------------------------------------
    public override void Delete(object[] keys)
    {
      DeleteEntities<AnketRel>(keys, Db.AnketRels, null);
    }
    //-------------------------------------------------------------------------
    public override bool Save(object data, bool add) 
    {
      return SaveEntity<AnketRel>(Db.AnketRels, data, add);
    }
    //-------------------------------------------------------------------------
    public override void CloneEntity(object src, object dst)
    {
      AnketRel obj = (AnketRel)src, res = (AnketRel)dst;
      res.Anket = obj.Anket;
      res.AnketId = obj.AnketId;
      res.Popul = obj.Popul;
      res.PopulId = obj.PopulId;
      res.Lingua = obj.Lingua;
      res.LinguaId = obj.LinguaId;
      res.Origin = obj.Origin;
    }
    //-------------------------------------------------------------------------
    protected override void CheckEntity(object data, Dictionary<string, string> errs)
    {
      AnketRel obj = (AnketRel)data;
      
      if (obj.AnketId == 0 || obj.Anket == null)
        errs.Add("sbAnket", "Связь с анкетой: " + msgNoValue);
      
      if (obj.PopulId == 0 || obj.Popul == null)
        errs.Add("sbPopul", msgNoValue);

      if (obj.RelTypeId == 0 || obj.RelType == null)
        errs.Add("sbRelType", msgNoValue);
      else if (Db.AnketRels.Any(x => x.RelTypeId == obj.RelTypeId && x.AnketId == obj.AnketId && x.AnketRelId != obj.AnketRelId))
        errs.Add("sbRelType", msgNoUnique);

      if (obj.LinguaId == 0 || obj.Lingua == null)
        errs.Add("sbLingua", msgNoValue);
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
