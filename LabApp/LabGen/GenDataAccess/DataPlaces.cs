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
  class DataPlaces : DataObject
  {
    public DB Db { get { return (DB)db; } }

    public DataPlaces(DB context, string name = "Main", DataObject p = null) : base(context, name, p) { }
    //-------------------------------------------------------------------------
    public override object GetEntity(object key)
    {
      if (key == null || !KeyExists<int>(key, "PlaceId")) return typeof(Place);
      return GetObjectFresh(key);
    }
    //-------------------------------------------------------------------------
    public override object GetObject(object key)
    {
      CheckKey<int>(key, "PlaceId");
      return Db.Places.Where(x => x.PlaceId == GetKey<int>(key, "PlaceId")).SingleOrDefault();
    }
    //-------------------------------------------------------------------------
    public override void GetDataBinds(object key, object filter) { }
    //-------------------------------------------------------------------------
    public override object GetList(object key, object filter)
    {
      ChangeContext(new DB(conn));
      return Db.Places.Select(x => x);
    }
    //-------------------------------------------------------------------------
    public override void GetEditData(object key, bool add, object addKey) {}
    //-------------------------------------------------------------------------
    public override void SetDefaults(object data, object addKey) {}
    //-------------------------------------------------------------------------
    public override void Delete(object[] keys)
    {
      DeleteEntities<Place>(keys, Db.Places, null);
    }
    //-------------------------------------------------------------------------
    public override bool Save(object data, bool add)
    {
      return SaveEntity(data, add, (d, a) =>
        {
          if (!string.IsNullOrWhiteSpace(((Place)d).KladrCode))
            throw new Exception("Нельзя редактировать запись, загруженную из КЛАДР !");
        });
    }
    //-------------------------------------------------------------------------
    public override void CloneEntity(object src, object dst)
    {
      Place entity = (Place)src, res = (Place)dst;
      res.Name = entity.Name;
      res.Region = entity.Region;
      res.Raion = entity.Raion;
      res.City = entity.City;
      res.Punkt = entity.Punkt;
    }
    //-------------------------------------------------------------------------
    protected override void CheckEntity(object data, Dictionary<string, string> errs)
    {
      Place entity = (Place)data;

      if (string.IsNullOrWhiteSpace(entity.Name))
        errs.Add("Name", msgNoValue);
    }
    //-------------------------------------------------------------------------
    public override object ExecCommand(string command, object key, object filter, object data, object[] keys)
    {
      if (command == "GetKladr")
      {
        //var obj = Db.TestProc();
        //return obj;
        Loger.SendMess("Здесь будет загрузка из КЛАДР...");
      }
      return null;
    }
    //-------------------------------------------------------------------------
    public override void SetCommands(object cmds, object key, object data, object[] keys, string code) { }
  }
}
