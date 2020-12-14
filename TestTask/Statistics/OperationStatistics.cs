using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Statistics
{
    public class OperationStatistics
    {
        private IList<Letters> StatsByLetter
        {
            get;
            set;
        }
        public OperationStatistics(IList<Letters> statsByLetter)
        {
            StatsByLetter = statsByLetter;
        }

        /// <summary>
        /// Удаление букв из статистики
        /// </summary>
        /// <param name="lettersToDelete">какие буквы нужно удалить</param>
        public void DeleteLetter(IList<char> lettersToDelete)
        {
            for (int i = 0; i < StatsByLetter.Count; i++)
            {
                for (int j = 0; j < lettersToDelete.Count; j++)
                {
                    if (StatsByLetter[i].Letter == lettersToDelete[j])
                    {
                        StatsByLetter.RemoveAt(i);
                        i--;
                        break;
                    }
                    if (StatsByLetter[i].Letter == Char.ToUpper(lettersToDelete[j]))
                    {
                        StatsByLetter.RemoveAt(i);
                        i--;
                        break;
                    }
                }
            }
        }
    }
}
