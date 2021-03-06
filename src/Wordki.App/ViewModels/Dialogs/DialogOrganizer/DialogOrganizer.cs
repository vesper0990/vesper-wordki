﻿using System.Windows;

namespace Wordki.ViewModels.Dialogs
{
    public class DialogOrganizer : IDialogOrganizer
    {
        public Window Dialog { get; set; }

        public void HideDialog()
        {
            if (Dialog != null)
            {
                Dialog.Close();
            }
        }

        public void ShowDialog(Window window)
        {
            if (Dialog != null && Dialog.IsVisible)
            {
                Dialog.Close();
            }
            Dialog = window;
            window.ShowDialog();
        }
    }
}
