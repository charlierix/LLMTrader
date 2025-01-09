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
                if (_settingsWindow == null)
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
                string item_name = "eagle";

                new PropertyEditDialog()
                {
                    LorePrompts =
                    [
                        new PropertyEditDialog.LorePrompt()
                        {
                            Name = "Description",
                            Prompt = $"You will receive some text about a {item_name} in a game world.  Please generate a creative description expanding on that text.  This isn't a story, it is a description of that {item_name}",
                            ParseType = PropertyEditDialog.LorePrompt_ParseType.None,
                        },

                        // the gemma model is really chatty and generates too much surrounding text
                        new PropertyEditDialog.LorePrompt()
                        {
                            Name = "Tags",
                            Prompt = $"You will receive some text about a {item_name} in a game world.  Please generate a bullet list of tags that help categorize that {item_name}.  This bullet list will be interpretted progromatically, so please only give the tags, no other text or symbols.  Don't reply with anything except a bullet list",
                            //Examples = [("it is very big", @"")],     // how to provide a meaningful and still be generic (and not misleading)
                            ParseType = PropertyEditDialog.LorePrompt_ParseType.BulletList,
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