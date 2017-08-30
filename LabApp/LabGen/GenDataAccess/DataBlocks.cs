using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Manager;
using Context;
using Common;


namespace GenDataAccess
{
  class DataBlocks : DataObject
  {
    public DB Db { get { return (DB)db; } }
    const string blockType = "BLOCK";
    public DataBlocks(DB context, string name = "Main", DataObject p = null) : base(context, name, p) { }
    //-------------------------------------------------------------------------
    public override object GetEntity(object key)
    {
      if (key == null || !KeyExists<int>(key, "BlockId")) return typeof(Block);
      return GetObjectFresh(key);
    }
    //-------------------------------------------------------------------------
    public override object GetObject(object key)
    {
      CheckKey<int>(key, "BlockId");
      return Db.Blocks.Where(x => x.BlockId == GetKey<int>(key, "BlockId")).SingleOrDefault();
    }
    //-------------------------------------------------------------------------
    public override void GetDataBinds(object key, object filter)
    {
      DataBinds.Add("Store", null);
      GetParentFuncs.Add("Store", (new DataStores(Db)).GetEntity);
      var fridges = Db.Stores.Select(x => x.Fridge).Distinct().ToArray();
      DataBinds["Fridges"] = fridges.Union(new[] { "" });
    }
    //-------------------------------------------------------------------------
    public override object GetList(object key, object filter)
    {
      ChangeContext(new DB(conn));
      var set = Db.Blocks.Where(w => w.BlockType == blockType).Select(x =>
        new
        {
          x.BlockId,
          x.BlockCode,
          x.StoreId,
          Lab = x.Store.Lab,
          Fridge = x.Store.Fridge,
          FridgeModule = x.Store.FridgeModule,
          FridgeShelf = x.Store.FridgeShelf,
          Container = x.Store.Container,
          x.BlockItems
        });

      string Fridge = KeyValue<string>(filter, "Fridge");
      if (!string.IsNullOrWhiteSpace(Fridge))
        set = set.Where(x => SqlMethods.Like(x.Fridge, Fridge));

      return set;
    }
    //-------------------------------------------------------------------------
    public override void GetEditData(object key, bool add, object addKey)
    {
      var obj = GetEntityEditData<Block>(key, add, addKey);
      obj.IsSetItems = add;
      DataBinds["Store"] = obj.Store ?? (object)typeof(Store);
    }
    //-------------------------------------------------------------------------
    /// <summary>
    /// установка умолчаний
    /// </summary>
    /// <param name="data"></param>
    /// <param name="addKey">ключ внешнего(родительского) объекта</param>
    public override void SetDefaults(object data, object addKey)
    {
      Block obj = (Block)data;
      obj.BlockType = blockType;
      obj.BlockCode = (obj.BlockCode ?? "").Trim();
    }
    //-------------------------------------------------------------------------
    public override void Delete(object[] keys)
    {
      Action<object> a = (o) =>
      {
        if (Db.BlockItems.Any(x => x.BlockId == ((Block)o).BlockId 
              && (Db.SampleItems.Any(s => s.BlockItemId == x.BlockItemId) || Db.DnkItems.Any(d => d.BlockItemId == x.BlockItemId)))) 
          throw new Exception("В штативе есть образцы!");
      };
      DeleteEntities<Block>(keys, Db.Blocks, a);
    }
    //-------------------------------------------------------------------------
    public override bool Save(object data, bool add) 
    {
      bool res = SaveEntity<Block>(Db.Blocks, data, add);
      Block obj = (Block)data;
      if (res && obj.IsSetItems)
      {
        try
        {
          string msg = null;
          int ret = Db.BlockItemsGenerate(obj.BlockId, obj.DimX, obj.DimY, ref msg);
          if (ret > 0)
            throw new Exception(string.IsNullOrWhiteSpace(msg) ? "Код ошибки = " + ret.ToString() : msg);
        }
        catch (Exception e)
        {
          Loger.SendMess(e, "Ошибка формирования ячеек штатива!");
        }
      }
      return res;
    }
    //-------------------------------------------------------------------------
    public override void CloneEntity(object src, object dst)
    {
      Block obj = (Block)src, res = (Block)dst;
      res.BlockType = obj.BlockType;
      res.Store = obj.Store;
      res.StoreId = obj.StoreId;
    }
    //-------------------------------------------------------------------------
    protected override void CheckEntity(object data, Dictionary<string, string> errs)
    {
      Block obj = (Block)data;
      
      if (string.IsNullOrWhiteSpace(obj.BlockCode))
        errs.Add("BlockCode", msgNoValue);
      else if (Db.Blocks.Any(x => x.BlockCode == obj.BlockCode.Trim() && x.BlockId != obj.BlockId))
        errs.Add("BlockCode", msgNoUnique);

      if (obj.IsSetItems && obj.DimX <= 0)
        errs.Add("DimX", msgNoValue);
      if (obj.IsSetItems && obj.DimY <= 0)
        errs.Add("DimY", msgNoValue);
    }
    //-------------------------------------------------------------------------
    public override object ExecCommand(string command, object key, object filter, object data, object[] keys)
    {
      return null;
    }
    //-------------------------------------------------------------------------
    public override void SetCommands(object cmds, object key, object data, object[] keys, string code)
    {
      if (!(cmds is Dictionary<string, Action<string>>))
        return;
      var cs = (Dictionary<string, Action<string>>)cmds;
    }

  }
}
