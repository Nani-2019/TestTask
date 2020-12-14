using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Statistics
{
    /// <summary>
    /// Для хранения
    /// </summary>
    public class LetterForLanguage
    {
        /// <summary>
        /// Язык
        /// </summary>
        public string Language;

        /// <summary>
        /// Список гласных букв
        /// </summary>
        public List<char> Consonants;
        /// <summary>
        /// Список согласных букв
        /// </summary>
        public List<char> Vowel;

        public LetterForLanguage()
        {
            Consonants = new List<char>();
            Vowel = new List<char>();
        }
    }
}
