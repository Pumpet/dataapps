using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using Manager;
using Context;
using Common;

namespace GenDataAccess
{
  class DataEtnos : DataObject
  {
    public DB Db { get { return (DB)db; } }
    public DataEtnos(DB context, string name = "Main", DataObject p = null) : base(context, name, p)
    {
      OnCloneEntity = null;
    }
    //-------------------------------------------------------------------------
    public override object GetEntity(object key)
    {
      if (key == null || !KeyExists<int>(key, "EtnoId")) return typeof(Etno);
      return GetObjectFresh(key);
    }
    //-------------------------------------------------------------------------
    public override object GetObject(object key)
    {
      CheckKey<int>(key, "EtnoId");
      return Db.Etnos.Where(x => x.EtnoId == GetKey<int>(key, "EtnoId")).SingleOrDefault();
    }
    //-------------------------------------------------------------------------
    public override void GetDataBinds(object key, object filter) { }
    //-------------------------------------------------------------------------
    public override object GetList(object key, object filter)
    {
      ChangeContext(new DB(conn));
      return Db.Etnos.Select(x => x);
    }
    //-------------------------------------------------------------------------
    public override void GetEditData(object key, bool add, object addKey) { }
    //-------------------------------------------------------------------------
    public override void SetDefaults(object data, object addKey)
    {
      Etno obj = (Etno)data;

      if (string.IsNullOrWhiteSpace(obj.NameEn) && !string.IsNullOrWhiteSpace(obj.Name))
        obj.NameEn = Translit.GetTranslit(obj.Name);
    }
    //-------------------------------------------------------------------------
    public override void Delete(object[] keys)
    {
      DeleteEntities<Etno>(keys, Db.Etnos, null);
    }
    //-------------------------------------------------------------------------
    public override bool Save(object data, bool add)
    {
      return SaveEntity(data, add, null);
    }
    //-------------------------------------------------------------------------
    public override void CloneEntity(object src, object dst) { }
    //-------------------------------------------------------------------------
    protected override void CheckEntity(object data, Dictionary<string, string> errs)
    {
      Etno entity = (Etno)data;

      if (string.IsNullOrWhiteSpace(entity.Name))
        errs.Add("Name", msgNoValue);
      else if (Db.Etnos.Any(x => x.Name == entity.Name.Trim() && x.EtnoId != entity.EtnoId))
        errs.Add("Name", msgNoUnique);
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
