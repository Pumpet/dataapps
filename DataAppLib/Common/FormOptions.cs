using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Common
{
  /// <summary>Контейнер параметров форм
  /// </summary>
  public class FormsConfig
  {
    string fileName;
    [XmlArrayItem("Form")]
    public List<FormOptions> Forms = new List<FormOptions>();
    //-------------------------------------------------------------------------
    /// <summary>Заполняет контейнер параметров форм из файла. Если файла нет - создает новый.
    /// </summary>
    /// <param name="fileName">имя файла, по умолчанию: Forms + имя приложения.xml</param>
    /// <returns>контейнер параметров форм</returns>
    public static FormsConfig Create(string fileName = "")
    {
      if (string.IsNullOrWhiteSpace(fileName))
        fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Forms" + Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName) + ".xml");
      try
      {
        if (!File.Exists(fileName))
          OptionsSerializer.Save(fileName, new FormsConfig());
        FormsConfig config = OptionsSerializer.Load<FormsConfig>(fileName);
        if (config != null)
          config.fileName = fileName;
        return config;
      }
      catch (Exception ex)
      {
        Loger.SendMess(ex, "Ошибка загрузки параметров интерфейса из файла " + fileName);
        return null;
      }
    }
    //-------------------------------------------------------------------------
    /// <summary>Запись контейнера в файл
    /// </summary>
    public void Save()
    {
      OptionsSerializer.Save(fileName, this);
    }
  }
  //===========================================================================
  /// <summary>Параметры отображения формы
  /// </summary>
  public class FormOptions
  {
    [XmlAttribute]
    public string Name { get; set; }
    [XmlAttribute]
    public string Mode { get; set; }
    [XmlAttribute]
    public int Top { get; set; }
    [XmlAttribute]
    public int Left { get; set; }
    [XmlAttribute]
    public int Width { get; set; }
    [XmlAttribute]
    public int Height { get; set; }

    [XmlArrayItem("Grid")]
    public List<GridOptions> Grids = new List<GridOptions>();
    [XmlArrayItem("Split")]
    public List<SplitOptions> Splits = new List<SplitOptions>(); 

    //-------------------------------------------------------------------------
    /// <summary>Загрузить параметры отображения и присвоить форме
    /// </summary>
    /// <param name="form">форма</param>
    public static void Load(Form form, bool sizeOnly = false)
    {
      if (form == null) return;

      FormsConfig config = FormsConfig.Create();
      if (config == null) return;

      string mode = form.StartPosition == FormStartPosition.CenterParent ? "Parent" : "Default";
      FormOptions opt = config.Forms.FirstOrDefault(x => x.Name == form.Name && x.Mode == mode);
      if (opt == null) return;

      if (opt.Top >= 0) form.Top = opt.Top;
      if (opt.Left >= 0) form.Left = opt.Left;
      if (opt.Width >= 0) form.Width = opt.Width;
      if (opt.Height >= 0) form.Height = opt.Height;

      if (sizeOnly) return;

      foreach (var item in opt.Splits)
      {
        Control c = CommonLib.GetControl(form, item.Name);
        if (c is SplitContainer && item.Distance > 0)
          ((SplitContainer)c).SplitterDistance = item.Distance;
      }

      foreach (var item in opt.Grids)
      {
        Control c = CommonLib.GetControl(form, item.Name);
        if (c is DataGridView)
        {
          DataGridView g = ((DataGridView)c);
          foreach (DataGridViewColumn col in g.Columns)
          {
            ColumnOptions copt = item.Columns.FirstOrDefault(x => x.Name == col.Name);
            if (copt != null)
            {
              col.Width = copt.Width;
              col.DisplayIndex = copt.Pos;
              col.Visible = copt.Visible;
            }
          }
        }
      }
    }
    //-------------------------------------------------------------------------
    /// <summary>Сохранить параметры отображения формы в контейнере
    /// </summary>
    /// <param name="form">форма</param>
    public static void Save(Form form)
    {
      if (form == null) return;

      FormsConfig config = FormsConfig.Create();
      if (config == null) return;

      string mode = form.StartPosition == FormStartPosition.CenterParent ? "Parent" : "Default";
      FormOptions opt = config.Forms.FirstOrDefault(x => x.Name == form.Name && x.Mode == mode);
      if (opt == null)
      {
        opt = new FormOptions() { Name = form.Name, Mode = mode };
        config.Forms.Add(opt);      
      }

      if (form.WindowState == FormWindowState.Normal)
      {
        opt.Top = form.Top;
        opt.Left = form.Left;
        opt.Width = form.Width;
        opt.Height = form.Height;
      }

      opt.Grids.Clear();
      CommonLib.ForControls(form, (c) => {
        if (c is DataGridView)
          opt.Grids.Add(new GridOptions((DataGridView)c)); 
      });

      opt.Splits.Clear();
      CommonLib.ForControls(form, (c) => {
        opt.Splits.Add(new SplitOptions() { Name = c.Name, Distance = ((SplitContainer)c).SplitterDistance });
      }, typeof(SplitContainer));

      config.Save();
    }
  }
  //===========================================================================
  /// <summary>Параметры отображения грида
  /// </summary>
  public class GridOptions
  {
    [XmlAttribute]
    public string Name { get; set; }
    [XmlArrayItem("Column")]
    public List<ColumnOptions> Columns = new List<ColumnOptions>();

    public GridOptions()
    { }

    /// <summary>Взять параметры отображения из указанного грида
    /// </summary>
    /// <param name="grid">грид</param>
    public GridOptions(DataGridView grid)
    {
      Name = grid.Name;
      foreach (DataGridViewColumn c in grid.Columns)
        Columns.Add(new ColumnOptions() { Name = c.Name, Width = c.Width, Pos = c.DisplayIndex, Visible = c.Visible });
    }
  }
  //===========================================================================
  /// <summary>Параметры отображения столбца грида
  /// </summary>
  public class ColumnOptions
  {
    [XmlAttribute]
    public string Name { get; set; }
    [XmlAttribute]
    public int Width { get; set; }
    [XmlAttribute]
    public int Pos   { get; set; }
    [XmlAttribute]
    public bool Visible { get; set; }
  }
  //===========================================================================
  /// <summary>Параметры отображения сплиттера
  /// </summary>
  public class SplitOptions
  {
    [XmlAttribute]
    public string Name { get; set; }
    [XmlAttribute]
    public int Distance { get; set; }
  }
}
