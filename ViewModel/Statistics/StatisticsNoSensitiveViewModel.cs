using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Statistics
{
    public class StatisticsNoSensitiveViewModel : StatisitcsViewModel
    {
        public StatisticsNoSensitiveViewModel()
        {
            PathFolderSourceFail = Properties.Settings.Default.PathFolderWithSourceFileNoPaired;
            option = 1;
        }
    }
}
