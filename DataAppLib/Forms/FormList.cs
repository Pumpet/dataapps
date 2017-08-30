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
  public partial class FormList : FormBase
  {
    /// <summary>список гридов формы</summary>
    protected List<DataList> dataLists;
    /// <summary>активный грид</summary>
    protected DataList activeList;

    /// <summary>главный грид</summary>
    [Browsable(true), Category("New options"), Description("главный грид")]
    public DataList MainList { get; set; }

    //-------------------------------------------------------------------------
    public FormList()
    {
      InitializeComponent();
    }
    //=========================================================================
    /// <summary>Начальные установки для контролов
    /// </summary>
    protected override void InitControls()
    {
      base.InitControls();
      
      dataLists = new List<DataList>();
      CommonLib.ForControls(this, (c) => { dataLists.Add((DataList)c); }, typeof(DataList));

      foreach (var item in dataLists)
      {
        // обработчики для событий гридов на уровне формы
        item.OnReload += LoadData;
        item.CellDoubleClick += DataList_CellDoubleClick;
        item.GotFocus += DataList_GotFocus;
        // определение главного списка
        if (MainList != null)
          item.MainList = (MainList == item);
        else if (item.MainList)
          MainList = item;
      }

      if (MainList == null && dataLists.Count == 1)
        MainList = dataLists[0];
    }
    //-------------------------------------------------------------------------
    /// <summary>Привязка контроллеров к обработчикам грида
    /// </summary>
    protected override void SetControllers()
    {
      base.SetControllers();
      Action<Control> doit = null;
      doit = (c) =>
      {
        DataList list = (DataList)c;
        IDataController d = (dc != null ? dc[list.DataControllerName] : null);
        if (d != null)
        {
          list.DoDelete += d.OnDelete;
          list.DoGetList += d.OnGetList;
          list.DoSetDefaults += d.OnSetDefaults;
          list.DoSave += d.OnSave;
          list.DoClone += d.OnCloneEntity;
          list.DoCheck += d.OnCheck;
          list.DoExecCommand += d.OnExecCommand;
          if (!string.IsNullOrWhiteSpace(list.EditFormName))
            list.DoExecEdit += ExecEditForm;
        }
      };
      CommonLib.ForControls(this, doit, typeof(DataList));
    }
    //-------------------------------------------------------------------------
    /// <summary>Установка команд
    /// </summary>
    protected override void SetCommands()
    {
      base.SetCommands();

      CommonLib.ForControls(this, (c) => { ((DataList)c).OnCheckMenu += cmds.SetBehaviors; }, typeof(DataList));

      cmds.Items.Add(new Command("Refresh", "Обновить", Properties.Resources.refresh, null, "Обновить (F5)", new[] { tools }));
      cmds["Refresh"].ActiveOnDefault = true;
      cmds["Refresh"].SetKeys(Keys.F5);
      cmds["Refresh"].SetBehavior(ExecRefresh, true, true);

      cmds.Items.Add(new Command("Select", "Выбрать", Properties.Resources.select, null, "Выбрать (Enter)", new[] { tools }));
      cmds["Select"].ActiveOnDefault = true;
      if (getResult)
      {
        cmds["Select"].SetKeys(Keys.Enter);
        cmds["Select"].SetBehavior(ExecSelect, true, true);
      }
      else
        cmds["Select"].SetBehavior(null, false, false);

      tools.Items.Add(new ToolStripSeparator());

      cmds.Items.Add(new Command("Add", "Добавить", Properties.Resources.add, null, "Добавить (Ctrl+Ins)", new[] { tools, menus }));
      cmds["Add"].SetKeys(Keys.Insert, Keys.Control);

      cmds.Items.Add(new Command("AddCopy", "Добавить копию", Properties.Resources.copy, null, "Добавить копию (Shift+Ins)", new[] { tools, menus }));
      cmds["AddCopy"].SetKeys(Keys.Insert, Keys.Shift);

      cmds.Items.Add(new Command("Edit", "Изменить", Properties.Resources.edit, null, "Изменить (Ctrl+Enter)", new[] { tools, menus }));
      cmds["Edit"].SetKeys(Keys.Enter, Keys.Control);

      cmds.Items.Add(new Command("Delete", "Удалить", Properties.Resources.del, null, "Удалить (Ctrl+Del)", new[] { tools, menus }));
      cmds["Delete"].SetKeys(Keys.Delete, Keys.Control);

      tools.Items.Add(new ToolStripSeparator());

      cmds.Items.Add(new Command("Filter", "Фильтр", Properties.Resources.filter, null, "Фильтр (Shift+F)", new[] { tools }));
      cmds["Filter"].SetKeys(Keys.F, Keys.Shift);

      cmds.Items.Add(new Command("Search", "Поиск", Properties.Resources.find, null, "Поиск (Ctrl+F)", new[] { tools }));
      cmds["Search"].SetKeys(Keys.F, Keys.Control);

      cmds.Items.Add(new Command("Excel", "Выгрузить в Excel", Properties.Resources.excel, null, "Выгрузить в Excel (Ctrl+E)", new[] { tools }));
      cmds["Excel"].SetKeys(Keys.E, Keys.Control);

      cmds.Items.Add(new Command("SelectCols", "Выбор столбцов", Properties.Resources.selcol, null, "Выбор столбцов (Alt+F)", new[] { tools }));
      cmds["SelectCols"].SetKeys(Keys.F, Keys.Alt);

      tools.Items.Add(new ToolStripSeparator());
      menus.Items[menus.Items.Add(new ToolStripSeparator())].Visible = false;
    }
    //-------------------------------------------------------------------------
    /// <summary>Установка фильтров, начитка данных в гриды, установка подчиненности, установка сортировок
    /// base: Начитка объектов данных в контроллерах
    /// </summary>
    protected override void InitData()
    {
      base.InitData();
      if (MainList != null)
        MainList.SetInnerFilter(inFilter);

      BindFilter();
      LoadData(MainList, inKey);
      SetBindingSources(data);
      Link();

      foreach (var li in dataLists)
        li.SetSort();

      if (MainList != null)
        MainList.Focus();
    }
    //-------------------------------------------------------------------------
    /// <summary>Установка фильтров (привязка данных к полям фильтра)</summary>
    protected virtual void BindFilter() { }
    //-------------------------------------------------------------------------
    /// <summary>Установка внешнего фильтра</summary>
    /// <param name="filter">Фильтр</param>
    public virtual void SetExternalFilter(object filter) { }
    //-------------------------------------------------------------------------
    /// <summary>Загрузка данных в гриды.
    /// Откликается на событие Reload грида и команду Refresh формы.
    /// Обновит вначале данные в главном, а потом обновит дочерние без начитки данных, т.к. они уже начитаны в главном.  
    /// </summary>
    /// <param name="sender">контрол, инициировавший загрузку данных</param>
    /// <param name="key">ключ текущего объекта</param>
    public override void LoadData(Control sender, object key) 
    {
      base.LoadData(sender, key);
      DataList list = sender is DataList ? (DataList)sender : null;
      Dictionary<DataList, object> keys = new Dictionary<DataList, object>();

      foreach (var li in dataLists)
        keys.Add(li, li == list && key != null ? key : li.GetKey());

      Cursor = Cursors.WaitCursor;
      try
      {
        if (MainList != null)
        {
          MainList.LoadData(keys[MainList], true);

          foreach (var li in dataLists)
            if (li != MainList)
              li.LoadData(keys[li], false);
        }
        else
          foreach (var li in dataLists)
            li.LoadData(keys[li], true);
      }
      finally
      {
        Cursor = Cursors.Default;
      }

      if (list != null)
        list.CheckMenu();
      else if (activeList != null)
        activeList.CheckMenu();
    }
    //-------------------------------------------------------------------------
    /// <summary>Установка подчиненности гридов</summary>
    protected virtual void Link() { }
    //=========================================================================
    /// <summary>Команда обновления списков
    /// </summary>
    /// <param name="cmd"></param>
    protected virtual void ExecRefresh(string cmd)
    {
      LoadData(null, null);
    }
    //-------------------------------------------------------------------------
    /// <summary>Команда выбора значения списка
    /// </summary>
    /// <param name="cmd"></param>
    protected virtual void ExecSelect(string cmd)
    {
      if (MainList != null && CallBack != null)
      {
        if (!MainList.ReadOnly)
        {
          MainList.EndEdit();
          MainList.ThisSource.EndEdit();
          if (!MainList.Save())
            return;
        }
        CallBack(MainList.GetKey());
        Close();
      }
    }
    //=========================================================================
    private void FormList_KeyDown(object sender, KeyEventArgs e)
    {
      // ключ в буфер обмена
      if (e.KeyCode == Keys.K && e.Modifiers == Keys.Control)
      {
        Func<object,string> keystr = (k) => {
          if (k == null)
            return "Null";
          else if (!(k is Dictionary<string, object>))
            return "Unknown key object";
          else
          {
            StringBuilder s = new StringBuilder("");
            ((Dictionary<string, object>)k).ToList().ForEach(x => s.AppendFormat("{0}={1};", x.Key, x.Value));
            if (s.Length == 0)
              return "Empty";
            return s.ToString();
          }
        };
        Clipboard.SetDataObject(keystr(dataLists.Select(x => { return x.Focused ? x.GetKey() : null; })
          .FirstOrDefault(k => k != null)));
      }
    }
    //-------------------------------------------------------------------------
    private void DataList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex < 0 || e.ColumnIndex < 0)
        return;
      if (getResult)
        cmds["Select"].Exec();
      else
        cmds["Edit"].Exec();
    }
    //-------------------------------------------------------------------------
    private void DataList_GotFocus(object sender, EventArgs e)
    {
      activeList = (DataList)sender;
    }
  }
}
