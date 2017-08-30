using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace Common
{
  /// <summary>Параметры приложения
  /// </summary>
  public class AppConfig
  {
    static AppConfig config;
    /// <summary>Значение параметра приложения
    /// </summary>
    /// <param name="name">Имя параметра</param>
    /// <returns>Значение параметра</returns>
    public static string Prop(string name)
    {
      return config != null ? config.GetProp(name) : null; 
    }

    string fileName;
    [XmlElement("Prop")]
    public List<Prop> Props { get; set; }
    //-------------------------------------------------------------------------
    public AppConfig ()
	  {
      Props = new List<Prop>();
	  }
    //-------------------------------------------------------------------------
    /// <summary>Загружает параметры приложения из файла. Если файла нет - создает новый.
    /// </summary>
    /// <param name="fileName">Имя файла, по умолчанию: имя приложения.xml</param>
    public static void Load(string fileName = "")
    {
      if (string.IsNullOrWhiteSpace(fileName))
        fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName) + ".xml");
      try
      {
        if (!File.Exists(fileName))
          OptionsSerializer.Save(fileName, new AppConfig());
        config = OptionsSerializer.Load<AppConfig>(fileName);
        if (config != null)
          config.fileName = fileName;
      }
      catch (Exception ex)
      {
        Loger.SendMess(ex, "Ошибка загрузки конфигурации из файла " + fileName);
      }
    }
    //-------------------------------------------------------------------------
    /// <summary>Записывает параметры приложения в файл.
    /// </summary>
    public static void Save()
    {
      if (config != null)
        OptionsSerializer.Save(config.fileName, config);
    }
    //-------------------------------------------------------------------------
    string GetProp(string name)
    {
      Prop p = Props.FirstOrDefault(x => x.Name == name);
      return p != null ? p.Value : null;
    }
  }
  //===========================================================================
  /// <summary>Параметр приложения
  /// </summary>
  public class Prop
  {
    [XmlAttribute]
    public string Name { get; set; }
    public string Value { get; set; }
  }
}
