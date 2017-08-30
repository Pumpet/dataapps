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
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Common;

namespace Ctrls
{
  public partial class DataList : DataGridView
  {
    DataList masterList; // родительский грид
    List<DataList> childLists = new List<DataList>(); // дочерние гриды

    // для выделения/подсветки строк
    int lastRowSelected = -1;
    List<int> prevRowSelected = new List<int>();
    bool rowHighlight = false;

    // ключи внешнего фильтра (например из родительского грида или из фильтра, полученного формой, или из полей формы)
    object inFilter { get; set; }

    bool filtered = false; // сейчас отфильтрован
    object prevKey = null; // ключ, чтобы на него вернуться
    List<Filter> filters = new List<Filter>(); // установленные фильтры
    Search search; // текущий поиск

    // состояния (просмотр, запись редактируется, редактируется новая запись)
    [Flags]
    enum ListMode { None = 0, Edit = 1, Add = 2 }
    ListMode mode = ListMode.None;

    // сортировка и первая видимая строка - чтобы вернуться
    DataGridViewColumn _sortedColumn;
    SortOrder _sortOrder;
    int _firstDisplayed;
    //-------------------------------------------------------------------------
    #region props
    /// <summary>источник данных (по умолчанию - из GetList контроллера)</summary>
    [Category("New options"), Description("источник данных (по умолчанию - из GetList контроллера")]
    public BindingSource ThisSource { get; set; }

    /// <summary>признак главного грида</summary>
    [Category("New options"), DefaultValue(true), Description("признак главного грида")]
    public bool MainList { get; set; }

    /// <summary>имя контроллера, для получения и обработки данных источника</summary>
    [Category("New options"), DefaultValue("Main"), Description("имя контроллера, для получения и обработки данных источника")]
    public string DataControllerName { get; set; }

    /// <summary>предупреждение перед удалением</summary>
    [Category("New options"), DefaultValue("Удалить выбранные записи?"), Description("предупреждение перед удалением")]
    public string DeleteMessage { get; set; }

    /// <summary>имена ключевых полей объекта источника данных через ;</summary>
    [Category("New options"), DefaultValue(""), Description("имена ключевых полей объекта источника через ;")]
    public string KeyNames { get; set; }

    /// <summary>пары "поле объекта источника данных = поле родительского объекта" через ;</summary>
    [Category("New options"), DefaultValue(""), Description("пары \"поле объекта источника = поле родительского объекта;\"")]
    public string FilterKeyNames { get; set; }

    /// <summary>имя формы для редактирования</summary>
    [Category("New options"), DefaultValue(""), Description("имя формы для редактирования")]
    public string EditFormName { get; set; }

    /// <summary>имя столбца для начальной сортировки с указанием направления: asc (по умолчанию) или desc</summary>
    [Category("New options"), DefaultValue(""), Description("имя столбца для начальной сортировки с указанием направления: asc (по умолчанию) или desc")]
    public string DefaultSort { get; set; }

    /// <summary>возможна фильтрация</summary>
    [Category("New options"), DefaultValue(true), Description("возможна фильтрация")]
    public bool CanFilter { get; set; }

    /// <summary>возможен поиск</summary>
    [Category("New options"), DefaultValue(true), Description("возможен поиск")]
    public bool CanSearch { get; set; }

    /// <summary>возможна выгрузка в Excel</summary>
    [Category("New options"), DefaultValue(true), Description("возможна выгрузка в Excel")]
    public bool CanExcelExport { get; set; }

    /// <summary>максимально допустимое количество строк для выгрузки в Excel</summary>
    [Category("New options"), DefaultValue(100000), Description("максимально допустимое количество строк для выгрузки в Excel")]
    public int MaxRowsToExcel { get; set; }

    /// <summary>признак нахождения в ячейке</summary>
    [Browsable(false)]
    public bool CellClicked { get; private set; }
    #endregion
    //-------------------------------------------------------------------------
    #region events
    /// <summary>переустановить меню (стандартно подписывается контейнер команд формы): Action(словарь "имя команды - делегат запуска")
    /// цель - перенаправить вызов команд формы на свои делегаты
    /// </summary>
    [Browsable(false)]
    public event Action<Dictionary<string, Action<string>>> OnCheckMenu;

    /// <summary>необходимо перечитать данные (стандартно подписывается форма, чтобы синхронизировать свое поведение, связанные гриды и т.п.):
    /// Action(грид, ключ)
    /// </summary>
    [Browsable(false)]
    public event Action<Control, object> OnReload;

    /// <summary>после установки команд (для установки дополнительных команд): Action(словарь команд "имя - делегат вызова")</summary>
    [Category("New options"), Description("после установки команд (для установки дополнительных команд): Action(словарь команд \"имя - делегат вызова\")")]
    public event Action<object> OnSetMenu;

    /// <summary>запрос ключа записи: Func(грид, индекс строки, имена ключевых полей)</summary>
    [Category("New options"), Description("при запросе ключа записи: Func(грид, индекс строки, имена ключевых полей) должен вернуть ключи")]
    public event Func<DataList, int, string, object> OnGetRowKey;

    /// <summary>постобработка команды: Action(код команды, объект результата)</summary>
    [Category("New options"), Description("")]
    public event Action<string, object> OnCommandResult;

    ///// <summary>формирование ключей фильтра: Func(существующие ключи фильтра) - вернуть новые ключи фильтра</summary>
    //[Category("New options"), Description("формирование ключей фильтра: Func(существующие ключи фильтра) - вернуть новые ключи фильтра")]
    //public event Func<object, object> OnSetFilter;

