using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;
using Ctrls;

namespace Forms
{
  /// <summary>Базовая форма для форм редактора данных объекта
  /// </summary>
  public partial class FormEdit : FormBase
  {
    /// <summary>имя редактируемого объекта (для выбора из словаря источников)</summary>
    [Browsable(true), Category("New options"), DefaultValue("Entity"), Description("имя редактируемого объекта (для выбора из словаря источников)")]
    public string EntityName { get; set; }
    /// <summary>имена ключевых полей через ;</summary>
    [Browsable(true), Category("New options"), DefaultValue(""), Description("имена ключевых полей через ;")]
    public string KeyNames { get; set; }
    //-------------------------------------------------------------------------
    #region external delegates - устанавливаются в форме = делегаты контроллера или формы
    /// <summary>делегат для сохранения -  из контроллера (IDataController.OnSave) </summary>
    protected Func<object, bool, bool> DoSave { get; set; }
    /// <summary>делегат для проверки -  из контроллера (IDataController.OnCheck) </summary>
    protected Func<object, object> DoCheck { get; set; }
    /// <summary>делегат для начитки данных объекта -  из контроллера (IDataController.OnGetEditData) </summary>
    protected Action<object, bool, object> DoGetEditData { get; set; }
    /// <summary>делегат для выполнения произвольной команды -  из контроллера (IDataController.OnExecCommand) </summary>
    protected Func<string, object, object, object, object[], object> DoExecCommand { get; set; }
    #endregion
    //-------------------------------------------------------------------------
    #region events instead of external delegates
    /// <summary>перед сохранением</summary>
    [Browsable(true), Category("New options"), Description("перед сохранением")]
    public event Action BeforeSave;
    /// <summary>при сохранении: Func(редактируемый объект, признак добавления) - вернет true если нет ошибок</summary>
    [Browsable(true), Category("New options"), Description("при сохранении")]
    public event Func<object, bool, bool> OnSave;
    /// <summary>при проверке: Func(редактируемый объект) - вернет словарь ошибок (имя поля, текст ошибки)</summary>
    [Browsable(true), Category("New options"), Description("при проверке")]
    public event Func<object, object> OnCheck;
    /// <summary>при начитке: Action(ключ, признак нового объекта, ключ родителя для нового объекта)</summary>
    [Browsable(true), Category("New options"), Description("при начитке")]
    public event Action<object, bool, object> OnGetEditData;
    /// <summary>при выполнении произвольной команды: Func(код команды, ключ, ключи фильтра, обрабатываемый объект) - вернет объект результата</summary>
    [Browsable(true), Category("New options"), Description("при выполнении произвольной команды")]
    public event Func<string, object, object, object, object[], object> OnExecCommand;
    #endregion

    /// <summary>после выполнения команды: Action(код команды, объект результата)</summary>
    [Browsable(true), Category("New options"), Description("после выполнения команды")]
    public event Action<string, object> OnCommandResult;
    /// <summary>после установки команд (для установки дополнительных команд): Action(словарь команд "имя - делегат вызова")</summary>
    [Browsable(true), Category("New options"), Description("после установки команд (для установки дополнительных команд): Action(словарь команд \"имя - делегат вызова\")")]
    public event Action<object> OnSetMenu;

