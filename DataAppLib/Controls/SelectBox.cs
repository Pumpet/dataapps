using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using Common;

namespace Ctrls
{
  /// <summary>Поведение SelectBox: выбор из выпадающего списка (Combo) или вызов формы для выбора (Form)
  /// </summary>
  public enum SelectBehaviorType { Combo, Form }
  //---------------------------------------------------------------------------
  public partial class SelectBox : ComboBox
  {
    #region props
    /// <summary>источник данных, которому присваивается выбранный объект</summary>
    [Category("New options"), Description("источник данных, которому присваивается выбранный объект")]
    public BindingSource ThisSource { get; set; }

    /// <summary>имена ключевых полей объекта источника данных через ;</summary>
    [Category("New options"), DefaultValue(""), Description("имена ключевых полей объекта источника данных через ;")]
    public string KeyNames { get; set; }

    /// <summary>другой источник данных (например - редактируемый объект), полям которого может быть присвоен выбранный объект или значения его полей</summary>
    [Category("New options"), Description("источник данных, полям которого может быть присвоен выбранный объект или значения его полей")]
    public BindingSource ExtSource { get; set; }

    /// <summary>имя поля в ExtSource, которому будет присвоен выбранный объект (например - родительский объект для редактируемого объекта)</summary>
    [Category("New options"), DefaultValue(""), Description("имя поля в ExtSource, которому будет присвоен выбранный объект")]
    public string ExtSourceParent { get; set; }

    /// <summary>пары "поле ExtSource = поле выбранного объекта" через ;</summary>
    [Category("New options"), DefaultValue(""), Description("пары \"поле ExtSource = поле выбранного объекта\" через ;")]
    public string ExtSourceFields { get; set; }

    /// <summary>имя формы для выбора</summary>
    [Category("New options"), DefaultValue(""), Description("имя формы для выбора")]
    public string SelectFormName { get; set; }

    /// <summary>допустимо пустое значение</summary>
    [Category("New options"), DefaultValue(false), Description("допустимо пустое значение")]
    public bool Nullable { get; set; }

    /// <summary>допустимо редактирование текста</summary>
    [Category("New options"), DefaultValue(false), Description("допустимо редактирование текста")]
    public bool Editable { get; set; }

    Color unlockedColor;
    ComboBoxStyle unlockedStyle;
    bool locked = false;
    /// <summary>заблокирован</summary>
    [Category("New options"), DefaultValue(false), Description("заблокирован")]
    public bool Locked { 
      get { return locked; }
      set {
        if (value && !locked)
        {
          unlockedColor = this.BackColor;
          unlockedStyle = this.DropDownStyle;
          this.BackColor = SystemColors.Control;
          this.DropDownStyle = ComboBoxStyle.Simple;
        }
        if (!value)
        {
          this.BackColor = unlockedColor;
          this.DropDownStyle = unlockedStyle;
        }
        locked = value;
      }
    }

    SelectBehaviorType selectBehavior = SelectBehaviorType.Form;
    /// <summary>поведение - выбор из выпадающего списка или из формы</summary>
    [Category("New options"), DefaultValue(SelectBehaviorType.Form), Description("поведение - выбор из выпадающего списка или из формы")]
    public SelectBehaviorType SelectBehavior
    {
      get { return selectBehavior; }
      set { selectBehavior = value;
            if (selectBehavior == SelectBehaviorType.Form)
            {
              DropDownHeight = 1;
              DropDownWidth = 1;
            }
            else
            {
              DropDownHeight = 106;
              DropDownWidth = Width;
            }
      }
    }
    #endregion
    //-----------------------------------------------------------------------
    #region events
    /// <summary>Вызов формы выбора: Action(ключ, ключи фильтра, метод обработки завершения выбора)</summary>
    [Category("New options"), Description("Вызов формы выбора: Action(ключ, ключи фильтра, метод обработки завершения выбора)")]
    public event Action<object, object, Action<object>> OnExecSelect;

