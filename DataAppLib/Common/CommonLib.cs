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
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace Common
{
  /// <summary>Общие методы 
  /// </summary>
  public static class CommonLib
  {
    //-------------------------------------------------------------------------
    /// <summary>Получить типизированное значение поля объекта
    /// </summary>
    /// <param name="obj">объект</param>
    /// <param name="fieldName">имя поля</param>
    /// <param name="defValue">значение по умолчанию</param>
    /// <returns>объект значения или null</returns> 
    public static object GetValueFromObject<T>(object obj, string fieldName, T defValue = default(T))
    {
      object res = GetValueFromObject(obj, fieldName);
      res = (res is T ? res : null) ?? defValue;
      return (T)res;
    }
    //-------------------------------------------------------------------------
    /// <summary>Получить значение поля объекта
    /// </summary>
    /// <param name="obj">объект</param>
    /// <param name="fieldName">имя поля</param>
    /// <returns>объект значения или null</returns> 
    public static object GetValueFromObject(object obj, string fieldName)
    {
      object res = null;
      if (!String.IsNullOrWhiteSpace(fieldName) && obj != null)
      {
        PropertyDescriptorCollection props = TypeDescriptor.GetProperties(obj);
        if (props.OfType<PropertyDescriptor>().Any(x => x.Name == fieldName))
          res = props[fieldName].GetValue(obj);
      }
      return res;
    }
    //-------------------------------------------------------------------------
    /// <summary>Установить значение поля объекта
    /// </summary>
    /// <param name="obj">объект</param>
    /// <param name="fieldName">имя поля</param>
    /// <param name="value">значение</param>
    public static void SetValueToObject(object obj, string fieldName, object value)
    {
      if (!String.IsNullOrWhiteSpace(fieldName) && obj != null)
      {
        PropertyDescriptorCollection props = TypeDescriptor.GetProperties(obj);
        if (props.OfType<PropertyDescriptor>().Any(x => x.Name == fieldName))
          props[fieldName].SetValue(obj, value);
        else
          Loger.SendMess("Не найдено поле " + fieldName, true);
      }
      else
        Loger.SendMess("Не задано поле объекта!", true);
    }
    //-------------------------------------------------------------------------
    /// <summary>Получить словарь ключ-значение (string=object) из полей объекта
    /// </summary>
    /// <param name="obj">объект</param>
    /// <param name="keyNames">имена ключевых полей через ;</param>
    /// <returns>объект словаря или null</returns>
    public static object GetKeyFromObject(object obj, string keyNames)
    {
      if (!String.IsNullOrWhiteSpace(keyNames) && obj != null)
      {
        Dictionary<string, object> key = new Dictionary<string, object>();
        PropertyDescriptorCollection props = TypeDescriptor.GetProperties(obj);
        foreach (string keyName in keyNames.Split(';').Select(x => x.Trim()).Where(x2 => x2 != ""))
          if (props.OfType<PropertyDescriptor>().Any(x => x.Name == keyName))
            key.Add(keyName, props[keyName].GetValue(obj));
        return key;
      }
      else
        return null;
    }
    //-------------------------------------------------------------------------
    /// <summary>Получить словарь "имя поля - значение" (string=object) из полей внешнего объекта для полей нашего объекта
    /// </summary>
    /// <param name="obj">внешний объект</param>
    /// <param name="pairStr">строка из пар "поле нашего объекта = поле внешнего объекта;"</param>
    /// <returns>объект словаря или null</returns>
    public static object GetKeyFromObjectForPairs(object obj, string pairStr) 
    {
      if (string.IsNullOrWhiteSpace(pairStr))
        return null;

      Dictionary<string, string> pairs = new Dictionary<string, string>();
      string e = "", keyNames = "";
      foreach (string s in pairStr.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
      {
        string[] ss = s.Split('=');
        if (ss.Length == 2 && !string.IsNullOrWhiteSpace(ss[0]) && !string.IsNullOrWhiteSpace(ss[1])
            && !pairs.ContainsKey(ss[0].Trim()))
        {
          string v = ss[1].Trim();
          if (!pairs.ContainsValue(v))
            keyNames = keyNames + v + ";";
          pairs.Add(ss[0].Trim(), v);
        }
        else
          e = e + s + ";";
      }
      if (e != "")
        Loger.SendMess("Невозможно обработать пару полей: " + e);

      object keys = GetKeyFromObject(obj, keyNames);

      if (keys is Dictionary<string, object>)
      {
        Dictionary<string, object> res = new Dictionary<string, object>();

        foreach (var key in (Dictionary<string, object>)keys)
          foreach (var dst in pairs.Where(x=>x.Value == key.Key))
            res.Add(dst.Key, key.Value);
        
        return res.Count > 0 ? res : null;
      }
      else
        return null;
    }
    //-------------------------------------------------------------------------
    /// <summary>Сравнить 2 словаря ключ-значение (string=object)
    /// </summary>
    /// <param name="key1">объект словаря</param>
    /// <param name="key2">объект словаря</param>
    /// <returns>true если оба словаря = null или одинаковый состав словарей</returns>
    public static bool CompareKeys(object key1, object key2)
    {
      if (key1 == null && key2 == null)
        return true;
      if (!(key1 is Dictionary<string, object>) || !(key2 is Dictionary<string, object>))
        return false;

      Dictionary<string, object> d1 = (Dictionary<string, object>)key1
        , d2 = (Dictionary<string, object>)key2;

      return d2.SequenceEqual(d1);
    }
    //=========================================================================
    /// <summary>Действие для каждого контрола этого контейнера и вложенных
    /// </summary>
    /// <param name="c">контейнер</param>
    /// <param name="doIt">что сделать</param>
    /// <param name="type">тип контрола (по умолчанию любой)</param>
    public static void ForControls(Control c, Action<Control> doIt, Type type = null)
    {
      if (c == null || doIt == null)
        return;
      foreach (Control item in c.Controls)
      {
        if (item.GetType() == type || type == null)
          doIt(item);
        ForControls(item, doIt, type);
      }
    }
    //-------------------------------------------------------------------------
    /// <summary>Находит контрол в контейнере (в указанном и во вложенных) по имени контрола или связанного поля
    /// </summary>
    /// <param name="c">контейнер</param>
    /// <param name="name">имя контрола или связанного поля</param>
    /// <param name="onBinding">искать по имени связанного поля</param>
    /// <param name="dataSource">источник привязки (по умолчанию любой)</param>
    /// <param name="type">тип контрола (по умолчанию любой)</param>
    /// <returns></returns>
    public static Control GetControl(Control c, string name, bool onBinding = false, object dataSource = null, Type type = null)
    {
      Control res = null;
      if (c == null || string.IsNullOrEmpty(name))
        return res;
      foreach (Control item in c.Controls)
      {
        res = (((!onBinding && item.Name == name)
                || (onBinding && item.DataBindings != null && item.DataBindings.Count > 0
                    && (item.DataBindings[0].DataSource == dataSource
                        || (item.DataBindings[0].DataSource is BindingSource && ((BindingSource)item.DataBindings[0].DataSource).DataSource == dataSource)
                        || dataSource == null)
                    && item.DataBindings[0].BindingMemberInfo.BindingField == name))
              && (item.GetType() == type || type == null)) ? item : GetControl(item, name, onBinding, dataSource, type);
        if (res != null)
          break;
      }
      return res;
    }
    //-------------------------------------------------------------------------
    /// <summary>Получить контролы указанного типа из указанного контейнера и вложенных
    /// </summary>
    /// <typeparam name="T">Control и наследники</typeparam>
    /// <param name="c">контейнер</param>
    /// <returns>коллекция контролов</returns>
    public static List<T> GetControls<T>(Control c) where T : Control
    {
      List<T> res = new List<T>();
      if (c == null)
        return res;
      foreach (Control item in c.Controls)
      {
        if (item is T)
          res.Add((T)item);
        res.AddRange(GetControls<T>(item));
      }
      return res;
    }
    //-------------------------------------------------------------------------
    /// <summary>Развернуть Header грида вертикально
    /// </summary>
    public static void DoVerticalCols(DataGridView grid, DataGridViewCellPaintingEventArgs e, string[] colNames, int maxHeight = 0)
    {
      if (e.RowIndex == -1 && e.ColumnIndex >= 0 && e.ColumnIndex < grid.ColumnCount
        && colNames.Contains(grid.Columns[e.ColumnIndex].Name))
      {
        int W = maxHeight == 0 ? TextRenderer.MeasureText(e.Value.ToString(), e.CellStyle.Font).Width : maxHeight;
        e.PaintBackground(e.ClipBounds, true);
        Rectangle rect = grid.GetColumnDisplayRectangle(e.ColumnIndex, true);
        Size titleSize = TextRenderer.MeasureText(e.Value.ToString(), e.CellStyle.Font);
        if (grid.ColumnHeadersHeight < W)
          grid.ColumnHeadersHeight = W;

        e.Graphics.TranslateTransform(0, W);
        e.Graphics.RotateTransform(-90.0F);

        e.Graphics.DrawString(e.Value.ToString(),
          grid.Columns[e.ColumnIndex].HeaderCell.InheritedStyle.Font,
          new SolidBrush(grid.Columns[e.ColumnIndex].HeaderCell.InheritedStyle.ForeColor),
          new PointF(rect.Y, rect.X));

        e.Graphics.RotateTransform(90.0F);
        e.Graphics.TranslateTransform(0, -W);
        e.Handled = true;
      }
    }
  }
}
