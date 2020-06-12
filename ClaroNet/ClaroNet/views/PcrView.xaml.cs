using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClaroNet.views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PcrView : ContentPage, INotifyPropertyChanged
    {
        public PcrView()
        {
            InitializeComponent();        
           
        }       
    }
   
}