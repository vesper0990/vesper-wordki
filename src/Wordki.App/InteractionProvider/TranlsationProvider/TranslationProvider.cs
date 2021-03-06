﻿using System.Collections.Generic;
using System.Linq;
using WordkiModel;

using Wordki.ViewModels.Dialogs;
using Wordki.Views.Dialogs;
using Wordki.Helpers.TranslationPusher;
using Oazachaosu.Core.Common;

namespace Wordki.InteractionProvider
{
    public class TranslationProvider : InteractionProviderBase, ITranslationProvider
    {
        public TranslationDirection TranslationDirection { get; set; }
        public IWord Word { get; set; }
        public IEnumerable<string> Items { get; set; }

        protected override void DispatcherWork()
        {
            IDialogOrganizer organizer = DialogOrganizerSingleton.Instance;
            TranslationListDialogViewModel viewModel = new TranslationListDialogViewModel(Items);
            TranslationListDialog dialog = new TranslationListDialog()
            {
                ViewModel = viewModel,
            };
            organizer.ShowDialog(dialog);
            if (viewModel.Canceled)
            {
                return;
            }
            IEnumerable<string> selectedItems = viewModel.Items.Where(x => x.Approved).Select(x => x.Translation);
            ITranslationPusher translationPusher = new TranslationPusher()
            {
                TranslationDirection = TranslationDirection,
                TranslationsList = selectedItems,
            };
            translationPusher.SetTranslation(Word);
        }
    }
}
