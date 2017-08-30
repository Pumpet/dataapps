using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data.Linq;

namespace Common
{
  /// <summary>обработчик сообщений 
  /// </summary>
  public class Loger
  {
    /// <summary>Сформировать сообщение об ошибке
    /// </summary>
    /// <param name="e">исключение</param>
    /// <param name="mess">сообщение, предваряющее сообщение об исключении</param>
    public static void SendMess(Exception e, string mess = "")
    {
      // вариант со стандартным окном
      //mess = (!string.IsNullOrEmpty(mess) ? mess + "\n" : "") + (Debugger.IsAttached ? e.ToString() : e.Message);
      //SendMess(mess, true);

      mess = (!string.IsNullOrWhiteSpace(mess) ? mess + "\n" : "") + ExceptionMessage(e).Trim();
      FormErrMess f = new FormErrMess(mess, e.ToString());
      f.ShowDialog();
    }
    //-------------------------------------------------------------------------
    /// <summary>Сформировать сообщение
    /// </summary>
    /// <param name="mess">сообщение</param>
    /// <param name="err">признак ошибки</param>
    public static void SendMess(string mess, bool err = false)
    {
      MessageBox.Show(mess, "", MessageBoxButtons.OK, err ? MessageBoxIcon.Error : MessageBoxIcon.Information);
    }
    //-------------------------------------------------------------------------
    /// <summary>Особое описание для некоторых исключений
    /// </summary>
    /// <param name="e">Объект исключения</param>
    /// <returns>Описание исключения. По умолчанию - e.Message</returns>
    public static string ExceptionMessage(Exception e)
    {
      string mess = "";

      if (e is SqlException)
      {
        SqlException esql = (SqlException)e;
        foreach (SqlError err in esql.Errors)
        {
          switch (err.Number)
          {
            case 547:
              mess = mess + "Ошибка доступа к связанным данным\n";
              break;
            case 2601:
              mess = mess + "Попытка добавить повторно уникальные данные\n";
              break;
            case 515:
              mess = mess + "Невозможно сохранить пустое значение\n";
              break;
            default:
              if (!new[] { 3621 }.Contains(err.Number))
                mess = mess + string.Format("Ошибка работы с БД ({0}): {1}\n", err.Number, err.Message);
              break;
          }
        }
      }
      else if (e is ChangeConflictException)
        mess = "Данные не актуальны";
      else
        mess = e.Message;
      return mess;
    }
  }
}
