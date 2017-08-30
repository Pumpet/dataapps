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
  class DataStores : DataObject
  {
    public DB Db { get { return (DB)db; } }

    public DataStores(DB context, string name = "Main", DataObject p = null) : base(context, name, p) { }
    //-------------------------------------------------------------------------
    public override object GetEntity(object key)
    {
      if (key == null || !KeyExists<int>(key, "StoreId")) return typeof(Store);
      return GetObjectFresh(key);
    }
    //-------------------------------------------------------------------------
    public override object GetObject(object key)
    {
      CheckKey<int>(key, "StoreId");
      return Db.Stores.Where(x => x.StoreId == GetKey<int>(key, "StoreId")).SingleOrDefault();
    }
    //-------------------------------------------------------------------------
    public override void GetDataBinds(object key, object filter)
    {
    }
    //-------------------------------------------------------------------------
    public override object GetList(object key, object filter)
    {
      ChangeContext(new DB(conn));
      return Db.Stores.Select(x => x);
    }
    //-------------------------------------------------------------------------
    public override void GetEditData(object key, bool add, object addKey)
    {
      var obj = GetEntityEditData<Store>(key, add, addKey);
      DataBinds["Labs"] = Db.Stores.Select(x => x.Lab).Distinct();
      DataBinds["Fridges"] = Db.Stores.Select(x => x.Fridge).Distinct();
      DataBinds["FridgeModules"] = Db.Stores.Select(x => x.FridgeModule).Distinct();
      DataBinds["FridgeShelfs"] = Db.Stores.Select(x => x.FridgeShelf).Distinct();
      DataBinds["Containers"] = Db.Stores.Select(x => x.Container).Distinct();
    }
    //-------------------------------------------------------------------------
    public override void SetDefaults(object data, object addKey)
    {
      Store obj = (Store)data;
      if (string.IsNullOrWhiteSpace(obj.Lab))
        obj.Lab = Db.Stores.Select(x => x.Lab).FirstOrDefault();

      obj.Lab = (obj.Lab ?? "").Trim();
      obj.Fridge = (obj.Fridge ?? "").Trim();
      obj.FridgeModule = (obj.FridgeModule ?? "").Trim();
      obj.FridgeShelf = (obj.FridgeShelf ?? "").Trim();
      obj.Container = (obj.Container ?? "").Trim();
    }
    //-------------------------------------------------------------------------
    public override void Delete(object[] keys)
    {
      DeleteEntities<Store>(keys, Db.Stores, null);
    }
    //-------------------------------------------------------------------------
    public override bool Save(object data, bool add) 
    {
      return SaveEntity<Store>(Db.Stores, data, add);
    }
    //-------------------------------------------------------------------------
    public override void CloneEntity(object src, object dst)
    {
      Store obj = (Store)src, res = (Store)dst;
      res.Lab = obj.Lab;
      res.Fridge = obj.Fridge;
      res.FridgeModule = obj.FridgeModule;
      res.FridgeShelf = obj.FridgeShelf;
      res.Container = obj.Container;
    }
    //-------------------------------------------------------------------------
    protected override void CheckEntity(object data, Dictionary<string, string> errs)
    {
      Store obj = (Store)data;
      
      if (string.IsNullOrWhiteSpace(obj.Lab))
        errs.Add("Lab", msgNoValue);

      if (string.IsNullOrWhiteSpace(obj.Fridge) && (!string.IsNullOrWhiteSpace(obj.FridgeModule) || !string.IsNullOrWhiteSpace(obj.FridgeShelf)))
        errs.Add("Fridge", msgNoValue);

      if (!string.IsNullOrWhiteSpace(obj.Container) 
          && Db.Stores.Any(x => x.StoreId != obj.StoreId
                            && x.Lab == obj.Lab
                            && x.Container == obj.Container))
        errs.Add("Container", msgNoUnique);

      if (Db.Stores.Any(x => x.StoreId != obj.StoreId 
                            && x.Lab == obj.Lab 
                            && x.Fridge == obj.Fridge
                            && x.FridgeModule == obj.FridgeModule
                            && x.FridgeShelf == obj.FridgeShelf
                            && x.Container == obj.Container))
        errs.Add("FIELD", msgNoUnique);
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
