//
//  THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
//  PURPOSE.
//
//  License: GNU Lesser General Public License (LGPLv3)
//
//  Email: rozanov.alex.home@yandex.ru
//  Copyright (C) Alex Rozanov, 2017 
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Manager;
using Forms;
using GenDataAccess;
using GenForms;
using Common;


namespace Genetica
{
  static class Program
  {
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      try
      {
        AppConfig.Load();
        Form form = new FMain();
        FormManager.Io = new FormManager(form);
        Master master = new Master(new DataManager(), FormManager.Io);
        Application.Run(form);
      }
      catch(Exception e)
      {
        Loger.SendMess(e, "Ошибка запуска приложения!");
      }
    }
  }
}
