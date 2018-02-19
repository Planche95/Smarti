using ChartJSCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smarti.Services
{
    public class ChartGenerator : IChartGenerator
    {
        public Chart GenerateBarChart(List<double> resourceData)
        {
            Chart chart = new Chart
            {
                Type = "bar"
            };

            ChartJSCore.Models.Data data = new ChartJSCore.Models.Data
            {
                Labels = new List<string>() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" }
            };

            BarDataset dataset = new BarDataset()
            {
                Label = "Electric energy consumption (kWh)",
                Data = resourceData, 
                BackgroundColor = new List<string>()
                {
                "rgba(255, 99, 132, 0.2)",
                "rgba(54, 162, 235, 0.2)",
                "rgba(255, 206, 86, 0.2)",
                "rgba(75, 192, 192, 0.2)",
                "rgba(153, 102, 255, 0.2)",
                "rgba(255, 159, 64, 0.2)",
                "rgba(54, 159, 64, 0.2)"
                },
                BorderColor = new List<string>()
                {
                "rgba(255,99,132,1)",
                "rgba(54, 162, 235, 1)",
                "rgba(255, 206, 86, 1)",
                "rgba(75, 192, 192, 1)",
                "rgba(153, 102, 255, 1)",
                "rgba(255, 159, 64, 1)",
                "rgba(54, 159, 64, 1)"
                },
                BorderWidth = new List<int>() { 1 }
            };

            data.Datasets = new List<Dataset>
            {
                dataset
            };

            chart.Data = data;

            Options options = new Options()
            {
                Scales = new Scales(),
                Tooltips = new ToolTip()
            };

            Scales scales = new Scales()
            {
                YAxes = new List<Object>()
                {
                    new CartesianScale()
                    {
                        Ticks = new CartesianLinearTick()
                        {
                            BeginAtZero = true
                        }
                    }
                }
            };

            ToolTip toolTip = new ToolTip
            {
                Enabled = true,
                Mode = "single",
                Callbacks = new Callback
                {
                    Label = "function(tooltipItems, data) {return tooltipItems.yLabel + ' kWh';}"
                }
            };

            options.Scales = scales;
            options.Tooltips = toolTip;

            chart.Options = options;

            return chart;
        }

        public Chart GeneratePieChart(List<string> labels, PieDataset dataset)
        {
            Chart chart = new Chart
            {
                Type = "pie"
            };

            ChartJSCore.Models.Data data = new ChartJSCore.Models.Data
            {
                Labels = labels 
            };

            data.Datasets = new List<Dataset>
            {
                dataset
            };

            Options options = new Options()
            {
                Tooltips = new ToolTip()
            };

            ToolTip toolTip = new ToolTip
            {
                Enabled = true,
                Mode = "single",
                Callbacks = new Callback
                {
                    Label = "function(tooltipItem, data) { var indice = tooltipItem.index; return  data.labels[indice] +': '+data.datasets[0].data[indice] + ' kWh';}"
                }
            };

            options.Tooltips = toolTip;

            chart.Options = options;
            chart.Data = data;

            return chart;
        }

        #region NotUsedForNow

        public Chart GenerateLineChart()
        {
            Chart chart = new Chart
            {
                Type = "line"
            };

            ChartJSCore.Models.Data data = new ChartJSCore.Models.Data
            {
                Labels = new List<string>() { "January", "February", "March", "April", "May", "June", "July" }
            };

            LineDataset dataset = new LineDataset()
            {
                Label = "My First dataset",
                Data = new List<double>() { 65, 59, 80, 81, 56, 55, 40 },
                Fill = false,
                LineTension = 0.1,
                BackgroundColor = "rgba(75, 192, 192, 0.4)",
                BorderColor = "rgba(75,192,192,1)",
                BorderCapStyle = "butt",
                BorderDash = new List<int> { },
                BorderDashOffset = 0.0,
                BorderJoinStyle = "miter",
                PointBorderColor = new List<string>() { "rgba(75,192,192,1)" },
                PointBackgroundColor = new List<string>() { "#fff" },
                PointBorderWidth = new List<int> { 1 },
                PointHoverRadius = new List<int> { 5 },
                PointHoverBackgroundColor = new List<string>() { "rgba(75,192,192,1)" },
                PointHoverBorderColor = new List<string>() { "rgba(220,220,220,1)" },
                PointHoverBorderWidth = new List<int> { 2 },
                PointRadius = new List<int> { 1 },
                PointHitRadius = new List<int> { 10 },
                SpanGaps = false
            };

            data.Datasets = new List<Dataset>
            {
                dataset
            };

            Options options = new Options()
            {
                Scales = new Scales()
            };

            Scales scales = new Scales()
            {
                YAxes = new List<Object>()
                {
                    new CartesianScale()
                }
            };

            CartesianScale yAxes = new CartesianScale()
            {
                Ticks = new Tick()
            };

            Tick tick = new Tick()
            {
                Callback = "function(value, index, values) {return '$' + value;}"
            };

            yAxes.Ticks = tick;
            scales.YAxes = new List<Scale>() { yAxes };
            options.Scales = scales;
            chart.Options = options;

            chart.Data = data;

            return chart;
        }

        public Chart GenerateLineScatterChart()
        {
            Chart chart = new Chart
            {
                Type = "line"
            };

            ChartJSCore.Models.Data data = new ChartJSCore.Models.Data();

            LineScatterDataset dataset = new LineScatterDataset()
            {
                Label = "Scatter Dataset",
                Data = new List<LineScatterData>()
            };

            LineScatterData scatterData1 = new LineScatterData();
            LineScatterData scatterData2 = new LineScatterData();
            LineScatterData scatterData3 = new LineScatterData();

            scatterData1.x = -10;
            scatterData1.y = 0;
            dataset.Data.Add(scatterData1);

            scatterData2.x = 0;
            scatterData2.y = 10;
            dataset.Data.Add(scatterData2);

            scatterData3.x = 10;
            scatterData3.y = 5;
            dataset.Data.Add(scatterData3);

            data.Datasets = new List<Dataset>
            {
                dataset
            };

            chart.Data = data;

            Options options = new Options()
            {
                Scales = new Scales()
            };

            Scales scales = new Scales()
            {
                XAxes = new List<Object>()
                {
                    new CartesianScale()
                    {
                        Type = "linear",
                        Position = "bottom",
                        Ticks = new CartesianLinearTick()
                        {
                            BeginAtZero = true
                        }
                    }
                }
            };

            options.Scales = scales;

            chart.Options = options;

            return chart;
        }

        public Chart GenerateRadarChart()
        {
            Chart chart = new Chart
            {
                Type = "radar"
            };

            ChartJSCore.Models.Data data = new ChartJSCore.Models.Data
            {
                Labels = new List<string>() { "Eating", "Drinking", "Sleeping", "Designing", "Coding", "Cycling", "Running" }
            };

            RadarDataset dataset1 = new RadarDataset()
            {
                Label = "My First dataset",
                BackgroundColor = "rgba(179,181,198,0.2)",
                BorderColor = "rgba(179,181,198,1)",
                PointBackgroundColor = new List<string>() { "rgba(179,181,198,1)" },
                PointBorderColor = new List<string>() { "#fff" },
                PointHoverBackgroundColor = new List<string>() { "#fff" },
                PointHoverBorderColor = new List<string>() { "rgba(179,181,198,1)" },
                Data = new List<double>() { 65, 59, 80, 81, 56, 55, 40 }
            };

            RadarDataset dataset2 = new RadarDataset()
            {
                Label = "My Second dataset",
                BackgroundColor = "rgba(255,99,132,0.2)",
                BorderColor = "rgba(255,99,132,1)",
                PointBackgroundColor = new List<string>() { "rgba(255,99,132,1)" },
                PointBorderColor = new List<string>() { "#fff" },
                PointHoverBackgroundColor = new List<string>() { "#fff" },
                PointHoverBorderColor = new List<string>() { "rgba(255,99,132,1)" },
                Data = new List<double>() { 28, 48, 40, 19, 96, 27, 100 }
            };

            data.Datasets = new List<Dataset>
            {
                dataset1,
                dataset2
            };

            chart.Data = data;

            return chart;
        }

        public Chart GeneratePolarChart()
        {
            Chart chart = new Chart
            {
                Type = "polarArea"
            };

            ChartJSCore.Models.Data data = new ChartJSCore.Models.Data
            {
                Labels = new List<string>() { "Red", "Green", "Yellow", "Grey", "Blue" }
            };

            PolarDataset dataset = new PolarDataset()
            {
                Label = "My dataset",
                BackgroundColor = new List<string>() { "#FF6384", "#4BC0C0", "#FFCE56", "#E7E9ED", "#36A2EB" },
                Data = new List<double>() { 11, 16, 7, 3, 14 }
            };

            data.Datasets = new List<Dataset>
            {
                dataset
            };

            chart.Data = data;

            return chart;
        }

        public Chart GenerateBubbleChart()
        {
            Chart chart = new Chart
            {
                Type = "bubble"
            };

            ChartJSCore.Models.Data data = new ChartJSCore.Models.Data();

            BubbleDataset dataset = new BubbleDataset()
            {
                Label = "Bubble Dataset",
                Data = new List<BubbleData>()
            };

            BubbleData bubbleData1 = new BubbleData();
            BubbleData bubbleData2 = new BubbleData();

            bubbleData1.x = 20;
            bubbleData1.y = 30;
            bubbleData1.r = 15;
            dataset.Data.Add(bubbleData1);

            bubbleData2.x = 40;
            bubbleData2.y = 10;
            bubbleData2.r = 10;
            dataset.Data.Add(bubbleData2);

            data.Datasets = new List<Dataset>
            {
                dataset
            };

            dataset.BackgroundColor = new List<string>() { "#FF6384" };
            dataset.HoverBackgroundColor = new List<string>() { "#FF6384" };

            chart.Data = data;

            return chart;
        }

        #endregion
    }
}
