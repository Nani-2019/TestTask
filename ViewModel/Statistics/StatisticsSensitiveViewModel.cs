using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Statistics
{
    public class StatisticsSensitiveViewModel : StatisitcsViewModel
    {
        public StatisticsSensitiveViewModel()
        {
            PathFolderSourceFail = Properties.Settings.Default.PathFolderWithSourceFilePaired;
            option = 0;
        }
    }
}
