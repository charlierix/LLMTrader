using Core;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LLMTrader_WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Background = SystemColors.ControlBrush;
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new SettingsWindow().Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MarketSessionTester1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new MarketSessionTest1Window().Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void InventoryTester_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new InventoryTestWindow().Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void PropertyEditDialog_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new PropertyEditDialog()
                {
                    LoreContext = "This is a description of an eagle",
                    OriginalText = "too round to fly",
                }.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}