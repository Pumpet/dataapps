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
  class DataBlockItems : DataObject
  {
    public DB Db { get { return (DB)db; } }

    public DataBlockItems(DB context, string name = "Main", DataObject p = null) : base(context, name, p) { }
    //-------------------------------------------------------------------------
    public override object GetEntity(object key)
    {
      if (key == null || !KeyExists<int>(key, "BlockItemId")) return typeof(BlockItem);
      return GetObjectFresh(key);
    }
    //-------------------------------------------------------------------------
    public override object GetObject(object key)
    {
      CheckKey<int>(key, "BlockItemId");
      return Db.BlockItems.Where(x => x.BlockItemId == GetKey<int>(key, "BlockItemId")).SingleOrDefault();
    }
    //-------------------------------------------------------------------------
    public override void GetDataBinds(object key, object filter)
    {
      DataBinds.Add("Block", null);
      GetParentFuncs.Add("Block", (new DataBlocks(Db)).GetEntity);
    }
    //-------------------------------------------------------------------------
    public override object GetList(object key, object filter)
    {
      ChangeContext(new DB(conn));
      return Db.BlockItems.Select(x => x);
    }
    //-------------------------------------------------------------------------
    public override void GetEditData(object key, bool add, object addKey)
    {
      var obj = GetEntityEditData<BlockItem>(key, add, addKey);
      DataBinds["Block"] = obj.Block ?? (object)typeof(Block);
    }
    //-------------------------------------------------------------------------
    public override void SetDefaults(object data, object addKey)
    {
      BlockItem obj = (BlockItem)data;
      if (KeyExists<int>(addKey, "BlockId"))
      {
        obj.Block = (Block)(new DataBlocks(Db)).GetObject(addKey);
        if (obj.Block != null) obj.BlockId = KeyValue<int>(addKey, "BlockId");
      }
    }
    //-------------------------------------------------------------------------
    public override void Delete(object[] keys)
    {
      Action<object> a = (o) =>
      {
        if (((BlockItem)o).SampleItems.Count > 0 || ((BlockItem)o).DnkItems.Count > 0)
          throw new Exception("В ячейке есть образец!");
      };
      DeleteEntities<BlockItem>(keys, Db.BlockItems, a);
    }
    //-------------------------------------------------------------------------
    public override bool Save(object data, bool add) 
    {
      return SaveEntity<BlockItem>(Db.BlockItems, data, add);
    }
    //-------------------------------------------------------------------------
    public override void CloneEntity(object src, object dst)
    {
      BlockItem obj = (BlockItem)src, res = (BlockItem)dst;
      res.Block = obj.Block;
      res.BlockId = obj.BlockId;
    }
    //-------------------------------------------------------------------------
    protected override void CheckEntity(object data, Dictionary<string, string> errs)
    {
      BlockItem obj = (BlockItem)data;
      
      if (string.IsNullOrWhiteSpace(obj.BlockItemCode))
        errs.Add("BlockItemCode", msgNoValue);
      else if (Db.BlockItems.Any(x => x.BlockItemCode == obj.BlockItemCode.Trim() && x.BlockId == obj.BlockId && x.BlockItemId != obj.BlockItemId))
        errs.Add("BlockItemCode", msgNoUnique);
      if (obj.BlockId == 0 || obj.Block == null)
        errs.Add("BlockId", msgNoValue);
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
