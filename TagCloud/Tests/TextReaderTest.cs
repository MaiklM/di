﻿using System.IO;
using NUnit.Framework;

namespace TagCloud.Tests
{
    [TestFixture]
    class TextReaderTest
    {
        private TextReader reader;
        private const string path = "./fileTest.txt";

        [SetUp]
        public void SetUp() => reader = new TextReader();

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("as dsa")]
        [TestCase("hjkl\nasdf\n\nsadf asdf fda")]
        public void WriteTextAndRead_ShouldBeEquals(string text)
        {
            File.WriteAllText(path, text);
            var result = reader.ReadText(path);
            Assert.AreEqual(text, result);
            File.Delete(path);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("as dsa")]
        [TestCase("hjkl\nasdf\n\nsadf asdf fda")]
        public void ReadTextFromFileDoesntWxist_ShouldBeNull(string text)
        {
            var result = reader.ReadText(path);
            Assert.IsNull(result);
        }
    }
}
