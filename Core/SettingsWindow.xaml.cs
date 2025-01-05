using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Core
{
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();

            Background = SystemColors.ControlBrush;
            border.BorderBrush = SystemColors.WindowFrameBrush;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var settings = SettingsManager.Settings;

                txtOllamaURL.Text = settings.llm.url;
                txtOllamaModelGeneral.Text = settings.llm.model;
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
                if (e.OriginalSource is Border || e.OriginalSource is Grid)     // these are elements that are background and draggable
                    DragMove();
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
                        model = txtOllamaModelGeneral.Text,
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
    }
}
