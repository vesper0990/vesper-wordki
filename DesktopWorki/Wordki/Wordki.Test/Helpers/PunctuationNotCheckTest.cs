﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wordki.Models.Lesson.WordComparer;

namespace Wordki.Test.Helpers
{

    [TestClass]
    public class PunctuationNotCheckTest
    {
        [TestMethod]
        public void AnyPunctuationTextTest()
        {
            string textBefore = "so metextasdf";
            string textAfter = "so metextasdf";
            INotCheck notChecker = new PunctuationNotCheck();
            string textConverted = notChecker.Convert(new StringBuilder(textBefore)).ToString();
            Assert.IsTrue(textAfter.Equals(textConverted));
        }

        [TestMethod]
        public void SomePunctuationInTextTest()
        {
            string textBefore = "s ome12te#xt.asdf^%$";
            string textAfter = "s ometextasdf";
            INotCheck notChecker = new PunctuationNotCheck();
            string textConverted = notChecker.Convert(new StringBuilder(textBefore)).ToString();
            Assert.IsTrue(textAfter.Equals(textConverted));
        }


        [TestMethod]
        public void OnlyPunctuationInTextTest()
        {
            string textBefore = "(&*@(*&^$#>}>";
            string textAfter = "";
            INotCheck notChecker = new PunctuationNotCheck();
            string textConverted = notChecker.Convert(new StringBuilder(textBefore)).ToString();
            Assert.IsTrue(textAfter.Equals(textConverted));
        }
    }
}
