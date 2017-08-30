using System;
using System.Collections.Generic;
using Context;
using Manager;
using Common;
using System.Threading;

namespace GenDataAccess
{
  public class DataManager : IDataManager
  {
    string conn;

    public DataManager()
    {
      conn = AppConfig.Prop("ConnectionString");
    }
    //-------------------------------------------------------------------------
    public bool CheckConnection()
    {
      conn = AppConfig.Prop("ConnectionString");
      try
      {
        DB db = new DB(conn);
        db.Connection.Open();
        db.Connection.Close();
        return true;
      }
      catch (Exception e)
      {
        Loger.SendMess(e, "Ошибка подключения!");
        return false;
      }
    }
    //-------------------------------------------------------------------------
    public List<IDataController> GetDataObjects(string viewType)
    {
      List<IDataController> res = new List<IDataController>();
      DB db = new DB(conn);

      if (viewType == "FEtnos")
      {
        res.Add(new DataEtnos(db));
      }

      if (viewType == "FPopuls" || viewType == "FPopulEdit")
      {
        res.Add(new DataPopuls(db));
      }

      if (viewType == "FExpeds")
      {
        var d = new DataExpeds(db);
        res.Add(d);
        res.Add(new DataPoints(db, "Points", d));
      }

      if (viewType == "FExpedEdit")
      {
        res.Add(new DataExpeds(db));
      }

      if (viewType == "FPoints" || viewType == "FPointEdit")
      {
        res.Add(new DataPoints(db));
      }

      if (viewType == "FLists")
      {
        res.Add(new DataLists(db));
      }

      if (viewType == "FPlaces")
      {
        res.Add(new DataPlaces(db));
      }

      if (viewType == "FAnkets")
      {
        var d = new DataAnkets(db);
        res.Add(d);
        res.Add(new DataAnketDocs(db, "AnketDocs", d));
        res.Add(new DataAnketAttrs(db, "AnketAttrs", d));
        res.Add(new DataAnketRels(db, "AnketRels", d));
        res.Add(new DataSamples(db, "Samples", d));
      }

      if (viewType == "FAnketEdit")
      {
        res.Add(new DataAnkets(db));
      }

      if (viewType == "FAnketDocEdit")
      {
        res.Add(new DataAnketDocs(db));
      }

      if (viewType == "FAnketAttrEdit")
      {
        res.Add(new DataAnketAttrs(db));
      }

      if (viewType == "FAnketRelEdit")
      {
        res.Add(new DataAnketRels(db));
      }

      if (viewType == "FStores" || viewType == "FStoreEdit")
      {
        res.Add(new DataStores(db));
      }

      if (viewType == "FBlocks")
      {
        var d = new DataBlocks(db);
        res.Add(d);
        res.Add(new DataBlockItems(db, "BlockItems", d));
      }

      if (viewType == "FBlockEdit")
      {
        res.Add(new DataBlocks(db));
      }

      if (viewType == "FSamples")
      {
        var d = new DataSamples(db);
        res.Add(d);
        res.Add(new DataSampleItems(db, "SampleItems", d));
        res.Add(new DataDnkItems(db, "DnkItems", d));
        res.Add(new DataResults(db, "Results", d));
      }

      if (viewType == "FSampleEdit")
      {
        res.Add(new DataSamples(db));
      }

      if (viewType == "FSampleItemEdit")
      {
        res.Add(new DataSampleItems(db));
      }

      if (viewType == "FDnkItemEdit")
      {
        res.Add(new DataDnkItems(db));
      }

      if (viewType == "FResultEdit")
      {
        res.Add(new DataResults(db));
      }

      if (viewType == "FResults")
      {
        res.Add(new DataResults(db));
      }

      if (viewType == "FRepSamples")
      {
        res.Add(new RepSamples(db));
      }

      return res;
    }
  }
}
