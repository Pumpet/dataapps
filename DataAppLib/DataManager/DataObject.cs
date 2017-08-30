//
//  THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
//  PURPOSE.
//
//  License: GNU Lesser General Public License (LGPLv3)
//
//  Email: pumpet.net@gmail.com
//  Copyright (C) Alex Rozanov, 2017 
//

using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Common;
using System.Linq;
using Common;

namespace Manager
{
  /// <summary>Базовый контроллер данных
  /// Абстрактные методы реализуются в рабочих контроллерах-наследниках и определяют логику обработки и получения данных для определенной сущности модели или связанных сущностей
  /// Методы, прописанные в IDataController вызываются из представлений, которым контроллеры будут назначены через IViewDataManager -> IDataManager
  /// Например, базовые формы подцепят IDataController к соответствующим событиям форм и их элементов управления (гриды, кнопки и т.д.)
  /// </summary>
  public abstract class DataObject : IDataController
  {
    /// <summary>модель</summary>
    protected DataContext db;
    protected DbConnection conn;
    protected DataObject parentDataObject;
    protected List<DataObject> childDataObjects = new List<DataObject>();

    protected DateTime minSqlSmallDate = DateTime.ParseExact("19000101", "yyyyMMdd", null);
    protected DateTime minSqlDate = DateTime.ParseExact("17530101", "yyyyMMdd", null);

    protected string msgNoValue = "Не задано значение";
    protected string msgNoUnique = "Значение не уникально";
    protected string msgIncorrect = "Неверное значение";

