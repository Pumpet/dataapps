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
  class DataAnketAttrs : DataObject
  {
    public DB Db { get { return (DB)db; } }

    public DataAnketAttrs(DB context, string name = "Main", DataObject p = null) : base(context, name, p) { }
    //-------------------------------------------------------------------------
    public override object GetEntity(object key)
    {
      if (key == null || !KeyExists<int>(key, "AttrId")) return typeof(AnketAttr);
      return GetObjectFresh(key);
    }
    //-------------------------------------------------------------------------
    public override object GetObject(object key)
    {
      CheckKey<int>(key, "AttrId");
      return Db.AnketAttrs.Where(x => x.AttrId == GetKey<int>(key, "AttrId")).SingleOrDefault();
    }
    //-------------------------------------------------------------------------
    public override void GetDataBinds(object key, object filter)
    {
      DataBinds.Add("Anket", null);
      GetParentFuncs.Add("Anket", (new DataAnkets(Db)).GetEntity);

      DataBinds.Add("ListTypes", Db.Lists.Where(w => w.ListType.Code == "ANKETATTR").Select(x => x));
      DataBinds.Add("Type", null);
      GetParentFuncs.Add("Type", (new DataLists(Db)).GetEntity);
    }
    //-------------------------------------------------------------------------
    public override object GetList(object key, object filter)
    {
      ChangeContext(new DB(conn));
      return Db.AnketAttrs.Select( x => x );
    }
    //-------------------------------------------------------------------------
    public override void GetEditData(object key, bool add, object addKey)
    {
      var obj = GetEntityEditData<AnketAttr>(key, add, addKey);
      DataBinds["Anket"] = obj.Anket ?? (object)typeof(Anket);
      DataBinds["Type"] = obj.Type ?? (object)typeof(List);
    }
    //-------------------------------------------------------------------------
    public override void SetDefaults(object data, object addKey)
    {
      AnketAttr obj = (AnketAttr)data;
      if (KeyExists<int>(addKey, "AnketId"))
      {
        obj.Anket = (Anket)(new DataAnkets(Db)).GetObject(addKey);
        if (obj.Anket != null) obj.AnketId = KeyValue<int>(addKey, "AnketId");
      }
    }
    //-------------------------------------------------------------------------
    public override void Delete(object[] keys)
    {
      DeleteEntities<AnketAttr>(keys, Db.AnketAttrs, null);
    }
    //-------------------------------------------------------------------------
    public override bool Save(object data, bool add) 
    {
      return SaveEntity<AnketAttr>(Db.AnketAttrs, data, add);
    }
    //-------------------------------------------------------------------------
    public override void CloneEntity(object src, object dst)
    {
      AnketAttr obj = (AnketAttr)src, res = (AnketAttr)dst;
      res.Anket = obj.Anket;
      res.AnketId = obj.AnketId;
      res.Type = obj.Type;
      res.TypeId = obj.TypeId;
    }
    //-------------------------------------------------------------------------
    protected override void CheckEntity(object data, Dictionary<string, string> errs)
    {
      AnketAttr obj = (AnketAttr)data;

      if (obj.AnketId == 0 || obj.Anket == null)
        errs.Add("sbAnket", "Связь с анкетой: " + msgNoValue);

      if (obj.TypeId == 0 || obj.Type == null)
        errs.Add("sbType", msgNoValue);
      
      if (string.IsNullOrWhiteSpace(obj.Value))
        errs.Add("Value", msgNoValue);
      else if (Db.AnketAttrs.Any(x => x.Value == obj.Value.Trim() && x.TypeId == obj.TypeId 
                                      && x.AnketId == obj.AnketId && x.AttrId != obj.AttrId))
        errs.Add("Value", msgNoUnique);
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