    /// <summary>формирование ключей фильтра: Func(существующие ключи фильтра) - вернуть новые ключи фильтра</summary>
    [Category("New options"), Description("формирование ключей фильтра: Func() - вернуть новые ключи фильтра")]
    public event Func<object> OnSetFilter;
    #endregion
    //-------------------------------------------------------------------------
    #region events instead of external delegates
    /// <summary>получить коллекцию данных: Func(ключ, ключи фильтра)</summary>
    [Category("New options"), Description("получить коллекцию данных: Func(ключ, ключи фильтра)")]
    public event Func<object, object, object> OnGetList;

    /// <summary>установка умолчаний: Action(объект, ключ внешнего(родительского) объекта)</summary>
    [Category("New options"), Description("установка умолчаний: Action(объект, ключ внешнего(родительского) объекта)")] 
    public event Action<object, object> OnSetDefaults;

    /// <summary>удаление объектов: Action(массив ключей)</summary>
    [Category("New options"), Description("удаление объектов: Action(массив ключей)")]
    public event Action<object[]> OnDelete;

    /// <summary>сохранение объекта: Func(объект, признак нового) - вернуть true если нет ошибок</summary>
    [Category("New options"), Description("сохранение объекта: Func(объект, признак нового) - вернуть true если нет ошибок")]
    public event Func<object, bool, bool> OnSave;

    /// <summary>передача данных из одного объекта в другой: Action(объект-источник, объект-приемник)</summary>
    [Category("New options"), Description("передача данных из одного объекта в другой: Action(объект-источник, объект-приемник)")]
    public event Action<object, object> OnClone;

    /// <summary>проверка данных объекта: Func(объект) - вернуть словарь "поле-ошибка"</summary>
    [Category("New options"), Description("проверка данных объекта: Func(объект) - вернуть словарь \"поле-ошибка\"")]
    public event Func<object, object> OnCheck;

    /// <summary>обработать команду: Func(код команды, ключ, ключи фильтра, обрабатываемый объект) - вернуть объект результата</summary>
    [Category("New options"), Description("обработать команду: Func(код команды, ключ, ключи фильтра, обрабатываемый объект) - вернуть объект результата")]
    public event Func<string, object, object, object, object[], object> OnExecCommand;

