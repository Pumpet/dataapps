using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
  /// <summary>Интерфейс контроллера данных - как правило разрабатывается под объекты(сущности) определенного типа, с учетом связанных объектов
  /// </summary>
  public interface IDataController
  {
    /// <summary>имя контроллера</summary>
    string Name { get; set; }
    /// <summary>словарь объектов данных (имя-объект)</summary>
    Dictionary<string, object> DataBinds { get; set; }
    /// <summary>словарь функций (имя-делегат) для возврата объекта по ключу</summary>
    Dictionary<string, Func<object, object>> GetParentFuncs { get; set; }

    /// <summary>заполнить словарь объектов данных и словарь функций (ключ, ключи фильтра)</summary>
    Action<object, object> OnGetDataBinds { get; set; }
    /// <summary>получить коллекцию данных (ключ, ключи фильтра)</summary>
    Func<object, object, object> OnGetList { get; set; }
    /// <summary>заполнить в словаре объект для редактирования (ключ, признак нового, ключ внешнего(родительского) объекта)</summary>
    Action<object, bool, object> OnGetEditData { get; set; }
    /// <summary>установка умолчаний (объект, ключ внешнего(родительского) объекта)</summary>
    Action<object, object> OnSetDefaults { get; set; }
    /// <summary>удаление объектов (массив ключей)</summary>
    Action<object[]> OnDelete { get; set; }
    /// <summary>сохранение объекта (объект, признак нового) - вернуть true если нет ошибок</summary>
    Func<object, bool, bool> OnSave { get; set; }
    /// <summary>передача данных из одного объекта в другой (объект-источник, объект-приемник)</summary>
    Action<object, object> OnCloneEntity { get; set; }
    /// <summary>проверка данных объекта (объект) - вернуть словарь поле-ошибка</summary>
    Func<object, Dictionary<string, string>> OnCheck { get; set; }
    /// <summary>обработать команду (код команды, ключ, ключи фильтра, обрабатываемый объект) - вернуть объект результата</summary>
    Func<string, object, object, object, object[], object> OnExecCommand { get; set; }
    /// <summary>установить набор команд из интерфейса (набор команд (слоарь код-действие), ключ, обрабатываемый объект, массив ключей выделенных записей)  /// </summary>
    Action<object, object, object, object[], string> OnSetCommands { get; set; }
  }
  //---------------------------------------------------------------------------
  /// <summary>Интерфейс менеджера данных - связывает модель и представление 
  /// </summary>
  public interface IDataManager
  {
    /// <summary>Создать и вернуть контроллеры данных для указанного представления 
    /// </summary>
    /// <param name="viewType">имя представления</param>
    /// <returns>коллекция контроллеров данных</returns>
    List<IDataController> GetDataObjects(string viewType);
    /// <summary>Проверить подключение</summary>
    /// <returns>Успешно или нет</returns>
    bool CheckConnection();
  }
  //---------------------------------------------------------------------------
  /// <summary>Интерфейс менеджера представления данных - обмен данными с представлением  
  /// </summary>
  public interface IViewDataManager
  {
    /// <summary>Запрос данных для представления - по имени View(например формы) получаем контейнер контроллеров данных
    /// </summary>
    event Func<string, DataControllers> OnGetControllers;
    /// <summary>Проверить подключение</summary>
    event Func<bool> OnCheckConnection;
  }
}