    /// <summary>Получить объект по выбранному ключу: Func(ключ) - должен вернуть объект или typeof.
    /// Возврат null вызывает ошибку.
    /// Вызывается из метода обработки завершения выбора, если не задано OnGetSelected.
    /// </summary>
    [Category("New options"), Description("Получить объект по выбранному ключу: Func(ключ) - должен вернуть объект или typeof. Вызывается если не задано OnGetSelected")]
    public event Func<object, object> OnGetParent;

    /// <summary>Получить ключ объекта по именам ключевых полей (вместо стандартного способа)
    /// </summary>
    [Category("New options"), Description("Получить ключ объекта по именам ключевых полей (вместо стандартного способа)")]
    public event Func<object, string, object> OnGetKey;

    /// <summary>Получить ключи фильтра для передачи на форму для выбора
    /// </summary>
    [Category("New options"), Description("Получить ключи фильтра для передачи на форму для выбора")]
    public event Func<object> OnSetFilter;

    /// <summary>Обработка выбранного ключа. Перекрывает OnGetParent
    /// Вызывается из метода обработки завершения выбора
    /// </summary>
    [Category("New options"), Description("Обработка выбранного ключа. Перекрывает OnGetParent")]
    public event Action<object> OnGetSelected;

    /// <summary>После обработки завершения выбора</summary>
    [Category("New options"), Description("После обработки завершения выбора")]
    public event EventHandler AfterGetSelected;
    #endregion
    //-----------------------------------------------------------------------
    /// <summary>Вызов формы выбора: Action(this, имя формы, ключ, ключи фильтра, метод обработки завершения выбора).
    /// В базовой форме устанавливается делегат базовой формы. Вызывается если не задано OnExecSelect.
    /// </summary>
    [Browsable(false)]
    public Action<Control, string, object, object, Action<object>> DoExecSelect { get; set; }

