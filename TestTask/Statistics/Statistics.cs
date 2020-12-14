using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Statistics
{
    /// <summary>
    /// Статистика
    /// </summary>
    public class Statistics
    {
        /// <summary>
        /// Знаки, которые не надо учитывать в статистики
        /// </summary>
        private char[] Marks;
        private OperationStatistics Operation;
        public List<Letters> StatsByLetter
        {
            get;
            set;
        }

        public Statistics()
        {
            StatsByLetter = new List<Letters>();
            Operation = new OperationStatistics(StatsByLetter);
            Marks = new char[41];
            Marks[0] = '—';
            Marks[1] = '.';
            Marks[2] = '?';
            Marks[3] = '!';
            Marks[4] = ')';
            Marks[5] = '(';
            Marks[6] = ',';
            Marks[7] = ':';
            Marks[8] = ' ';
            Marks[9] = '–';
            Marks[10] = '"';
            Marks[11] = '+';
            Marks[12] = '-';
            Marks[13] = '!';
            Marks[14] = '@';
            Marks[15] = '#';
            Marks[16] = '$';
            Marks[17] = '%';
            Marks[18] = '^';
            Marks[19] = '&';
            Marks[20] = '*';
            Marks[21] = '{';
            Marks[22] = '}';
            Marks[23] = '[';
            Marks[24] = ']';
            Marks[25] = '\\';
            Marks[26] = '/';
            Marks[27] = '<';
            Marks[28] = '>';
            Marks[29] = '=';
            Marks[30] = '~';
            Marks[31] = '_';
            Marks[32] = ';';
            Marks[33] = '|';
            Marks[34] = "'".ToCharArray()[0];
            Marks[35] = '№';
            Marks[36] = '…';
            Marks[37] = '„';
            Marks[38] = '“';
            Marks[39] = '’';
            Marks[40] = '‑';
        }

        StringBuilder StringWithData;
        /// <summary>
        /// Вычисление статистики
        /// </summary>
        /// <param name="stringWithData">строка из которой надо вычислять статистику</param>
        /// <param name="option">0 - не регистрозависимо, 1 - регистрозависимо</param>
        public void CalculationStatistics(StringBuilder stringWithData, short option)
        {
            //if (option == 0)
            //{
            int id = -1;
            StringWithData = stringWithData;
            for (int i = 0; i < StringWithData.Length; i++)
            {
                if (option == 0)
                {
                    id = StatsByLetter.FindIndex(x => x.Letter.Equals(StringWithData[i]));
                }
                if (option == 1)
                {
                    currentSymvol = Char.ToUpper(StringWithData[i]);
                    id = StatsByLetter.FindIndex(x => x.Letter.Equals(currentSymvol));
                }

                if (id != -1)
                {
                    StatsByLetter[id].Count++;
                }
                else
                {
                    AddValue(i, 1, option);
                }
            }
        }

        char currentSymvol;
        /// <summary>
        /// Добавление нового символа в статистику
        /// </summary>
        /// <param name="id">номер символа</param>
        /// <param name="value">сам символ</param>
        /// <param name="option">какой регистр</param>
        private void AddValue(int id, int value, short option)
        {
            if (option == 0)
            {
                currentSymvol = StringWithData[id];
            }
            if (option == 1)
            {
                currentSymvol = Char.ToUpper(StringWithData[id]);
            }
            if (!CheckNumber(currentSymvol) && !CheckPunctuationMark(currentSymvol) && !CheckTrash(currentSymvol))
            {
                Letters letters = new Letters();
                letters.Letter = currentSymvol;
                letters.Count = value;
                StatsByLetter.Add(letters);
            }
        }

        /// <summary>
        /// Проверка на число
        /// </summary>
        /// <returns>true - является</returns>
        private bool CheckNumber(char symvol)
        {
            if (Char.IsDigit(symvol))
            {
                return true;
            }
            return false;
        }

        private bool CheckTrash(char symvol)
        {
            if (symvol == '\n' || symvol == '\r' || symvol == '\t' || symvol == '﻿')
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Проверка на пунктационнй знак
        /// </summary>
        /// <param name="symbol">символ для проверки</param>
        /// <returns>true - да он, false - нет не пунктационнй знак</returns>
        private bool CheckPunctuationMark(char symvol)
        {
            for (int i = 0; i < Marks.Length; i++)
            {
                if (symvol == Marks[i])
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Удаление символов из статистики
        /// </summary>
        /// <param name="lettersForDelete"></param>
        public void DeleteLetter(IList<char> lettersForDelete)
        {
            Operation.DeleteLetter(lettersForDelete);
        }

        /// <summary>
        /// Сортировка символов для их показа по алфавиту
        /// </summary>
        public void SortByLetter()
        {
            StatsByLetter.Sort(new LettersComparer((a, b) => Char.ToUpper(a.Letter).CompareTo(Char.ToUpper(b.Letter))));
        }
    }
}
