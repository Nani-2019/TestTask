using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.File
{
    /// <summary>
    /// Файл где высчитывают статитику
    /// </summary>
    public class FileForStatistics
    {
        /// <summary>
        /// Путь к папке с файлами для подсчета символов
        /// </summary>
        string[] PathSourceFile;

        public TestTask.Statistics.Statistics Statistics { get; set; }
        /// <summary>
        /// Конструктор класса. 
        /// Т.к. происходит прямая работа с файлом, обеспечено ГАРАНТИРОВАННОЕ закрытие файла после окончания работы с таковым!
        /// </summary>
        /// <param name="fileFullPath">Полный путь до файла для чтения</param>
        public FileForStatistics(string[] pathSourceFile)
        {
            PathSourceFile = pathSourceFile;
            Statistics = new TestTask.Statistics.Statistics();
        }

        public void ReadFiles(short option)
        {
            StringBuilder line = new StringBuilder(1);
            for (int i = 0; i < PathSourceFile.Length; i++)
            {
                if (PathSourceFile[i].Split('.')[1] != "txt")
                {
                    continue;
                }

                long length = new FileInfo(PathSourceFile[i]).Length;
                using (var stream = System.IO.File.Open(PathSourceFile[i], FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (BinaryReader binReader = new BinaryReader(stream))
                    {
                        while (binReader.PeekChar() > -1)
                        {
                            line.Append(binReader.ReadChars(10));
                            Statistics.CalculationStatistics(line, option);
                            line.Clear();
                        }
                    }
                }
            }
        }
    }
}
