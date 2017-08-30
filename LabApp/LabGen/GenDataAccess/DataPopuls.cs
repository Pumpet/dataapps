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
  class DataPopuls : DataObject
  {
    public DB Db { get { return (DB)db; } }

    public DataPopuls(DB context, string name = "Main", DataObject p = null) : base(context, name, p) { }
    //-------------------------------------------------------------------------
    public override object GetEntity(object key)
    {
      if (key == null || !KeyExists<int>(key, "PopulId")) return typeof(Popul);
      return GetObjectFresh(key);
    }
    //-------------------------------------------------------------------------
    public override object GetObject(object key)
    {
      CheckKey<int>(key, "PopulId");
      return Db.Populs.Where(x => x.PopulId == GetKey<int>(key, "PopulId")).SingleOrDefault();
    }
    //-------------------------------------------------------------------------
    public override void GetDataBinds(object key, object filter)
    {
      DataBinds.Add("Etno", null);
      GetParentFuncs.Add("Etno", (new DataEtnos(Db)).GetEntity);
    }
    //-------------------------------------------------------------------------
    public override object GetList(object key, object filter)
    {
      ChangeContext(new DB(conn));
      return Db.Populs.Select(x => new
        {
          EtnoName = x.Etno.Name,
          x.Name,
          x.NameEn,
          x.PopulId,
          x.Codes, 
          x.Comment
        });
    }
    //-------------------------------------------------------------------------
    public override void GetEditData(object key, bool add, object addKey)
    {
      var obj = GetEntityEditData<Popul>(key, add, addKey);
      DataBinds["Etno"] = obj.Etno ?? (object)typeof(Etno);
    }
    //-------------------------------------------------------------------------
    public override void SetDefaults(object data, object addKey)
    {
      Popul obj = (Popul)data;

      if (string.IsNullOrWhiteSpace(obj.Codes) && !string.IsNullOrWhiteSpace(obj.Name))
        obj.Codes = obj.Name.Length >= 3 ? obj.Name.Substring(0, 3) : obj.Name;
      if (string.IsNullOrWhiteSpace(obj.NameEn) && !string.IsNullOrWhiteSpace(obj.Name))
        obj.NameEn = Translit.GetTranslit(obj.Name);
    }
    //-------------------------------------------------------------------------
    public override void Delete(object[] keys)
    {
      DeleteEntities<Popul>(keys, Db.Populs, null);
    }
    //-------------------------------------------------------------------------
    public override bool Save(object data, bool add) 
    {
      return SaveEntity<Popul>(Db.Populs, data, add);
    }
    //-------------------------------------------------------------------------
    public override void CloneEntity(object src, object dst)
    {
      Popul entity = (Popul)src, res = (Popul)dst;
      res.Codes = entity.Codes;
      res.Etno = entity.Etno;
      res.EtnoId = entity.EtnoId;
      res.Comment = entity.Comment;
    }
    //-------------------------------------------------------------------------
    protected override void CheckEntity(object data, Dictionary<string, string> errs)
    {
      Popul entity = (Popul)data;

      if (string.IsNullOrWhiteSpace(entity.Name))
        errs.Add("Name", msgNoValue);
      else if (Db.Populs.Any(x => x.Name == entity.Name.Trim() && x.PopulId != entity.PopulId))
        errs.Add("Name", msgNoUnique);

      if (entity.EtnoId == 0 || entity.Etno == null)
        errs.Add("sbEtno", msgNoValue);
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
