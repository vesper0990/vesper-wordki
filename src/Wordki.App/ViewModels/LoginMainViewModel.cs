﻿using System.Windows;
using Wordki.Helpers;
using Wordki.Helpers.Notification;
using System.Windows.Input;
using WordkiModel;
using System;
using Wordki.Database;
using Wordki.InteractionProvider;
using Wordki.ViewModels.Dialogs;
using Oazachaosu.Core.Common;
using Wordki.Helpers.AutoMapper;

namespace Wordki.ViewModels
{
    public abstract class LoginMainViewModel : ViewModelBase
    {

        #region Properties
        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand ExitCommand { get; set; }
        public ICommand ChangeStateCommand { get; set; }
        public ICommand LoadedWindowCommand { get; set; }

        #endregion

        #region Constructors

        protected LoginMainViewModel()
        {
            ExitCommand = new Util.BuilderCommand(Exit);
            //LoadedWindowCommand = new BuilderCommand(LoadedWindow);
            ChangeStateCommand = new Util.BuilderCommand(ChangeState);
        }

        #endregion

        public override void InitViewModel(object parameter = null)
        {
            UserName = "";
            Password = "";
        }

        public override void Back()
        {
        }

        #region Commands

        protected abstract void ChangeState(object obj);

        private void Exit(object obj)
        {
            Application.Current.Shutdown();
        }

        //private void LoadedWindow(object obj)
        //{
        //    User lUser = UserManager.GetInstance().FindLoginedUser();
        //    if (lUser == null)
        //    {
        //        return;
        //    }
        //    CommandQueue<ICommand> queue = new CommandQueue<ICommand> { CreateDialog = true };
        //    queue.MainQueue.AddFirst(new SimpleCommand(() =>
        //    {
        //        IDatabase database = Database.GetDatabase();
        //        UserManager.GetInstance().User = lUser;
        //        return database.LoadDatabase();
        //    }));
        //    queue.OnQueueComplete += success =>
        //    {
        //        if (success)
        //        {
        //            StartWithUser(lUser);
        //        }
        //    };
        //    queue.Execute();
        //}

        #endregion

        #region Methods


        protected void OnUserRequestCompete(UserDTO userDto)
        {
            IUser user = new UserHandler(AutoMapperConfig.Instance, DatabaseOrganizerSingleton.Instance).Handle(userDto);
            StartWithUser(user);
        }

        protected void StartWithUser(IUser user)
        {
            IUserManager userManager = UserManagerSingleton.Instence;
            userManager.Set(user);
            userManager.Update();
            DatabaseSingleton.Instance.LoadDatabase();
            Start();
        }

        protected void Start()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Switcher.Switch(Switcher.State.Menu);
            });
        }

        public override void Loaded()
        {
            UserName = "";
            Password = "";
        }

        protected void ShowInfoDialog(string message)
        {
            IInfoProvider infoProvider = new SimpleInfoProvider
            {
                ViewModel = new InfoDialogViewModel
                {
                    ButtonLabel = "Ok",
                    Message = message,
                }
            };
            infoProvider.Interact();
        }

        protected bool CheckPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                ShowInfoDialog("Hasło nie może być puste!");
                return false;
            }
            return true;
        }

        protected bool CheckUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                ShowInfoDialog("Nazwa użytkwonika nie może być pusta!");
                return false;
            }
            return true;
        }

        #endregion

    }
}
