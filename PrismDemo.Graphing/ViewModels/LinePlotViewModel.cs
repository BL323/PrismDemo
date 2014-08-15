using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;
using Microsoft.Research.DynamicDataDisplay.Charts;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using PrismDemo.Graphing.Events;
using PrismDemo.Infrastructure;
using PrismDemo.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace PrismDemo.Graphing.ViewModels
{
    public class LinePlotViewModel : UpdateBase
    {
        #region Private Fields
        private int _pertCount = 1;
        private HorizontalDateTimeAxis dateAxis;
        private CompositeDataSource editedDs;
        private ICommand _addPerturbationsCommand;
        private ObservableCollection<LinePerturbationViewModel> _linePerturbations = new ObservableCollection<LinePerturbationViewModel>();

        private IUnityContainer _container = null;
        private IEventAggregator _eventAggregator = null;
        #endregion

        #region Public Properties
        public ICommand AddPerturbationsCommand
        {
            get
            {
                return _addPerturbationsCommand ?? (_addPerturbationsCommand = new RelayCommand(o => AddLineGraphs(), o => true));
            }
        }
        public ObservableCollection<LinePerturbationViewModel> LinePerturbations
        {
            get { return _linePerturbations; }
            set
            {
                if (_linePerturbations != value)
                {
                    _linePerturbations = value;
                    RaisePropertyChanged(() => LinePerturbations);
                }
            }
        }
        public ObservableCollection<LinePerturbationViewModel> VisiblePerturbations
        {
            get
            {
                var res = LinePerturbations.Where(x => x.IsVisible);
                return new ObservableCollection<LinePerturbationViewModel>(res);
            }
        }
        #endregion

        #region Constructor
        public LinePlotViewModel(IUnityContainer container)
        {
            _container = container;
            _eventAggregator = _container.Resolve<IEventAggregator>();

            _eventAggregator.GetEvent<PerturbationsUpdatedEvent>().Subscribe(RefreshLineGraphs, true);
        }
        #endregion

        #region Private Methods
        private void AddLineGraphs()
        {
            const int N = 10;
            const int M = 20;
            double[] x = new double[N];
            double[] y = new double[N];

            ////double[] x1 = new double[N];
            ////double[] y1 = new double[N];
            double[] x1 = new double[M];
            double[] y1 = new double[M];
            DateTime[] date1 = new DateTime[M];

            DateTime[] date = new DateTime[N];

            for (int i = 0; i < N * 2; i = i + 2)
            {
                x[i / 2] = i / 2 * 0.1;
                //x1[i/2] = i/2 * 0.2;
                y[i / 2] = Math.Sin(x[i / 2]);
                //y1[i/2] = Math.Cos(x1[i/2]) * this.factor;
                date[i / 2] = DateTime.Now.AddMinutes(-(N * 2) + i / 2);
            }

            for (int i = 0; i < M; i++)
            {

                x1[i] = i * 0.2;
                y1[i] = Math.Cos(x1[i]) * 2;
                date1[i] = DateTime.Now.AddMinutes(-M + i);
            }

            EnumerableDataSource<double> xs = new EnumerableDataSource<double>(y);

            xs.SetYMapping(_y => _y);
            EnumerableDataSource<DateTime> ys = new EnumerableDataSource<DateTime>(date);
            this.dateAxis = new HorizontalDateTimeAxis();
            ys.SetXMapping(dateAxis.ConvertToDouble);
            CompositeDataSource ds = new CompositeDataSource(xs, ys);
            LinePerturbationViewModel lineGraphViewModel = new LinePerturbationViewModel(_container);
            lineGraphViewModel.PointDataSource = ds;
            this.editedDs = ds;
            lineGraphViewModel.Name = string.Format("Test{0}", _pertCount++);
            lineGraphViewModel.Color = Color.FromRgb(255, 0, 0);
            lineGraphViewModel.EntityId = Guid.NewGuid();
            lineGraphViewModel.LineAndMarker = false;
            lineGraphViewModel.Thickness = 1;
            this.LinePerturbations.Add(lineGraphViewModel);

            EnumerableDataSource<double> xs1 = new EnumerableDataSource<double>(y1);
            xs1.SetYMapping(_y1 => _y1 / 2);
            EnumerableDataSource<DateTime> ys1 = new EnumerableDataSource<DateTime>(date1);
            ys1.SetXMapping(dateAxis.ConvertToDouble);
            CompositeDataSource ds1 = new CompositeDataSource(xs1, ys1);

            lineGraphViewModel = new LinePerturbationViewModel(_container);
            lineGraphViewModel.PointDataSource = ds1;
            lineGraphViewModel.Name = string.Format("Test{0}", _pertCount++);
            lineGraphViewModel.Color = Color.FromRgb(0, 0, 255);
            lineGraphViewModel.EntityId = Guid.NewGuid();
            lineGraphViewModel.LineAndMarker = true;
            lineGraphViewModel.Thickness = 1;
            this.LinePerturbations.Add(lineGraphViewModel);
        }
        private void RefreshLineGraphs(LinePerturbationViewModel viewModel)
        {
            RaisePropertyChanged(() => LinePerturbations);
            RaisePropertyChanged(() => VisiblePerturbations);
        }
        #endregion

    }
}