    /// <summary>Получить объект по выбранному ключу: Func(ключ) - должен вернуть объект или typeof.
    /// Возврат null вызывает ошибку.
    /// При инициализации устанавливается делегат контроллера данных. 
    /// Вызывается если не задано OnGetSelected или OnGetParent
    /// </summary>
    [Browsable(false)]
    public Func<object, object> DoGetParent { get; set; }
    //-----------------------------------------------------------------------
    public SelectBox()
    {
      InitializeComponent();
      StartValues();
    }
    //-----------------------------------------------------------------------
    public SelectBox(IContainer container)
    {
      container.Add(this);
      InitializeComponent();
      StartValues();
    }
    protected void StartValues()
    {
      unlockedColor = this.BackColor;
      unlockedStyle = this.DropDownStyle;
    }
    //=======================================================================
    #region Standard Bindings
    //-----------------------------------------------------------------------
    /// <summary>Задает поведение для выбора только значений выбранного объекта из формы со списком
    /// </summary>
    /// <param name="extSource">источник данных</param>
    /// <param name="extSourceFields">поля источника данных = полям выбранного объекта (имяполя=имяполе;...)</param>
    /// <param name="form">имя формы выбора</param>
    /// <param name="getFunc">делегат контроллера данных, который вернет объект по ключу выбранного объекта</param>
    /// <param name="propName">имя свойства источника данных для привязки свойства Text</param>
    /// <param name="editable">true - допустимо редактирование текста</param>
    /// <param name="nullable">true - допустимо пустое значение</param>
    /// <param name="bind">привязка свойства источника данных к свойству контрола (не используется, если задано propName)</param>
    public void BindTextFromForm(BindingSource extSource, string extSourceFields, 
      string form, Func<object, object> getFunc, string propName,
      bool editable, bool nullable, Binding bind = null)
    {
      SelectBehavior = SelectBehaviorType.Form;
      Editable = editable;
      Nullable = nullable;
      ThisSource = null;
      KeyNames = null;
      ExtSource = extSource;
      ExtSourceParent = null;
      ExtSourceFields = extSourceFields;
      SelectFormName = form;
      DoGetParent = getFunc;
      if (!string.IsNullOrWhiteSpace(propName) && extSource != null)
        DataBindings.Add("Text", extSource, propName, true, DataSourceUpdateMode.OnValidation);
      else if (bind is Binding)
        DataBindings.Add(bind);
    }
    //-----------------------------------------------------------------------
    /// <summary>Задает поведение для выбора объекта из формы со списком (например - родительский объект для редактируемого объекта)
    /// </summary>
    /// <param name="thisSource">источник данных, которому присваивается выбранный объект</param>
    /// <param name="keyNames">имена ключевых полей источника данных (через ;)</param>
    /// <param name="extSource">другой источник данных</param>
    /// <param name="extSourceParent">имя поля другого источника данных = выбранному объекту</param>
    /// <param name="extSourceFields">поля другого источника данных = полям выбранного объекта (имяполя=имяполе;...)</param>
    /// <param name="form">имя формы выбора</param>
    /// <param name="getFunc">делегат контроллера данных, который вернет объект по ключу выбранного объекта</param>
    /// <param name="propName">имя свойства источника данных для привязки свойства Text</param>
    /// <param name="nullable">true - допустимо пустое значение</param>
    /// <param name="bind">привязка свойства источника данных к свойству контрола (не используется, если задано propName)</param>
    public void BindParentFromForm(BindingSource thisSource, string keyNames,
      BindingSource extSource, string extSourceParent, string extSourceFields, 
      string form, Func<object, object> getFunc, string propName,
      bool nullable, Binding bind = null)
    {
      SelectBehavior = SelectBehaviorType.Form;

      Editable = false;
      Nullable = nullable;
        
      ThisSource = thisSource;
      KeyNames = keyNames;

      ExtSource = extSource;
      ExtSourceParent = extSourceParent;
      ExtSourceFields = extSourceFields;
        
      SelectFormName = form;
      DoGetParent = getFunc;
        
      if (!string.IsNullOrWhiteSpace(propName) && thisSource != null)
        DataBindings.Add("Text", thisSource, propName, false, DataSourceUpdateMode.Never);
      else if (bind is Binding)
        DataBindings.Add(bind);
    }
    //-----------------------------------------------------------------------
    /// <summary>Задает поведение для выбора значения из выпадающего списка
    /// </summary>
    /// <param name="ds">источник выпадающего списка</param>
    /// <param name="member">имя свойства источника выпадающего списка для отображения в списке</param>
    /// <param name="bs">источник данных для propName</param>
    /// <param name="propName">имя свойства источника данных для привязки к контролу</param>
    /// <param name="editable">true - допустимо редактирование текста, свойство источника данных будет привязано к Text, иначе - к SelectedItem</param>
    /// <param name="nullable">true - допустимо пустое значение</param>
    /// <param name="bind">привязка свойства источника данных к свойству контрола (не используется, если задано propName)</param>
    public void BindTextFromCombo(object ds, string member, BindingSource bs, string propName,
      bool editable, bool nullable, Binding bind = null)
    {
      SelectBehavior = SelectBehaviorType.Combo;
        
      Editable = editable;
      Nullable = nullable;

      DataSource = ds;
      DisplayMember = member;
        
      ThisSource = null;
      KeyNames = null;
        
      ExtSource = null;
      ExtSourceParent = null;
      ExtSourceFields = null;
        
      SelectFormName = null;
      DoGetParent = null;
        
      if (!string.IsNullOrWhiteSpace(propName) && bs != null)
      {
        if (Editable)
          DataBindings.Add("Text", bs, propName, true, DataSourceUpdateMode.OnValidation);
        else
        {
          DataBindings.Add("SelectedItem", bs, propName, true, DataSourceUpdateMode.OnValidation);
          if (ds is BindingSource && ((BindingSource)ds).IndexOf(CommonLib.GetValueFromObject(bs.Current, propName)) == -1)
            SelectedIndex = -1;
        }
      }
      else if (bind is Binding)
        DataBindings.Add(bind);
    }
    //-----------------------------------------------------------------------
    /// <summary>Задает поведение для выбора объекта из выпадающего списка (например - родительский объект для редактируемого объекта)
    /// </summary>
    /// <param name="ds">источник выпадающего списка</param>
    /// <param name="member">имя свойства источника выпадающего списка для отображения в списке</param>
    /// <param name="thisSource">источник данных, которому присваивается выбранный объект</param>
    /// <param name="keyNames">имена ключевых полей источника данных (через ;)</param>
    /// <param name="extSource">другой источник данных</param>
    /// <param name="extSourceParent">имя поля другого источника данных = выбранному объекту</param>
    /// <param name="extSourceFields">поля другого источника данных = полям выбранного объекта (имяполя=имяполе;...)</param>
    /// <param name="getFunc">делегат контроллера данных, который вернет объект по ключу выбранного объекта</param>
    /// <param name="nullable">true - допустимо пустое значение</param>
    public void BindParentFromCombo(object ds, string member,
      BindingSource thisSource, string keyNames,
      BindingSource extSource, string extSourceParent, string extSourceFields,
      Func<object, object> getFunc,
      bool nullable)
    {
      SelectBehavior = SelectBehaviorType.Combo;
        
      Editable = false;
      Nullable = nullable;

      DataSource = ds;
      DisplayMember = member;

      ThisSource = thisSource;
      KeyNames = keyNames;
        
      ExtSource = extSource;
      ExtSourceParent = extSourceParent;
      ExtSourceFields = extSourceFields;
        
      SelectFormName = null;
      DoGetParent = getFunc;

      if (!string.IsNullOrWhiteSpace(ExtSourceParent) && extSource != null)
      {
        DataBindings.Add("SelectedItem", extSource, extSourceParent, true, DataSourceUpdateMode.OnValidation);
        if (thisSource != null && ds is BindingSource && ((BindingSource)ds).IndexOf(thisSource.Current) == -1)
          SelectedIndex = -1;
      }
    }
    #endregion
    //=======================================================================
    #region Overrides
    //-----------------------------------------------------------------------
    protected override void OnKeyDown(KeyEventArgs e)
    {
      if (!Editable || Locked)
        e.SuppressKeyPress = !((e.KeyValue >= 33 && e.KeyValue <= 40)
          || e.KeyCode == Keys.Shift || e.KeyCode == Keys.Control || e.KeyCode == Keys.Escape
          || (e.Modifiers == Keys.Control && e.KeyCode == Keys.C)
          || e.KeyCode == Keys.F4
          || (e.KeyCode == Keys.Delete && Nullable));

      if (e.KeyCode == Keys.F4 && e.Modifiers == Keys.None && !Locked)
      {
        e.Handled = true;
        OnDropDown(new EventArgs());
      }
      // сброс для случая Nullable по Delete
      if (!Editable && Nullable && !Locked && e.KeyCode == Keys.Delete)
      {
        e.Handled = true;
        if (SelectBehavior == SelectBehaviorType.Form)
          GetSelected(null);
        else if (SelectBehavior == SelectBehaviorType.Combo)
          SelectedIndex = -1;
      }
    }
    //-----------------------------------------------------------------------
    protected override void OnDropDown(EventArgs e)
    {
      if (Locked)
      {
        SendKeys.Send("{ESC}");
        return;
      }

      if (SelectBehavior == SelectBehaviorType.Form)
      {
        object obj = ThisSource is BindingSource ? ThisSource.Current : null;
        
        object filter = null;
        if (OnSetFilter != null)
          filter = OnSetFilter();
       
        if (OnExecSelect != null)
          OnExecSelect(GetKey(obj), filter, GetSelected);
        else if (DoExecSelect != null && !string.IsNullOrWhiteSpace(SelectFormName))
          DoExecSelect(this, SelectFormName, GetKey(obj), filter, GetSelected);
        //!! if (!Debugger.IsAttached) 
          SendKeys.Send("{ESC}");
      }
      else
        base.OnDropDown(e);
    }
    //-----------------------------------------------------------------------
    protected override void OnDropDownClosed(EventArgs e)
    {
      base.OnDropDownClosed(e);
      this.Select(0, 0);
    }
    //-----------------------------------------------------------------------
    protected override void OnSelectedValueChanged(EventArgs e)
    {
      base.OnSelectedValueChanged(e);
      if (SelectBehavior == SelectBehaviorType.Combo)
        GetSelected(GetKey(SelectedValue));
    }
    #endregion
    //=======================================================================
    private object GetKey(object obj)
    {
      var a = OnGetKey ?? CommonLib.GetKeyFromObject;
      if (a != null)
        return a(obj, KeyNames);
      else
        return null;
    }
    //-----------------------------------------------------------------------
    /* Обработка завершения выбора */
    /* Вызывается или при завершении выбора из формы (предварительно передается в форму как callback) 
       или из OnSelectedValueChanged для Combo 
       или для сброса - из OnKeyDown по Delete */
    private void GetSelected(object key)
    {
      try
      {
        if (OnGetSelected == null)
        {
          var a = OnGetParent ?? DoGetParent;
          if (a == null)
            return;
          object obj = a(key);

          if (obj == null) // даже если ключа нет, объект должен быть - или пустой, или typeof(тип объекта)
            throw new Exception("Функция не вернула объект");

          if (ThisSource is BindingSource)
          {
            ThisSource.DataSource = obj;
            ThisSource.ResetBindings(true);
          }

          if (key == null) // для внешнего источника объекта нет если ключа нет
            obj = null;

          SetObject(obj);
        }
        else
          OnGetSelected(key);

        if (AfterGetSelected != null)
          AfterGetSelected(this, null);
      }
      catch (Exception e)
      {
        MessageBox.Show("Ошибка получения выбранного объекта:\n" + e.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    //-----------------------------------------------------------------------
    /* Установка полей внешнего источника из выбранного объекта */
    private void SetObject(object obj)
    {
      if (!(ExtSource is BindingSource))
        return;

      PropertyDescriptorCollection propExt = TypeDescriptor.GetProperties(ExtSource.Current);
      PropertyDescriptorCollection propThis = obj != null ? TypeDescriptor.GetProperties(obj) : null;
        
      // в это поле попадет сам объект
      if (!string.IsNullOrWhiteSpace(ExtSourceParent)) 
      {
        string fExt = ExtSourceParent.Trim();
        if (propExt.OfType<PropertyDescriptor>().Any(x => x.Name == fExt))
          propExt[fExt].SetValue(ExtSource.Current, obj);
      }

      // установка значений полей согласно заданному соответствию имен
      if (!string.IsNullOrWhiteSpace(ExtSourceFields))
        foreach (string pair in ExtSourceFields.Split(new[]{';'}, StringSplitOptions.RemoveEmptyEntries))
        {
          string[] fs = pair.Split('=');
          if (fs.Length == 2)
          {
            string fExt = fs[0].Trim(), fThis = fs[1].Trim();
            if (propExt.OfType<PropertyDescriptor>().Any(x => x.Name == fExt)
                && (propThis == null || propThis.OfType<PropertyDescriptor>().Any(x => x.Name == fThis)))
              propExt[fExt].SetValue(ExtSource.Current, propThis != null ? propThis[fThis].GetValue(obj) : null);
            else
              MessageBox.Show("Невозможно обновить поле из указанной пары: " + pair, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
          }
          else
            MessageBox.Show("Невозможно обработать пару полей: " + pair, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
  }
}
