﻿namespace Wordki.ViewModels.Dialogs
{
    public class InfoDialogViewModel : DialogViewModelBase
    {

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                if(_message == value)
                {
                    return;
                }
                _message = value;
                OnPropertyChanged();
            }
        }


        private string _buttonLabel;
        public string ButtonLabel
        {
            get { return _buttonLabel; }
            set
            {
                if (_buttonLabel == value)
                {
                    return;
                }
                _buttonLabel = value;
                OnPropertyChanged();
            }
        }

        public override void InitViewModel(object parameter = null)
        {
            base.InitViewModel();
        }
    }
}
