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
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;
using Common;

namespace Forms
{
  /// <summary>Флаги поведения формы
  /// 1: вернет результат (сохранение из редактора или выбор из списка)
  /// 2: новый объект (для редактора)
  /// 3: будет модальное окно
  /// 4: возможен повторный вызов из меню
  /// </summary>
  [Flags]
  public enum FormModes { Default = 0, GetResult = 1, NewEntity = 2, Modal = 4, NotSingle = 8 }

  /// <summary>Менеджер форм приложения. Живет в FormManager.Io 
  /// </summary>
  public class FormManager : IViewDataManager
  {
    /// <summary>Экземпляр для приложения 
    /// </summary>
    public static FormManager Io { get; set; }
    Form main;
    public Form MainForm { get { return main; } }
    Dictionary<string, Form> forms = new Dictionary<string, Form>();
    // IViewDataManager -----------------------------------------------------------
    /// <summary>Получить коллекцию контроллеров данных по имени типа формы
    /// Сюда цепляем метод менеджера данных
    /// </summary>
    public event Func<string, DataControllers> OnGetControllers;
    /// <summary>Проверить подключение
    /// </summary>
    public event Func<bool> OnCheckConnection;
    // ------------------------------------------------------------------------
    /// <summary>Конструктор менеджера
    /// </summary>
    /// <param name="m">главная форма приложения</param>
    public FormManager(Form m)
    {
      main = m;
      if (AppConfig.Prop("AppMode") == "DEBUG" && main != null)
      {
        main.Text = main.Name;
        CommonLib.ForControls(main, (c) =>
        {
          foreach (ToolStripItem b in ((ToolStrip)c).Items)
          {
            b.Image = Properties.Resources.tmp;
            b.DisplayStyle = ToolStripItemDisplayStyle.Image;
            b.ToolTipText = b.Text;
            if (b is ToolStripDropDownButton)
              foreach (ToolStripItem dd in ((ToolStripDropDownButton)b).DropDownItems)
              {
                dd.Image = Properties.Resources.tmp;
                dd.DisplayStyle = ToolStripItemDisplayStyle.Image;
                dd.ToolTipText = dd.Text;
              }
          }
        }, typeof(ToolStrip));
      }
    }
    //-------------------------------------------------------------------------
    /// <summary>Проверить подключение
    /// </summary>
    /// <param name="formWillClose">закрыть эту форму в случае ошибки</param>
    public void CheckConnection(Form formWillClose)
    {
      if (OnCheckConnection != null)
      {
        Form splash = new FormSplash();
        splash.Show();
        splash.Refresh();
        bool ok = OnCheckConnection();
        splash.Close();
        if (!ok && formWillClose != null)
          formWillClose.Close();
      }
    }
    //-------------------------------------------------------------------------
    /// <summary>Создать форму по имени типа из сборки, в которой находится главная форма.
    /// Отработает конструктор без параметров
    /// </summary>
    /// <param name="typeName">имя типа формы</param>
    /// <returns>объект Form</returns>
    public Form GetForm(string typeName)
    {
      Form form = null;
      try
      {
        if (string.IsNullOrEmpty((typeName ?? "").Trim()))
          throw new Exception("Не указано имя формы!");

        if (main == null) throw new Exception("Не задана главная форма!");
        typeName = main.GetType().Namespace + "." + typeName;
        Type t = Assembly.GetAssembly(main.GetType()).GetType(typeName, false, true);

        if (!typeof(Form).IsAssignableFrom(t)) throw new Exception("Не найден тип " + typeName);
        form = (Form)t.GetConstructor(Type.EmptyTypes).Invoke(null);
        if (!(form is Form)) throw new Exception("Невозможно создать форму типа " + typeName);
      }
      catch (Exception e)
      {
        Loger.SendMess(e);
      }
      return form;
    }
    //-------------------------------------------------------------------------
    /// <summary>Создать форму известного типа
    /// Отработает конструктор без параметров
    /// </summary>
    /// <typeparam name="T">тип от Form</typeparam>
    /// <returns>объект указанного типа</returns>
    public T GetForm<T>() where T : Form
    {
      T form = null;
      try
      {
        form = (T)typeof(T).GetConstructor(Type.EmptyTypes).Invoke(null);
        if (!(form is T)) throw new Exception("Невозможно создать форму типа " + typeof(T).ToString());
      }
      catch (Exception e)
      {
        Loger.SendMess(e);      
      }
      return form;
    }
    //-------------------------------------------------------------------------
    /// <summary>Создание и запуск формы по имени ее типа
    /// </summary>
    /// <param name="typeName">имя типа из сборки, в которой находится главная форма</param>
    /// <param name="parent">форма-родитель</param>
    /// <param name="modes">флаги поведения</param>
    /// <param name="callback">делегат вызывающей формы</param>
    /// <param name="key">ключ объекта на вход</param>
    /// <param name="filter">ключи фильтра на вход</param>
    /// <returns>true - если форма была создана как модальная и закрылась с результатом</returns>
    public bool ExecForm(string typeName, Form parent, FormModes modes = FormModes.Default, Action<object> callback = null,
      object key = null, object filter = null, bool reset = false)
    {
      return ExecForm(GetForm(typeName), parent, modes, callback, key, filter, reset);
    }
    //-------------------------------------------------------------------------
    /// <summary>Запуск созданной формы</summary>
    /// <param name="form">объект формы</param> 
    /// <param name="parent">форма-родитель</param>
    /// <param name="modes">флаги поведения</param>
    /// <param name="callback">делегат вызывающей формы</param>
    /// <param name="key">ключ объекта на вход</param>
    /// <param name="filter">ключи фильтра на вход</param>
    /// <returns>true - если форма была создана как модальная и закрылась с результатом</returns>
    public bool ExecForm(Form form, Form parent, FormModes modes = FormModes.Default, Action<object> callback = null,
      object key = null, object filter = null, bool reset = false)
    {
      bool single = !modes.HasFlag(FormModes.NotSingle), modal = modes.HasFlag(FormModes.Modal);

