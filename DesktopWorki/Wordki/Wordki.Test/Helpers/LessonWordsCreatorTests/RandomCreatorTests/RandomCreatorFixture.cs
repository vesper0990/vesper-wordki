﻿using NUnit.Framework;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wordki.Helpers;

namespace Wordki.Test.Helpers.LessonWordsCreatorTests.RandomCreatorTests
{
    [TestFixture(30)]
    [TestFixture(10)]
    public class RandomCreatorFixture
    {

        ILessonWordsCreator creator;
        int wordToChoose;

        public RandomCreatorFixture(int wordToChoose)
        {
            this.wordToChoose = wordToChoose;
        }

        [SetUp]
        public void SetUp()
        {
            creator = new RandomCreator();
            creator.AllWords = true;
            creator.Count = wordToChoose;
            creator.Groups = Utility.GetGroups(2);
        }

        [Test]
        public void Get_words_test()
        {
            IList<IWord> words = creator.GetWords().ToList();
            Assert.AreEqual(wordToChoose, words.Count);
        }

    }
}