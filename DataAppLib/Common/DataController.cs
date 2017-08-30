using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
  /// <summary>Контейнер контроллеров данных 
  /// </summary>
  public class DataControllers
  {
    /// <summary>Коллекция контроллеров данных</summary>
    public List<IDataController> Items { get; set; }
    //-------------------------------------------------------------------------
    public DataControllers()
    {
      Items = new List<IDataController>();
    }
    //-------------------------------------------------------------------------
    /// <summary>Контроллер данных</summary>
    /// <value>контроллер данных</value>
    /// <param name="name">имя контроллера</param>
    /// <returns>контроллер данных или null</returns>
    public IDataController this[string name]
    {
      get { return GetController(name);  }
    }
    //-------------------------------------------------------------------------
    /// <summary>Контроллер данных по имени
    /// </summary>
    /// <param name="name">имя контроллера</param>
    /// <returns>контроллер данных или null</returns>
    public IDataController GetController(string name)
    {
      return Items.FirstOrDefault(x => x.Name == name);
    }
    //-------------------------------------------------------------------------
    /// <summary>Получить объект данных
    /// </summary>
    /// <param name="name">имя контроллера данных</param>
    /// <param name="bindName">имя объекта данных в словаре объектов</param>
    /// <returns>объект данных</returns>
    public object GetData(string name, string bindName)
    {
      object data = null;
      if (this[name] != null && this[name].DataBinds != null && this[name].DataBinds.ContainsKey(bindName))
        data = this[name].DataBinds[bindName];
      return data;
    }
    //-------------------------------------------------------------------------
    /// <summary>Получить делегат для возврата объекта по ключу
    /// </summary>
    /// <param name="name">имя контроллера данных</param>
    /// <param name="funcName">имя делегата в словаре функций</param>
    /// <returns>делегат</returns>
    public Func<object, object> GetParentFunc(string name, string funcName)
    {
      Func<object, object> func = null;
      if (this[name] != null && this[name].GetParentFuncs != null && this[name].GetParentFuncs.ContainsKey(funcName))
        func = this[name].GetParentFuncs[funcName];
      return func;
    }
  }
}
