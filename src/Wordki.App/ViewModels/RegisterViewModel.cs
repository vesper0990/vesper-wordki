﻿using Oazachaosu.Core.Common;
using System.Windows.Input;
using Util.Threads;
using Wordki.Helpers.Connector.Requests;
using Wordki.Helpers.Connector.Work;
using Wordki.InteractionProvider;
using Wordki.Models;
using WordkiModel;

namespace Wordki.ViewModels
{
    public class RegisterViewModel : LoginMainViewModel
    {

        #region Properties

        private string _repeatPassword;
        public string RepeatPassword
        {
            get { return _repeatPassword; }
            set
            {
                if (_repeatPassword != value)
                {
                    _repeatPassword = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand RegisterCommand { get; set; }

        #endregion

        #region Constructors

        public RegisterViewModel()
        {
            RegisterCommand = new Util.BuilderCommand(Register);
        }

        #endregion

        public override void InitViewModel(object parameter = null)
        {
            base.InitViewModel();
            RepeatPassword = "";
        }

        #region Commands

        private void Register(object obj)
        {
            if (!CheckPassword(Password))
            {
                return;
            }
            if (!CheckPasswordRepeat(Password, RepeatPassword))
            {
                return;
            }
            if (!CheckUserName(UserName))
            {
                return;
            }
            IUser user = new User
            {
                Name = UserName,
                Password = Password
            };
            ApiWork<UserDTO> registerRequest = new ApiWork<UserDTO>
            {
                Request = new PostUsersRequest(user),
                OnCompletedFunc = OnUserRequestCompete,
                OnFailedFunc = RegisterFailed
            };
            BackgroundQueueWithProgressDialog queue = new BackgroundQueueWithProgressDialog();
            ProcessProvider provider = new ProcessProvider()
            {
                ViewModel = new Dialogs.Progress.ProgressDialogViewModel()
                {
                    ButtonLabel = "Anuluj",
                    DialogTitle = "Rejestrowanie użytkownika w chmurze...",
                    CanCanceled = true,
                }
            };
            queue.Dialog = provider;
            queue.AddWork(registerRequest);
            queue.Execute();
        }

        private void RegisterFailed(ErrorDTO error)
        {
            ShowInfoDialog($"Wystąpił błąd serwera w trakcie wykonywania żądania: '{error.Message}'.\nKod błędu: '{error.Code}'");
        }

        protected override void ChangeState(object obj)
        {
            Switcher.Reset();
        }

        #endregion

        public override void Loaded()
        {
            
        }

        public override void Unloaded()
        {
            
        }

        private bool CheckPasswordRepeat(string password, string passwordRepeat)
        {
            if (string.IsNullOrEmpty(RepeatPassword))
            {
                ShowInfoDialog("Powtórz hasło!");
                return false;
            }
            if (!Password.Equals(RepeatPassword))
            {
                ShowInfoDialog("Hasła nie są identyczne!");
                return false;
            }
            return true;
        }
    }
}
