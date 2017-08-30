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
using System.Diagnostics;

namespace GenDataAccess
{
  class DataAnketDocs : DataObject
  {
    public DB Db { get { return (DB)db; } }
    public DataAnketDocs(DB context, string name = "Main", DataObject p = null) : base(context, name, p) 
    {
      OnCloneEntity = null;
    }
    //-------------------------------------------------------------------------
    public override object GetEntity(object key)
    {
      if (key == null || !KeyExists<int>(key, "AnketDocId")) return typeof(AnketDoc);
      return GetObjectFresh(key);
    }
    //-------------------------------------------------------------------------
    public override object GetObject(object key)
    {
      CheckKey<int>(key, "AnketDocId");
      return Db.AnketDocs.Where(x => x.AnketDocId == GetKey<int>(key, "AnketDocId")).SingleOrDefault();
    }
    //-------------------------------------------------------------------------
    public override void GetDataBinds(object key, object filter)
    {
      DataBinds.Add("Anket", null);
      GetParentFuncs.Add("Anket", (new DataAnkets(Db)).GetEntity);
    }
    //-------------------------------------------------------------------------
    public override object GetList(object key, object filter)
    {
      ChangeContext(new DB(conn));
      return Db.AnketDocs.Select(x => x);
    }
    //-------------------------------------------------------------------------
    public override void GetEditData(object key, bool add, object addKey)
    {
      var obj = GetEntityEditData<AnketDoc>(key, add, addKey);
      DataBinds["Anket"] = obj.Anket ?? (object)typeof(Anket);
      DataBinds["DocTypes"] = Db.AnketDocs.Select(x => x.DocType).Distinct();
    }
    //-------------------------------------------------------------------------
    public override void SetDefaults(object data, object addKey)
    {
      AnketDoc obj = (AnketDoc)data;
      
      if (KeyExists<int>(addKey, "AnketId"))
      {
        obj.Anket = (Anket)(new DataAnkets(Db)).GetObject(addKey);
        if (obj.Anket != null) obj.AnketId = KeyValue<int>(addKey, "AnketId");
      }
    }
    //-------------------------------------------------------------------------
    public override void Delete(object[] keys)
    {
      DeleteEntities<AnketDoc>(keys, Db.AnketDocs, null);
    }
    //-------------------------------------------------------------------------
    public override bool Save(object data, bool add) 
    {
      return SaveEntity<AnketDoc>(Db.AnketDocs, data, add);
    }
    //-------------------------------------------------------------------------
    public override void CloneEntity(object src, object dst) {}
    //-------------------------------------------------------------------------
    protected override void CheckEntity(object data, Dictionary<string, string> errs)
    {
      AnketDoc obj = (AnketDoc)data;

      if (obj.AnketId == 0 || obj.Anket == null)
        errs.Add("sbAnket", "Связь с анкетой: " + msgNoValue);

      if (string.IsNullOrWhiteSpace(obj.DocType))
        errs.Add("DocType", msgNoValue);
      
      if (string.IsNullOrWhiteSpace(obj.Link))
        errs.Add("Link", msgNoValue);
      else if (Db.AnketDocs.Any(x => x.Link == obj.Link.Trim() && x.AnketId == obj.AnketId && x.AnketDocId != obj.AnketDocId))
        errs.Add("Link", msgNoUnique);
      else if (!File.Exists(obj.Link))
        errs.Add("Link", "Файл не найден!");
    }
    //-------------------------------------------------------------------------
    public override object ExecCommand(string command, object key, object filter, object data, object[] keys)
    {
      if (command == "OpenFile")
      {
        string file = ((AnketDoc)GetObject(key)).Link;
        if (File.Exists(file))
        {
          Process p = new Process();
          p.StartInfo.FileName = file;
          p.Start();
        }
        else
          Loger.SendMess("Файл не существует", true);
      }
      return null;
    }
    //-------------------------------------------------------------------------
    public override void SetCommands(object cmds, object key, object data, object[] keys, string code) { }
  }
}
