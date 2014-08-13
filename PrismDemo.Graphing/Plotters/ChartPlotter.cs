﻿using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.PointMarkers;
using PrismDemo.Graphing.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace PrismDemo.Graphing.Plotters
{
    public class ChartPlotter : Microsoft.Research.DynamicDataDisplay.ChartPlotter
    {
        // taken from --> http://dynamicdatadisplay.codeplex.com/discussions/78607

        private IDictionary<Guid, LinePerturbationViewModel> lineGraphsList;
        private IList<LineGraph> lineGraphLines = new List<LineGraph>();
        private IList<LineAndMarker<MarkerPointsGraph>> lineAndMarkerGraphs = new List<LineAndMarker<MarkerPointsGraph>>();

        /// <summary>
        /// DependencyProperty for LineGraphs
        /// </summary>
        public static readonly DependencyProperty LineGraphsProperty = DependencyProperty.Register("LineGraphs", typeof(ObservableCollection<LinePerturbationViewModel>), typeof(ChartPlotter), new FrameworkPropertyMetadata(new PropertyChangedCallback(ChangeLineGraphs)));

        /// <summary>
        /// Gets or sets the line graphs.
        /// </summary>
        /// <value>The line graphs.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "ok")]
        public ObservableCollection<LinePerturbationViewModel> LineGraphs
        {
            get
            {
                return (ObservableCollection<LinePerturbationViewModel>)GetValue(LineGraphsProperty);
            }

            set
            {
                SetValue(LineGraphsProperty, value);

                this.LineGraphs.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(OnLineGraphsCollectionChanged);
            }
        }

        /// <summary>
        /// Changes the line graphs.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="eventArgs">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        public static void ChangeLineGraphs(DependencyObject source, DependencyPropertyChangedEventArgs eventArgs)
        {
            (source as ChartPlotter).UpdateLineGraphs((ObservableCollection<LinePerturbationViewModel>)eventArgs.NewValue);
        }

        private void UpdateLineGraphs(ObservableCollection<LinePerturbationViewModel> lineGrphs)
        {
            this.LineGraphs = lineGrphs;
            this.lineGraphsList = new Dictionary<Guid, LinePerturbationViewModel>();
        }

        private void OnLineGraphsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    {
                        foreach (LinePerturbationViewModel viewModel in e.NewItems)
                        {
                            if (viewModel.LineAndMarker)
                            {
                                SolidColorBrush brush = new SolidColorBrush();
                                brush.Color = viewModel.Color;
                                LineAndMarker<MarkerPointsGraph> lineAndMarker = this.AddLineGraph(viewModel.PointDataSource, new Pen(brush, viewModel.Thickness), new CirclePointMarker { Size = 7, Fill = brush }, new PenDescription(viewModel.Name));
                                lineAndMarker.LineGraph.Name = viewModel.Name;
                                this.lineAndMarkerGraphs.Add(lineAndMarker);
                            }
                            else
                            {
                                LineGraph lineGraph = this.AddLineGraph(viewModel.PointDataSource, viewModel.Color, viewModel.Thickness, viewModel.Name);
                                lineGraph.Name = viewModel.Name;
                                this.lineGraphLines.Add(lineGraph);
                            }
                        }

                        break;
                    }

                case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
                    {
                        break;
                    }

                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    {
                        break;
                    }

                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    {
                        bool bTemp = false;
                        foreach (LinePerturbationViewModel viewModel in e.NewItems)
                        {
                            foreach (LineGraph line in this.lineGraphLines)
                            {
                                if (this.Children.Contains(line) && line.Name == viewModel.Name)
                                {
                                    this.Children.Remove(line);
                                    this.lineGraphLines.Remove(line);
                                    bTemp = true;
                                    break;
                                }
                            }

                            foreach (LineAndMarker<MarkerPointsGraph> line in this.lineAndMarkerGraphs)
                            {
                                if (this.Children.Contains(line.LineGraph) && line.LineGraph.Name == viewModel.Name)
                                {
                                    this.Children.Remove(line.LineGraph);
                                    this.Children.Remove(line.MarkerGraph);
                                    this.lineAndMarkerGraphs.Remove(line);
                                    bTemp = true;
                                    break;
                                }
                            }

                            if (bTemp)
                            {
                                if (viewModel.LineAndMarker)
                                {
                                    SolidColorBrush brush = new SolidColorBrush();
                                    brush.Color = viewModel.Color;
                                    LineAndMarker<MarkerPointsGraph> lineAndMarker = this.AddLineGraph(viewModel.PointDataSource, new Pen(brush, viewModel.Thickness), new CirclePointMarker { Size = 7, Fill = brush }, new PenDescription(viewModel.Name));
                                    lineAndMarker.LineGraph.Name = viewModel.Name;
                                    this.lineAndMarkerGraphs.Add(lineAndMarker);
                                }
                                else
                                {
                                    LineGraph lineGraph = this.AddLineGraph(viewModel.PointDataSource, viewModel.Color, viewModel.Thickness, viewModel.Name);
                                    lineGraph.Name = viewModel.Name;
                                    this.lineGraphLines.Add(lineGraph);
                                }

                                bTemp = false;
                            }
                        }

                        break;
                    }

                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                    {
                        break;
                    }
            }
        }

    }
}
