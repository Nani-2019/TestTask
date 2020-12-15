using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void ListViewMenu_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;
            MoveCuursorMenu(index);

            switch (index)
            {
                case 0:
                    GridPrincipial.Children.Clear();
                    GridPrincipial.Children.Add(new SettingsWindow());
                    break;
                case 1:
                    GridPrincipial.Children.Clear();
                    GridPrincipial.Children.Add(new StatisticsSensitiveWindows());
                    break;
                case 2:
                    GridPrincipial.Children.Clear();
                    GridPrincipial.Children.Add(new StatisticsNoSensitiveWindow());
                    break;
                default:
                    break;
            }
        }

        private void MoveCuursorMenu(int index)
        {
            TransitionContentSlide.OnApplyTemplate();
            GridCursor.Margin = new Thickness(0, 70 * index, 0, 0);
        }
    }
}