    /// <summary>запуск редактора: Action(признак нового, ключ, ключи фильтра)</summary>
    [Category("New options"), Description("Action(признак нового, ключ, ключи фильтра)")]
    public event Action<bool, object, object> OnExecEdit;
    #endregion
    //-------------------------------------------------------------------------
    #region external delegates - устанавливаются в форме = делегаты контроллера или формы
    /// <summary>получить коллекцию данных: Func(ключ, ключи фильтра)</summary>
    [Browsable(false)]
    public Func<object, object, object> DoGetList { get; set; }
    /// <summary>установка умолчаний: Action(объект, ключ внешнего(родительского) объекта)</summary>
    [Browsable(false)]
    public Action<object, object> DoSetDefaults { get; set; }
    /// <summary>удаление объектов: Action(массив ключей)</summary>
    [Browsable(false)]
    public Action<object[]> DoDelete { get; set; }
    /// <summary>сохранение объекта: Func(объект, признак нового) - вернуть true если нет ошибок</summary>
    [Browsable(false)]
    public Func<object, bool, bool> DoSave { get; set; }
    /// <summary>передача данных из одного объекта в другой: Action(объект-источник, объект-приемник)</summary>
    [Browsable(false)]
    public Action<object, object> DoClone { get; set; }
    /// <summary>проверка данных объекта: Func(объект) - вернуть словарь "поле-ошибка"</summary>
    [Browsable(false)]
    public Func<object, object> DoCheck { get; set; }
    /// <summary>обработать команду: Func(код команды, ключ, ключи фильтра, обрабатываемый объект) - вернуть объект результата</summary>
    [Browsable(false)]
    public Func<string, object, object, object, object[], object> DoExecCommand { get; set; }
    /// <summary>запуск редактора: Action(this, имя форма, признак нового, ключ, ключи фильтра, метод обработки завершения редактирования)</summary>
    [Browsable(false)]
    public Action<Control, string, bool, object, object, Action<object>> DoExecEdit { get; set; }
    #endregion
    //-------------------------------------------------------------------------
    public DataList()
    {
      InitializeComponent();
      AutoGenerateColumns = false;
      MainList = true;
      DataControllerName = "Main";
      DeleteMessage = "Удалить выбранные записи?";
      KeyNames = "";
      FilterKeyNames = "";
      EditFormName = "";
      CanFilter = true;
      CanSearch = true;
      CanExcelExport = true;
      MaxRowsToExcel = 100000;
      
      ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
      
      if (ThisSource == null)
        ThisSource = (DataSource is BindingSource) ? (BindingSource)DataSource : new BindingSource();
      if (DataSource == null)
        DataSource = ThisSource; // вся дальнейшая работа с данными - через ThisSource.DataSource и ThisSource.Current
    }
    //=========================================================================
    #region Overrides
    //-------------------------------------------------------------------------
    protected override void OnPaint(PaintEventArgs pe)
    {
      base.OnPaint(pe);
    }
    //-------------------------------------------------------------------------
    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);
      if (Columns.Count > 0)
      {
        DataGridViewColumn cc = null;
        foreach (DataGridViewColumn c in Columns)
        {
          if (c.Visible && (cc == null || c.DisplayIndex > cc.DisplayIndex))
            cc = c;
          c.AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
        }
        if (cc != null)
        {
          int w = cc.Width;
          if (cc.Displayed)
            cc.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
          else
            cc.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
          if (w > cc.Width) 
          {
            cc.AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
            cc.Width = w;
          }
        }
      }
    }
    //-------------------------------------------------------------------------
    protected override void OnSelectionChanged(System.EventArgs e)
    {
      base.OnSelectionChanged(e);

      if (ThisSource.Position >= 0)
      {
        Action<DataList> fu = null;
        fu = (d) =>
        {
          foreach (var item in d.childLists)
          {
            fu(item);
            item.SetSort();
            item.SetInnerFilter(CommonLib.GetKeyFromObjectForPairs(ThisSource.Current, item.FilterKeyNames));

            if (item.filtered)
              foreach (var f in item.filters)
                item.MarkFilteredHeader(item.Columns[f.ColName]);
          }
        };
        fu(this);
      }

      RowHighlight();
      CellClicked = true;
      CheckMenu();
    }
    //-------------------------------------------------------------------------
    protected override void OnGotFocus(EventArgs e)
    {
      base.OnGotFocus(e);
      RowHighlight(true);
      CheckMenu();
    }
    //-------------------------------------------------------------------------
    protected override void OnLostFocus(EventArgs e)
    {
      base.OnLostFocus(e);
      RowHighlight();
      CheckMenu(true);
    }
    //-------------------------------------------------------------------------
    protected override void OnRowLeave(DataGridViewCellEventArgs e)
    {
      base.OnRowLeave(e);
      BeforeLoadDataChilds();
    }
    //-------------------------------------------------------------------------
    protected override void OnKeyDown(KeyEventArgs e)
    {
      if (e.KeyCode == Keys.F && e.Modifiers == Keys.Control)
        ExecSearch(null);
      if (e.KeyCode == Keys.F && e.Modifiers == Keys.Shift)
        ExecFilter(null);
      if (e.KeyCode == Keys.E && e.Modifiers == Keys.Control)
        ExecExcel(null);

      if (e.KeyCode == Keys.F3 && ModifierKeys == Keys.None)
        DoSearch(null, SearchMode.Left);
      else if (e.KeyCode == Keys.F3 && ModifierKeys == Keys.Shift)
        DoSearch(null, SearchMode.Right);
      else if (e.KeyCode == Keys.F3 && ModifierKeys == Keys.Control)
        DoSearch(null, SearchMode.Down);
      else if (e.KeyCode == Keys.F3 && ModifierKeys == (Keys.Control | Keys.Shift))
        DoSearch(null, SearchMode.Up);

      if (e.KeyCode == Keys.F5)
        LoadData(null,true);

      if (e.KeyCode == Keys.F && e.Modifiers == Keys.Alt)
        ExecSelectCols(null);

      base.OnKeyDown(e);
    }
    //-------------------------------------------------------------------------
    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      CellClicked = false;
      var h = HitTest(e.X, e.Y);

      bool validThis = (e.Button != MouseButtons.Right || Validate(true, false, false));
      bool validChild = (e.Button != MouseButtons.Right || Validate(false, true, false));
      bool validParent = validChild && (e.Button != MouseButtons.Right || Validate(false, false, true));

      if (h.Type == DataGridViewHitTestType.Cell)
      {
        if (e.Button == MouseButtons.Right && validThis && validChild && validParent)
        {
          if (this[h.ColumnIndex, h.RowIndex].Visible)
            CurrentCell = this[h.ColumnIndex, h.RowIndex];
          else if (FirstDisplayedCell != null && FirstDisplayedCell.Visible)
            CurrentCell = FirstDisplayedCell;
          RowHighlight(h.RowIndex);
        }
        CellClicked = true;
      }

      if (e.Button == MouseButtons.Right)
      {
        Focus();
      }
      CheckMenu(!validChild || !validParent);
    }
    //-------------------------------------------------------------------------
    protected override void OnColumnHeaderMouseClick(DataGridViewCellMouseEventArgs e)
    {
      if (e.ColumnIndex >= 0 && Columns[e.ColumnIndex].SortMode == DataGridViewColumnSortMode.Automatic && ThisSource.SupportsSorting)
        Cursor = Cursors.WaitCursor;
      prevKey = GetKey();
      base.OnColumnHeaderMouseClick(e);
    }
    //-------------------------------------------------------------------------
    protected override void OnSorted(EventArgs e)
    {
      base.OnSorted(e);
      if (prevKey != null)
        SetRowFocus(prevKey);
      prevKey = null;
      Cursor = Cursors.Default;
    }
    //-------------------------------------------------------------------------
    protected override void OnCellBeginEdit(DataGridViewCellCancelEventArgs e)
    {
      mode |= ListMode.Edit;
      base.OnCellBeginEdit(e);
    }
    //-------------------------------------------------------------------------
    protected override void OnRowValidating(DataGridViewCellCancelEventArgs e)
    {
      e.Cancel = !Validate(true, true, false);
      base.OnRowValidating(e);
    }
    //-------------------------------------------------------------------------
    protected override void OnCellValidating(DataGridViewCellValidatingEventArgs e)
    {
      e.Cancel = !Validate(false, true, false);

      if (!e.Cancel)
        e.Cancel = !Validate(false, false, true);

      base.OnCellValidating(e);
    }
    //-------------------------------------------------------------------------
    /* попытка сохранить текущую запись - свою, дочерних гридов, родительского грида */
    private bool Validate(bool checkThis, bool checkChild, bool checkParent)
    {
      if (checkThis && !Save())
        return false;

      if (checkChild)
      {
        bool err = false;
        Action<DataList> fu = null;
        fu = (d) =>
        {
          foreach (var item in d.childLists)
          {
            if (err) break;
            if (!item.Save())
            {
              err = true;
              break;
            }
            fu(item);
          }
        };
        fu(this);
        if (err) return false;
      }

      if (checkParent && masterList != null && !masterList.Save())
          return false;

      return true;
    }
    #endregion
    //=========================================================================
    #region RowHighlight
    internal void RowHighlight(bool set, int idx = -1)
    {
      rowHighlight = set;
      RowHighlight(idx);
    }
    //-------------------------------------------------------------------------
    /// <summary>Подсветить строку. Также подсветит другие выделенные.
    /// </summary>
    /// <param name="idx">Индекс строки, по умолчанию подсветит текущую</param>
    public void RowHighlight(int idx = -1)
    {
      if (idx == -1 && CurrentRow != null)
        idx = CurrentRow.Index;
      if (!rowHighlight || idx < 0 || idx >= RowCount) return;

      if (lastRowSelected >= 0 && lastRowSelected < RowCount)
        Rows[lastRowSelected].DefaultCellStyle.BackColor = SystemColors.Window;

      foreach (int r in prevRowSelected)
        if (r >= 0 && r < RowCount && !SelectedCells.OfType<DataGridViewCell>().Any(x => x.RowIndex == r))
          Rows[r].DefaultCellStyle.BackColor = SystemColors.Window;
      prevRowSelected.Clear();

      foreach (int r in SelectedCells.OfType<DataGridViewCell>().Select(x => x.RowIndex).Distinct())
      {
        Rows[r].DefaultCellStyle.BackColor = SystemColors.Control;
        prevRowSelected.Add(r);
      }

      Rows[idx].DefaultCellStyle.BackColor = Focused ? SystemColors.Info : SystemColors.ControlLight;
      lastRowSelected = idx;
    }
    #endregion
    //=========================================================================
    #region Sevices
    //-------------------------------------------------------------------------
    /// <summary>Получить ключ объекта, связанного со строкой
    /// </summary>
    /// <param name="idx">Индекс строки или текущий</param>
    /// <param name="keyNames">перечень имен ключевых полей (по умолчанию - из Keynames)</param>
    /// <returns>ключ</returns>
    public object GetKey(int idx = -1, string keyNames = null)
    {
      if (idx == -1 && CurrentRow != null)
        idx = CurrentRow.Index;
      if (idx < 0 || idx >= RowCount)
        return null;

      if (string.IsNullOrWhiteSpace(keyNames))
        keyNames = KeyNames;

      if (OnGetRowKey != null)
        return OnGetRowKey(this, idx, keyNames);
      else
        return CommonLib.GetKeyFromObject(Rows[idx].DataBoundItem, keyNames);
    }
    //-------------------------------------------------------------------------
    /// <summary>Создает словарь команд ("имя команды - делегат вызова") со стандартными командами.
    /// Инициирует OnSetMenu и OnCheckMenu.
    /// </summary>
    /// <param name="clear">true для создания пустого словаря команд</param>
    public void CheckMenu(bool clear = false)
    {
      Dictionary<string, Action<string>> cmds = new Dictionary<string, Action<string>>();

      if (!clear)
      {
        if (OnExecEdit == null && DoExecEdit == null && ReadOnly)
        {
          cmds.Add("Add", null);
          cmds.Add("AddCopy", null);
          cmds.Add("Edit", null);
        }
        else
        {
          if (DataSource != null && (masterList == null || masterList.CurrentCell != null))
            cmds.Add("Add", ExecAdd);

          if (CurrentCell != null && CellClicked)
          {
            cmds.Add("AddCopy", (OnClone != null || DoClone != null) ? ExecAdd : (Action<string>)null);
            cmds.Add("Edit", ReadOnly ? ExecEdit : (Action<string>)null);
          }
        }

        if (OnDelete == null && DoDelete == null)
          cmds.Add("Delete", null);
        else if (CurrentCell != null && CellClicked)
          cmds.Add("Delete", ExecDelete);

        if (mode == ListMode.None)
        {
          if (CurrentCell != null)
          {
            cmds.Add("Filter", ExecFilter);
            cmds.Add("Search", ExecSearch);
          }
          cmds.Add("Excel", ExecExcel);
          cmds.Add("SelectCols", ExecSelectCols);
        }

        if (!CanFilter) cmds["Filter"] = null;
        if (!CanSearch) cmds["Search"] = null;
        if (!CanExcelExport) cmds["Excel"] = null;

        if (OnSetMenu != null)
          OnSetMenu(cmds);
      }
      if (OnCheckMenu != null)
        OnCheckMenu(cmds);
    }
    //-------------------------------------------------------------------------
    /// <summary>Обработка набора команд через контроллер (вызывается например из обработчика OnSetMenu)</summary>
    /// <param name="c">контроллер</param>
    /// <param name="cmds">набор команд для меню</param>
    /// <param name="code">код, если например нужно отличать разные обработки в одом контроллере</param>
    public void SetMenuThruController(IDataController c, object cmds, string code = "")
    { 
      object[] keys = SelectedCells.OfType<DataGridViewCell>().Select(x => x.RowIndex).Distinct().Select(x2 => GetKey(x2)).ToArray();
      c.OnSetCommands(cmds, GetKey(), ThisSource.Current, keys, code);   
    }
    //-------------------------------------------------------------------------
    internal void BeforeLoadDataChilds()
    {
      Action<DataList> fu = null;
      fu = (d) =>
      {
        foreach (var item in d.childLists)
        {
          fu(item);
          item.BeforeLoadData();
        }
      };
      fu(this);
    }
    //-------------------------------------------------------------------------
    internal void BeforeLoadData()
    {
      RowHighlight(false);
      _sortedColumn = SortedColumn;
      _sortOrder = SortOrder;
      _firstDisplayed = FirstDisplayedScrollingRowIndex;
    }
    //-----------------------------------------------------------------------
    /// <summary>Обновить данные грида
    /// </summary>
    /// <param name="key">Ключ текущей записи</param>
    public void Reload(object key = null)
    {
      if (OnReload != null)
        OnReload(this, key);
      else
        LoadData(key, true);
      CheckMenu();
    }
    //-------------------------------------------------------------------------
    /// <summary>Установка сохраненной сортировки или сортировки по умолчанию
    /// </summary>
    public void SetSort()
    {
      if (Columns.Count == 0) return;
      if (!(DataSource is BindingSource) || !((BindingSource)DataSource).SupportsSorting)
        return;

      DataGridViewColumn col = _sortedColumn;
      SortOrder so = _sortOrder;

      string[] s = { "", "asc" };
      if (!string.IsNullOrWhiteSpace(DefaultSort))
        s = DefaultSort.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
      if (col == null)
        col = Columns.OfType<DataGridViewColumn>()
          .FirstOrDefault(x => x.Name.Equals(s[0], StringComparison.OrdinalIgnoreCase) || x.DataPropertyName.Equals(s[0], StringComparison.OrdinalIgnoreCase)); 
      if (col != null)
      {
        if (so == SortOrder.None)
          so = (s.Length < 2 || (s[1] != "desc" && s[1] != "descending") ? so = SortOrder.Ascending : SortOrder.Descending);

        Sort(col, so == SortOrder.Ascending ? ListSortDirection.Ascending : ListSortDirection.Descending);

        _sortedColumn = col;
        _sortOrder = so;
      }
    }
    //-------------------------------------------------------------------------
    /* индекс объекта с указанным ключом в коллекции источника */
    private int GetKeyPosition(BindingSource bs, object key)
    {
      int pos = -1;
      if (key is Dictionary<string, object>)
      {
        Dictionary<string, object> keys = (Dictionary<string, object>)key;
        if (bs == null || bs.Current == null || keys.Count == 0)
          return pos;
        PropertyDescriptorCollection props = TypeDescriptor.GetProperties(bs.Current);
        if (keys.Any(k => !props.OfType<PropertyDescriptor>().Any(x => x.Name == k.Key)))
          return pos;

        var li = bs.List.OfType<object>().FirstOrDefault(o => keys.All(k => props[k.Key].GetValue(o).Equals(k.Value)));
        if (li != null)
          pos = bs.IndexOf(li);
      }
      else if (key != null)
        pos = bs.IndexOf(key);

      return pos;
    }
    //-------------------------------------------------------------------------
    /* установить фокус на строку, содержащую объект с указанным ключом */
    private void SetRowFocus(object key)
    {
      if (_firstDisplayed >= 0 && _firstDisplayed < RowCount)
        FirstDisplayedScrollingRowIndex = _firstDisplayed;
      _firstDisplayed = -1;

      int idxCol = CurrentCell != null ? CurrentCell.ColumnIndex : 0;

      int pos = GetKeyPosition(ThisSource, key);
      if (pos >= 0)
        ThisSource.Position = pos;

      int idx = CurrentRow != null ? CurrentRow.Index : Rows.Count > 0 ? 0 : -1;

      if (idx != -1 && !this[idxCol, idx].Visible && FirstDisplayedCell != null)
      {
        idx = FirstDisplayedCell.RowIndex;
        idxCol = FirstDisplayedCell.ColumnIndex;
      }
      
      CurrentCell = idx != -1 && this[idxCol, idx].Visible ? this[idxCol, idx] : null;

      if (CurrentRow != null
          && (CurrentRow.Index > FirstDisplayedScrollingRowIndex + DisplayedRowCount(true)
              || CurrentRow.Index < FirstDisplayedScrollingRowIndex))
        FirstDisplayedScrollingRowIndex = CurrentRow.Index;

      RowHighlight(true);
      CheckMenu();
    }
    //-----------------------------------------------------------------------
    /// <summary>Связать с объектом родительского грида
    /// </summary>
    /// <param name="master">родительский грид</param>
    /// <param name="masterPropName">имя свойства-коллекции объекта родительского грида, которое будет источником</param>
    public void SetMaster(DataList master, string masterPropName)
    {
      RowHighlight(false);
      ThisSource.DataSource = master.ThisSource;
      ThisSource.DataMember = masterPropName;
      masterList = master;
      master.SetChild(this);
      RowHighlight(true);
    }
    //-----------------------------------------------------------------------
    /// <summary>Установить внутренний фильтр списка
    /// </summary>
    /// <param name="f">фильтр</param>
    public void SetInnerFilter(object f)
    {
      if (!(f is Dictionary<string, object>)) return;
      if (inFilter == null)
        inFilter = new Dictionary<string, object>((Dictionary<string, object>)f);
      else
        foreach (var fi in (Dictionary<string, object>)f)
          ((Dictionary<string, object>)inFilter)[fi.Key] = fi.Value;
    }
    //-----------------------------------------------------------------------
    /* добавить грид в коллекцию дочерних */
    internal void SetChild(DataList child)
    {
      childLists.Add(child);
    }
    #endregion
    //=========================================================================
    /// <summary>Обновить грид.
    /// </summary>
    /// <param name="key">ключ объекта, на строку с которым надо бы встать</param>
    /// <param name="reload">перечитать данные (в этом случае вызывает OnSetFilter, затем OnGetList или DoGetList)</param>
    public void LoadData(object key, bool reload)
    {
      //if (DataSource == null)
      //  return;

      prevKey = null;
      ClearFilter();
      key = key ?? GetKey();

      if (reload && (OnGetList != null || DoGetList != null))
      {
        Cursor = Cursors.WaitCursor;
        try
        {
          BeforeLoadDataChilds();

          Action<DataList> fu = null;
          fu = (d) =>
          {
            foreach (var item in d.childLists)
            {
              fu(item);
              item.Save();
              item.mode = ListMode.None;
              item.NotifyCurrentCellDirty(false);
            }
          };
          fu(this);

          BeforeLoadData();
          Save();
          mode = ListMode.None;
          NotifyCurrentCellDirty(false);

          if (OnSetFilter != null)
            SetInnerFilter(OnSetFilter());
          ThisSource.DataSource = OnGetList != null ? OnGetList(key, inFilter) : DoGetList(key, inFilter);
        }
        finally
        {
          Cursor = Cursors.Default;
        }
      }
      SetSort();
      SetRowFocus(key);
    }
    //=========================================================================
    #region Add-Edit-Check-Save
    //-------------------------------------------------------------------------
    /* В случае ReadOnly - поднимет форму, иначе добавляем строчку */
    private void ExecAdd(string cmd)
    {
      bool copy = cmd == "AddCopy";

      if (ReadOnly)
      {
        Edit(true, copy);
        return;
      }

      if (!Save())
        return;

      CausesValidation = false;
      RowHighlight(false);

      ThisSource.ResetBindings(true);

      int idx = CurrentRow != null ? CurrentRow.Index : -1;

      ClearSelection();

      try
      {
        ThisSource.AddNew();
        CurrentCell.Selected = true;

        if (copy && idx >= 0)
        {
          var a = OnClone ?? DoClone;
          if (a != null) a(Rows[idx].DataBoundItem, ThisSource.Current);
        }
        else
        {
          var a = OnSetDefaults ?? DoSetDefaults;
          if (a != null) a(ThisSource.Current, inFilter);
        }

        ThisSource.EndEdit();
        mode = ListMode.Add;
        NotifyCurrentCellDirty(true);
      }
      catch (Exception e)
      {
        Loger.SendMess(e, "Не удалось добавить запись!");
        mode = ListMode.Add;
        NotifyCurrentCellDirty(true);
        Reload();
      }
      finally
      {
        RowHighlight(true);
        CausesValidation = true;
      }
    }
    //-------------------------------------------------------------------------
    /* В случае ReadOnly - поднимет форму */
    private void ExecEdit(string cmd)
    {
      if (ReadOnly)
        Edit(false);
    }
    //-------------------------------------------------------------------------
    /* Поднимаем форму редактора */
    private void Edit(bool add, bool copy = false)
    {
      if (OnExecEdit != null)
        OnExecEdit(add, add && !copy ? null : GetKey(), inFilter);
      else if (DoExecEdit != null)
        DoExecEdit(this, EditFormName, add, add && !copy ? null : GetKey(), inFilter, Reload);
    }
    //-----------------------------------------------------------------------
    /// <summary>Сохранить данные текущего объекта (для редактируемого грида). 
    /// (вызывает проверку (OnCheck или DoCheck) и если успешно - вызывает OnSave или DoSave) 
    /// </summary>
    /// <returns>true если грид нередактируемый или нет данных, или false если не прошло проверку, или результат OnSave/DoSave</returns>
    public bool Save()
    {
      if (mode == ListMode.None || CurrentRow == null || ThisSource == null)
        return true;

      if (!CheckData())
      {
        NotifyCurrentCellDirty(true);
        return false;
      }
      
      var a = OnSave ?? DoSave;
      bool res = (a == null || a(ThisSource.Current, false));

      mode = ListMode.None;
      NotifyCurrentCellDirty(false);

      if (!res) Reload();

      return res;
    }
    //-------------------------------------------------------------------------
    /* проверяем перед сохранением */
    private bool CheckData()
    {
      if (CurrentRow == null)
        return true;

      foreach (DataGridViewCell cell in CurrentRow.Cells)
        cell.ErrorText = "";

      if (ThisSource == null || ThisSource.Current == null)
        return true;

      EndEdit();
      ThisSource.EndEdit();
      
      var a = OnCheck ?? DoCheck;
      if (a == null)
        return true;

      var errs = ((Dictionary<string, string>)a(ThisSource.Current)).Where(x => !String.IsNullOrWhiteSpace(x.Value));

      if (errs.Count() == 0)
        return true;
      else
      {
        foreach (var err in errs)
        {
          DataGridViewColumn c = Columns.OfType<DataGridViewColumn>().FirstOrDefault(y => y.DataPropertyName == err.Key);
          if (c != null)
            CurrentRow.Cells[c.Name].ErrorText = err.Value;
          else
            Loger.SendMess(err.Value, true);
        }
        return false;
      }
    }
    #endregion
    //=========================================================================
    /* удаляем выделенные */
    private void ExecDelete(string cmd)
    {
      if (mode.HasFlag(ListMode.Add)) // для добавленной записи не нужно обращаться к источнику
      {
        if (CurrentRow != null)
          Rows.Remove(CurrentRow);

        mode = ListMode.None;
        NotifyCurrentCellDirty(false);
        
        Reload();
        return;
      }
      
      if (!Save())
        return;
      
      if (OnDelete == null && DoDelete == null)
        return;

      if (!string.IsNullOrEmpty(DeleteMessage) && MessageBox.Show(DeleteMessage, "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Cancel)
        return;

      Cursor = Cursors.WaitCursor;
      try
      {
        object[] keys = SelectedCells.OfType<DataGridViewCell>().Select(x => x.RowIndex).Distinct().Select(x2 => GetKey(x2)).ToArray();
        if (OnDelete != null)
          OnDelete(keys);
        else
          DoDelete(keys);
      }
      finally
      {
        Cursor = Cursors.Default;
      }

      Reload();
    }
    //=========================================================================
    /// <summary>Обработать команду 
    /// (вызов OnExecCommand или DoExecCommand, затем OnCommandResult)
    /// </summary>
    /// <param name="cmd">имя команды</param>
    public void ExecCommand(string cmd)
    {
      if (!Save())
        return;
      if (OnExecCommand != null || DoExecCommand != null)
      {
        Cursor = Cursors.WaitCursor;
        try
        {
          object[] keys = SelectedCells.OfType<DataGridViewCell>().Select(x => x.RowIndex).Distinct().Select(x2 => GetKey(x2)).ToArray();
          var a = OnExecCommand ?? DoExecCommand;
          object res = a(cmd, GetKey(), inFilter, ThisSource.Current, keys);
          if (OnCommandResult != null)
            OnCommandResult(cmd, res);
        }
        finally
        {
          Cursor = Cursors.Default;
        }
        Reload();
      }
    }
    //=========================================================================
    #region Filter
    //-------------------------------------------------------------------------
    /// <summary>Запуск формы фильтра
    /// </summary>
    /// <param name="cmd">не используется</param>
    public void ExecFilter(string cmd)
    {
      if (CurrentCell != null && mode == ListMode.None && CanFilter)
      {
        FormFilter ff = new FormFilter(CurrentCell.OwningColumn, CurrentCell.Value, SetFilter);
        ff.ShowDialog(this.Parent);
      }
    }
    //-------------------------------------------------------------------------
    /// <summary>Установить фильтр 
    /// (считается установленным если в источнике остаются записи, удовлетворяющие условиям фильтра)
    /// Для установленного фильтра столбец выделяется
    /// </summary>
    /// <param name="filter">объект фильтра</param>
    /// <returns>-1: нельзя установить или ошибка, иначе вернет кол-во оставшихся записей</returns>
    public int SetFilter(Filter filter)
    {
      int res = DoFilter(filter);
      if (res <= 0)
        return res;

      prevKey = GetKey();
      filter.MasterKey = inFilter;
      filters.Add(filter);
      filtered = true;

      MarkFilteredHeader(Columns[filter.ColName]);

      BeforeLoadDataChilds();
      SetRowFocus(prevKey);
      prevKey = null;

      return res;
    }
    //-------------------------------------------------------------------------
    /* если есть фильтры для этого столбца и родительского ключа - пометить столбец и вывести описание в тултип, если нет - очистить пометки */
    private void MarkFilteredHeader(DataGridViewColumn col, bool clear = false)
    {
      string filter = clear ? ""
        : string.Join("; ", filters.Where(f => f.ColName == col.Name && CommonLib.CompareKeys(f.MasterKey, inFilter))
          .Select(x => x.FilterName).Distinct());

      if (filter != "")
      {
        col.HeaderCell.Style.Font = new Font(DefaultCellStyle.Font, FontStyle.Bold);
        col.HeaderCell.Style.ForeColor = Color.DarkRed;
        col.HeaderCell.ToolTipText = filter;
      }
      else 
      {
        col.HeaderCell.Style.Font = new Font(DefaultCellStyle.Font, FontStyle.Regular);
        col.HeaderCell.Style.ForeColor = SystemColors.ControlText;
        col.HeaderCell.ToolTipText = "";
      }
    }
    //-------------------------------------------------------------------------
    /// <summary>Сбросить все фильтры и пометки столбцов
    /// </summary>
    public void ClearFilter()
    {
      foreach (var item in filters)
        MarkFilteredHeader(Columns[item.ColName], true);

      filters.Clear();
      filtered = false;
    }
    //-------------------------------------------------------------------------
    private int DoFilter(Filter filter)
    {
      if (mode != ListMode.None || filter == null)
        return -1;

      int res = 0;

      SuspendLayout();
      ScrollBars sb = this.ScrollBars;
      this.ScrollBars = ScrollBars.None;

      Cursor = Cursors.WaitCursor;
      try
      {
        List<DataGridViewRow> rows = new List<DataGridViewRow>();
        for (int r = 0; r < Rows.Count; r++)
        {
          DataGridViewRow row = Rows[r];
          if (!filter.Check(row.Cells[filter.ColName].Value))
            rows.Add(row);
        }
        res = Rows.Count - rows.Count;
        if (res > 0 && res < Rows.Count)
        {
          ThisSource.SuspendBinding();
          ThisSource.Position = -1;
          
          // так очень медленно
          //for (int i = 0; i < rows.Count; i++)
          //  Rows.Remove(rows[i]);
          for (int i = rows.Count - 1; i >= 0; i--)
            Rows.RemoveAt(rows[i].Index);
        }
      }
      catch (Exception e)
      {
        Loger.SendMess(e, "Ошибка установки фильтра!");
        return -1;
      }
      finally
      {
        ThisSource.ResumeBinding();
        this.ScrollBars = sb;
        ResumeLayout();
        Cursor = Cursors.Default;
      }

      return res;
    }
    #endregion
    //=========================================================================
    #region Search
    //-------------------------------------------------------------------------
    /// <summary>Запуск формы поиска
    /// </summary>
    /// <param name="cmd">не используется</param>
    public void ExecSearch(string cmd)
    {
      if (CurrentCell != null && mode == ListMode.None && CanSearch)
      {
        FormSearch fs = new FormSearch(DoSearch);
        fs.ShowDialog(this.FindForm());
      }
    }
    //-------------------------------------------------------------------------
    /// <summary>Поискать
    /// </summary>
    /// <param name="newSearch">условия поиска</param>
    /// <param name="mode">направление</param>
    /// <returns>успех</returns>
    public bool DoSearch(Search newSearch, SearchMode mode) 
    {
      if (newSearch != null)
        search = newSearch;

      if (search == null || CurrentCell == null || search.Check(null))
        return false;

      bool fwd = (mode == SearchMode.Left || mode == SearchMode.Down), 
        incol = (mode == SearchMode.Up || mode == SearchMode.Down);

      Func<int, int, List<int>> fn = (r, c) =>
      {
        return Rows[r].Cells.OfType<DataGridViewCell>()
          .Where(x => x.OwningColumn.Index >= 0 && (!incol ? (fwd ? x.OwningColumn.Index > c : c > x.OwningColumn.Index) : c == x.OwningColumn.Index))
          .Where(y => search.Check(y.Value))
          .Select(z => z.OwningColumn.Index).ToList();
      };

      if (MoveCell(fwd, incol, false, fn))
      {
        RowHighlight();
        return true;
      }
      else
        return false;
    }
    //-------------------------------------------------------------------------
    /* перемещение в определенную ячейку из отобранных делегатом */
    private bool MoveCell(bool fwd, bool inCol, bool inRow, Func<int, int, List<int>> GetNextPosInRow)
    {
      // fwd - двигаться вперед, inCol/inRow - двигаться в пределах текущего столбца/строки
      bool res = false;
      if (CurrentCell == null || (inCol && inRow)) 
        return res;
      Cursor = Cursors.WaitCursor;
      try
      {
        int step = fwd ? 1 : -1; // шаг по строкам
        int row = CurrentRow.Index; // текущая строка
        int r = row + (inCol ? step : 0); // стартовая строка
        int c = CurrentCell.OwningColumn.Index; // стартовый столбец
        bool start = true;
        List<int> cn = null;

        while (start || (!inRow && (fwd ? r <= row : r >= row))) // если только начали или еще не пошли столбец по второму кругу
        {
          if (fwd ? r >= RowCount : r < 0) // дошли до конца/начала столбца - начнем сначала/сконца столбца
          {
            r = fwd ? 0 : (RowCount - 1); 
            start = false;
          }
          cn = null;
          if (GetNextPosInRow != null) 
            cn = GetNextPosInRow(r, c).Where(x => Columns[x].Visible).ToList(); // подходящие ячейки в этой строке
          if (cn != null && cn.Count > 0)
            break;
          if (!inRow) //
            r = r + step;
          else if (c == (fwd ? -1 : ColumnCount)) // строка пошла еще раз сначала/сконца
            start = false;
          if (!inCol) c = fwd ? -1 : ColumnCount; // строку - сначала/сконца
        }

        if (cn != null && cn.Count > 0) // есть подходящие ячейки в строке r
        {
          c = (fwd ? cn.Min() : cn.Max()); // нужная с учетом направления
          CurrentCell = this[c, r]; 
          res = true;
        }
      }
      finally
      {
        Cursor = Cursors.Default;
      }
      return res;
    }
    #endregion
    //=========================================================================
    /// <summary>Выгрузить в эксель
    /// </summary>
    /// <param name="cmd">не используется</param>
    public void ExecExcel(string cmd)
    {
      if (mode == ListMode.None && CanExcelExport)
        ExcelLib.GridToExcel(this, MaxRowsToExcel);
    }
    //=========================================================================
    #region SelectCols
    //-------------------------------------------------------------------------
    /// <summary>Запуск формы выбора столбцов
    /// </summary>
    /// <param name="cmd">не используется</param>
    public void ExecSelectCols(string cmd)
    {
      if (mode == ListMode.None)
      {
        FormSelectCols fs = new FormSelectCols(DoSelectCols, Columns);
        fs.ShowDialog(this.FindForm());
        SetRowFocus(null);
        OnResize(null);
      }
    }
    public void DoSelectCols(List<string> colNames)
    {
      for (int i = 0; i < Columns.Count; i++)
        Columns[i].Visible = colNames.Contains(Columns[i].Name);
      if (Columns.Count > 0 && !Columns.OfType<DataGridViewColumn>().Any(x => x.Visible))
        Columns[0].Visible = true;
    }
    #endregion

  }
}
