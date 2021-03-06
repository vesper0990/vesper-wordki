﻿using NUnit.Framework;

using System.Linq;
using System.Collections.Generic;
using Wordki.Models.Lesson;
using Wordki.Helpers.WordComparer;
using Util.Serializers;
using WordkiModel;
using Oazachaosu.Core.Common;

namespace Wordki.Test.Lessons
{
    [TestFixture]
    public class LessonSerializeTests
    {
        ISerializer<Lesson> serializer;
        Lesson lesson;
        IEnumerable<IWord> words;
        

        [SetUp]
        public void SetUp()
        {
            serializer = new BinarySerializer<Lesson>()
            {
                Settings = new BinarySerializerSettings
                {
                    Append = false,
                    RemoveAfterRead = false,
                    Path = "C:/test.dat",
                }
            };
            words = Utility.GetGroup().Words;
            lesson = new TypingLesson();
            lesson.WordComparer = new WordComparer();
            lesson.WordComparer.Settings = new WordComparerSettings();
            lesson.WordComparer.Settings.NotCheckers.Add(new LetterCaseNotCheck());
            lesson.WordComparer.Settings.NotCheckers.Add(new SpaceNotCheck());
            lesson.WordComparer.Settings.NotCheckers.Add(new Utf8NotCheck());
            ILessonSettings lessonSettings = new LessonSettings()
            {
                AllWords = true,
                TranslationDirection = TranslationDirection.FromFirst,
                Timeout = 0,
            };
            lesson.LessonSettings = lessonSettings;
            lesson.InitLesson(words);
        }

        [Test]
        public void Try_to_serialize_typing_lesson_to_binnary_file_test()
        {
            Assert.IsTrue(serializer.Write(lesson), "Error in write lesson object");
            Lesson readedLesson = serializer.Read();
            CheckLessons(lesson, readedLesson);
        }

        [Test]
        public void Try_to_serialize_typing_lesson_to_binary_file_after_start_lesson_test()
        {
            lesson.NextWord();
            lesson.Check("");
            lesson.Known();
            lesson.Check("");
            lesson.Unknown();
            lesson.Check("");
            lesson.Known();
            lesson.Check("");
            lesson.Unknown();
            Assert.IsTrue(serializer.Write(lesson), "Error in write lesson object");
            Lesson readedLesson = serializer.Read();
            CheckLessons(lesson, readedLesson);
        }

        private void CheckLessons(Lesson expected, Lesson actual)
        {
            Assert.AreEqual(expected.BeginWordsList.Count, actual.BeginWordsList.Count, "BeginWordsCount not equal");
            for (int i = 0; i < expected.BeginWordsList.Count; i++)
            {
                Assert.AreEqual(expected.BeginWordsList[i], actual.BeginWordsList[i]);
            }
            Assert.AreEqual(expected.Counter, actual.Counter, "Counter not equal");
            Assert.AreEqual(expected.CurrentDrawer, actual.CurrentDrawer, "CurrentDrawer not equal");
            Assert.AreEqual(expected.LessonSettings, actual.LessonSettings, "LessonSettings not equal");
            Assert.AreEqual(expected.SelectedWord, actual.SelectedWord, "SelectedWord not equal");

            Assert.AreEqual(expected.WordQueue.Count, actual.WordQueue.Count, "WordList not equal");
            for (int i = 0; i < expected.WordQueue.Count; i++)
            {
                Assert.AreEqual(expected.WordQueue.ToArray()[i], actual.WordQueue.ToArray()[i]);
            }

            Assert.AreEqual(expected.ResultList.Count, actual.ResultList.Count, "ResultList not equal");
            for (int i = 0; i < expected.ResultList.Count; i++)
            {
                Assert.AreEqual(expected.ResultList[i], actual.ResultList[i]);
            }

            Assert.AreEqual(expected.WordComparer.Settings.NotCheckers.Count, actual.WordComparer.Settings.NotCheckers.Count, "WordComparer not equal");
            for (int i = 0; i < expected.ResultList.Count; i++)
            {
                Assert.AreSame(expected.WordComparer.Settings.NotCheckers.ElementAt(i).GetType(), actual.WordComparer.Settings.NotCheckers.ElementAt(i).GetType(), "Notchecker not equal");
            }
        }

    }
}
