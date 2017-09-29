﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordki.Models.Lesson.WordComparer
{
    public class SpaceNotCheck : INotCheck
    {
        public string Convert(string text)
        {
            return text.Replace(" ", "");
        }
    }
}