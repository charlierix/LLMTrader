using Microsoft.SemanticKernel.ChatCompletion;
using OllamaSharp;
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

namespace LLMTrader_WPF
{
    public partial class MarketSessionTestWindow : Window
    {
        public MarketSessionTestWindow()
        {
            InitializeComponent();

            Background = SystemColors.ControlBrush;
        }

        // TODO: once designs are more solid, move the llm callers to core project

        private async void Generate_MarketSessionRoot_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IChatCompletionService chatService = new OllamaApiClient(txtOllamaURL.Text, txtOllamaModelGenerate.Text).AsChatCompletionService();

                ChatHistory chatHistory = new ChatHistory("You are a helpful assistant that knows about AI.");

                chatHistory.AddUserMessage("Hi, I'm looking for book suggestions");

                var reply = await chatService.GetChatMessageContentAsync(chatHistory);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
