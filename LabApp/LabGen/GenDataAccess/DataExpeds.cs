using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using Manager;
using Context;
using Common;
using System.ComponentModel;

namespace GenDataAccess
{
  class DataExpeds : DataObject
  {
    public DB Db { get { return (DB)db; } }

    public DataExpeds(DB context, string name = "Main", DataObject p = null) : base(context, name, p)
    {
      OnCloneEntity = null;
    }
    //-------------------------------------------------------------------------
    public override object GetEntity(object key)
    {
      if (key == null || !KeyExists<int>(key, "ExpedId")) return typeof(Exped);
      return GetObjectFresh(key);
    }
    //-------------------------------------------------------------------------
    public override object GetObject(object key)
    {
      CheckKey<int>(key, "ExpedId");
      return Db.Expeds.Where(x => x.ExpedId == GetKey<int>(key, "ExpedId")).SingleOrDefault();
    }
    //-------------------------------------------------------------------------
    public override void GetDataBinds(object key, object filter) { }
    //-------------------------------------------------------------------------
    public override object GetList(object key, object filter)
    {
      ChangeContext(new DB(conn));
      var set = Db.Expeds.Select(x => 
        new {
          x.DateEnd, 
          x.DateStart,
          x.ExpedId,
          x.Head, 
          x.Info, 
          x.Name, 
          x.Points,
          x.Region
        });
      
      return set;
    }
    //-------------------------------------------------------------------------
    public override void GetEditData(object key, bool add, object addKey) 
    {
      GetEntityEditData<Exped>(key, add, addKey);      
    }
    //-------------------------------------------------------------------------
    public override void SetDefaults(object data, object addKey) {}
    //-------------------------------------------------------------------------
    public override void Delete(object[] keys)
    {
      DeleteEntities<Exped>(keys, Db.Expeds, null);
    }
    //-------------------------------------------------------------------------
    public override bool Save(object data, bool add)
    {
      return SaveEntity<Exped>(Db.Expeds, data, add);
    }
    //-------------------------------------------------------------------------
    public override void CloneEntity(object src, object dst) {}
    //-------------------------------------------------------------------------
    protected override void CheckEntity(object data, Dictionary<string, string> errs) 
    {
      Exped entity = (Exped)data;

      if (string.IsNullOrWhiteSpace(entity.Name))
        errs.Add("Name", msgNoValue);
      else if (Db.Expeds.Any(x => x.Name == entity.Name.Trim() && x.ExpedId != entity.ExpedId))
        errs.Add("Name", msgNoUnique);

      if (DateTime.Compare(entity.DateStart, minSqlSmallDate) <= 0)
        errs.Add("DateStart", "Не задана дата");
      if (DateTime.Compare(entity.DateEnd, minSqlSmallDate) <= 0)
        errs.Add("DateEnd", "Не задана дата");
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
