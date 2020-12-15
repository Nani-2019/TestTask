using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestTask.File;
using TestTask.Statistics;

namespace Test
{
    [TestClass]
    public class UnitTest1
    {
        FileForStatistics fileTxt;
        string[] pathFile = new string[1] { "C:\\Users\\nikit\\Source\\Repos\\TestTask\\Test\\TestFile\\XZ.txt" };
        public UnitTest1()
        {
            fileTxt = new FileForStatistics(pathFile);

            LetterForLanguage letterForLanguageConsonants = new LetterForLanguage();
            letterForLanguageConsonants.Consonants = new List<char>() { 'б', 'в', 'г', 'д', 'ж', 'з', 'й', 'к', 'л', 'м', 'н', 'п', 'р', 'с', 'т', 'ф', 'х', 'ц', 'ч', 'ш', 'щ' };
            letterForLanguageConsonants.Language = "RU";
            Consonants = new List<LetterForLanguage>();
            Consonants.Add(letterForLanguageConsonants);

            LetterForLanguage letterForLanguageVowel = new LetterForLanguage();
            letterForLanguageVowel.Vowel = new List<char>() { 'а', 'о', 'и', 'е', 'ё', 'э', 'ы', 'у', 'ю', 'я' };
            letterForLanguageVowel.Language = "RU";
            Vowel = new List<LetterForLanguage>();
            Vowel.Add(letterForLanguageVowel);
        }

        [TestMethod]
        public void TestMethodСaseSensitiveSearch()
        {
            fileTxt.ReadFiles(0);
            Assert.AreEqual(-1, fileTxt.Statistics.StatsByLetter.FindIndex(x => x.Letter.Equals('3')));
            Assert.AreEqual(78, fileTxt.Statistics.StatsByLetter.Find(x => x.Letter.Equals('а')).Count);
            Assert.AreEqual(7, fileTxt.Statistics.StatsByLetter.Find(x => x.Letter.Equals('А')).Count);
            Assert.AreEqual(66, fileTxt.Statistics.StatsByLetter.Find(x => x.Letter.Equals('н')).Count);
            Assert.AreEqual(3, fileTxt.Statistics.StatsByLetter.Find(x => x.Letter.Equals('Н')).Count);
            Assert.AreEqual(23, fileTxt.Statistics.StatsByLetter.Find(x => x.Letter.Equals('я')).Count);
            Assert.AreEqual(-1, fileTxt.Statistics.StatsByLetter.FindIndex(x => x.Letter.Equals('Я')));
            Assert.AreEqual(-1, fileTxt.Statistics.StatsByLetter.FindIndex(x => x.Letter.Equals('.')));
        }

        [TestMethod]
        public void TestMethodNoСaseSensitiveSearch()
        {
            fileTxt.ReadFiles(1);
            Assert.AreEqual(-1, fileTxt.Statistics.StatsByLetter.FindIndex(x => x.Letter.Equals('3')));
            Assert.AreEqual(85, fileTxt.Statistics.StatsByLetter.Find(x => x.Letter.Equals('А')).Count);
            Assert.AreEqual(69, fileTxt.Statistics.StatsByLetter.Find(x => x.Letter.Equals('Н')).Count);
            Assert.AreEqual(23, fileTxt.Statistics.StatsByLetter.Find(x => x.Letter.Equals('Я')).Count);
            Assert.AreEqual(-1, fileTxt.Statistics.StatsByLetter.FindIndex(x => x.Letter.Equals('.')));
        }