      if (form == null)
      {
        Loger.SendMess("Невозможно запустить форму!", true);
        return false;
      }
      if (form.Visible)
        return false;

      // проверка уже запущенных
      string t = (parent != null ? parent.GetType().Name + "." : "") + form.GetType().Name;
      if (single && forms.ContainsKey(t) && forms[t] != null && !forms[t].IsDisposed)
      {
        if (forms[t].WindowState == FormWindowState.Minimized)
          forms[t].WindowState = FormWindowState.Normal;
        
        // обновление списка с установкой тек.записи и переустановка фильтров
        if (reset && forms[t] is FormList)
        {
          FormList formList = (FormList)forms[t];
          formList.SetExternalFilter(filter);
          formList.LoadData(formList.MainList, key);
        }

        forms[t].Activate();
        form.Dispose();
        return false;
      }

      bool res = false;

      Action<bool> wait = (b) =>
      {
        if (parent != null)
          parent.Cursor = b ? Cursors.WaitCursor : Cursors.Default;
        else if (main != null)
          main.Cursor = b ? Cursors.WaitCursor : Cursors.Default;
        form.Cursor = b ? Cursors.WaitCursor : Cursors.Default;
      };
      wait(true);
      try
      {
        // инициализация
        if (form is FormBase)
        {
          DataControllers dc = OnGetControllers != null ? OnGetControllers(form.GetType().Name) : new DataControllers();
          ((FormBase)form).Init(dc, modes, callback, key, filter);
        }

        if (form == null || form.IsDisposed)
          return false;

        if (single)
          forms.Add(t, form);
        form.Load += form_Load;
        form.FormClosed += form_FormClosed; // действия по закрытию 

        // запуск
        if (modal)
        {
          form.StartPosition = FormStartPosition.CenterParent;
          res = (form.ShowDialog(parent) == (DialogResult.OK | DialogResult.Yes));
        }
        else
        {
          //!! if (parent == main && !Debugger.IsAttached) parent = null;
          form.StartPosition = FormStartPosition.Manual;
          if (parent == null) form.Show(); else form.Show(parent);
          if (main != null && form.Top < main.Bottom)
            form.Top = main.Bottom;
        }
      }
      catch (Exception e)
      {
        if (form != null)
          form.Dispose();
        if (forms.ContainsKey(t))
          forms.Remove(t);
        Loger.SendMess(e);
      }
      finally
      {
        wait(false);
      }

      return res;
    }
    //-------------------------------------------------------------------------
    void form_Load(object sender, EventArgs e)
    {
      FormOptions.Load((Form)sender);
    }
    //-------------------------------------------------------------------------
    void form_FormClosed(object sender, FormClosedEventArgs e) // действия по закрытию формы
    {
      if (sender is Form)
      {
        Form f = (Form)sender;
        string t = (f.Owner != null ? f.Owner.GetType().Name + "." : "") + f.GetType().Name;
        if (forms.ContainsKey(t))
          forms.Remove(t);
        FormOptions.Save(f);
      }
    }
    //-------------------------------------------------------------------------
    /// <summary>найти главную форму приложения
    /// </summary>
    /// <param name="form">активная форма</param>
    public void ShowMain(Form form) //  
    {
      if (main != null)
      {
        if (form.WindowState == FormWindowState.Maximized)
          form.WindowState = FormWindowState.Normal;
        if (main.WindowState == FormWindowState.Minimized)
          main.WindowState = FormWindowState.Normal;
        main.Activate();
        CommonLib.GetControls<ToolStrip>(main).ForEach(x => { if (x.Items.Count > 0) x.Items[0].Select(); });
      }
    }
    //-------------------------------------------------------------------------
    /// <summary>развернуть с учетом главной формы приложения
    /// </summary>
    /// <param name="form">форма</param>
    public void FormDefaultPos(Form form, bool max) //  
    {
      if (main != null && main.WindowState == FormWindowState.Maximized)
      {
        if (form.WindowState == FormWindowState.Maximized)
          form.WindowState = FormWindowState.Normal;
        form.Left = 0;
        form.Top = main.Bottom;
        if (max)
        {
          form.Width = main.Right;
          form.Height = Screen.GetWorkingArea(main).Height - main.Bottom;
        }
      }
    }
  }
}
