﻿using WordkiModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Wordki.Helpers.Converters
{
    public class GroupsToResultsCountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            IEnumerable<IGroup> groups = value as IEnumerable<IGroup>;
            if(groups == null)
            {
                return 0;
            }
            return groups.Sum(x => x.Results.Count);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
