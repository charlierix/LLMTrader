using Core;
using System.Windows;

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
                        //new PropertyEditDialog.LorePrompt()
                        //{
                        //    Name = "Description",
                        //    Prompt = $"You will receive some text about a {item_name} in a game world.  Please generate a creative description expanding on that text.  This isn't a story, it is a description of that {item_name}",
                        //    ParseType = PropertyEditDialog.LorePrompt_ParseType.None,
                        //},

                        // the gemma model is really chatty and generates too much surrounding text.  ended up using a markup parser
                        // to only get the bullet list
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

        private void Markdown_BoldedList_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string text =
@"* hello *
* there *
* everybody *";

                string parsed = UtilityLLM.ExtractOnlyText(text);





            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}