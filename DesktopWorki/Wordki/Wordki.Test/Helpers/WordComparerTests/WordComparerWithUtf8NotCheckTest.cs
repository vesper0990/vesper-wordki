﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wordki.Models.Lesson.WordComparer;

namespace Wordki.Test.Helpers.WordComparerTests
{
    [TestClass]
    public class WordComparerWithUtf8NotCheckTest
    {

        INotCheck notCheck;
        IWordComparer comparer;

        [TestInitialize]
        public void Init()
        {
            notCheck = new Utf8NotCheck();
            comparer = new WordComparer();
        }

        [TestMethod]
        public void Compare_Not_Same_Word_Without_Utf8_Not_Check_Test()
        {
            string word1 = "teśt";
            string word2 = "test";
            Assert.IsFalse(comparer.Compare(word1, word2));
        }

        [TestMethod]
        public void Compare_Same_Word_With_utf8_Not_Check_Test()
        {
            string word1 = "test";
            string word2 = "test";
            comparer.NotCheckers.Add(notCheck);
            Assert.IsTrue(comparer.Compare(word1, word2));
        }

        [TestMethod]
        public void Compare_Not_Same_Word_With_Letter_Not_Check_Test()
        {
            string word1 = "teśt";
            string word2 = "tęst";
            comparer.NotCheckers.Add(notCheck);
            Assert.IsTrue(comparer.Compare(word1, word2));
        }

        [TestMethod]
        public void Compare_Wrong_Word_With_Letter_Not_Check_Test()
        {
            string word1 = "test2";
            string word2 = "Test";
            comparer.NotCheckers.Add(notCheck);
            Assert.IsFalse(comparer.Compare(word1, word2));
        }


    }
}