using Game.Core;
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

        #region Declaration Section

        public ObservableCollection<string> ModelList { get; private set; } = [];
        public ObservableCollection<string> ModelDetailsList_ORIG { get; private set; } = [];
        public ObservableCollection<ModelDetail> ModelDetailsList { get; private set; } = [];

        private readonly DropShadowEffect _errorEffect;

        #endregion

        #region Constructor

        public SettingsWindow()
        {
            InitializeComponent();

            DataContext = this;

            Background = SystemColors.ControlBrush;
            //border.BorderBrush = SystemColors.WindowFrameBrush;

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
                var client = new OllamaApiClient(txtOllamaURL.Text);
                var models = client.ListLocalModelsAsync().GetAwaiter().GetResult().
                    OrderByDescending(o => o.Size).
                    ThenBy(o => o.Name).
                    ToArray();

                ModelList.Clear();
                ModelDetailsList.Clear();

                foreach (var model in models)
                {
                    ModelList.Add(model.Name);
                    ModelDetailsList_ORIG.Add($"{model.Name} | params: {model.Details.ParameterSize} | quant: {model.Details.QuantizationLevel} | family: {model.Details.Family} | total size: {SizeSuffix(model.Size)}");
                    ModelDetailsList.Add(new ModelDetail()
                    {
                        Name = model.Name,
                        ParameterSize = model.Details.ParameterSize,
                        QuantizationLevel = model.Details.QuantizationLevel,
                        Family = model.Details.Family,
                        TotalSize = SizeSuffix(model.Size),
                    });
                }

                txtOllamaURL.Effect = null;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString(), Title, MessageBoxButton.OK, MessageBoxImage.Error);
                txtOllamaURL.Effect = _errorEffect;
            }
        }

        private void lstModelDetails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                lstModelDetails.SelectedIndex = -1;
            }
            catch (Exception ex)
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

        #region Private Methods

        // https://stackoverflow.com/questions/14488796/does-net-provide-an-easy-way-convert-bytes-to-kb-mb-gb-etc
        private static readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

        private static string SizeSuffix(long value, int decimalPlaces = 1)
        {
            if (value < 0)
                return "-" + SizeSuffix(-value, decimalPlaces);

            if (value == 0)
                return "0 bytes";

            int i = 0;
            decimal dValue = (decimal)value;
            while (Math.Round(dValue, decimalPlaces) >= 1000)
            {
                dValue /= 1024;
                i++;
            }

            return string.Format("{0:n" + decimalPlaces + "} {1}", dValue, SizeSuffixes[i]);
        }

        #endregion
    }
}
