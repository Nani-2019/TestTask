﻿using System;
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
using ViewModel.Statistics;

namespace View
{
    /// <summary>
    /// Логика взаимодействия для StatisticsNoSensitiveWindow.xaml
    /// </summary>
    public partial class StatisticsNoSensitiveWindow : UserControl
    {
        public StatisticsNoSensitiveWindow()
        {
            InitializeComponent();
            DataContext = new StatisticsNoSensitiveViewModel();
        }
    }
}
