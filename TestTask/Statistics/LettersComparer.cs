using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Statistics
{
    /// <summary>
    /// Класс нужный для сравнения
    /// </summary>
    public class LettersComparer : IComparer<Letters>
    {
        private Func<Letters, Letters, int> compFunc;

        public LettersComparer(Func<Letters, Letters, int> comp)
        {
            compFunc = comp;
        }

        public int Compare(Letters x, Letters y)
        {
            return compFunc(x, y);
        }
    }
}