    //-------------------------------------------------------------------------
    #region IDataController
    /// <summary>
    /// имя контроллера
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// словарь объектов данных (имя-объект)
    /// </summary>
    public Dictionary<string, object> DataBinds { get; set; }
    /// <summary>
    /// словарь функций (имя-делегат) для возврата объекта по ключу
    /// </summary>
    public Dictionary<string, Func<object, object>> GetParentFuncs { get; set; }
    /// <summary>
    /// заполнить словарь объектов данных и словарь функций
    /// </summary>
    public Action<object, object> OnGetDataBinds { get; set; }
    /// <summary>
    /// получить коллекцию данных (ключ, ключи фильтра)
    /// </summary>
    public Func<object, object, object> OnGetList { get; set; }
    /// <summary>
    /// заполнить в словаре объект для редактирования (ключ, признак нового, ключ внешнего(родительского) объекта)
    /// </summary>
    public Action<object, bool, object> OnGetEditData { get; set; }
    /// <summary>
    /// установка умолчаний (объект, ключ внешнего(родительского) объекта)
    /// </summary>
    public Action<object, object> OnSetDefaults { get; set; }
    /// <summary>
    /// удаление объектов (массив ключей)
    /// </summary>
    public Action<object[]> OnDelete { get; set; }
    /// <summary>
    /// сохранение объекта (объект, признак нового) - вернуть true если нет ошибок
    /// </summary>
    public Func<object, bool, bool> OnSave { get; set; }
    /// <summary>
    /// передача данных из одного объекта в другой (объект-источник, объект-приемник)
    /// </summary>
    public Action<object, object> OnCloneEntity { get; set; }
    /// <summary>
    /// проверка данных объекта (объект) - вернуть словарь поле-ошибка
    /// </summary>
    public Func<object, Dictionary<string, string>> OnCheck { get; set; }
    /// <summary>
    /// обработать команду (код команды, ключ, ключи фильтра, обрабатываемый объект) - вернуть объект результата
    /// </summary>
    public Func<string, object, object, object, object[], object> OnExecCommand { get; set; }
    /// <summary>
    /// установить набор команд из интерфейса (набор команд (слоарь код-действие), ключ, обрабатываемый объект, массив ключей выделенных записей)  
    /// </summary>
    public Action<object, object, object, object[], string> OnSetCommands { get; set; }
    #endregion
    //-------------------------------------------------------------------------
    /// <summary>Базовая инициализация контроллера данных, установка всех делегатов для IDataController.
    /// При необходимости отключить некоторый функционал нужные делегаты IDataController обнуляются в конструкторах наследников
    /// </summary>
    /// <param name="context">модель</param>
    /// <param name="name">имя контроллера</param>
    public DataObject(DataContext context, string name = "Main", DataObject parent = null)
    {
      if (parent != null)
      {
        parentDataObject = parent;
        parentDataObject.childDataObjects.Add(this);
        db = parentDataObject.db;
      }
      else
        db = context;
      conn = db.Connection;
      Name = name;
      DataBinds = new Dictionary<string, object>() { { "Entity", null } };
      GetParentFuncs = new Dictionary<string, Func<object, object>>();

      OnGetDataBinds += GetDataBinds;
      OnGetList += GetList;
      OnGetEditData += GetEditData;
      OnSetDefaults += SetDefaults;
      OnDelete += Delete;
      OnSave += Save;
      OnCloneEntity += CloneEntity;
      OnCheck += Check;
      OnExecCommand += ExecCommand;
      OnSetCommands += SetCommands;
    }
    //-------------------------------------------------------------------------
    /// <summary>Переустановка модели контекста, в этом и связанных (parent, childs) контроллерах
    /// </summary>
    /// <param name="o">модель</param>
    public void ChangeContext(DataContext context)
    {
      db = context;
      if (parentDataObject != null)
        parentDataObject.db = context;
      foreach (var item in childDataObjects)
        item.db = context;
    }
    //-------------------------------------------------------------------------
    /// <summary>Проверка наличия ключа по имени и типу
    /// </summary>
    /// <typeparam name="T">тип значения ключа</typeparam>
    /// <param name="o">словарь ключей</param>
    /// <param name="name">имя ключа</param>
    /// <exception cref="System.Exception">исключение при отсутствии ключа</exception>
    public void CheckKey<T>(object o, string name)
    {
      bool res = false;
      if (o is Dictionary<string, object> && !String.IsNullOrWhiteSpace(name))
      {
        Dictionary<string, object> d = (Dictionary<string, object>)o;
        res = (d.ContainsKey(name) && d[name] is T);
      }
      if (!res)
        throw new Exception(String.Format("Не определен ключ {0}<{1}>", name, typeof(T).Name));
    }
    //-------------------------------------------------------------------------
    /// <summary>Выдать значение ключа. Сообщить если не найден по имени и типу
    /// </summary>
    /// <typeparam name="T">тип значения ключа</typeparam>
    /// <param name="o">словарь ключей</param>
    /// <param name="name">имя ключа</param>
    /// <returns>значение ключа приведенное к типу или значение типа по умолчанию</returns>
    public T GetKey<T>(object o, string name)
    {
      try
      {
        CheckKey<T>(o, name);
        return (T)((Dictionary<string, object>)o)[name];
      }
      catch (Exception e)
      {
        Loger.SendMess(e, "Ошибка проверки ключа!");
        return default(T);
      }
    }
    //-------------------------------------------------------------------------
    /// <summary>Проверка существования ключа по имени и типу
    /// </summary>
    /// <typeparam name="T">тип значения ключа</typeparam>
    /// <param name="o">словарь ключей</param>
    /// <param name="name">имя ключа</param>
    /// <returns>true если существует</returns>
    public bool KeyExists<T>(object o, string name)
    {
      bool res = false;
      if (o is Dictionary<string, object> && !String.IsNullOrWhiteSpace(name))
      {
        Dictionary<string, object> d = (Dictionary<string, object>)o;
        res = (d.ContainsKey(name) && d[name] is T);
      }
      return res;
    }
    //-------------------------------------------------------------------------
    /// <summary>Значение ключа по имени и типу
    /// </summary>
    /// <typeparam name="T">тип значения ключа</typeparam>
    /// <param name="o">словарь ключей</param>
    /// <param name="name">имя ключа</param>
    /// <returns>значение ключа приведенное к типу или значение типа по умолчанию</returns>
    public T KeyValue<T>(object o, string name)
    {
      T res = default(T);
      try
      {
        if (KeyExists<T>(o, name))
          res = (T)((Dictionary<string, object>)o)[name];
      }
      catch (Exception e)
      {
        Loger.SendMess(e, "Ошибка получения значения ключа " + name);
      }
      return res;
    }
    //-------------------------------------------------------------------------
    /// <summary>Сбросить помеченные к удалению или вставке. Перечитать измененные
    /// </summary>
    protected void Reject()
    {
      ChangeSet s = db.GetChangeSet();
      try
      {
        foreach (object o in s.Deletes)
          db.GetTable(o.GetType()).InsertOnSubmit(o);
        foreach (object o in s.Inserts)
          db.GetTable(o.GetType()).DeleteOnSubmit(o);
        foreach (object o in s.Updates)
          db.Refresh(RefreshMode.OverwriteCurrentValues, o);
      }
      catch (Exception e)
      {
        Loger.SendMess(e, "Ошибка сброса кэша изменений!");
      }
    }
    //=========================================================================
    /// <summary>вернуть текущий обновленный объект по ключу, ключ обязателен
    /// </summary>
    /// <param name="key">ключ</param>
    /// <returns>объект</returns>
    protected object GetObjectFresh(object key)
    { 
      var o = GetObject(key);
      db.Refresh(RefreshMode.OverwriteCurrentValues, o);
      return o;
    }
    //-------------------------------------------------------------------------    
    /// <summary>вернуть текущий объект по ключу, ключ обязателен
    /// </summary>
    /// <param name="key">ключ</param>
    /// <returns>объект</returns>
    public abstract object GetObject(object key);
    //-------------------------------------------------------------------------
    /// <summary>вернуть актуальный объект сущности по ключу (использовать для получения родительского объекта), 
    /// необходимо обновить коллекцию, в случае отсутствия ключа вернуть тип или новый объект
    /// </summary>
    /// <param name="key">ключ</param>
    /// <returns>объект</returns>
    public abstract object GetEntity(object key);
    //-------------------------------------------------------------------------
    /// <summary>заполнить словарь объектов данных и словарь функций
    /// <param name="key">ключ</param>
    /// <param name="filter">ключи фильтра</param>
    /// </summary>
    public abstract void GetDataBinds(object key, object filter);
    //-------------------------------------------------------------------------
    /// <summary>получить коллекцию данных
    /// </summary>
    /// <param name="key">ключ</param>
    /// <param name="filter">ключи фильтра</param>
    /// <returns>коллекция данных</returns>
    public abstract object GetList(object key, object filter);
    //-------------------------------------------------------------------------
    /// <summary>подготовить данные для редактирования
    /// </summary>
    /// <param name="key">ключ</param>
    /// <param name="add">признак нового</param>
    /// <param name="addKey">ключ внешнего(родительского) объекта</param>
    public abstract void GetEditData(object key, bool add, object addKey); 
    //-------------------------------------------------------------------------
    /// <summary>получить данные объекта для редактирования
    /// </summary>
    /// <param name="key">ключ</param>
    /// <param name="add">признак нового</param>
    /// <param name="addKey">ключ внешнего(родительского) объекта</param>
    protected T GetEntityEditData<T>(object key, bool add, object addKey) where T : class, new()
    {
      T obj = null;
      try
      {
        if (add && key == null)
        {
          obj = new T();
          SetDefaults(obj, addKey);
        }
        else
        {
          var keyObj = (T)GetObject(key);
          if (keyObj == null)
            throw new Exception("Объект не найден!");
          if (add)
          {
            obj = new T();
            CloneEntity(keyObj, obj);
          }
          else
            obj = keyObj;
        }
      }
      catch (Exception e)
      {
        Loger.SendMess(e, "Ошибка получения данных объекта!");
        obj = null;
      }
      DataBinds["Entity"] = obj;
      return obj;
    }
    //-------------------------------------------------------------------------
    /// <summary>установка умолчаний
    /// </summary>
    /// <param name="obj">объект</param>
    /// <param name="addKey">ключ внешнего(родительского) объекта</param>
    public abstract void SetDefaults(object data, object addKey); 
    //-------------------------------------------------------------------------
    /// <summary>удаление объектов по ключам - нужно вызывать DeleteEntities<T> или явно реализовывать
    /// </summary>
    /// <param name="keys">массив ключей</param>
    public abstract void Delete(object[] keys);
    //-------------------------------------------------------------------------
    /// <summary>удаление объектов по ключам - стандартный способ, с предобработкой по каждому объекту
    /// </summary>
    /// <param name="keys">массив ключей</param>
    /// <param name="table">коллекция, из которой удаляем</param>
    /// <param name="beforeDelete">предобработка - получит объект до удаления</param>
    protected void DeleteEntities<T>(object[] keys, Table<T> table, Action<object> beforeDelete) where T : class
    {
      try
      {
        for (int i = 0; i < keys.Length; i++)
        {
          T obj = (T)GetObject(keys[i]);
          if (beforeDelete != null)
            beforeDelete(obj);
          table.DeleteOnSubmit(obj);
        }
        db.SubmitChanges();
      }
      catch (ChangeConflictException ex)
      {
        Loger.SendMess(ex, "Ошибка удаления данных!");
        db.ChangeConflicts.ResolveAll(RefreshMode.OverwriteCurrentValues);
      }
      catch (Exception e)
      {
        Loger.SendMess(e, "Ошибка удаления данных!");
        Reject();
      }
    }
    //-------------------------------------------------------------------------
    /// <summary>сохранение - нужно вызывать SaveEntity или явно реализовывать
    /// </summary>
    /// <param name="data">объект</param>
    /// <param name="add">признак нового</param>
    /// <returns>true если нет ошибок</returns>
    public abstract bool Save(object data, bool add);
    //-------------------------------------------------------------------------
    /// <summary>сохранение объекта - стандартный способ, с учетом вставки
    /// </summary>
    /// <param name="table">коллекция для вставки</param>
    /// <param name="data">объект</param>
    /// <param name="add">признак нового</param>
    /// <returns>true если нет ошибок</returns>
    protected bool SaveEntity<T>(Table<T> table, object data, bool add) where T : class
    {
      if (add)
      {
        T entity = (T)data;
        table.InsertOnSubmit(entity);
      }
      return SaveEntity(data, add, null);
    }
    //-------------------------------------------------------------------------
    /// <summary>сохранение объекта - стандартный способ, с предобработкой
    /// </summary>
    /// <param name="data">объект</param>
    /// <param name="add">признак нового</param>
    /// <param name="beforeSave">предобработка - получит data и add</param> 
    /// <returns>true если нет ошибок</returns>
    protected bool SaveEntity(object data, bool add, Action<object, bool> beforeSave)
    {
      try
      {
        if (beforeSave != null)
          beforeSave(data, add);
        db.SubmitChanges();
        return true;
      }
      catch (ChangeConflictException ex)
      {
        Loger.SendMess(ex, "Ошибка сохранения данных!");
        db.ChangeConflicts.ResolveAll(RefreshMode.OverwriteCurrentValues);
        return false;
      }
      catch (Exception e)
      {
        Loger.SendMess(e, "Ошибка сохранения данных!");
        Reject();
        return false;
      }
    }
    //-------------------------------------------------------------------------
    /// <summary>передача данных из одного объекта в другой
    /// </summary>
    /// <param name="src">объект-источник</param>
    /// <param name="dst">объект-приемник</param>
    public abstract void CloneEntity(object src, object dst); 
    //-------------------------------------------------------------------------
    /// <summary>проверка данных объекта, с предварительной установкой умолчаний,
    /// переопределяем при необходимости изменить стандартный подход к проверке
    /// </summary>
    /// <param name="data">объект</param>
    /// <returns>словарь поле-ошибка</returns>
    public virtual Dictionary<string, string> Check(object data) 
    {
      Dictionary<string, string> res = new Dictionary<string, string>();
      SetDefaults(data, null);
      CheckEntity(data, res);
      return res;
    }
    //-------------------------------------------------------------------------
    /// <summary>проверка данных объекта, заполнение словаря ошибок
    /// </summary>
    /// <param name="data">объект</param>
    /// <param name="errs">словарь поле-ошибка</param>
    protected abstract void CheckEntity(object data, Dictionary<string, string> errs); 
    //-------------------------------------------------------------------------
    /// <summary>обработать команду
    /// </summary>
    /// <param name="command">код команды</param>
    /// <param name="key">ключ текущего(обрабатываемого) объекта</param>
    /// <param name="filter">ключи фильтра</param>
    /// <param name="data">текущий(обрабатываемый) объект</param>
    /// <param name="keys">массив ключей (для обработки выделенных записей)</param>
    /// <returns>объект результата</returns>
    public abstract object ExecCommand(string command, object key, object filter, object data, object[] keys);
    /// <summary>
    /// установить/скорректировать набор команд из интерфейса
    /// </summary>
    /// <param name="cmds">набор команд</param>
    /// <param name="key">ключ текущего(обрабатываемого) объекта</param>
    /// <param name="data">текущий(обрабатываемый) объект</param>
    /// <param name="keys">массив ключей (для обработки выделенных записей)</param>
    public abstract void SetCommands(object cmds, object key, object data, object[] keys, string code);
  }
}

