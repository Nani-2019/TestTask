using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace ViewModel
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        public SettingsViewModel()
        {
            PathFolderWithSourceFileNoPaired = Properties.Settings.Default.PathFolderWithSourceFileNoPaired;
            PathFolderWithSourceFilePaired = Properties.Settings.Default.PathFolderWithSourceFilePaired;

            SelectPathFolderWithSourceFilePaired = new RelayCommand(arg => СhoicePathFolderWithSourceFilePaired());
            SelectPathFolderWithSourceFileNoPaired = new RelayCommand(arg => СhoicePathFolderWithSourceFileNoPaired());

            CountFile();
            ListLanguages = arrayPathSourceFile;
        }


        List<string> arrayPathSourceFile;
        private void CountFile()
        {
            arrayPathSourceFile = new List<string>();
            string path = Directory.GetCurrentDirectory() + "\\Language";
            DirectoryInfo dir = new DirectoryInfo(path);

            foreach (FileInfo files in dir.GetFiles())
            {
                arrayPathSourceFile.Add(/*path + "\\" + */files.Name.Replace(".txt", ""));
            }
        }

        /// <summary>
        /// Путь к файлу где считыванием пары букв(регистрозависимо)
        /// </summary>
        private string pathFolderWithSourceFileNoPaired;
        public string PathFolderWithSourceFileNoPaired
        {
            get { return pathFolderWithSourceFileNoPaired; }
            set
            {
                if (pathFolderWithSourceFileNoPaired != value)
                {
                    pathFolderWithSourceFileNoPaired = value;
                    Properties.Settings.Default.PathFolderWithSourceFileNoPaired = pathFolderWithSourceFileNoPaired;
                    Properties.Settings.Default.Save();
                    OnPropertyChanged("PathFolderWithSourceFileNoPaired");
                }
            }
        }

        /// <summary>
        /// Путь к файлу где считыванием одиночных букв(не регистрозависимо)
        /// </summary>
        private string pathFolderWithSourceFilePaired;
        public string PathFolderWithSourceFilePaired
        {
            get { return pathFolderWithSourceFilePaired; }
            set
            {
                if (pathFolderWithSourceFilePaired != value)
                {
                    pathFolderWithSourceFilePaired = value;
                    Properties.Settings.Default.PathFolderWithSourceFilePaired = pathFolderWithSourceFilePaired;
                    Properties.Settings.Default.Save();
                    OnPropertyChanged("PathFolderWithSourceFilePaired");
                }
            }
        }

        /// <summary>
        /// Список языков
        /// </summary>
        private List<string> listLanguages;
        public List<string> ListLanguages
        {
            get { return listLanguages; }
            set
            {
                if (listLanguages != value)
                {
                    listLanguages = value;
                    OnPropertyChanged("ListLanguages");
                }
            }
        }


        public ICommand SelectCommand => new RelayCommand(o => Delete((Collection<object>)o));

        private void Delete(Collection<object> o)
        {
            List<string> list = o.Cast<string>().ToList();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var itr in o)
            {
                stringBuilder.Append(itr + "|");
            }
            stringBuilder.Remove(stringBuilder.Length - 1, 1);
            Properties.Settings.Default.Languages = stringBuilder.ToString();
        }


        /// <summary>
        /// Кнопка выбора папки где лежит файлы для статистики по парным буквам Аа
        /// </summary>
        public ICommand SelectPathFolderWithSourceFilePaired { get; set; }
        public void СhoicePathFolderWithSourceFilePaired()
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            DialogResult result = folderBrowser.ShowDialog();

            if (!string.IsNullOrWhiteSpace(folderBrowser.SelectedPath))
            {
                PathFolderWithSourceFilePaired = folderBrowser.SelectedPath;
            }
        }

        /// <summary>
        /// Кнопка выбора папки где лежит файлы для статистики по не парным буквам А, а
        /// </summary>
        public ICommand SelectPathFolderWithSourceFileNoPaired { get; set; }
        public void СhoicePathFolderWithSourceFileNoPaired()
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            DialogResult result = folderBrowser.ShowDialog();

            if (!string.IsNullOrWhiteSpace(folderBrowser.SelectedPath))
            {
                PathFolderWithSourceFileNoPaired = folderBrowser.SelectedPath;
            }
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

