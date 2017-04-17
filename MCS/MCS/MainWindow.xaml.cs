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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MCS.ViewModels;

namespace MCS
{
    public delegate void UpdateConsoleText(string text);

    public partial class MainWindow : Window
    {
        private const string CONSOLE_LINE_BEGINNING = ">";
        private LocalConsoleViewModel localConsoleVM;

        public MainWindow()
        {
            InitializeComponent();
            localConsoleVM = LocalConsoleView.DataContext as LocalConsoleViewModel;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AppendToConsole(CONSOLE_LINE_BEGINNING);
        }

        private async Task RunCommand(string[] args)
        {
            await Task.Run(() => Command.Run(args, (x) => AppendToConsole(x), (x) => AppendLineToConsole(x)));
        }

        private void AppendLineToConsole(string text)
        {
            AppendToConsole((text ?? "") + Environment.NewLine);
        }

        private void AppendToConsole(string text)
        {
            localConsoleVM.AppendConsoleText("test test here.");
            //Console_TextBox.Dispatcher.BeginInvoke(new UpdateConsoleText(UpdateText), (text ?? ""));
        }

        private void UpdateText(string text)
        {
            /*Console_TextBox.AppendText(text);
            currentConsoleLineBeginning = Console_TextBox.Text.Length;
            Console_TextBox.CaretIndex = currentConsoleLineBeginning;*/
        }

        private async void Console_TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            /*if (e.Key == Key.Enter)
            {
                Console_TextBox.IsReadOnly = true;
                AppendLineToConsole(null);

                await RunCommand(Console_TextBox.Text.Split(' '));

                AppendToConsole(CONSOLE_LINE_BEGINNING);
                Console_TextBox.IsReadOnly = false;
            }*/
        }

        private void Console_TextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            /*if (Console_TextBox.CaretIndex < currentConsoleLineBeginning)
            {
                Console_TextBox.CaretIndex = currentConsoleLineBeginning;
            }*/
        }
    }
}
