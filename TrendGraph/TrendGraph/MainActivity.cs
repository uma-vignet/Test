using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Util;
using AChartEngine.Models;
using AChartEngine.Renderers;
using Android.Graphics;
using static Android.Graphics.Paint;
using Android.Views;
using AChartEngine;

namespace TrendGraph
{
    [Activity(Label = "TrendGraph", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private View chart;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            //openChart();
            
        }

        protected override void OnStart()
        {
            base.OnStart();
            openChart();
        }

        private IList<CGMMeasurementModel> input()
        {
            List<CGMMeasurementModel> listCgmModel = new List<CGMMeasurementModel>();
            listCgmModel.Add(new CGMMeasurementModel() { CGMValue = 48, OffsetTime = 2, SessionId = 1 });
            listCgmModel.Add(new CGMMeasurementModel() { CGMValue = 60, OffsetTime = 30, SessionId = 1 });
            listCgmModel.Add(new CGMMeasurementModel() { CGMValue = 109, OffsetTime = 65, SessionId = 1 });
            listCgmModel.Add(new CGMMeasurementModel() { CGMValue = 78, OffsetTime = 80, SessionId = 1 });
            listCgmModel.Add(new CGMMeasurementModel() { CGMValue = 200, OffsetTime = 106, SessionId = 1 });
            listCgmModel.Add(new CGMMeasurementModel() { CGMValue = 250, OffsetTime = 146, SessionId = 1 });
            listCgmModel.Add(new CGMMeasurementModel() { CGMValue = 150, OffsetTime = 178, SessionId = 1 });
            return listCgmModel;
        }

        private IList<CGMMeasurementModel> _latestTrendData
        {
            get
            {
                return input();
            }
            //protected set { RaiseAndSetIfChanged(ref _latestTrendData, value); }
        }

        XYSeries firstSeries = new XYSeries("70");
        XYSeries secondSeries = new XYSeries("80");
        XYSeries thirdSeries = new XYSeries("140");
        XYSeries fourthSeries = new XYSeries("250");
        XYSeries CGMSeries = new XYSeries("");
        // Creating a dataset to hold each series
        XYMultipleSeriesDataset dataset = new XYMultipleSeriesDataset();

        private void createSeries()
        {
            int Max = 180;
            const int lowerThreshold = 70;
            const int lowerTherapy = 80;
            const int higherThreshold = 250;
            const int higherTherapy = 140;

            
            for (int i = 0; i < Max; i++)
            {
                firstSeries.Add(i, lowerThreshold);
                secondSeries.Add(i, lowerTherapy);
                thirdSeries.Add(i, higherTherapy);
                fourthSeries.Add(i, higherThreshold);

            }
            
            int index = 0;
            if (_latestTrendData != null)
            {
                foreach (CGMMeasurementModel cgmModel in _latestTrendData)
                {
                    if (cgmModel != null)
                    {
                        var offsetTime = cgmModel.OffsetTime;
                        var CGMValue = cgmModel.CGMValue;
                        Log.Error("MainActivity :", "CGMValue:" + CGMValue);

                        CGMSeries.Add(offsetTime, (double)CGMValue);
                        index++;
                    }
                }
            }

            //CGMMeasurementModel.CGMValue
            
            // Adding First Series to the dataset
            dataset.AddSeries(firstSeries);
            // Adding Second Series to dataset
            dataset.AddSeries(secondSeries);
            // Adding Third Series to dataset
            dataset.AddSeries(thirdSeries);
            // Adding Fourth Series to dataset
            dataset.AddSeries(fourthSeries);
            dataset.AddSeries(CGMSeries);
        }

        // Creating XYSeriesRenderer to customize firstSeries
        XYSeriesRenderer firstRenderer = new XYSeriesRenderer();
        // Creating XYSeriesRenderer to customize secondSeries
        XYSeriesRenderer secondRenderer = new XYSeriesRenderer();
        // Creating XYSeriesRenderer to customize thirdSeries
        XYSeriesRenderer thirdRenderer = new XYSeriesRenderer();
        // Creating XYSeriesRenderer to customize fourthSeries
        XYSeriesRenderer fourthRenderer = new XYSeriesRenderer();
        XYSeriesRenderer cgmRenderer = new XYSeriesRenderer();

        private void createRenderer()
        {
            firstRenderer.Color = Color.Red; //color of the graph set to red
            firstRenderer.FillPoints = true;
            firstRenderer.LineWidth = 2f;
            //setting stroke of the line chart to solid
            firstRenderer.Stroke = BasicStroke.Solid;
            
            secondRenderer.Color = Color.Yellow;//color of the graph set to Yellow
            secondRenderer.FillPoints = true;
            secondRenderer.LineWidth = 2f;
            //setting stroke of the line chart to solid
            secondRenderer.Stroke = BasicStroke.Solid;
            
            thirdRenderer.Color = Color.Blue; //color of the graph set to blue
            thirdRenderer.FillPoints = true;
            thirdRenderer.LineWidth = 2f;
            //setting stroke of the line chart to solid
            thirdRenderer.Stroke = BasicStroke.Solid; 
           
            fourthRenderer.Color = Color.Cyan; //color of the graph set to cyan
            fourthRenderer.FillPoints = true;
            fourthRenderer.LineWidth = 2f;
            //setting stroke of the line chart to solid
            fourthRenderer.Stroke = BasicStroke.Solid;

            cgmRenderer.Color = Color.White;
            cgmRenderer.FillPoints = true;
            cgmRenderer.LineWidth = 2f;
            cgmRenderer.Stroke = BasicStroke.Solid;
        }

        // Creating a XYMultipleSeriesRenderer to customize the whole chart
        XYMultipleSeriesRenderer multiRenderer = new XYMultipleSeriesRenderer();

        private void multiRendererSetting()
        {    
            multiRenderer.XLabels = 0;
            /***
             * Customizing graphs
             */
            //setting text size of the title
            multiRenderer.ChartTitleTextSize = 28;
            //setting text size of the axis title
            multiRenderer.AxisTitleTextSize = 24;
            //setting text size of the graph lable
            multiRenderer.LabelsTextSize = 24;
            //setting zoom buttons visiblity
            multiRenderer.ZoomButtonsVisible = false;
            //setting pan enablity which uses graph to move on both axis
            multiRenderer.PanEnabled = false;
            //multiRenderer.setPanEnabled(true, false);
            //setting click false on graph
            multiRenderer.ClickEnabled = false;
            //setting zoom to false on both axis
            multiRenderer.ZoomEnabled = false;

            //setting lines to display on y axis
            multiRenderer.ShowGridY = true;
            //setting lines to display on x axis
            multiRenderer.ShowGridX = true;
            //setting legend to fit the screen size
            multiRenderer.FitLegend = true;
            //setting displaying line on grid
            multiRenderer.ShowGridX = true;
            //setting zoom to false
            multiRenderer.ZoomEnabled = false;

            //setting external zoom functions to false
            multiRenderer.ExternalZoomEnabled = false;
            //setting displaying lines on graph to be formatted(like using graphics)
            multiRenderer.Antialiasing = true;
            //setting to in scroll to false
            multiRenderer.InScroll = false;
            //setting to set legend height of the graph
            multiRenderer.LegendHeight = 20;
            //multiRenderer.setShowLegend(false);
            //setting x axis label align
            multiRenderer.XLabelsAlign = Align.Left;
            //setting y axis label to align
            //multiRenderer.YLabelsAlign=Align.Left;
            //setting text style
            multiRenderer.TextTypeface = Typeface.Default;
            //multiRenderer.TextTypeface="sans_serif", Typeface.NORMAL;
            //setting no of values to display in y axis
            //multiRenderer.clearYTextLabels();
            multiRenderer.GridColor = Color.Transparent;
            //multiRenderer.setYAxisMin(70,1);
            //multiRenderer.setYAxisMax(250,1);


            // setting y axis max value, Since i'm using static values inside the graph so i'm setting y max value to 400.
            // if you use dynamic values then get the max y value and set here
            multiRenderer.YAxisMax = 400;
            multiRenderer.YAxisMin = 0;
            //multiRenderer.setYLabels(2);
            //setting used to move the graph on xaxiz to .5 to the right
            multiRenderer.XAxisMin = 0;
            //setting used to move the graph on xaxiz to .5 to the right
            multiRenderer.XAxisMax = 180;
            multiRenderer.ZoomRate = 2f;
            //Setting background color of the graph to transparent
            multiRenderer.BackgroundColor = Color.Transparent;
            //Setting margin color of the graph to transparent
            multiRenderer.MarginsColor = Resource.Color.transparent_background;
            multiRenderer.ApplyBackgroundColor = true;
            multiRenderer.Scale = 2f;
            //setting x axis point size
            multiRenderer.PointSize = 4f;
            //setting the margin size for the graph in the order top, left, bottom, right
            multiRenderer.SetMargins(new int[] { 30, 30, 30, 30 });
            // Adding incomeRenderer and expenseRenderer to multipleRenderer
            // Note: The order of adding dataseries to dataset and renderers to multipleRenderer
            // should be same
            multiRenderer.AddSeriesRenderer(firstRenderer);
            multiRenderer.AddSeriesRenderer(secondRenderer);
            multiRenderer.AddSeriesRenderer(thirdRenderer);
            multiRenderer.AddSeriesRenderer(fourthRenderer);
            multiRenderer.AddSeriesRenderer(cgmRenderer);
        }
        public void openChart()
        {
            createSeries();
            createRenderer();
            multiRendererSetting();
            
            //this part is used to display graph on the xml
            LinearLayout chartContainer = FindViewById<LinearLayout>(Resource.Id.chart);
            //remove any views before u paint the chart
            chartContainer.RemoveAllViews();
            //drawing bar chart
            chart = ChartFactory.GetLineChartView(this, dataset, multiRenderer);
            //Intent intent = ChartFactory.GetLineChartIntent(this, dataset, multiRenderer);
            //StartActivity(intent);
            //adding the view to the linearlayout
            chartContainer.AddView(chart);
        }
    }
}

