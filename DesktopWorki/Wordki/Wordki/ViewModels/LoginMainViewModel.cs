﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Controls.Notification;
using Newtonsoft.Json;
using Wordki.Helpers;
using Wordki.Models;
using Wordki.Models.Connector;

namespace Wordki.ViewModels {
  public abstract class LoginMainViewModel : INotifyPropertyChanged, IViewModel {

    #region INotifyPropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;

    public void OnPropertyChanged([CallerMemberName] string property = "") {
      if (PropertyChanged != null)
        PropertyChanged(this, new PropertyChangedEventArgs(property));
    }
    #endregion


    #region Properties
    private string _userName;
    public string UserName {
      get { return _userName; }
      set {
        if (_userName != value) {
          _userName = value;
          OnPropertyChanged();
        }
      }
    }

    private string _password;
    public string Password {
      get { return _password; }
      set {
        if (_password != value) {
          _password = value;
          OnPropertyChanged();
        }
      }
    }

    public BuilderCommand ExitCommand { get; set; }
    public BuilderCommand ChangeStateCommand { get; set; }
    public BuilderCommand LoadedWindowCommand { get; set; }

    #endregion

    #region Constructors

    protected LoginMainViewModel() {
      ExitCommand = new BuilderCommand(Exit);
      LoadedWindowCommand = new BuilderCommand(LoadedWindow);
      ChangeStateCommand = new BuilderCommand(ChangeState);
    }

    #endregion

    public virtual void InitViewModel() {
      UserName = "";
      Password = "";
    }

    public void Back() {
    }

    #region Commands

    protected abstract void ChangeState(object obj);

    private void Exit(object obj) {
      Application.Current.Shutdown();
    }

    private void LoadedWindow(object obj) {
      User lUser = UserManager.FindLoginedUser();
      if (lUser == null) {
        return;
      }
      CommandQueue<ICommand> queue = new CommandQueue<ICommand> { CreateDialog = true };
      queue.MainQueue.AddFirst(new SimpleCommand(() => {
        Database database = Database.GetDatabase();
        database.User = lUser;
        return database.LoadDatabase();
      }));
      queue.OnQueueComplete += success => {
        if (success) {
          StartWithUser(lUser);
        }
      };
      queue.Execute();
    }

    #endregion

    #region Methods

    protected void StartWithUser(User pUser) {
      UserManager.LoginUser(pUser);
      Database.GetDatabase().User = pUser;
      Database.GetDatabase().UpdateUser(pUser);
      Start();
    }

    protected void HandleResponse(ApiResponse response) {
      if (response.IsError) {
        return;
      }
      User lUser = JsonConvert.DeserializeObject<User>(response.Message);
      lUser.IsLogin = true;
      lUser.IsRegister = true;
      Database database = Database.GetDatabase();
      User dbUser = database.GetUser(lUser.UserId);
      if (dbUser == null) {
        database.AddUser(lUser);
      }
      database.User = lUser;
      database.LoadDatabase();
      StartWithUser(lUser);
    }

    private void Start() {
      Logger.LogInfo("Loguje użytkownika: {0}", Database.GetDatabase().User.GetStringFromObject());
      Application.Current.Dispatcher.Invoke(() => {
        Toaster.NewToast(new ToastProperties { Message = "Zalogowoano" });
        Switcher.GetSwitcher().Switch(Switcher.State.Menu);
      });
    }

    #endregion

  }
}
