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
using System.Linq;
using Common;

namespace Manager
{
  /// <summary>Контроллер приложения
  /// </summary>
  public class Master
  {
    public static IViewDataManager vm;
    public IDataManager dm;
    public Master(IDataManager dataManager, IViewDataManager viewManager)
    {
      dm = dataManager;
      vm = viewManager;
      if (vm != null)
      {
        vm.OnGetControllers += GetControllers; // подписка на запрос контроллеров данных 
        vm.OnCheckConnection += dm.CheckConnection;
      }
    }
    //-------------------------------------------------------------------------
    /// <summary>Вернуть контейнер с контроллерами данных для представления
    /// </summary>
    /// <param name="viewType">тип представления</param>
    /// <returns>контейнер контроллеров данных</returns>
    DataControllers GetControllers(string viewType)
    {
      DataControllers dc = new DataControllers();
      dc.Items.AddRange(dm.GetDataObjects(viewType)); 
      return dc;
    }
  }
}
