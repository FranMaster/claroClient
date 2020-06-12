using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ClaroNet.viewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        public void onPropetyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}
