using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace Forms
{
  /// <summary>Контейнер команд
  /// </summary>
  public class Commands
  {
    /// <summary>список команд</summary>
    public List<Command> Items { get; set; }
    //-------------------------------------------------------------------------
    /// <summary>Создает контейнер команд с пустым списком
    /// </summary>
    public Commands()
    {
      Items = new List<Command>();
    }
    //-------------------------------------------------------------------------
    /// <summary>команда по имени
    /// </summary>
    /// <param name="name">имя команды</param>
    /// <returns>команда или null</returns>
    public Command this[string name]
    {
      get { return GetCommand(name); }
    }
    //-------------------------------------------------------------------------
    /// <summary>Получить команду по имени
    /// </summary>
    /// <param name="name">имя команды</param>
    /// <returns>команда или null</returns>
    public Command GetCommand(string name)
    {
      return Items.FirstOrDefault(x => x.Name == name);
    }
    //-------------------------------------------------------------------------
    /// <summary>Получить команду для контрола
    /// </summary>
    /// <param name="toolItem">контрол</param>
    /// <returns>команда или null</returns>
    public Command GetCommand(ToolStripItem toolItem)
    {
      return Items.FirstOrDefault(x => x.Ctrls.Any(c => c == toolItem));
    }
    //-------------------------------------------------------------------------
    /// <summary>Получить команду для заданной комбинации клавиш
    /// </summary>
    /// <param name="e">данные события нажатия</param>
    /// <returns>команда или null</returns>
    public Command GetCommand(KeyEventArgs e)
    {
      return Items.FirstOrDefault(x => x.CheckKeys(e));
    }
    //-------------------------------------------------------------------------
    /// <summary>Переустановка поведения команд 
    /// Активность - только для активных по умолчанию и переданных в словаре "имя_команды - делегат"
    /// Видимость - для всех, кроме переданных без делегатов
    /// </summary>
    /// <param name="cmds">словарь "имя_команды - делегат"</param>
    public void SetBehaviors(Dictionary<string, Action<string>> cmds)
    {
      foreach (var item in Items)
      {
        item.Active = item.ActiveOnDefault;
      }
      foreach (var item in cmds.Where(x => this[x.Key] != null))
        this[item.Key].SetBehavior(item.Value, item.Value != null, true);

      // чтобы не двоились ToolStripSeparator-ы
      IEnumerable<ToolStrip> menus = Items.Where(w => w.Ctrls != null).SelectMany(x => x.Ctrls.Select(c => c.GetCurrentParent())).Where(t => t != null).Distinct();
      foreach (var ts in menus)
      {
        bool sep = false, first = true;
        int iVis = 0;
        List<ToolStripItem> tis = ts.Items.OfType<ToolStripItem>().ToList();
        for (int i = 0; i < tis.Count; i++)
        {
          var item = tis[i];
          if (!(item is ToolStripSeparator) && (ts is ContextMenuStrip ? !item.Enabled : !item.Visible))
            continue;
          if (item is ToolStripSeparator)
          {
            item.Enabled = !first && !sep && i < tis.Count - 1;
            item.Visible = !first && !sep && i < tis.Count - 1;
            sep = true;
          }
          else
            sep = false;
          first = false;
          if (ts is ContextMenuStrip ? item.Enabled : item.Visible) iVis = i;
        }
        if (iVis < tis.Count && tis[iVis] is ToolStripSeparator)
        {
          tis[iVis].Enabled = false;
          tis[iVis].Visible = false;
        }
        if (tis.Count > 0 && tis[0] is ToolStripSeparator)
        {
          tis[0].Enabled = false;
          tis[0].Visible = false;        
        }
      }
    }
  }
  //===========================================================================
  /// <summary>команда для запуска из ToolStripItem (элемент меню, тулбаров и т.п.)
  /// </summary>
  public class Command
  {
    Keys keyCode = Keys.None; // клавиша
    Keys keyModifiers = Keys.None; // управляющие клавиши (флаги)
    /// <summary>имя команды</summary>
    public string Name { get; private set; }
    /// <summary>делегат для вызова, получает имя команды</summary>
    public Action<string> OnExec { get; private set; }
    /// <summary>текст для ToolStripItem</summary>
    public string Text { get; set; }
    /// <summary>текст для кнопки</summary>
    public string ButtonText { get; set; }
    /// <summary>текст для тултипа</summary>
    public string ToolTipText { get; set; }
    /// <summary>image</summary>
    public Image Img { get; set; }
    /// <summary>команда остается активной при переустановке коллекции команд
    /// (активность не сбрасывается при переустановке коллекции команд в Commands.SetBehaviors)</summary>
    public bool ActiveOnDefault { get; set; }
    
    /// <summary>контролы ToolStripItem, к которым привязан обработчик</summary>
    public List<ToolStripItem> Ctrls { get; private set; }
    //-------------------------------------------------------------------------
    bool active;
    /// <summary>активность - устанавливает Enabled контролов и Visible для ContextMenuStrip</summary>
    public bool Active
    {
      get { return active; }
      set
      {
        active = value;
        foreach (var item in Ctrls)
        {
          item.Enabled = active;
          if (item.GetCurrentParent() is ContextMenuStrip)
          {
            item.Visible = active;
            item.GetCurrentParent().Refresh();
          }
        }
      }
    }
    //-------------------------------------------------------------------------
    /// <summary>видимость - управляет свойством Visible контролов</summary>
    bool visible;
    public bool Visible
    {
      get { return visible; }
      set 
      { 
        visible = value;
        foreach (var item in Ctrls)
          item.Visible = visible;
      }
    }
    //-------------------------------------------------------------------------
    /// <summary>Создает объект команды
    /// </summary>
    /// <param name="name">имя команды</param>
    /// <param name="text">текст контролов</param>
    /// <param name="img">image</param>
    /// <param name="buttonText">текст кнопки</param>
    /// <param name="tooltipText">текст для тултипа</param>
    /// <param name="ts">массив контейнеров, в которых создадим контрол с типом по умолчанию</param>
    /// <param name="k">кнопка</param>
    /// <param name="km">кнопки управления (флаги)</param>
    /// <param name="onExec">делегат для вызова</param>
    /// <param name="visible">видимость контролов</param>
    /// <param name="active">доступность контролов</param>
    /// <param name="activeOnDefault">активность остается при смене набора команд</param>
    public Command(string name, string text, 
      Image img = null, string buttonText = null, string tooltipText = null,
      ToolStrip[] ts = null, Keys k = Keys.None, Keys km = Keys.None,
      Action<string> onExec = null, bool visible = true, bool active = true, bool activeOnDefault = false)
    {
      Name = name;
      Text = text;
      ButtonText = buttonText;
      ToolTipText = tooltipText;
      Img = img;
      SetKeys(k, km);
      
      Ctrls = new List<ToolStripItem>();
      if (ts != null)
        foreach (var t in ts)
          AddControl(t);

      SetBehavior(onExec, visible, active);
      ActiveOnDefault = activeOnDefault;
    }
    //-------------------------------------------------------------------------
    /// <summary>Создает новый контрол в контейнере и связывает его с командой
    /// </summary>
    /// <param name="t">контейнер</param>
    public void AddControl(ToolStrip t)
    {
      ToolStripItem tsi = t.Items.Add(Text, Img);
      if (tsi is ToolStripButton && Img != null)
        tsi.Text = ButtonText;
      if (tsi is ToolStripMenuItem)
        ToolTipText = "";
      tsi.ToolTipText = ToolTipText == null ? Text : ToolTipText;
      LinkControl(tsi);
    }
    //-------------------------------------------------------------------------
    /// <summary>Связывает контрол с командой
    /// </summary>
    /// <param name="tsi">контрол</param>
    public void LinkControl(ToolStripItem tsi)
    {
      Ctrls.Add(tsi);
      tsi.Click += Handler;
    }
    //-------------------------------------------------------------------------
    /// <summary>Установка комбинации клавиш для команды
    /// </summary>
    /// <param name="k">кнопка</param>
    /// <param name="km">кнопки управления (флаги)</param>
    public void SetKeys(Keys k, Keys km = Keys.None)
    {
      keyCode = k;
      keyModifiers = km;
    }
    //-------------------------------------------------------------------------
    /// <summary>Проверяет комбинацию клавиш
    /// </summary>
    /// <param name="e">данные события нажатия</param>
    /// <returns>true если соответствует команде</returns>
    public bool CheckKeys(KeyEventArgs e)
    {
      return ((e.KeyCode == keyCode) && (e.Modifiers == keyModifiers));
    }
    //-------------------------------------------------------------------------
    /// <summary>Установка поведения команды - задает делегат для вызова, видимость и активность
    /// </summary>
    /// <param name="onExec">делегат для вызова</param>
    /// <param name="visible">видимость контролов</param>
    /// <param name="active">активность контролов (неактивны если не задан делегат)</param>
    public void SetBehavior(Action<string> onExec, bool visible, bool active)
    {
      OnExec = onExec;
      Visible = visible;
      Active = active && OnExec != null;
    }
    //-------------------------------------------------------------------------
    /// <summary>Запуск команды (при условии что активна и задан делегат)
    /// </summary>
    public virtual void Exec()
    {
      if (OnExec != null && Active)
        OnExec(Name);
    }
    //-------------------------------------------------------------------------
    /// <summary>Обработчик для события Click контролов
    /// </summary>
    public void Handler(object sender, EventArgs e)
    {
      Exec();
    }
  }
}
