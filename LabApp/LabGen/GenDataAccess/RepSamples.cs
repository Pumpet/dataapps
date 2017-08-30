using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manager;
using Context;
using Common;

namespace GenDataAccess
{
  class RepSamplesParams
  {
    public int ExpedId { get; set; }
    public Exped Exped { get; set; }
    public int PopulId { get; set; }
    public Popul Popul { get; set; }
    public string SampleCode { get; set; }
    public string RUSID { get; set; }
    public string GPID { get; set; }
  }
  //---------------------------------------------------------------------------
  class RepSamples : DataObject
  {
    public DB Db { get { return (DB)db; } }

    public RepSamples(DB context, string name = "Main", DataObject p = null) : base(context, name, p)
    {
    }
    //-------------------------------------------------------------------------
    public override object GetEntity(object key)
    {
      return typeof(RepSamplesParams);
    }
    //-------------------------------------------------------------------------
    public override object GetObject(object key)
    {
      return new RepSamplesParams();
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
      return null;
    }
    //-------------------------------------------------------------------------
    public override void GetEditData(object key, bool add, object addKey)
    {
      DataBinds["Entity"] = new RepSamplesParams();   
      DataBinds["Popul"] = (object)typeof(Popul);
      DataBinds["Exped"] = (object)typeof(Exped);
    }
    //-------------------------------------------------------------------------
    public override void SetDefaults(object data, object addKey)
    {
    }
    //-------------------------------------------------------------------------
    public override void Delete(object[] keys)
    {
    }
    //-------------------------------------------------------------------------
    public override bool Save(object data, bool add)
    {
      RepSamplesParams obj = (RepSamplesParams)data;
      try
      {
        var repData = Db.RepSamplesProc(obj.ExpedId, obj.PopulId, obj.SampleCode, obj.RUSID, obj.GPID).ToList();
        if (!(repData is IEnumerable<object>))
          throw new Exception("Неверный формат результата");
        if (repData.Count() == 0)
          throw new Exception("Нет данных");
        ExcelLib.ObjectsToExcel(repData, false, AppConfig.Prop("RepSamples"));
      }
      catch (Exception e)
      {
        Loger.SendMess(e, "Отчет не сформирован");
        return false;
      }
      return true;
    }
    //-------------------------------------------------------------------------
    public override void CloneEntity(object src, object dst) 
    {
    }
    //-------------------------------------------------------------------------
    protected override void CheckEntity(object data, Dictionary<string, string> errs)
    {
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
