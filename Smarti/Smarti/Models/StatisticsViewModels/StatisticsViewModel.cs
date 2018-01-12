using ChartJSCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smarti.Models.StatisticsViewModels
{
    public class StatisticsViewModel
    {
        public string Header { get; set; }
        public Chart PieChart { get; set; }
        public Chart BarChart { get; set; }

        public bool Expanded { get; set; }
    }
}
