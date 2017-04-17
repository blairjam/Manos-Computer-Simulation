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

        public LocalConsole Console
        {
            get
            {
                return console;
            }

            set
            {
                if (console != value)
                {
                    console = value;
                    RaisePropertyChanged("Console");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
