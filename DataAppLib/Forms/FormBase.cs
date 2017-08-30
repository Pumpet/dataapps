using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ctrls;
using Common;

namespace Forms
{
  /// <summary>Базовая форма для формы списка (FormList) и формы редактора (FormEdit)
  /// </summary>
  public partial class FormBase : Form
  {
    /// <summary>словарь источников</summary>
    protected Dictionary<string, BindingSource> bss;
    protected ErrorProvider error;
    /// <summary>контейнер контроллеров данных</summary>
    protected DataControllers dc;
    /// <summary>основной контроллер данных</summary>
    protected IDataController data;
    /// <summary>контейнер команд</summary>
    protected Commands cmds;
    /// <summary>ключ внешнего объекта</summary>
    protected object inKey;
    public object InKey { get { return inKey; } set { inKey = value; } }
    /// <summary>ключи внешнего фильтра</summary>
    protected object inFilter;
    public object InFilter { get { return inFilter; } set { inFilter = value; } }
    /// <summary>форма вернет результат</summary>
    protected bool getResult;
    /// <summary>форма задает новый объект</summary>
    protected bool newEntity;
    /// <summary>форма инициализирована</summary>
    protected bool isReady;
    /// <summary>делегат вызывающей формы, получает ключ объекта</summary>
    protected Action<object> CallBack;

    /// <summary>имя основного контроллера</summary>
    [Browsable(true), Category("New options"), DefaultValue("Main"), Description("имя основного контроллера")]
    public string DataControllerName { get; set; }
    /// <summary>имя основного контроллера</summary>
    [Browsable(true), Category("New options"), Description("имя первого контрола на форме (для установки фокуса)")]
    public string DefaultControlName { get; set; }

    /// <summary>При входе в контрол или изменении данных в нем: Action(контрол, true если вошли в контрол)
    /// Используется для реализации логики взаимодействия контролов на форме</summary>
    [Browsable(true), Category("New options"), Description("При входе в контрол или изменении данных в нем")]
    public event Action<Control, bool> OnControlChanged;
    //-------------------------------------------------------------------------
    public FormBase()
    {
      InitializeComponent();
      isReady = false;
      DataControllerName = "Main";
    }
    //-------------------------------------------------------------------------
    /// <summary>Инициализация свойств формы. Вызов InitControls, SetControllers, SetCommands
    /// </summary>
    /// <param name="d">контейнер контроллеров данных</param>
    /// <param name="modes">режимы поведения формы</param>
    /// <param name="callback">делегат вызывающей формы</param>
    /// <param name="key">ключ объекта</param>
    /// <param name="filter">ключи фильтра</param>
    public void Init(DataControllers d, FormModes modes = FormModes.Default, Action<object> callback = null,
      object key = null, object filter = null)
    {
      bss = new Dictionary<string, BindingSource>();

      error = new ErrorProvider();
      error.BlinkStyle = ErrorBlinkStyle.NeverBlink;

      dc = d ?? new DataControllers();
      data = dc[DataControllerName];
      cmds = new Commands();
      
      getResult = modes.HasFlag(FormModes.GetResult);
      newEntity = modes.HasFlag(FormModes.NewEntity);
      inKey = key;
      inFilter = filter;

      if (callback != null)
        CallBack += callback;

