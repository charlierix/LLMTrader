using Game.Core;
using Game.Core.Threads;
using Game.Math_WPF.WPF;
using OllamaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Core
{
    public partial class SettingsWindow : Window
    {
        #region record: ModelDetail

        public record ModelDetail
        {
            public string Name { get; init; }
            public string ParameterSize { get; init; }
            public string QuantizationLevel { get; init; }
            public string Family { get; init; }
            public string TotalSize { get; init; }
        }

        #endregion
        #region record: OllamaQuery

        private record OllamaQuery_Request
        {
            public string URL { get; init; }
        }

        private record OllamaQuery_Response
        {
            public OllamaSharp.Models.Model[] Models { get; init; }
            public Exception Ex { get; init; }
        }

        #endregion

        #region Declaration Section

        public ObservableCollection<string> ModelList { get; private set; } = [];
        public ObservableCollection<ModelDetail> ModelDetailsList { get; private set; } = [];

        /// <summary>
        /// This does work in a background thread and makes sure that only the last call to start
        /// calls finish event.  All intermediate calls to start are silently ignored
        /// 
        /// This allows the user to type out the url without issue
        /// </summary>
        private readonly BackgroundTaskWorker<OllamaQuery_Request, OllamaQuery_Response> _modelQuery;

        private readonly DropShadowEffect _errorEffect;

        #endregion

        #region Constructor

        public SettingsWindow()
        {
            InitializeComponent();

            DataContext = this;

            Background = SystemColors.ControlBrush;

            _modelQuery = new BackgroundTaskWorker<OllamaQuery_Request, OllamaQuery_Response>(GetOllamaModels, FinishedOllamaModels, ExceptionOllamaModels);

            _errorEffect = new DropShadowEffect()
            {
                Color = UtilityWPF.ColorFromHex("C02020"),
                Direction = 0,
                ShadowDepth = 0,
                BlurRadius = 8,
                Opacity = .8,
            };
        }

        #endregion

        #region Event Listeners

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var settings = SettingsManager.Settings;

                txtOllamaURL.Text = settings.llm.url;
                cboOllamaModelGeneral.Text = settings.llm.model;
                txtOllamaThreadsGeneral.Text = settings.llm.max_threads.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                // NOTE: this is pretty hacky, but it will work until more controls are added that break it.  These are elements
                // that are background and draggable

                if (e.OriginalSource is Border cast_border && cast_border.TemplatedParent == this)      // need to make sure it's the border that is a child of the window and not part of lisbox or dropdown button
                    DragMove();

                else if (e.OriginalSource is Grid cast_grid)        // this grid only seems to be part of the unclickable part of the expander header
                    DragMove();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txtOllamaURL_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var request = new OllamaQuery_Request()
                {
                    URL = txtOllamaURL.Text,
                };

                ModelList.Clear();
                ModelDetailsList.Clear();
                txtOllamaURL.Effect = _errorEffect;     // let the finish task set this to null if valid

                _modelQuery.Start(request);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private static OllamaQuery_Response GetOllamaModels(OllamaQuery_Request request, CancellationToken cancel)
        {
            try
            {
                if (cancel.IsCancellationRequested)
                    return null;        // it doesn't matter what gets returned, it will be ignored

                var client = new OllamaApiClient(request.URL);
                var models = client.ListLocalModelsAsync().
                    GetAwaiter().
                    GetResult().
                    ToArray();

                return new OllamaQuery_Response() { Models = models };
            }
            catch (Exception ex)
            {
                return new OllamaQuery_Response() { Ex = ex };
            }
        }
        private void FinishedOllamaModels(OllamaQuery_Request request, OllamaQuery_Response response)
        {
            try
            {
                // NOTE: this gets invoked on the main thread, so the below is threadsafe

                ModelList.Clear();
                ModelDetailsList.Clear();

                if (response.Ex != null)
                {
                    txtOllamaURL.Effect = _errorEffect;
                    return;
                }

                var models = response.Models.
                    OrderByDescending(o => o.Size).
                    ThenBy(o => o.Name).
                    ToArray();

                foreach (var model in models)
                {
                    ModelList.Add(model.Name);
                    ModelDetailsList.Add(new ModelDetail()
                    {
                        Name = model.Name,
                        ParameterSize = model.Details.ParameterSize,
                        QuantizationLevel = model.Details.QuantizationLevel,
                        Family = model.Details.Family,
                        TotalSize = UtilityCore.Format_SizeSuffix(model.Size),
                    });
                }

                txtOllamaURL.Effect = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ExceptionOllamaModels(OllamaQuery_Request request, Exception ex)
        {
            try
            {
                // NOTE: this gets invoked on the main thread, so this is threadsafe
                txtOllamaURL.Effect = _errorEffect;
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex.ToString(), Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!int.TryParse(txtOllamaThreadsGeneral.Text, out int max_threads))
                {
                    MessageBox.Show("Couldn't parse max threads", Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var settings = SettingsManager.Settings;

                settings = settings with
                {
                    llm = settings.llm with
                    {
                        url = txtOllamaURL.Text,
                        model = cboOllamaModelGeneral.Text,
                        max_threads = max_threads,
                    }
                };

                SettingsManager.Save(settings);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion
    }
}
