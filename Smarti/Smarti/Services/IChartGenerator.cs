using ChartJSCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smarti.Services
{
    public interface IChartGenerator
    {
        Chart GenerateBarChart(List<double> resourceData);

        Chart GeneratePieChart(List<string> labels, PieDataset dataset);
    }
}
