﻿using WordkiModel;
using System;
using System.Collections.Generic;

namespace Wordki.Helpers
{
    public class RandomCreator : ILessonWordsCreator
    {

        public int Count { get; set; }
        public bool AllWords { get; set; }
        public IList<IGroup> Groups { get; set; }
        public IEnumerable<IWord> GetWords()
        {
            Random random = new Random();
            for (int i = 0; i < Count; i++)
            {
                IGroup group = Groups[random.Next(Groups.Count - 1)];
                yield return group.Words[random.Next(group.Words.Count - 1)];
            }
        }
    }
}
