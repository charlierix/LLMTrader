using Game.Math_WPF.WPF;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Core
{
    public partial class PropertyEditDialog : Window
    {
        public PropertyEditDialog()
        {
            InitializeComponent();

            DataContext = this;

            Background = SystemColors.ControlBrush;
            txtOrig.Background = new SolidColorBrush(UtilityWPF.AlphaBlend(SystemColors.WindowColor, SystemColors.ControlColor, 0.333));
            txtOrig.Foreground = new SolidColorBrush(UtilityWPF.AlphaBlend(SystemColors.GrayTextColor, SystemColors.WindowTextColor, 0.666));
        }

        /// <summary>
        /// A summary of information about this text, will be sent to the llm as background information
        /// </summary>
        public string LoreContext
        {
            get { return (string)GetValue(LoreContextProperty); }
            set { SetValue(LoreContextProperty, value); }
        }
        public static readonly DependencyProperty LoreContextProperty = DependencyProperty.Register("LoreContext", typeof(string), typeof(PropertyEditDialog), new PropertyMetadata(""));

        /// <summary>
        /// The text that will be modified.  This copy stays untouched and is used as a reference
        /// </summary>
        public string OriginalText
        {
            get { return (string)GetValue(OriginalTextProperty); }
            set { SetValue(OriginalTextProperty, value); }
        }
        public static readonly DependencyProperty OriginalTextProperty = DependencyProperty.Register("OriginalText", typeof(string), typeof(PropertyEditDialog), new PropertyMetadata(""));

        public ObservableCollection<string> LLMSuggestions = new ObservableCollection<string>();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
