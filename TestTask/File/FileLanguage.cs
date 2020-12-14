using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Statistics;

namespace TestTask.File
{
    /// <summary>
    /// Считывает файлы с языками
    /// </summary>
    public class FileLanguage
    {
        public LetterForLanguage LetterForLanguage;
        /// <summary>
        /// Названия файлов
        /// </summary>
        public string[] NameFiles;
        /// <summary>
        /// Путь к папке где лежат файлы с буквами языков
        /// </summary>
        string Path;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameFiles">Путь к папке где лежат файлы с буквами языков</param>
        public FileLanguage(string[] nameFiles)
        {
            LetterForLanguage = new LetterForLanguage();
            Path = Directory.GetCurrentDirectory() + "\\Language";
            NameFiles = nameFiles;
        }

        /// <summary>
        /// Считывание букв из файлов
        /// </summary>
        public void ReadFile()
        {
            StringBuilder str = new StringBuilder();
            int NumberLine = 1;
            if (NameFiles[0] == "No")
            {
                return;
            }
            for (int i = 0; i < NameFiles.Length; i++)
            {
                using (var f = new StreamReader(Path + "\\" + NameFiles[i] + ".txt", Encoding.GetEncoding(1251)))
                {
                    while (str.Append(f.ReadLine()) != null)
                    {
                        if (NumberLine == 1)
                        {
                            LetterForLanguage.Consonants.AddRange(str.ToString().Split(',').ToList<string>().Select(c => Char.Parse(c)));
                            str.Clear();
                            NumberLine++;
                            continue;
                        }
                        if (NumberLine == 2)
                        {
                            LetterForLanguage.Vowel.AddRange(str.ToString().Split(',').ToList<string>().Select(c => Char.Parse(c)));
                            str.Clear();
                            NumberLine = 1;
                            break;
                        }
                    }
                }
            }
        }
    }
}
