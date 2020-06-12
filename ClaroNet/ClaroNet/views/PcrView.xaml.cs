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
            BindingContext = this;
           
        }

     


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string Prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Prop));
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        private void ImageButton_Clicked_1(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PushAsync(new RecargasView());
        }
    }
   
}