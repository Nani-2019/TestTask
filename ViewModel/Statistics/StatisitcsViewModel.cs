using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.File;
using TestTask.Statistics;

namespace ViewModel.Statistics
{
    public class StatisitcsViewModel : INotifyPropertyChanged
    {
        protected FileForStatistics FileStatistics;
        protected short option;
        protected string PathFolderSourceFail;
        public StatisitcsViewModel()
        {
            IsCalculationStatistics = true;
            IsProgressStatistics = false;
            IsDelete = false;
        }

        bool isDelete;
        /// <summary>
        /// Можно ли удалить статистику
        /// </summary>
        public bool IsDelete
        {
            get { return isDelete; }
            set
            {
                if (isDelete != value)
                {
                    isDelete = value;
                    OnPropertyChanged("IsDelete");
                }
            }
        }

        bool isVowel;
        /// <summary>
        /// Выбраны ли согласные
        /// </summary>
        public bool IsVowel
        {
            get { return isVowel; }
            set
            {
                if (isVowel != value)
                {
                    isVowel = value;
                    OnPropertyChanged("IsVowel");
                }
            }
        }

        bool isConsonants;
        /// <summary>
        /// Выбраны ли гласные
        /// </summary>
        public bool IsConsonants
        {
            get { return isConsonants; }
            set
            {
                if (isConsonants != value)
                {
                    isConsonants = value;
                    OnPropertyChanged("IsConsonants");
                }
            }
        }

        /// <summary>
        /// Включен ли прогресс бар
        /// </summary>
        private bool isProgressStatistics;
        public bool IsProgressStatistics
        {
            get
            {
                return isProgressStatistics;
            }
            set
            {
                if (isProgressStatistics != value)
                {
                    isProgressStatistics = value;
                }
                OnPropertyChanged("IsProgressStatistics");
            }
        }

        /// <summary>
        /// Можно ли получать статистику
        /// </summary>
        private bool isCalculationStatistics;
        public bool IsCalculationStatistics
        {
            get
            {
                return isCalculationStatistics;
            }
            set
            {
                if (isCalculationStatistics != value)
                {
                    isCalculationStatistics = value;
                }
                OnPropertyChanged("IsCalculationStatistics");
            }
        }

        private ObservableCollection<Letters> statisticsView;
        public ObservableCollection<Letters> StatisticsView
        {
            get
            {
                return statisticsView;
            }
            set
            {
                if (statisticsView != value)
                {
                    statisticsView = value;
                }
                OnPropertyChanged("StatisticsView");
            }
        }

        RelayCommand calculationStatistics;
        public RelayCommand CalculationStatistics
        {
            get
            {
                return calculationStatistics ??
                    (calculationStatistics = new RelayCommand((selectedItem) =>
                    {
                        if (calculationStatistics != null)
                        {
                            BackgroundWorker worker = new BackgroundWorker();
                            worker.WorkerReportsProgress = true;
                            worker.DoWork += WorkerStatistics;
                            worker.ProgressChanged += WorkerProgress;
                            worker.RunWorkerCompleted += WorkerRunWorkerCompleted;
                            worker.RunWorkerAsync(10000);
                        }
                    }));
            }
        }

        RelayCommand deleteStatistics;
        public RelayCommand DeleteStatistics
        {
            get
            {
                return deleteStatistics ??
                    (deleteStatistics = new RelayCommand((selectedItem) =>
                    {
                        if (deleteStatistics != null)
                        {
                            BackgroundWorker worker = new BackgroundWorker();
                            worker.WorkerReportsProgress = true;
                            worker.DoWork += WorkerDelete;
                            worker.ProgressChanged += WorkerProgress;
                            worker.RunWorkerCompleted += WorkerRunWorkerCompleted;
                            worker.RunWorkerAsync(10000);
                        }
                    }));
            }
        }
        FileLanguage fileLanguage;
        void WorkerDelete(object sender, DoWorkEventArgs e)
        {
            int progress = 0;
            (sender as BackgroundWorker).ReportProgress(progress, 0);
            fileLanguage = new FileLanguage(Properties.Settings.Default.Languages.Split('|'));
            fileLanguage.ReadFile();
            if (IsConsonants)
            {
                FileStatistics.Statistics.DeleteLetter(fileLanguage.LetterForLanguage.Consonants);
            }
            if (IsVowel)
            {
                FileStatistics.Statistics.DeleteLetter(fileLanguage.LetterForLanguage.Vowel);
            }

            StatisticsView = new ObservableCollection<Letters>();
            progress = 1;
            (sender as BackgroundWorker).ReportProgress(progress);
            e.Result = false;
        }

        void WorkerStatistics(object sender, DoWorkEventArgs e)
        {
            int progress = 0;
            (sender as BackgroundWorker).ReportProgress(progress, 0);
            FileStatistics = new FileForStatistics(CreateFileGroup(PathFolderSourceFail));
            FileStatistics.ReadFiles(option);

            StatisticsView = new ObservableCollection<Letters>();
            progress = 1;
            (sender as BackgroundWorker).ReportProgress(progress);
            e.Result = false;
        }


        /// <summary>
        /// Обновления
        /// </summary>
        void WorkerProgress(object sender, ProgressChangedEventArgs e)
        {
            IsCalculationStatistics = false;
            IsDelete = false;
            IsProgressStatistics = true;
        }

        /// <summary>
        /// Закончилось
        /// </summary>
        void WorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IsCalculationStatistics = true;
            IsDelete = true;
            IsProgressStatistics = (bool)e.Result;
            FileStatistics.Statistics.SortByLetter();

            for (int i = 0; i < FileStatistics.Statistics.StatsByLetter.Count; i++)
            {
                StatisticsView.Add(FileStatistics.Statistics.StatsByLetter[i]);
            }
        }

        /// <summary>
        /// Для обработки путей группы файлов(пакетов файлов)
        /// </summary>
        /// <param name="paths">путь к файлам</param>
        /// <returns>массив с этими путями</returns>
        private string[] CreateFileGroup(string path)
        {
            List<string> arrayPathSourceFile = new List<string>();
            DirectoryInfo dir = new DirectoryInfo(path);

            foreach (FileInfo files in dir.GetFiles())
            {
                arrayPathSourceFile.Add(path + "\\" + files.Name);
            }
            return arrayPathSourceFile.ToArray();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