        List<LetterForLanguage> Vowel;
        [TestMethod]
        public void TestMethodDeleteLetterVowelSensitive()
        {
            fileTxt.ReadFiles(0);
            fileTxt.Statistics.DeleteLetter(Vowel[0].Vowel);
            Assert.AreEqual(-1, fileTxt.Statistics.StatsByLetter.FindIndex(x => x.Letter.Equals('3')));
            Assert.AreEqual(-1, fileTxt.Statistics.StatsByLetter.FindIndex(x => x.Letter.Equals('А')));
            Assert.AreEqual(-1, fileTxt.Statistics.StatsByLetter.FindIndex(x => x.Letter.Equals('а')));
            Assert.AreEqual(3, fileTxt.Statistics.StatsByLetter.Find(x => x.Letter.Equals('Н')).Count);
            Assert.AreEqual(66, fileTxt.Statistics.StatsByLetter.Find(x => x.Letter.Equals('н')).Count);
            Assert.AreEqual(-1, fileTxt.Statistics.StatsByLetter.FindIndex(x => x.Letter.Equals('Я')));
            Assert.AreEqual(-1, fileTxt.Statistics.StatsByLetter.FindIndex(x => x.Letter.Equals('.')));
        }

        [TestMethod]
        public void TestMethodDeleteLetterVowelNoSensitive()
        {
            fileTxt.ReadFiles(1);
            fileTxt.Statistics.DeleteLetter(Vowel[0].Vowel);
            Assert.AreEqual(-1, fileTxt.Statistics.StatsByLetter.FindIndex(x => x.Letter.Equals('3')));
            Assert.AreEqual(-1, fileTxt.Statistics.StatsByLetter.FindIndex(x => x.Letter.Equals('А')));
            Assert.AreEqual(-1, fileTxt.Statistics.StatsByLetter.FindIndex(x => x.Letter.Equals('а')));
            Assert.AreEqual(69, fileTxt.Statistics.StatsByLetter.Find(x => x.Letter.Equals('Н')).Count);
            Assert.AreEqual(-1, fileTxt.Statistics.StatsByLetter.FindIndex(x => x.Letter.Equals('Я')));
            Assert.AreEqual(-1, fileTxt.Statistics.StatsByLetter.FindIndex(x => x.Letter.Equals('.')));
        }

        List<LetterForLanguage> Consonants;
        [TestMethod]
        public void TestMethodDeleteLetterConsonantsSensitive()
        {
            fileTxt.ReadFiles(0);
            fileTxt.Statistics.DeleteLetter(Consonants[0].Consonants);
            Assert.AreEqual(-1, fileTxt.Statistics.StatsByLetter.FindIndex(x => x.Letter.Equals('3')));
            Assert.AreEqual(-1, fileTxt.Statistics.StatsByLetter.FindIndex(x => x.Letter.Equals('Н')));
            Assert.AreEqual(-1, fileTxt.Statistics.StatsByLetter.FindIndex(x => x.Letter.Equals('н')));
            Assert.AreEqual(78, fileTxt.Statistics.StatsByLetter.Find(x => x.Letter.Equals('а')).Count);
            Assert.AreEqual(7, fileTxt.Statistics.StatsByLetter.Find(x => x.Letter.Equals('А')).Count);
            Assert.AreEqual(23, fileTxt.Statistics.StatsByLetter.Find(x => x.Letter.Equals('я')).Count);
            Assert.AreEqual(-1, fileTxt.Statistics.StatsByLetter.FindIndex(x => x.Letter.Equals('.')));
        }

        [TestMethod]
        public void TestMethodDeleteLetterConsonantsNoSensitive()
        {
            fileTxt.ReadFiles(1);
            fileTxt.Statistics.DeleteLetter(Consonants[0].Consonants);
            Assert.AreEqual(-1, fileTxt.Statistics.StatsByLetter.FindIndex(x => x.Letter.Equals('3')));
            Assert.AreEqual(-1, fileTxt.Statistics.StatsByLetter.FindIndex(x => x.Letter.Equals('Н')));
            Assert.AreEqual(-1, fileTxt.Statistics.StatsByLetter.FindIndex(x => x.Letter.Equals('н')));
            Assert.AreEqual(85, fileTxt.Statistics.StatsByLetter.Find(x => x.Letter.Equals('А')).Count);
            Assert.AreEqual(23, fileTxt.Statistics.StatsByLetter.Find(x => x.Letter.Equals('Я')).Count);
            Assert.AreEqual(-1, fileTxt.Statistics.StatsByLetter.FindIndex(x => x.Letter.Equals('.')));
        }
    }
}
