﻿using Oazachaosu.Core.Common;
using System.Windows.Media.Imaging;

namespace WordkiModel
{
    public interface ILanguage
    {

        LanguageType Type { get; }
        string Code { get; }
        string Description { get; }
        string Name { get; }
        string ShortName { get; }
        BitmapImage Flag { get; }

    }
}