      try
      {
        InitControls();
        SetControllers();
        SetCommands();
      }
      catch (Exception e)
      {
        Loger.SendMess(e);
        this.Close();
      }
    }
    //-------------------------------------------------------------------------
    /// <summary>Вызов InitData и Bind, завершение инициализации, начатой в Init.
    /// </summary>
    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      Cursor = Cursors.Default;
      if (!DesignMode)
      {
        try
        {
          InitData();
          Bind();

          if (AppConfig.Prop("AppMode") == "DEBUG")
          {
            Text = Translit.GetTranslit(Text);
            CommonLib.ForControls(this, (c) =>
            {
              foreach (DataGridViewColumn col in ((DataList)c).Columns)
                col.HeaderText = Translit.GetTranslit(col.HeaderText);
            }, typeof(DataList));
            CommonLib.ForControls(this, (c) =>
            {
              c.Text = Translit.GetTranslit(c.Text);
            }, typeof(Label));
          }

          isReady = true;
        }
        catch (Exception ex)
        {
          Loger.SendMess(ex, ex.StackTrace.Contains("Bind()") ? "Ошибка привязки данных к элементам формы!" : "");
          this.Close();
        }
      }
    }
    //=========================================================================
    /// <summary>Начальные установки для контролов
    /// </summary>
    protected virtual void InitControls()
    {
      // меню - сброс стандартного для всех, установка своего для гридов
      this.ContextMenuStrip = null;
      this.ContextMenu = null;
      CommonLib.ForControls(this, (c) => { c.ContextMenuStrip = new ContextMenuStrip(); c.ContextMenu = new ContextMenu(); });
      CommonLib.ForControls(this, (c) => { ((DataList)c).ContextMenuStrip = menus; }, typeof(DataList));
      // устраняем мерцание гридов
      Action<Control> doit = null;
      doit = (c) =>
      {
        System.Reflection.BindingFlags bFlags = System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic;
        c.GetType().GetProperty("DoubleBuffered", bFlags).SetValue(c, true, null);
      };
      CommonLib.ForControls(this, doit, typeof(DataList));
      // обработчики при активизации и изменении контролов
      CommonLib.ForControls(this, (c) =>
      {
        c.Enter += ControlEntered;
        if (c is CheckBox) ((CheckBox)c).CheckedChanged += ControlChanged;
        if (c is RadioButton) ((RadioButton)c).CheckedChanged += ControlChanged;
        if (c is ComboBox) ((ComboBox)c).SelectedIndexChanged += ControlChanged;
        if (c is SelectBox) ((SelectBox)c).AfterGetSelected += ControlChanged; 
      });
      // установка вызова формы выбора для SelectBox
      CommonLib.ForControls(this, (c) => { SelectBox box = (SelectBox)c; box.DoExecSelect += ExecSelectForm; }, typeof(SelectBox));
    }
    //-------------------------------------------------------------------------
    private void ControlEntered(object sender, EventArgs e)
    {
      if (isReady && OnControlChanged != null)
        OnControlChanged((Control)sender, true);
    }
    //-------------------------------------------------------------------------
    private void ControlChanged(object sender, EventArgs e)
    {
      if (isReady && OnControlChanged != null)
        OnControlChanged((Control)sender, false);
    }
    //-------------------------------------------------------------------------
    /// <summary>Привязка контроллеров к обработчикам
    /// </summary>
    protected virtual void SetControllers() { }
    //-------------------------------------------------------------------------
    /// <summary>Установка команд
    /// </summary>
    protected virtual void SetCommands() { }
    //-------------------------------------------------------------------------
    /// <summary>Начитка объектов данных в контроллерах, наполнение словаря источников из объектов основного контроллера. 
    /// </summary>
    protected virtual void InitData() 
    {
      foreach (var item in dc.Items)
        if (item.OnGetDataBinds != null)
          item.OnGetDataBinds(InKey, InFilter);
      SetBindingSources(data);
    }
    //-------------------------------------------------------------------------
    /// <summary>Привязка контролов к данным.
    /// </summary>
    protected virtual void Bind() { }
    //-------------------------------------------------------------------------
    /// <summary>Загрузка данных из контроллера.
    /// </summary>
    /// <param name="sender">контрол, инициировавший загрузку данных</param>
    /// <param name="key">ключ текущего объекта</param>
    public virtual void LoadData(Control sender, object key) { }
    //=========================================================================
    /// <summary>Запуск формы редактора
    /// </summary>
    /// <param name="sender">Контрол, инициировавший запуск</param>
    /// <param name="formName">Имя формы</param>
    /// <param name="add">Редактируется новый объект</param>
    /// <param name="key">Ключ объекта</param>
    /// <param name="filter">Фильтр</param>
    /// <param name="callback">Метод обработки завершения редактирования (получает ключ объекта)</param>
    protected virtual void ExecEditForm(Control sender, string formName, bool add, object key, object filter, Action<object> callback) 
    {
      FormModes modes = FormModes.GetResult | FormModes.Modal;
      if (add) modes |= FormModes.NewEntity;
      FormManager.Io.ExecForm(formName, this, modes, callback, key, filter);
    }
    //-------------------------------------------------------------------------
    /// <summary>Запуск формы списка для выбора
    /// </summary>
    /// <param name="sender">Контрол, инициировавший запуск</param>
    /// <param name="formName">Имя формы</param>
    /// <param name="key">Ключ текущего объекта</param>
    /// <param name="filter">Фильтр</param>
    /// <param name="callback">Метод обработки завершения выбора (получает ключ выбранного объекта)</param>
    protected virtual void ExecSelectForm(Control sender, string formName, object key, object filter, Action<object> callback)
    {
      FormModes modes = FormModes.GetResult | FormModes.Modal;
      FormManager.Io.ExecForm(formName, this, modes, callback, key, filter);
    }
    //=========================================================================
    private void FormBase_KeyDown(object sender, KeyEventArgs e)
    {
      Command cmd = cmds.GetCommand(e);
      if (cmd != null) // есть команда, соответствующая комбинации клавиш
      {
        e.Handled = true;
        cmd.Exec();
        return;
      }
      if (e.KeyCode == Keys.F9 && e.Modifiers == Keys.None && !Modal)
      {
        e.Handled = true;
        FormManager.Io.ShowMain(this);
      }
      if (e.KeyCode == Keys.F9 && (e.Modifiers == Keys.Control || e.Modifiers == Keys.Shift) && !Modal)
      {
        e.Handled = true;
        FormManager.Io.FormDefaultPos(this, e.Modifiers == Keys.Control);
      }
      if (e.KeyCode == Keys.F9 && (e.Modifiers == (Keys.Control | Keys.Shift) || e.Modifiers == (Keys.Alt | Keys.Shift)) && !Modal)
      {
        e.Handled = true;
        FormOptions.Load(this, e.Modifiers == (Keys.Control | Keys.Shift));
      }
    }
    //-------------------------------------------------------------------------
    private void FormBase_Shown(object sender, EventArgs e)
    {
      Control c = CommonLib.GetControls<Control>(this).FirstOrDefault(x => x.Name == DefaultControlName);
      if (c != null) // задан контрол для установки фокуса
      {
        if (c is TextBox)
          ((TextBox)c).Select(0, 0);
        else
          c.Select();
        c.Focus();
      }
    }
    //=========================================================================
    /// <summary>Записать данные из контролов в биндинги всех источников из словаря источников
    /// </summary>
    protected void SetControlsData()
    {
      foreach (var bs in bss.Where(x => x.Value != null))
        SetControlsData(bs.Value);
    }
    //-------------------------------------------------------------------------
    /// <summary>Записать данные из контролов в биндинги указанного источника
    /// </summary>
    /// <param name="src">Объект источника данных</param>
    protected void SetControlsData(object src)
    {
      if (src == null)
        return;
      foreach (Binding b in BindingContext[src].Bindings)
        b.ControlUpdateMode = ControlUpdateMode.Never;
      foreach (Binding b in BindingContext[src].Bindings)
        b.WriteValue();
      foreach (Binding b in BindingContext[src].Bindings)
        b.ControlUpdateMode = ControlUpdateMode.OnPropertyChanged;
    }
    //-------------------------------------------------------------------------
    /// <summary>Заполнение словаря источников из объектов данных контроллера
    /// </summary>
    /// <param name="d">контроллер данных</param>
    protected virtual void SetBindingSources(IDataController d)
    {
      if (d == null) return;
      foreach (var item in d.DataBinds)
      {
        if (!bss.ContainsKey(item.Key))
          bss.Add(item.Key, new BindingSource());
        bss[item.Key].DataSource = item.Value;
      }
    }
  }
}
