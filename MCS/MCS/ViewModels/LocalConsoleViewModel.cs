using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS.Models;

namespace MCS.ViewModels
{
    public class LocalConsoleViewModel : INotifyPropertyChanged
    {
        private LocalConsole console;

        public LocalConsoleViewModel()
        {
            console = new LocalConsole();
        }

        public string ConsoleText
        {
            get
            {
                return console.Text;
            }

            set
            {
                if (console.Text != value)
                {
                    console.Text = value;
                    RaisePropertyChanged("ConsoleText");
                }
            }
        }

        public void AppendConsoleText(string text)
        {
            ConsoleText += text;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
