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
  class DataLists : DataObject
  {
    public DB Db { get { return (DB)db; } }

    public DataLists(DB context, string name = "Main", DataObject p = null) : base(context, name, p)
    {
      OnCloneEntity = null;
    }
    //-------------------------------------------------------------------------
    public override object GetEntity(object key)
    {
      if (key == null || !KeyExists<int>(key, "ListId")) return typeof(List);
      return GetObjectFresh(key);
    }
    //-------------------------------------------------------------------------
    public override object GetObject(object key)
    {
      CheckKey<int>(key, "ListId");
      return Db.Lists.Where(x => x.ListId == GetKey<int>(key, "ListId")).SingleOrDefault();
    }
    //-------------------------------------------------------------------------
    public override void GetDataBinds(object key, object filter)
    {
      DataBinds.Add("ListTypes", Db.ListTypes.Select(x => x).OrderBy(o => o.Name));
      DataBinds.Add("Filter", new ListType());
    }
    //-------------------------------------------------------------------------
    public override object GetList(object key, object filter)
    {
      ChangeContext(new DB(conn));
      return Db.Lists.Select(x => x).Where(w => w.TypeCode == KeyValue<string>(filter, "TypeCode"));
    }
    //-------------------------------------------------------------------------
    public override void GetEditData(object key, bool add, object addKey) {}
    //-------------------------------------------------------------------------
    public override void SetDefaults(object data, object addKey)
    {
      List obj = (List)data;
      
      if (KeyExists<string>(addKey, "TypeCode"))
      {
        string sid = KeyValue<string>(addKey, "TypeCode");
        obj.ListType = (ListType)Db.ListTypes.Where(x => x.Code == sid).SingleOrDefault();
        if (obj.ListType != null) obj.TypeCode = sid;
      }

      if (obj.Code == null) obj.Code = "";
    }
    //-------------------------------------------------------------------------
    public override void Delete(object[] keys)
    {
      DeleteEntities<List>(keys, Db.Lists, null);
    }
    //-------------------------------------------------------------------------
    public override bool Save(object data, bool add)
    {
      return SaveEntity(data, add, null);
    }
    //-------------------------------------------------------------------------
    public override void CloneEntity(object src, object dst) {}
    //-------------------------------------------------------------------------
    protected override void CheckEntity(object data, Dictionary<string, string> errs)
    {
      List entity = (List)data;

      if (string.IsNullOrWhiteSpace(entity.Item))
        errs.Add("Item", msgNoValue);
      else if (Db.Lists.Any(x => x.Item == entity.Item.Trim() && x.TypeCode == entity.TypeCode && x.ListId != entity.ListId))
        errs.Add("Item", msgNoUnique);

      if (!string.IsNullOrWhiteSpace(entity.Code) && Db.Lists.Any(x => x.Code == entity.Code.Trim() && x.ListId != entity.ListId))
        errs.Add("Code", msgNoUnique);
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
