using Game.Math_WPF.WPF;
using Microsoft.SemanticKernel.ChatCompletion;
using OllamaSharp;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Core
{
    public partial class PropertyEditDialog : Window
    {
        #region record: LorePrompt

        public record LorePrompt
        {
            /// <summary>
            /// A quick name that will show in the tab control's header
            /// </summary>
            public string Name { get; init; }

            /// <summary>
            /// Instructions to the llm about what is requested of it
            /// </summary>
            public string Prompt { get; init; }
        }

        #endregion

        #region Declaration Section

        private ListBox[] _listboxes = null;

        private int _currentCalls = 0;

        private bool _loaded = false;

        #endregion

        #region Constructor

        public PropertyEditDialog()
        {
            InitializeComponent();

            DataContext = this;

            Background = SystemColors.ControlBrush;
            txtOrig.Background = new SolidColorBrush(UtilityWPF.AlphaBlend(SystemColors.WindowColor, SystemColors.ControlColor, 0.333));
            txtOrig.Foreground = new SolidColorBrush(UtilityWPF.AlphaBlend(SystemColors.GrayTextColor, SystemColors.WindowTextColor, 0.666));
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The text that will be modified.  This copy stays untouched and is used as a reference
        /// </summary>
        public string OriginalText
        {
            get { return (string)GetValue(OriginalTextProperty); }
            set { SetValue(OriginalTextProperty, value); }
        }
        public static readonly DependencyProperty OriginalTextProperty = DependencyProperty.Register("OriginalText", typeof(string), typeof(PropertyEditDialog), new PropertyMetadata(""));

        /// <summary>
        /// Each of these will get sent to an llm along with the text that the user writes
        /// </summary>
        /// <remarks>
        /// Any given call to the llm will only see one of these entries.  There are multiple so that the same input
        /// could be used to create different types of text
        /// 
        /// For example, one could request tags, one could ask for a paragraph description, one could ask for historical
        /// events related to the input text
        /// </remarks>
        public LorePrompt[] LorePrompts { get; set; }

        public Dictionary<string, List<string>> LLMSuggestions = new Dictionary<string, List<string>>();

        #endregion

        #region Event Handlers

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (LorePrompts == null || LorePrompts.Length == 0)
                {
                    txtEdit.Text = "ERROR: LorePrompts property is empty";
                    return;
                }

                txtEdit.Text = OriginalText ?? "";

                tabcontrol.Items.Clear();

                _listboxes = new ListBox[LorePrompts.Length];
                for (int i = 0; i < LorePrompts.Length; i++)
                {
                    _listboxes[i] = new ListBox();

                    tabcontrol.Items.Add(new TabItem()
                    {
                        Header = LorePrompts[i].Name,
                        Content = _listboxes[i],
                    });
                }

                _loaded = true;
            }
            catch (Exception ex)
            {
            }
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            try
            {

                // TODO: cancel any calls

            }
            catch (Exception ex)
            {
            }
        }

        private void CallLLM_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtEdit.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter some text first", Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var settings = SettingsManager.Settings;

                IChatCompletionService chatService = new OllamaApiClient(settings.llm.url, settings.llm.model).AsChatCompletionService();

                var schedule = TaskScheduler.FromCurrentSynchronizationContext();

                for (int i = 0; i < LorePrompts.Length; i++)
                {
                    int index = i;      // can't send i to the continuewith, since it will fully iterate before continue executes.  Each iteration of the for loop will have its own copy of index that the corresponding continuewith will see

                    ChatHistory chatHistory = new ChatHistory(LorePrompts[i].Prompt);
                    chatHistory.AddUserMessage(txtEdit.Text);

                    UpdateStatus_CurrentCalls(1);

                    chatService.GetChatMessageContentAsync(chatHistory).
                        ContinueWith(response =>
                        {
                            UpdateStatus_CurrentCalls(-1);

                            if (response.IsFaulted && response.Exception != null)
                            {
                                // TODO: figure what what a timeout exception looks like and ignore it - maybe retry
                                //if (!IsTimeoutException(response.Exception))
                                    throw response.Exception;
                            }
                            else
                            {
                                _listboxes[index].Items.Add(response.Result.ToString());
                            }
                        }, scheduler: schedule);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Private Methods

        private void UpdateStatus_CurrentCalls(int delta)
        {
            _currentCalls += delta;

            lblStatusCurrentCalls.Text = _currentCalls > 0 ?
                $"llm calls: {_currentCalls}" :
                "";
        }

        #endregion
    }
}
