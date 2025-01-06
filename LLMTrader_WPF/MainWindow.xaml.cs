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
        private SettingsWindow _settingsWindow = null;

        public MainWindow()
        {
            InitializeComponent();

            Background = SystemColors.ControlBrush;
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(_settingsWindow == null)
                {
                    _settingsWindow = new SettingsWindow();
                    _settingsWindow.Closed += (_, _) => _settingsWindow = null;
                    _settingsWindow.Show();
                }
                else
                {
                    _settingsWindow.Focus();
                }
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
                    LorePrompts =
                    [
                        new PropertyEditDialog.LorePrompt()
                        {
                            Name = "Description",
                            Prompt = "You will receive some text about an eagle in a game world.  Please generate a creative description expanding on that text",
                        },

                        new PropertyEditDialog.LorePrompt()
                        {
                            Name = "Tags",
                            Prompt = "You will receive some text about an eagle in a game world.  Please generate a bullet list of tags that help categorize that eagle",
                        },
                    ],

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