    //-------------------------------------------------------------------------
    public FormEdit()
    {
      InitializeComponent();
      EntityName = "Entity";
      KeyNames = "";
    }
    //-------------------------------------------------------------------------
    /// <summary>Привязка основного контроллера к обработчикам
    /// </summary>
    protected override void SetControllers()
    {
      base.SetControllers();

      if (data == null)
        throw new Exception("Не определен основной контроллер: \"" + DataControllerName + "\"");

      DoCheck += data.OnCheck;
      DoSave += data.OnSave;
      DoGetEditData += data.OnGetEditData;
      DoExecCommand += data.OnExecCommand;
    }
    //-------------------------------------------------------------------------
    /// <summary>Установка команд
    /// </summary>
    protected override void SetCommands()
    {
      base.SetCommands();

      //CommonLib.ForControls(this, (c) => { ((DataList)c).OnCheckMenu += cmds.SetBehaviors; }, typeof(DataList));

      if (getResult)
      {
        cmds.Items.Add(new Command("Save", "Сохранить", Properties.Resources.save, null, "Сохранить (Ctrl+Enter)", new[] { tools }));
        cmds["Save"].SetKeys(Keys.Enter, Keys.Control);
        cmds["Save"].SetBehavior(ExecCommand, true, (DoSave != null || OnSave != null));
      }

      cmds.Items.Add(new Command("Cancel", "Отменить", Properties.Resources.undo, null, "Отменить и закрыть (Alt+F4)", new[] { tools }));
      cmds["Cancel"].ActiveOnDefault = true;
      cmds["Cancel"].SetBehavior(ExecCommand, true, true);

      tools.Items.Add(new ToolStripSeparator());
    }
    //-------------------------------------------------------------------------
    /// <summary>Обработка набора команд через контроллер (вызывается например из обработчика OnSetMenu)</summary>
    /// <param name="c">контроллер</param>
    /// <param name="cmds">набор команд для меню</param>
    /// <param name="code">код, если например нужно отличать разные обработки в одом контроллере</param>
    public void SetMenuThruController(IDataController c, object cmds, string code = "")
    {
      c.OnSetCommands(cmds, GetEntityKey(), GetEntityBind(), null, code);
    }
    //-------------------------------------------------------------------------
    /// <summary>Начитка данных редактора (вызов LoadData).
    /// Заполнение BindingSources.
    /// Настройка меню.
    /// base: Начитка объектов данных в контроллерах
    /// </summary>
    /// <exception cref="System.Exception">Can't define Entity: \"" + EntityName + "\"</exception>
    protected override void InitData()
    {
      base.InitData();
      if (string.IsNullOrEmpty(EntityName) || !data.DataBinds.ContainsKey(EntityName))
        throw new Exception("Не найден редактируемый объект по имени: \"" + EntityName + "\"");
      Text += newEntity ? " - Добавить" : " - Изменить";
      LoadData(null, inKey);
      SetBindingSources(data);
      // настройка меню
      if (OnSetMenu != null)
      {
        var c = new Dictionary<string, Action<string>>();
        foreach (var item in cmds.Items)
          c.Add(item.Name, item.OnExec);
        OnSetMenu(c);
        cmds.SetBehaviors(c);
      }
    }
    //-------------------------------------------------------------------------
    /// <summary>Загрузка данных редактируемого объекта. 
    /// </summary>
    /// <param name="sender">контрол, инициировавший загрузку данных</param>
    /// <param name="key">ключ текущего объекта</param>
    public override void LoadData(Control sender, object key)
    {
      base.LoadData(sender, key);
      var a = OnGetEditData ?? DoGetEditData;
      if (a == null) return;
      try
      {
        a(key, newEntity, inFilter);
      }
      catch (Exception e)
      {
        Loger.SendMess(e);
      }
    }
    //=========================================================================
    /// <summary>Редактируемый объект
    /// </summary>
    /// <returns>объект основного контроллера по имени EntityName</returns>
    protected virtual object GetEntityBind()
    {
      return data.DataBinds[EntityName];
    }
    //-------------------------------------------------------------------------
    /// <summary>Ключ редактируемого объекта
    /// </summary>
    /// <returns>ключ</returns>
    protected virtual object GetEntityKey()
    {
      return CommonLib.GetKeyFromObject(GetEntityBind(), KeyNames);
    }
    //=========================================================================
    /// <summary>Обработчик команды
    /// </summary>
    /// <param name="cmd">Код команды</param>
    protected void ExecCommand(string cmd)
    {
      Cursor = Cursors.WaitCursor;
      try
      {
        if (cmd == "Save")
        {
          SaveData();
          return;
        }
        if (cmd == "Cancel")
        {
          this.DialogResult = DialogResult.Cancel;
          Close();
          return;
        }
        if (OnExecCommand != null || DoExecCommand != null)
        {
          object key = GetEntityKey();
          var a = OnExecCommand ?? DoExecCommand;
          object res = a(cmd, key, null, GetEntityBind(), new object[] { key });
          if (OnCommandResult != null)
            OnCommandResult(cmd, res);
        }
      }
      finally
      {
        Cursor = Cursors.Default;
      }
    }
    //-------------------------------------------------------------------------
    /// <summary>Сохранение данных редактируемого объекта
    /// </summary>
    protected virtual void SaveData()
    {
      foreach (var item in CommonLib.GetControls<DateTimeBox>(this))
        if (!item.CheckText())
          return;

      var a = OnSave ?? DoSave;
      if (a == null) return;

      SetControlsData();
      if (BeforeSave != null)
        BeforeSave();
      error.Clear();

      if (CheckData(GetEntityBind()) && a(GetEntityBind(), newEntity))
      {
        if (CallBack != null)
          CallBack(GetEntityKey());
        this.DialogResult = DialogResult.OK;
        Close();
      }
    }
    //-------------------------------------------------------------------------
    /// <summary>Проверка данных объекта и установка ошибок
    /// </summary>
    /// <param name="o">объект для проверки</param>
    /// <returns></returns>
    protected virtual bool CheckData(object o)
    {
      var a = OnCheck ?? DoCheck;
      if (a == null)
        return true;

      var errs = ((Dictionary<string, string>)a(o)).Where(x => !String.IsNullOrEmpty(x.Value));

      if (errs.Count() == 0)
        return true;
      else
      {
        foreach (var err in errs)
        {
          Control c = CommonLib.GetControl(this, err.Key, true, GetEntityBind()) ?? CommonLib.GetControl(this, err.Key);
          if (c != null)
            error.SetError(c, err.Value);
          else
            Loger.SendMess(err.Value, true);
        }
        return false;
      }
    }
    //=========================================================================
    /// <summary>Установка размеров формы по контролу на табличной панели
    /// (правый край указанного контрола Х нижняя строка табличной панели)
    /// </summary>
    /// <param name="control">контрол на табличной панели</param>
    protected void SetFormEditSize(Control control) 
    {
      TableLayoutPanel panel = control.Parent is TableLayoutPanel ? (TableLayoutPanel)control.Parent : null;
      if (panel == null)
        return;

      int max = panel.GetColumn(control) + panel.GetColumnSpan(control);
      max = max <= panel.ColumnCount - 1 ? max : panel.ColumnCount - 1;

      int w = 0, h = 0;

      for (int i = 0; i < max; i++)
      {
        w += (int)panel.ColumnStyles[i].Width;
      }
      if (w > 0)
        Width = w + 20 + this.Width - panel.Width;

      for (int i = 0; i < panel.RowCount - 1; i++)
      {
        h += (int)panel.RowStyles[i].Height;
      }
      if (h > 0)
        Height = h + 10 + this.Height - panel.Height;

      MinimumSize = new Size(Width, Height);
    }
  }
}
